using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class SifreDegistirmeEkrani : ContentPage
{
	private readonly IOgrenciServices _ogrenciServices;
	Ogrenci _ogrenci;

	public void SetOgrenci(Ogrenci ogrenci)
	{
		_ogrenci = ogrenci;
		
    }
    public SifreDegistirmeEkrani()
	{
		InitializeComponent();
        _ogrenciServices = new OgrenciServices();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); // Küçültme efekti
        await button.ScaleTo(1, 100); // Eski haline getirme

        var ogrenciler = await _ogrenciServices.GetOgrencis();
		var guncelOgrenciBilgileri = ogrenciler.FirstOrDefault(x => x.Id == _ogrenci.Id);
		if(guncelOgrenciBilgileri is not null)
		{
            if (string.IsNullOrEmpty(sifre.Text) || string.IsNullOrEmpty(tekrarSifre.Text))
            {
                await DisplayAlert("", "Þifre alanlarý boþ olamaz!", "Tamam");
            }
            else if (sifre.Text == tekrarSifre.Text)
            {
                if (sifre.Text.Length > 7)
                {
                    await _ogrenciServices.guncelleOgrenci(new Ogrenci
                    {
                        Id = guncelOgrenciBilgileri.Id,
                        Ad = guncelOgrenciBilgileri.Ad,
                        Soyad = guncelOgrenciBilgileri.Soyad,
                        Email = guncelOgrenciBilgileri.Email,
                        Telefon = guncelOgrenciBilgileri.Telefon,
                        Bolum = guncelOgrenciBilgileri.Bolum,
                        Sinif = guncelOgrenciBilgileri.Sinif,
                        ogrenciNo = guncelOgrenciBilgileri.ogrenciNo,
                        TC = guncelOgrenciBilgileri.TC,
                        ogrenciResmi = guncelOgrenciBilgileri.ogrenciResmi,
                        ToplamCevaplananSoruSayisi = guncelOgrenciBilgileri.ToplamCevaplananSoruSayisi,
                        OrtalamaPuan = guncelOgrenciBilgileri.OrtalamaPuan,
                        Sifre = sifre.Text,
                        AnneAdi = guncelOgrenciBilgileri.AnneAdi,
                        BabaAdi = guncelOgrenciBilgileri.BabaAdi,
                    });
                    await DisplayAlert("", "Þifre Baþarýlý Þekilde Deðiþtirilmiþtir.", "Tamam");
                    ProfilEkrani profilEkrani = new ProfilEkrani();
                    profilEkrani.setOgrenci(guncelOgrenciBilgileri);
                    await Navigation.PushAsync(profilEkrani);
                }
                else
                {
                    await DisplayAlert("", "Þifre uzunluðu en az 8 karakter olmalýdýr", "Tamam");
                }
            }
            else
            {
                await DisplayAlert("", "Girilen þifreler birbirine eþit deðil!", "Tamam");
            }


        }
    }
}