using ProjeEkibiOneriSistemi.Dtos;

namespace ProjeEkibiOneriSistemi.View;

public partial class ProjelerimBilgiEkrani : ContentPage
{
	Ogrenci _ogrenci;
    Proje _Proje;
	public void SetProje(Proje proje)
    {
        _Proje = proje;
        projeAdi.Text = _Proje.Ad;
    }
    public void SetOgrenci(Ogrenci ogrenci)
    {
        _ogrenci = ogrenci;
    }
    public ProjelerimBilgiEkrani()
	{
		InitializeComponent();
	}
}