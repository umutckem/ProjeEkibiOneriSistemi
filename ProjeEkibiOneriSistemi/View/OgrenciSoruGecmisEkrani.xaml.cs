using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class OgrenciSoruGecmisEkrani : ContentPage
{
    private readonly IOgrenciServices _ogrenciServices;
    private readonly IKullaniciYanitiSerives _kullaniciYanitServices;
    
	Ogrenci _ogrenci;
	public OgrenciSoruGecmisEkrani()
	{
		InitializeComponent();
        _ogrenciServices = new OgrenciServices();
        _kullaniciYanitServices = new KullaniciYanitiServices();

    }
	public void setOgrenci(Ogrenci ogrenci)
    {
		_ogrenci = ogrenci;

    }

    private async void CollectionViewSoruGecmisi_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var secilenYanit = e.CurrentSelection.FirstOrDefault() as KullaniciYaniti;
        if (secilenYanit == null || _ogrenci == null)
            return;

        bool secim = await DisplayAlert(
            "Kategoriye Ait Tüm Yanýtlarý Sil",
            $"Kategori ID: {secilenYanit.KategoriId}\nBu kategoriye ait tüm yanýtlar silinecektir. Emin misiniz?",
            "Evet", "Hayýr");

        if (secim)
        {
            var tumYanitlar = await _kullaniciYanitServices.GetKullaniciYanitis();
            var silinecekler = tumYanitlar
                .Where(y => y.KategoriId == secilenYanit.KategoriId && y.OgrenciId == _ogrenci.Id)
                .ToList();

            foreach (var y in silinecekler)
            {
                await _kullaniciYanitServices.silKullaniciYaniti(y.Id);
            }

            await DisplayAlert("Silindi", $"{silinecekler.Count} adet yanýt silindi.", "Tamam");
        }
    }


    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await getSoruGecmisi(_ogrenci.Id);
    }
    public async Task getSoruGecmisi(Guid id)
    {
        var KullaniciYanitlari = await _kullaniciYanitServices.GetKullaniciYanitis();
        var ogrencininKullaniciYanitlari = KullaniciYanitlari.Where(x => x.OgrenciId == id).ToList();
        CollectionViewSoruGecmisi.ItemsSource = ogrencininKullaniciYanitlari;
    }
}