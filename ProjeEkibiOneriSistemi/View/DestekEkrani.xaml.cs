using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.View;

public partial class DestekEkrani : ContentPage
{
    private readonly IDestekServices _destekServices;
    Ogrenci _ogrenci;
    public void setOgrenci(Ogrenci ogrenci) 
    {
        _ogrenci = ogrenci;
    }
	public DestekEkrani()
	{
		InitializeComponent();
        _destekServices = new DestekServices();

    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        OgrenciEkran ogrenciEkran = new OgrenciEkran();
        ogrenciEkran.setOgrenci(_ogrenci);
        Application.Current.MainPage = new NavigationPage(ogrenciEkran);
    }


    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        if (_ogrenci != null) { 
            if (string.IsNullOrEmpty(Konu.Text) || string.IsNullOrEmpty(Acýklama.Text))
            {
                await DisplayAlert("", "Alanlarý Doldurunuz !", "Tamam");
            }
            else
            {
                await _destekServices.ekleDestek(new Destek
                {

                    Konu = Konu.Text,
                    Açýklama = Acýklama.Text,
                    OgrenciId = _ogrenci.Id,
                    OlusturmaTarihi = DateOnly.FromDateTime(DateTime.Now)
                });

                await DisplayAlert("Baþarýlý", "Destek talebiniz alýndý.", "Tamam");

            }
        }
        else
        {
            await DisplayAlert("", "Ogrenci Bilgileri Alýnamadý !!", "Tamam");

        }
    }
    private async void Button_Clicked_3(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        GecmisDestekTalepleri gecmisDestekTalepleri = new GecmisDestekTalepleri();
        gecmisDestekTalepleri.setOgrenci(_ogrenci);
        Application.Current.MainPage = new NavigationPage(gecmisDestekTalepleri);
    }
}