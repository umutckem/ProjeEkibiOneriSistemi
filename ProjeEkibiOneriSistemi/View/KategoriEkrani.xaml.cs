using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class KategoriEkrani : ContentPage
{
    private readonly IKategoriServices _kategoriServices;
    public Ogrenci ogrenci;
    public KategoriEkrani()
	{
		InitializeComponent();
        _kategoriServices = new KategoriServices();
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
            var secilenKategori = e.CurrentSelection[0] as Kategori; // Seçilen öðeyi al

            if (secilenKategori != null)
            {
                bool secim = await DisplayAlert("Seçilen Kategori", $"ID: {secilenKategori.Id}\nAd: {secilenKategori.Ad}", "Tamam" , "Hayýr");
                if(secim == true)
                {
                    TestEkrani testEkrani = new TestEkrani();
                    testEkrani.setKategori(secilenKategori);
                    await Navigation.PushAsync(testEkrani);
                }
            }
        }

    }
}