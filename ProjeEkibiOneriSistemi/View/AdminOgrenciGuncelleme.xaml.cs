using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminOgrenciGuncelleme : ContentPage
{
    private readonly IOgrenciServices _ogrenciservices;
	Ogrenci _ogrenci;
	Ogrenci _duzenlenecekOgrenci;

    public void setDuzenlenecekOgrenci(Ogrenci duzenlenecekOgrenci) {
        _duzenlenecekOgrenci = duzenlenecekOgrenci;
    }
	public void setOgrenci(Ogrenci ogrenci)
    {
        _ogrenci = ogrenci;
    }
    public AdminOgrenciGuncelleme()
	{
		InitializeComponent();
        _ogrenciservices = new OgrenciServices();

    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        ogrenciAd.Text = _duzenlenecekOgrenci.Ad;
        ogrenciSoyad.Text = _duzenlenecekOgrenci.Soyad;
        ogrenciBabaAdi.Text = _duzenlenecekOgrenci.BabaAdi;
        ogrenciAnneAdi.Text = _duzenlenecekOgrenci.AnneAdi;
        ogrenciEmail.Text = _duzenlenecekOgrenci.Email;
        ogrenciTelefon.Text = _duzenlenecekOgrenci.Telefon;
        ogrenciBolum.Text = _duzenlenecekOgrenci.Bolum;
        ogrenciSinif.Text = _duzenlenecekOgrenci.Sinif.ToString();
        ogrenciogrenciNo.Text = _duzenlenecekOgrenci.ogrenciNo.ToString();
        ogrenciTC.Text = _duzenlenecekOgrenci.TC.ToString();
        ogrenciogrenciResmi.Text = _duzenlenecekOgrenci.ogrenciResmi;


    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100);   

        bool secim = await DisplayAlert("", "Güncellemek istediðinize emin misiniz?", "Evet", "Hayýr");
        if (!secim)
            return;

        try
        {
            var guncellenecekOgrenciModeli = new Ogrenci
            {
                Id = _duzenlenecekOgrenci.Id,
                Ad = ogrenciAd.Text,
                Soyad = ogrenciSoyad.Text,
                BabaAdi = ogrenciBabaAdi.Text,
                AnneAdi = ogrenciAnneAdi.Text,
                Email = ogrenciEmail.Text,
                Telefon = ogrenciTelefon.Text,
                Bolum = ogrenciBolum.Text,
                Sinif = Convert.ToInt32(ogrenciSinif.Text),
                TC = ogrenciTC.Text,
                ogrenciNo = ogrenciogrenciNo.Text,
                ogrenciResmi = ogrenciogrenciResmi.Text,
                ToplamCevaplananSoruSayisi = _duzenlenecekOgrenci.ToplamCevaplananSoruSayisi,
                OrtalamaPuan = _duzenlenecekOgrenci.OrtalamaPuan,
                Sifre = _duzenlenecekOgrenci.Sifre,
            };

            
            await _ogrenciservices.guncelleOgrenci(guncellenecekOgrenciModeli);
            await DisplayAlert("Baþarýlý", "Öðrenci bilgileri güncellendi.", "Tamam");

            AdminEkran adminEkran = new AdminEkran();
            adminEkran.setAdmin(_ogrenci);
            Application.Current.MainPage = new NavigationPage(adminEkran);

        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Bir hata oluþtu: {ex.Message}", "Tamam");
        }
    }



    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        if(_ogrenci is not null) { 
        AdminEkran adminEkran = new AdminEkran();
        adminEkran.setAdmin(_ogrenci);
        Application.Current.MainPage = new NavigationPage(adminEkran);
        }
    }
}