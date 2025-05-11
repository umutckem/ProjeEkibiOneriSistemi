using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class MailGuncellemeEkrani : ContentPage
{
	private readonly IOgrenciServices _ogrenciServices;
	Ogrenci _ogrenci;
	public void setOgrenci(Ogrenci ogrenci)
	{
		_ogrenci = ogrenci;
	}
	public MailGuncellemeEkrani()
	{
		InitializeComponent();
		_ogrenciServices = new OgrenciServices();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        if (string.IsNullOrEmpty(mail.Text) || string.IsNullOrEmpty(tekrarMail.Text)){
			await DisplayAlert("", "Bütün Alanlarý Doldurun!", "Tamam");
		}
		else
		{
			if (mail.Text == tekrarMail.Text) {
				var ogrenciler = await _ogrenciServices.GetOgrencis();
				var ogrenci = ogrenciler.FirstOrDefault(x => x.Id == _ogrenci.Id);
				if(ogrenci is not null)
				{
					await _ogrenciServices.guncelleOgrenci(new Ogrenci
					{
                        Ad = ogrenci.Ad,
                        Soyad = ogrenci.Soyad,
                        Sinif = ogrenci.Sinif,
                        Bolum = ogrenci.Bolum,
                        Email = mail.Text,
                        Id = ogrenci.Id,
                        ogrenciNo = ogrenci.ogrenciNo,
                        OrtalamaPuan = ogrenci.OrtalamaPuan,
                        ToplamCevaplananSoruSayisi = ogrenci.ToplamCevaplananSoruSayisi,
                        ogrenciResmi = ogrenci.ogrenciResmi,
                        TC = ogrenci.TC,
                        Telefon = ogrenci.Telefon,
                        Sifre = ogrenci.Sifre,
                        BabaAdi = ogrenci.BabaAdi,
                        AnneAdi = ogrenci.AnneAdi,
                    });
					await DisplayAlert("","Mail Adresi Baþarýlý Bir Þekilde Güncellendi","Tamam");
					ProfilEkrani profilEkrani = new ProfilEkrani();
					profilEkrani.setOgrenci(ogrenci);
                    Application.Current.MainPage = new NavigationPage(profilEkrani);
                }
			}
			else
			{
				await DisplayAlert("","Girilen Mail Adresleri Uyuþmuyor!","Tamam");
			}
		}

    }
}