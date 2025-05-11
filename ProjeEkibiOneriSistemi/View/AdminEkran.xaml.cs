using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminEkran : ContentPage
{
    private readonly IOgrenciServices _ogrenciServices;
    private readonly IYetkiServices _yetkiServices;
    private readonly IRolServices _rolServices;
    private readonly IProjeServices _projeServices;
    private readonly IDestekServices _destekServices;
    private readonly ISoruServices _soruServices;
    private readonly IKategoriServices _kategori;
    Ogrenci _ogrenci;
	public void setAdmin(Ogrenci ogrenci)
	{
		_ogrenci = ogrenci;

    }
	public AdminEkran()
	{
		InitializeComponent();
        _ogrenciServices = new OgrenciServices();
        _yetkiServices = new YetkiServices();
        _rolServices = new RolServices();
        _projeServices = new ProjeServices();
        _destekServices = new DestekServices();
        _soruServices = new SoruServices();
        _kategori = new KategoriServices();
    }
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await getOgrenciSayisi();
        await getDestekSayisi();
        await getProjeSayisi();
        await getSoruSayisi();
        await getKategoriSayisi();
        BindingContext = null;
        BindingContext = _ogrenci;
        
    }

    private async void Cikis_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        bool secim = await DisplayAlert("", "Çýkmak Ýstediðinize Emin Misiniz ?", "Evet", "Hayýr");
        if(secim == true)
        {
            AnaEkran anaEkran = new AnaEkran();
            Application.Current.MainPage = new NavigationPage(anaEkran);
        }

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        AdminOgrenciEkrani adminOgrenciEkrani = new AdminOgrenciEkrani();
        adminOgrenciEkrani.setOgrenci(_ogrenci);
        Application.Current.MainPage = new NavigationPage(adminOgrenciEkrani);
    }
    private async Task getOgrenciSayisi()
    {
        var ogrenciler = await _ogrenciServices.GetOgrencis();
        var ogrenciSayisi = ogrenciler.Count();
        KayitliOgrenciSayisi.Text = ogrenciSayisi.ToString();

    }
    private async Task getDestekSayisi()
    {
        var destekler = await _destekServices.GetAllDestek();
        var destekSayisi = destekler.Count();
        KayitliDestekSayisi.Text = destekSayisi.ToString();
    }
    private async Task getProjeSayisi()
    {
        var projeler = await _projeServices.GetProjes();
        var projeSayisi = projeler.Count();
        KayitliProjeSayisi.Text = projeSayisi.ToString();
    }

    private async Task getSoruSayisi()
    {
        var sorular = await _soruServices.GetSorus();
        var soruSayisi = sorular.Count;
        KayitliSoruSayisi.Text = soruSayisi.ToString();
    }

    private async Task getKategoriSayisi()
    {
        var Kategoriler = await _kategori.GetKategoris();
        var kategoriSayisi = Kategoriler.Count;
        KayitliKategoriSayisi.Text = kategoriSayisi.ToString();
    }


    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        if(_ogrenci is not null)
        {
            var button = (Button)sender;
            await button.ScaleTo(0.9, 100); 
            await button.ScaleTo(1, 100); 

            AdminProjeErkani adminProjeErkani = new AdminProjeErkani();
            adminProjeErkani.setOgrenci(_ogrenci);
            Application.Current.MainPage = new NavigationPage(adminProjeErkani);
        }
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        AdminKategoriEkrani adminTestlerEkrani = new AdminKategoriEkrani();
        adminTestlerEkrani.setOgrenci(_ogrenci);
        Application.Current.MainPage = new NavigationPage(adminTestlerEkrani);
    }
}