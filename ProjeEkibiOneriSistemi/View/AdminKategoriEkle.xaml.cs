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

            if (string.IsNullOrWhiteSpace(girilenAd))
            {
                await DisplayAlert("Uyarý", "Lütfen bir kategori adý giriniz.", "Tamam");
                return;
            }

            Kategori yeniKategori = new Kategori
            {
                Ad = girilenAd
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
