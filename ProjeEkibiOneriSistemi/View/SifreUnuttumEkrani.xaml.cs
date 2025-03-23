using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class SifreUnuttumEkrani : ContentPage
{
	private readonly IOgrenciServices _ogrenciServices;
    Ogrenci _ogrenci;
	public void SetOgrenci(Ogrenci ogrenci)
	{
		_ogrenci = ogrenci;
    }
	public SifreUnuttumEkrani()
	{
		InitializeComponent();
        _ogrenciServices = new OgrenciServices();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {

        if(string.IsNullOrEmpty(TC.Text) || string.IsNullOrEmpty(telefonNo.Text) || string.IsNullOrEmpty(anaAdi.Text) || string.IsNullOrEmpty(babaAdi.Text))
        {
            await DisplayAlert("", "Alanlar Boþ Býrakýlamaz!", "Tamam");
        }
        else{ 

		    var ogrenciler = await _ogrenciServices.GetOgrencis();
		    var ogrenci = ogrenciler.FirstOrDefault(x => x.TC == TC.Text);

		    if(ogrenci is not null)
		    {
			    if(ogrenci.TC == TC.Text && ogrenci.Telefon == telefonNo.Text && ogrenci.AnneAdi == anaAdi.Text.ToUpper() && ogrenci.BabaAdi == babaAdi.Text.ToUpper()) 
			    {
				    await _ogrenciServices.guncelleOgrenci(new Ogrenci
				    {
                        Ad = ogrenci.Ad,
                        Soyad = ogrenci.Soyad,
                        Sinif = ogrenci.Sinif,
                        Bolum = ogrenci.Bolum,
                        Email = ogrenci.Email,
                        Id = ogrenci.Id,
                        ogrenciNo = ogrenci.ogrenciNo,
                        OrtalamaPuan = ogrenci.OrtalamaPuan,
                        ToplamCevaplananSoruSayisi = ogrenci.ToplamCevaplananSoruSayisi,
                        ogrenciResmi = ogrenci.ogrenciResmi,
                        TC = ogrenci.TC,
                        Telefon = ogrenci.Telefon,
                        Sifre = ogrenci.TC,
                        BabaAdi = ogrenci.BabaAdi,
                        AnneAdi = ogrenci.AnneAdi,
                    });
                    await DisplayAlert("","Þifre Sýfýrlama Ýþlemi Baþarýlý Þekilde Tamamlanmýþtýr.","Tamam");
			    }
                else
                {
                    await DisplayAlert("","Girilen Bilgiler Yanlýþ!","Tamam");
                }
		    }
            else
            {
                await DisplayAlert("", "Ogrenci Bilgileri Bulunamadý!", "Tamam");
            }
        }
    }
}