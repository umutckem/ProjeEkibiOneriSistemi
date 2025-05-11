using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;
using System;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminProjeOlustur : ContentPage
{
    private readonly IProjeServices _projeServices;
    Ogrenci _ogrenci;

    public void setOgrenci(Ogrenci ogrenci)
    {
        _ogrenci = ogrenci;
    }

    public AdminProjeOlustur()
    {
        InitializeComponent();
        _projeServices = new ProjeServices();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        // Sayfa yüklendiðinde yapýlacak iþlemler
    }

    private async void OlusturButton_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100);
        await button.ScaleTo(1, 100);

        try
        {
            var yeniProje = new Proje
            {
                Id = Guid.NewGuid(),
                Ad = entryAd.Text,
                Aciklama = editorAciklama.Text,
                Bolum = entryBolum.Text,
                ZorlukSeviyesi = pickerZorluk.SelectedIndex + 1,
                GerekenKategoriIdler = entryKategoriler.Text.Split(',').Select(x => int.Parse(x.Trim())).ToList(),
                BaslangicTarihi = datePickerBaslangic.Date,
                BitisTarihi = datePickerBitis.Date,
                AktifMi = switchAktif.IsToggled,
                projeyeKatilimSayisi = 0
            };

            await _projeServices.projeEkle(yeniProje); // async servis metodu varsayýmý
            await DisplayAlert("Baþarýlý", "Proje baþarýyla oluþturuldu.", "Tamam");
            AdminProjeErkani adminProjeErkani = new AdminProjeErkani();
            adminProjeErkani.setOgrenci(_ogrenci);
            Application.Current.MainPage = new NavigationPage(adminProjeErkani);
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

        AdminEkran adminEkran = new AdminEkran();
        adminEkran.setAdmin(_ogrenci);
        Application.Current.MainPage = new NavigationPage(adminEkran);

    }
}
