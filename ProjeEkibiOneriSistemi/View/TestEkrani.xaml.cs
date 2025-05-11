        using ProjeEkibiOneriSistemi.Dtos;
        using ProjeEkibiOneriSistemi.Services;

        namespace ProjeEkibiOneriSistemi.View;

        public partial class TestEkrani : ContentPage
        {
            private readonly ISoruServices _soruServices;
            private readonly IKullaniciYanitiSerives _yanitServices;
            private readonly IOgrenciServices _ogrenciServices;
            public Kategori Kategori;
            public Ogrenci ogrenci;
            private List<Soru> Sorular;
            private int MevcutSoruIndex = 0;
            private int SecilenPuan = 0; 

            public TestEkrani()
            {
                InitializeComponent();
                _soruServices = new SoruServices();
                _yanitServices = new KullaniciYanitiServices();
                _ogrenciServices = new OgrenciServices();
            }

            public void setKategori(Kategori _kategori)
            {
                Kategori = _kategori;
            }
            public void setOgrenci(Ogrenci _ogrenci)
            {
                ogrenci = _ogrenci;
            }

            private async void ContentPage_Loaded(object sender, EventArgs e)
            {
                await GetSorular();
            }

           
            public async Task GetSorular()
            {
                if (Kategori == null)
                {
                    await DisplayAlert("Hata", "Kategori bilgisi alýnamadý!", "Tamam");
                    return;
                }

                var testler = await _soruServices.GetSorus();
                Sorular = testler.Where(x => x.KategoriId == Kategori.Id).ToList();

                if (Sorular.Count == 0)
                {
                    await DisplayAlert("Bilgi", "Bu kategori için soru bulunamadý.", "Tamam");
                    return;
                }

                
                GosterSoru();
            }

            
            private void GosterSoru()
            {
                if (MevcutSoruIndex < Sorular.Count)
                {
                    lblSoruMetni.Text = Sorular[MevcutSoruIndex].Metin;
                    SecilenPuan = 0; 
                }
                else
                {
                    DisplayAlert("Tamamlandý", "Tüm sorular cevaplandý!", "Tamam");
                    Navigation.PopAsync();
                }
            }

           
            private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
            {
                if (sender is RadioButton radioButton && radioButton.IsChecked)
                {
                    SecilenPuan = int.Parse(radioButton.Value.ToString());
                }
            }

            
            private async void BtnSonrakiSoru_Clicked(object sender, EventArgs e)
            {

        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 


        if (MevcutSoruIndex >= Sorular.Count)
                    return;

                var seciliSoru = Sorular[MevcutSoruIndex];

                
                if (SecilenPuan == 0)
                {
                    await DisplayAlert("Uyarý", "Lütfen bir seçenek seçiniz.", "Tamam");
                    return;
                }

                if (ogrenci == null)
                {
                    await DisplayAlert("Hata", "Öðrenci bilgisi bulunamadý!", "Tamam");
                    return;
                }

        var yanit = new KullaniciYaniti
                {
                    OgrenciId = ogrenci.Id, 
                    SoruId = seciliSoru.Id,
                    KategoriId = seciliSoru.KategoriId,
                    Puan = SecilenPuan
                };

        await _yanitServices.ekleKullaniciYaniti(yanit);

                
                MevcutSoruIndex++;
                GosterSoru();
            }
        }
