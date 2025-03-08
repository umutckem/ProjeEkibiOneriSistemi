using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class ProfilEkrani : ContentPage
{
	private readonly IOgrenciServices _ogrenciServices;
	public Ogrenci ogrenci;
	public void setOgrenci(Ogrenci _ogrenci)
	{
		ogrenci = _ogrenci;
        BindingContext = null;  // Önce BindingContext'i temizliyoruz
        BindingContext = ogrenci;  // Sonra tekrar atýyoruz
    }
	public ProfilEkrani()
	{
		_ogrenciServices = new OgrenciServices();
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		var OgrenciBilgileri = await _ogrenciServices.GetOgrencis();
		var guncelOgrenciBilgileri = OgrenciBilgileri.FirstOrDefault(x => x.Id == ogrenci.Id);
		if(guncelOgrenciBilgileri is not null)
		{
			await DisplayAlert("Baþarýlý", "Bilgileriniz Güncellenmiþtir!", "Tamam");
			return;
		}
		await DisplayAlert("Baþarýsýz","Bir sorundan dolayý güncelleme baþarýsýz oldu!","Tamam");
    }
}