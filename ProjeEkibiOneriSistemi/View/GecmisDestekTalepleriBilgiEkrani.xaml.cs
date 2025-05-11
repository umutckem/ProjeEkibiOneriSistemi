using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class GecmisDestekTalepleriBilgiEkrani : ContentPage
{
	private readonly IDestekServices _destekServices;
	Ogrenci _ogrenci;
	Destek _destek;
	public void setOgrenci(Ogrenci ogrenci)
	{
		_ogrenci = ogrenci;
	}
	public void setDestek(Destek destek) 
	{
		_destek = destek; 
	}
	public GecmisDestekTalepleriBilgiEkrani()
	{
		InitializeComponent();
		_destekServices = new DestekServices();


    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        OgrenciEkran ogrenciEkran = new OgrenciEkran();
		ogrenciEkran.setOgrenci(_ogrenci);
		Application.Current.MainPage = new NavigationPage(ogrenciEkran);
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (_destek != null)
        {
			Konu.Text = _destek.Konu.ToString();
			Acýklama.Text = _destek.Açýklama.ToString();
        }
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        if (_destek != null)
		{
			bool secim = await DisplayAlert("","Silmek Ýstediðinize Emin Misiniz ?","Evet","Hayýr");
			if(secim == true)
			{
                await _destekServices.silDestek(_destek.Id);
				await DisplayAlert("","Silme Ýþlemi Baþarýlý","Tamam");
				OgrenciEkran ogrenciEkran = new OgrenciEkran();
				ogrenciEkran.setOgrenci(_ogrenci);
				Application.Current.MainPage = new NavigationPage(ogrenciEkran);
            }
			
			

        }
    }
}