using ProjeEkibiOneriSistemi.Dtos;

namespace ProjeEkibiOneriSistemi.View;

public partial class MailGuncellemeEkrani : ContentPage
{
	Ogrenci _ogrenci;
	public void setOgrenci(Ogrenci ogrenci)
	{
		_ogrenci = ogrenci;
	}
	public MailGuncellemeEkrani()
	{
		InitializeComponent();
	}
}