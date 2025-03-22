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
        var secilenYanit = e.CurrentSelection[0] as KullaniciYaniti; // Seçilen kategoriyi al
        if (secilenYanit != null)
        {
            bool secim = await DisplayAlert("Seçilen Kullanici Yaniti", $"ID: {secilenYanit.Id}\nKategori Id: {secilenYanit.KategoriId} \nBu soruyu silerseniz bu soruya ait bütün ilgili kategori'nin soru cevaplarýda silenecektir. ", "Tamam", "Hayýr");
            if (secim)
            {

            }
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