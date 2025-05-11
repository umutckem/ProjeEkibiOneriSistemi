using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class ProfilEkrani : ContentPage
{
	private readonly IOgrenciServices _ogrenciServices;
	private readonly IKullaniciYanitiSerives _kullaniciYanitiSerives;
	public Ogrenci ogrenci;
	public async void setOgrenci(Ogrenci _ogrenci)
	{
		ogrenci = _ogrenci;
        var ogrenciBilgileri = await _ogrenciServices.GetOgrencis();
        var guncelOgrenciBilgileri = ogrenciBilgileri.FirstOrDefault(x => x.Id == ogrenci.Id);
        if(guncelOgrenciBilgileri is not null)
        {
            BindingContext = null;  
            BindingContext = guncelOgrenciBilgileri;  
        }

    }
	public ProfilEkrani()
	{
		_ogrenciServices = new OgrenciServices();
		_kullaniciYanitiSerives = new KullaniciYanitiServices();
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        var OgrenciBilgileri = await _ogrenciServices.GetOgrencis();
        var guncelOgrenciBilgileri = OgrenciBilgileri.FirstOrDefault(x => x.Id == ogrenci.Id);

        if (guncelOgrenciBilgileri is not null)
        {
            await GuncelSoruSayisi(ogrenci.Id); 
            ogrenci = guncelOgrenciBilgileri;
            await puanHesapla(ogrenci.Id); 
            await DisplayAlert("Baþarýlý", "Bilgileriniz Güncellenmiþtir!", "Tamam");
            ProfilEkrani profilEkrani = new ProfilEkrani();
            profilEkrani.setOgrenci(guncelOgrenciBilgileri);
            Application.Current.MainPage = new NavigationPage(profilEkrani);
            return;
        }

        await DisplayAlert("Baþarýsýz", "Bir sorundan dolayý güncelleme baþarýsýz oldu!", "Tamam");
    }

    public async Task GuncelSoruSayisi(Guid id)
    {
        var ogrenciler = await _ogrenciServices.GetOgrencis();
        var ogrenci = ogrenciler.FirstOrDefault(x => x.Id == id);

        if (ogrenci != null)
        {
            var ogrenciYanitlari = await _kullaniciYanitiSerives.GetKullaniciYanitis();
            var ogrencininGuncelCevaplananSoruSayisi = ogrenciYanitlari.Where(x => x.OgrenciId == ogrenci.Id).ToList();

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
                ToplamCevaplananSoruSayisi = ogrencininGuncelCevaplananSoruSayisi.Count,
                ogrenciResmi = ogrenci.ogrenciResmi,
                TC = ogrenci.TC,
                Sifre = ogrenci.Sifre,
                Telefon = ogrenci.Telefon,
                BabaAdi = ogrenci.BabaAdi,
                AnneAdi = ogrenci.AnneAdi
            });
        }
    }

    public async Task puanHesapla(Guid id)
    {
        var puan = 0;
        var ogrenciler = await _ogrenciServices.GetOgrencis();
        var ogrenci = ogrenciler.FirstOrDefault(x => x.Id == id);

        if (ogrenci != null)
        {
            var ogrenciYanitlari = await _kullaniciYanitiSerives.GetKullaniciYanitis();
            var ogrencininKullaniciYanitlari = ogrenciYanitlari.Where(x => x.OgrenciId == ogrenci.Id).ToList();

            foreach (var item in ogrencininKullaniciYanitlari)
            {
                puan += item.Puan;
            }

            float ortalamaPuan = ogrencininKullaniciYanitlari.Count > 0
                ? (float)puan / ogrencininKullaniciYanitlari.Count
                : 0; 

            await _ogrenciServices.guncelleOgrenci(new Ogrenci
            {
                Ad = ogrenci.Ad,
                Soyad = ogrenci.Soyad,
                Sinif = ogrenci.Sinif,
                Bolum = ogrenci.Bolum,
                Email = ogrenci.Email,
                Id = ogrenci.Id,
                ogrenciNo = ogrenci.ogrenciNo,
                OrtalamaPuan = ortalamaPuan,
                ToplamCevaplananSoruSayisi = ogrenci.ToplamCevaplananSoruSayisi,
                ogrenciResmi = ogrenci.ogrenciResmi,
                TC = ogrenci.TC,
                Telefon = ogrenci.Telefon,
                Sifre = ogrenci.Sifre,
                BabaAdi = ogrenci.BabaAdi,
                AnneAdi = ogrenci.AnneAdi,
            });
        }
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        OgrenciSoruGecmisEkrani ogrenciSoruGecmisEkrani = new OgrenciSoruGecmisEkrani();
        ogrenciSoruGecmisEkrani.setOgrenci(ogrenci);
        await Navigation.PushAsync(ogrenciSoruGecmisEkrani);
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 
        SifreDegistirmeEkrani sifreDegistirmeEkrani = new SifreDegistirmeEkrani();
        sifreDegistirmeEkrani.SetOgrenci(ogrenci);
        await Navigation.PushAsync(sifreDegistirmeEkrani);
    }

    private async void Button_Clicked_3(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 
        MailGuncellemeEkrani mailGuncellemeEkrani = new MailGuncellemeEkrani();
        mailGuncellemeEkrani.setOgrenci(ogrenci);
        await Navigation.PushAsync(mailGuncellemeEkrani);
    }

    private async  void Button_Clicked_4(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        OgrenciEkran ogrenciEkran = new OgrenciEkran();
        ogrenciEkran.setOgrenci(ogrenci);
        Application.Current.MainPage = new NavigationPage(ogrenciEkran);
    }
}
