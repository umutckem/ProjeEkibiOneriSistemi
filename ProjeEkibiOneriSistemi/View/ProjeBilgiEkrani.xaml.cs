using ProjeEkibiOneriSistemi.Dtos;

namespace ProjeEkibiOneriSistemi.View;

public partial class ProjeBilgiEkrani : ContentPage
{
	Proje _Proje;
	Ogrenci _Ogrenci;
	public void setProje(Proje proje)
	{
		_Proje = proje;
        BindingContext = null;
        BindingContext = _Proje;
    }
	public void setOgrenci(Ogrenci ogrenci) 
	{
		_Ogrenci = ogrenci;
	}
	public ProjeBilgiEkrani()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); // Küçültme efekti
        await button.ScaleTo(1, 100); // Eski haline getirme
		

    }
}