using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminProjeGuncelleme : ContentPage
{
    private readonly IProjeServices _projeServices;
    Proje _proje;
    Ogrenci _ogrenci;

    public AdminProjeGuncelleme()
    {
        InitializeComponent();
        _projeServices = new ProjeServices();
    }

    public void SetProje(Proje proje)
    {
        _proje = proje;

        projeAd.Text = proje.Ad;
        projeAciklama.Text = proje.Aciklama;
        projeBolum.Text = proje.Bolum;
        projeZorluk.Text = proje.ZorlukSeviyesi.ToString();
        projeKatilim.Text = proje.projeyeKatilimSayisi.ToString();
        projeBaslangic.Date = proje.BaslangicTarihi;
        projeBitis.Date = proje.BitisTarihi ?? DateTime.Now;
        projeAktifMi.IsToggled = proje.AktifMi;
    }

    public void SetOgrenci(Ogrenci ogrenci)
    {
        _ogrenci = ogrenci;
    }

    private async void Button_Clicked_Guncelle(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); // Küçültme efekti
        await button.ScaleTo(1, 100); // Eski haline getirme

        if (_proje == null) return;

        // Entry ve picker'lardan gelen verileri _proje nesnesine aktar
        _proje.Ad = projeAd.Text;
        _proje.Aciklama = projeAciklama.Text;
        _proje.Bolum = projeBolum.Text;

        if (int.TryParse(projeZorluk.Text, out int zorluk))
            _proje.ZorlukSeviyesi = zorluk;
        else
        {
            await DisplayAlert("Hata", "Zorluk seviyesi sayýsal bir deðer olmalýdýr.", "Tamam");
            return;
        }

        if (int.TryParse(projeKatilim.Text, out int katilim))
            _proje.projeyeKatilimSayisi = katilim;
        else
        {
            await DisplayAlert("Hata", "Katýlým sayýsý sayýsal bir deðer olmalýdýr.", "Tamam");
            return;
        }

        _proje.BaslangicTarihi = projeBaslangic.Date;
        _proje.BitisTarihi = projeBitis.Date;
        _proje.AktifMi = projeAktifMi.IsToggled;

        try
        {
            await _projeServices.projeGuncelle(_proje);
            await DisplayAlert("Baþarýlý", "Proje bilgileri güncellendi.", "Tamam");
            AdminEkran adminEkran = new AdminEkran();
            adminEkran.setAdmin(_ogrenci);
            Application.Current.MainPage = new NavigationPage(adminEkran);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Güncelleme sýrasýnda bir hata oluþtu: {ex.Message}", "Tamam");
        }
    }

    private async void Button_Clicked_Anamenu(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); // Küçültme efekti
        await button.ScaleTo(1, 100); // Eski haline getirme

        AdminEkran adminEkran = new AdminEkran();
        adminEkran.setAdmin(_ogrenci);
        Application.Current.MainPage = new NavigationPage(adminEkran);
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        // Sayfa yüklendiðinde yapýlacak iþlemler
    }
}
