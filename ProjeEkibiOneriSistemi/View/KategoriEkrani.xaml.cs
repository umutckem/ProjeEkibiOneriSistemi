using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class KategoriEkrani : ContentPage
{
    private readonly IKategoriServices _kategoriServices;
    private readonly ISoruServices _soruServices;
    private readonly IKullaniciYanitiSerives _kullaniciYanitiSerives;
    public Ogrenci ogrenci;
    public KategoriEkrani()
	{
		InitializeComponent();
        _kategoriServices = new KategoriServices();
        _soruServices = new SoruServices();
        _kullaniciYanitiSerives = new KullaniciYanitiServices();

    }
	public void setOgrenci(Ogrenci _ogrenci)
    {
        ogrenci = _ogrenci;

    }
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await GetKategoriler();

    }
    private async Task GetKategoriler()
    {
        var kategoriler = await _kategoriServices.GetKategoris();
        var sorular = await _soruServices.GetSorus();
        var yanitlar = await _kullaniciYanitiSerives.GetKullaniciYanitis();

        // Sadece öðrenciye uygun olan kategoriler
        var uygunKategoriler = kategoriler
            .Where(k => string.Equals(k.IlgiliBolum?.Trim(), ogrenci?.Bolum?.Trim(), StringComparison.OrdinalIgnoreCase))
            .ToList();

        // Artýk anonymous object deðil, doðrudan kategori nesneleri atanýyor
        CollectionViewKategori.ItemTemplate = new DataTemplate(() =>
        {
            var frame = new Frame
            {
                BackgroundColor = Color.FromArgb("#222831"),
                BorderColor = Color.FromArgb("#00ADB5"),
                CornerRadius = 10,
                Padding = 15,
                Margin = 5,
            };

            var grid = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection {
                new ColumnDefinition { Width = new GridLength(100) },
                new ColumnDefinition { Width = GridLength.Star }
            },
                RowDefinitions = new RowDefinitionCollection {
                new RowDefinition(), new RowDefinition(), new RowDefinition(),
                new RowDefinition(), new RowDefinition()
            }
            };

            var lblId = new Label { TextColor = Colors.White };
            lblId.SetBinding(Label.TextProperty, "Id");

            var lblAd = new Label { TextColor = Colors.White };
            lblAd.SetBinding(Label.TextProperty, "Ad");

            var lblSoru = new Label { TextColor = Colors.White };
            var lblYanýt = new Label { TextColor = Colors.White };
            var lblDurum = new Label();

            frame.BindingContextChanged += async (s, e) =>
            {
                var kategori = frame.BindingContext as Kategori;
                if (kategori == null) return;

                var soruSayisi = sorular.Count(s => s.KategoriId == kategori.Id);
                var yanitSayisi = yanitlar.Count(y => y.KategoriId == kategori.Id && y.OgrenciId == ogrenci.Id);
                var durum = (soruSayisi > 0 && soruSayisi == yanitSayisi) ? "Tamamlandý" : "Eksik";
                var renk = (soruSayisi == yanitSayisi) ? Colors.LimeGreen : Colors.OrangeRed;

                lblSoru.Text = soruSayisi.ToString();
                lblYanýt.Text = yanitSayisi.ToString();
                lblDurum.Text = durum;
                lblDurum.TextColor = renk;
            };

            grid.Add(new Label { Text = "ID:", TextColor = Color.FromArgb("#00ADB5") }, 0, 0);
            grid.Add(lblId, 1, 0);

            grid.Add(new Label { Text = "Kategori:", TextColor = Color.FromArgb("#00ADB5") }, 0, 1);
            grid.Add(lblAd, 1, 1);

            grid.Add(new Label { Text = "Soru:", TextColor = Color.FromArgb("#00ADB5") }, 0, 2);
            grid.Add(lblSoru, 1, 2);

            grid.Add(new Label { Text = "Yanýt:", TextColor = Color.FromArgb("#00ADB5") }, 0, 3);
            grid.Add(lblYanýt, 1, 3);

            grid.Add(new Label { Text = "Durum:", TextColor = Color.FromArgb("#00ADB5") }, 0, 4);
            grid.Add(lblDurum, 1, 4);

            frame.Content = grid;
            return frame;
        });

        CollectionViewKategori.ItemsSource = uygunKategoriler; // Anonymous deðil, gerçek model
    }




    private async void CollectionViewKategori_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            var secilenKategori = e.CurrentSelection[0] as Kategori;

            if (secilenKategori != null)
            {
                bool secim = await DisplayAlert("Seçilen Kategori", $"ID: {secilenKategori.Id}\nAd: {secilenKategori.Ad}", "Tamam", "Hayýr");

                if (!secim) return;

                if (ogrenci == null)
                {
                    await DisplayAlert("Hata", "Öðrenci bilgisi bulunamadý!", "Tamam");
                    return;
                }

                
                var sorular = await _soruServices.GetSorus();
                var kategoriSorulari = sorular.Where(s => s.KategoriId == secilenKategori.Id).ToList();

                var kullaniciYanitlari = await _kullaniciYanitiSerives.GetKullaniciYanitis();
                var ogrencininKategoriYanitlari = kullaniciYanitlari
                    .Where(x => x.KategoriId == secilenKategori.Id && x.OgrenciId == ogrenci.Id)
                    .ToList();

                
                if (ogrencininKategoriYanitlari.Count > 0)
                {
                    foreach (var yanit in ogrencininKategoriYanitlari)
                    {
                        await _kullaniciYanitiSerives.silKullaniciYaniti(yanit.Id);
                    }

                    await DisplayAlert("Bilgi", "Bu kategoriye ait önceki yanýtlar silindi. Test ekranýna yönlendiriliyorsunuz.", "Tamam");
                }

                
                TestEkrani testEkrani = new TestEkrani();
                testEkrani.setKategori(secilenKategori);
                testEkrani.setOgrenci(ogrenci);
                await Navigation.PushAsync(testEkrani);
            }
        }
    }



}