using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;
using System;

namespace ProjeEkibiOneriSistemi.View
{
    public partial class AdminKategoriEkle : ContentPage
    {
        private readonly IKategoriServices _kategoriServices;
        private Ogrenci _ogrenci;

        public void setOgrenci(Ogrenci ogrenci)
        {
            _ogrenci = ogrenci;
        }

        public AdminKategoriEkle()
        {
            InitializeComponent();
            _kategoriServices = new KategoriServices();
        }

        private async void Button_Ekle_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await button.ScaleTo(0.9, 100); 
            await button.ScaleTo(1, 100); 

            string girilenAd = kategoriAd.Text?.Trim();
            string girilenBolum = ilgiliBolum.Text.Trim();

            if (string.IsNullOrWhiteSpace(girilenAd))
            {
                await DisplayAlert("Uyarý", "Lütfen bir kategori adý giriniz.", "Tamam");
                return;
            }
            if (girilenAd.Length < 3)
            {
                await DisplayAlert("Uyarý", "Kategori adý en az 3 karakter olmalýdýr.", "Tamam");
                return;
            }
            if (girilenAd.Length > 50)
            {
                await DisplayAlert("Uyarý", "Kategori adý en fazla 50 karakter olmalýdýr.", "Tamam");
                return;
            }
            if (string.IsNullOrWhiteSpace(girilenBolum))
            {
                await DisplayAlert("Uyarý", "Lütfen bir bölüm adý giriniz.", "Tamam");
                return;
            }
            if (girilenBolum.Length < 3)
            {
                await DisplayAlert("Uyarý", "Bölüm adý en az 3 karakter olmalýdýr.", "Tamam");
                return;
            }
            if (girilenBolum.Length > 50)
            {
                await DisplayAlert("Uyarý", "Bölüm adý en fazla 50 karakter olmalýdýr.", "Tamam");
                return;
            }

            Kategori yeniKategori = new Kategori
            {
                Ad = girilenAd,
                IlgiliBolum = girilenBolum
            };

            await _kategoriServices.ekleKategori(yeniKategori);
            await DisplayAlert("Baþarýlý", "Kategori baþarýyla eklendi.", "Tamam");

            kategoriAd.Text = string.Empty; 

        }

        private async void Button_AnaMenu_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await button.ScaleTo(0.9, 100); 
            await button.ScaleTo(1, 100); 

            AdminEkran ekran = new AdminEkran();
            ekran.setAdmin(_ogrenci);
            Application.Current.MainPage = new NavigationPage(ekran);
        }
    }
}
