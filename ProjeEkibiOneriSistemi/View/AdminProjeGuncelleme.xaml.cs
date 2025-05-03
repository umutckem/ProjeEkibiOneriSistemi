using ProjeEkibiOneriSistemi.Dtos;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminProjeGuncelleme : ContentPage
{
    Proje _proje;
    Ogrenci _ogrenci;
	public void SetOgrenci(Ogrenci ogrenci)
    {
        _ogrenci = ogrenci;
    }
    public void SetProje(Proje proje)
    {
        _proje = proje;
    }

    public AdminProjeGuncelleme()
	{
		InitializeComponent();
	}
}