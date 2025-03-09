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
        var Kategoriler = await _kategoriServices.GetKategoris();
        CollectionViewKategori.ItemsSource = Kategoriler;
    }

    private async void CollectionViewKategori_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            var secilenKategori = e.CurrentSelection[0] as Kategori; // Seçilen kategoriyi al

            if (secilenKategori != null)
            {
                bool secim = await DisplayAlert("Seçilen Kategori", $"ID: {secilenKategori.Id}\nAd: {secilenKategori.Ad}", "Tamam", "Hayýr");

                if (secim)
                {
                    if (ogrenci == null)
                    {
                        await DisplayAlert("Hata", "Öðrenci bilgisi bulunamadý!", "Tamam");
                        return;
                    }

                    var kullaniciYanitlari = await _kullaniciYanitiSerives.GetKullaniciYanitis();
                    var kullanicininYanitlari = kullaniciYanitlari.Where(x => x.OgrenciId == ogrenci.Id).ToList();
                    
                    bool cevaplandiMi = kullanicininYanitlari.Any(x => x.KategoriId == secilenKategori.Id);
                    if (cevaplandiMi)
                    {
                        await DisplayAlert("Bilgi", "Bu kategori için daha önce cevap verilmiþ.", "Tamam");
                        return;
                    }

                    // Test ekranýný baþlat
                    TestEkrani testEkrani = new TestEkrani();
                    testEkrani.setKategori(secilenKategori);
                    testEkrani.setOgrenci(ogrenci);
                    await Navigation.PushAsync(testEkrani);
                }
            }
        }
    }


}