using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminOgrenciOlustur : ContentPage
{
    private readonly IRolServices _rolServices;
    private readonly IYetkiServices _yetkiServices;
    private readonly IOgrenciServices _ogrenciServices;
	Ogrenci _ogrenci;
    public void setOgrenci(Ogrenci ogrenci)
    {
        _ogrenci = ogrenci;
    }
    public AdminOgrenciOlustur()
	{
		InitializeComponent();
        _ogrenciServices = new OgrenciServices();
        _yetkiServices = new YetkiServices();
        _rolServices = new RolServices();

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); // Küçültme efekti
        await button.ScaleTo(1, 100); // Eski haline getirme

        AdminEkran adminEkran = new AdminEkran();
        adminEkran.setAdmin(_ogrenci);
        Application.Current.MainPage = new NavigationPage(adminEkran);
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100);
        await button.ScaleTo(1, 100);

        // Boþ alan kontrolü
        if (string.IsNullOrWhiteSpace(ogrenciAd.Text) ||
            string.IsNullOrWhiteSpace(ogrenciSoyad.Text) ||
            string.IsNullOrWhiteSpace(ogrenciBabaAdi.Text) ||
            string.IsNullOrWhiteSpace(ogrenciAnneAdi.Text) ||
            string.IsNullOrWhiteSpace(ogrenciEmail.Text) ||
            string.IsNullOrWhiteSpace(ogrenciTelefon.Text) ||
            string.IsNullOrWhiteSpace(ogrenciBolum.Text) ||
            string.IsNullOrWhiteSpace(ogrenciSinif.Text) ||
            string.IsNullOrWhiteSpace(ogrenciTC.Text) ||
            string.IsNullOrWhiteSpace(ogrenciogrenciNo.Text) ||
            string.IsNullOrWhiteSpace(ogrenciogrenciResmi.Text))
        {
            await DisplayAlert("Eksik Bilgi", "Lütfen tüm alanlarý doldurun.", "Tamam");
            return;
        }

        // Sýnýf deðeri geçerli mi?
        if (!int.TryParse(ogrenciSinif.Text, out int sinifDegeri))
        {
            await DisplayAlert("Hatalý Giriþ", "Sýnýf alanýna yalnýzca sayý giriniz.", "Tamam");
            return;
        }

        // Kullanýcý onayý
        bool secim = await DisplayAlert("Onay", "Öðrenciyi oluþturmak istiyor musunuz?", "Evet", "Hayýr");
        if (!secim)
            return;

        try
        {
            // Yeni öðrenci nesnesi oluþtur
            var yeniOgrenci = new Ogrenci
            {
                Id = Guid.NewGuid(),
                Ad = ogrenciAd.Text,
                Soyad = ogrenciSoyad.Text,
                BabaAdi = ogrenciBabaAdi.Text,
                AnneAdi = ogrenciAnneAdi.Text,
                Email = ogrenciEmail.Text,
                Telefon = ogrenciTelefon.Text,
                Bolum = ogrenciBolum.Text,
                Sinif = sinifDegeri,
                TC = ogrenciTC.Text,
                ogrenciNo = ogrenciogrenciNo.Text,
                ogrenciResmi = ogrenciogrenciResmi.Text,
                ToplamCevaplananSoruSayisi = 0,
                OrtalamaPuan = 0,
                Sifre = ogrenciTC.Text
            };

            
            await _ogrenciServices.ekleOgrenci(yeniOgrenci);

           
            var roller = await _rolServices.GetAllRol();
            var ogrenciRol = roller.FirstOrDefault(r => r.KullaniciRol == "OGRENCI");

            if (ogrenciRol != null)
            {
                var yeniYetki = new Yetki
                {
                    Id = Guid.NewGuid(),
                    OgrenciId = yeniOgrenci.Id,
                    RolId = ogrenciRol.Id
                };

                await _yetkiServices.ekleYetki(yeniYetki);
            }

            await DisplayAlert("Baþarýlý", "Öðrenci baþarýyla oluþturuldu.", "Tamam");

            // Ana ekrana yönlendir
            var adminEkran = new AdminEkran();
            adminEkran.setAdmin(_ogrenci);
            Application.Current.MainPage = new NavigationPage(adminEkran);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Bir hata oluþtu:\n{ex.Message}", "Tamam");
        }
    }
}