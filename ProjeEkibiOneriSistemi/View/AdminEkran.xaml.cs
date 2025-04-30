using ProjeEkibiOneriSistemi.Dtos;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminEkran : ContentPage
{
	Ogrenci _ogrenci;
	public void setAdmin(Ogrenci ogrenci)
	{
		_ogrenci = ogrenci;

    }
	public AdminEkran()
	{
		InitializeComponent();
	}
}