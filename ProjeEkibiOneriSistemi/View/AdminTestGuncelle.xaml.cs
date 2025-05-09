using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminTestGuncelle : ContentPage
{
	private readonly ISoruServices _soruservices;
	Ogrenci _ogrenci;
    Soru _soru;
    public void setOgrenci(Ogrenci ogrenci)
	{
		_ogrenci = ogrenci;
	}
    public void setSoru(Soru soru)
	{
		_soru = soru;
	}
    public AdminTestGuncelle()
	{
		InitializeComponent();
		_soruservices = new SoruServices();

    }
}