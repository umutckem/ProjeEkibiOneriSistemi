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
}