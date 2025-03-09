        using ProjeEkibiOneriSistemi.Dtos;
        using ProjeEkibiOneriSistemi.Services;

        namespace ProjeEkibiOneriSistemi.View;

        public partial class TestEkrani : ContentPage
        {
            private readonly ISoruServices _soruServices;
            private readonly IKullaniciYanitiSerives _yanitServices;
            public Kategori Kategori;
            public Ogrenci ogrenci;
            private List<Soru> Sorular;
            private int MevcutSoruIndex = 0;
            private int SecilenPuan = 0; // Kullanýcýnýn seçtiði puaný tutar

            public TestEkrani()
            {
                InitializeComponent();
                _soruServices = new SoruServices();
                _yanitServices = new KullaniciYanitiServices();
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

            // Sorularý API'den Getir
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

                // Ýlk Soruyu Göster
                GosterSoru();
            }

            // Mevcut Soruyu Göster
            private void GosterSoru()
            {
                if (MevcutSoruIndex < Sorular.Count)
                {
                    lblSoruMetni.Text = Sorular[MevcutSoruIndex].Metin;
                    SecilenPuan = 0; // Yeni soru için sýfýrla
                }
                else
                {
                    DisplayAlert("Tamamlandý", "Tüm sorular cevaplandý!", "Tamam");
                    Navigation.PopAsync(); // Sayfayý Kapat
                }
            }

            // RadioButton Seçildiðinde Çalýþýr
            private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
            {
                if (sender is RadioButton radioButton && radioButton.IsChecked)
                {
                    SecilenPuan = int.Parse(radioButton.Value.ToString());
                }
            }

            // Kullanýcý Yanýtýný Kaydet ve Sonraki Soruyu Göster
            private async void BtnSonrakiSoru_Clicked(object sender, EventArgs e)
            {
                if (MevcutSoruIndex >= Sorular.Count)
                    return;

                var seciliSoru = Sorular[MevcutSoruIndex];

                // Eðer kullanýcý cevap seçmediyse
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
                    OgrenciId = ogrenci.Id, // Öðrencinin gerçek ID'sini al
                    SoruId = seciliSoru.Id,
                    KategoriId = seciliSoru.KategoriId,
                    Puan = SecilenPuan
                };

        await _yanitServices.ekleKullaniciYaniti(yanit);

                // Sonraki Soruya Geç
                MevcutSoruIndex++;
                GosterSoru();
            }
        }
