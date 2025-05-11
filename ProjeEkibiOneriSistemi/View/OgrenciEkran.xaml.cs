using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class OgrenciEkran : ContentPage
{
	private readonly IOgrenciServices _ogrenciServices;
	public Ogrenci ogrenci;

    public OgrenciEkran()
	{
		InitializeComponent();
        _ogrenciServices = new OgrenciServices();
        
    }
	public void setOgrenci(Ogrenci _ogrenci)
	{
		ogrenci = _ogrenci;
        BindingContext = null;  
        BindingContext = ogrenci;  
    }
    private async void OnImageTapped(object sender, EventArgs e)
    {
        ProfilEkrani profilEkrani = new ProfilEkrani();
        profilEkrani.setOgrenci(ogrenci);
        await Navigation.PushAsync(profilEkrani);
        
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 
        KategoriEkrani testEkrani = new KategoriEkrani();
        testEkrani.setOgrenci(ogrenci);
        await Navigation.PushAsync(testEkrani);
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100);
        await button.ScaleTo(1, 100); 
        ProjeEkrani projeEkrani = new ProjeEkrani();
        projeEkrani.setOgrenci(ogrenci);
        await Navigation.PushAsync(projeEkrani);

    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        ProjelerimEkrani projelerimEkrani = new ProjelerimEkrani();
        projelerimEkrani.setOgrenci(ogrenci);
        await Navigation.PushAsync(projelerimEkrani);
    }

    private async void Cikis_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100);
        await button.ScaleTo(1, 100); 
        bool secim = await DisplayAlert("","Çýkýþ Yapmak Ýstiyor musunuz ?","Evet","Hayýr");
        if(secim == true)
        {
            AnaEkran anaEkran = new AnaEkran();
            Application.Current.MainPage = new NavigationPage(anaEkran);
        }

    }

    private async void Button_Clicked_3(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 
        DestekEkrani destekEkrani = new DestekEkrani();
        destekEkrani.setOgrenci(ogrenci);
        await Navigation.PushAsync(destekEkrani);
    }
}