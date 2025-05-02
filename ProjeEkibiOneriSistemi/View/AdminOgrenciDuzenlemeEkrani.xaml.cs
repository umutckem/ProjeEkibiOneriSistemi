using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminOgrenciDuzenlemeEkrani : ContentPage
{
    private readonly IOgrenciServices _ogrenciServices;
	public Ogrenci _ogrenci;
	public Ogrenci _duzenlenecekOgrenci;
	public void setOgrenci(Ogrenci ogrenci)
	{
        _ogrenci = ogrenci;
    }
	public void setDuzenlenecekOgrenci(Ogrenci duzenlenecekOgrenci)
    {
        _duzenlenecekOgrenci = duzenlenecekOgrenci;
    }
    public AdminOgrenciDuzenlemeEkrani()
	{
		InitializeComponent();
        _ogrenciServices = new OgrenciServices();

    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        BindingContext = null;
        BindingContext = _duzenlenecekOgrenci;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); // Küçültme efekti
        await button.ScaleTo(1, 100); // Eski haline getirme

        AdminEkran adminEkran = new AdminEkran();
        adminEkran.setAdmin(_ogrenci);
        Application.Current.MainPage = new NavigationPage(adminEkran);
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); // Küçültme efekti
        await button.ScaleTo(1, 100); // Eski haline getirme

        if (_duzenlenecekOgrenci != null)
        {
            bool secim = await DisplayAlert("","Silmek Ýstediðinize Emin Misiniz ?","Evet","Hayýr");
            if(secim == true)
            {
                await _ogrenciServices.silOgrenci(_duzenlenecekOgrenci.Id);
                await DisplayAlert("","Silme Ýþlemi Baþarýlý","Tamam");

                AdminEkran adminEkran = new AdminEkran();
                adminEkran.setAdmin(_ogrenci);
                Application.Current.MainPage = new NavigationPage(adminEkran);
            }
        }
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); // Küçültme efekti
        await button.ScaleTo(1, 100); // Eski haline getirme

        if (_ogrenci is not null)
        {
            AdminOgrenciGuncelleme adminOgrenciGuncelleme = new AdminOgrenciGuncelleme();
            adminOgrenciGuncelleme.setOgrenci(_ogrenci);
            adminOgrenciGuncelleme.setDuzenlenecekOgrenci(_duzenlenecekOgrenci);
            Application.Current.MainPage = new NavigationPage(adminOgrenciGuncelleme);
        }
    }
}