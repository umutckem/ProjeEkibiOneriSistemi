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
        BindingContext = null;  // Önce BindingContext'i temizliyoruz
        BindingContext = ogrenci;  // Sonra tekrar atýyoruz
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
        await button.ScaleTo(0.9, 100); // Küçültme efekti
        await button.ScaleTo(1, 100); // Eski haline getirme
        KategoriEkrani testEkrani = new KategoriEkrani();
        testEkrani.setOgrenci(ogrenci);
        await Navigation.PushAsync(testEkrani);
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); // Küçültme efekti
        await button.ScaleTo(1, 100); // Eski haline getirme
        ProjeEkrani projeEkrani = new ProjeEkrani();
        projeEkrani.setOgrenci(ogrenci);
        await Navigation.PushAsync(projeEkrani);

    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); // Küçültme efekti
        await button.ScaleTo(1, 100); // Eski haline getirme

        ProjelerimEkrani projelerimEkrani = new ProjelerimEkrani();
        projelerimEkrani.setOgrenci(ogrenci);
        await Navigation.PushAsync(projelerimEkrani);
    }
}