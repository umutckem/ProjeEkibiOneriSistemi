using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace ProjeEkibiOneriSistemi.View
{
    public partial class AdminGrupBilgisi : ContentPage
    {
        private readonly IGrupServices _grupServices;
        private readonly IOgrenciServices _ogrenciServices;
        Ogrenci _Ogrenci;
        Grup _Grup;
        Proje _Proje;
        private List<Grup> _tumGruplar;

        public AdminGrupBilgisi()
        {
            InitializeComponent();
            _ogrenciServices = new OgrenciServices();
            _grupServices = new GrupServices();
        }

        public void setTumGruplar(List<Grup> tumGruplar)
        {
            _tumGruplar = tumGruplar;
        }

        public void setProje(Proje proje)
        {
            _Proje = proje;
        }

        public void setGrup(Grup grup)
        {
            _Grup = grup;
        }

        public void setOgrenci(Ogrenci ogrenci)
        {
            _Ogrenci = ogrenci;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_Grup != null)
            {
                await LoadOgrenciler();
            }
        }

        private async Task LoadOgrenciler()
        {
            try
            {
                
                var tumOgrenciler = await _ogrenciServices.GetOgrencis();
                _tumGruplar = await _grupServices.getGrups();

                var grupOgrencileri = tumOgrenciler
                    .Where(o => _tumGruplar.Any(g =>
                        g.GrupNo == _Grup.GrupNo &&
                        g.ProjeId == _Grup.ProjeId &&
                        g.OgrenciId == o.Id))
                    .ToList();

                GrupNoLabel.Text = $"Grup No: {_Grup.GrupNo}";
                ProjeAdLabel.Text = $"Proje Adý: {_Grup.ProjeId}"; 

                OgrenciListView.ItemsSource = grupOgrencileri;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", "Öðrenciler yüklenemedi: " + ex.Message, "Tamam");
            }
        }

        private async void EkleButton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await button.ScaleTo(0.9, 100);
            await button.ScaleTo(1, 100);

            AdminGrupOgrenciEkle adminGrupOgrenciEkle = new AdminGrupOgrenciEkle();
            adminGrupOgrenciEkle.setGrup(_Grup);
            adminGrupOgrenciEkle.setOgrenci(_Ogrenci);
            adminGrupOgrenciEkle.setProje(_Proje);
            await Navigation.PushAsync(adminGrupOgrenciEkle);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await button.ScaleTo(0.9, 100);
            await button.ScaleTo(1, 100);

            AdminEkran adminEkran = new AdminEkran();
            adminEkran.setAdmin(_Ogrenci);
            Application.Current.MainPage = new NavigationPage(adminEkran);
        }

        private async void GrubuSilButton_Clicked(object sender, EventArgs e)
        {
            if (_Grup == null)
            {
                await DisplayAlert("Hata", "Silinecek grup bilgisi alýnamadý.", "Tamam");
                return;
            }

            bool silinsinMi = await DisplayAlert("Uyarý", $"Grup {_Grup.GrupNo} silinecek. Onaylýyor musunuz?", "Evet", "Hayýr");
            if (silinsinMi)
            {
                await _grupServices.silGrup(_Grup.Id);
                await DisplayAlert("Baþarýlý", $"Grup {_Grup.GrupNo} baþarýyla silindi.", "Tamam");

                AdminProjeOgrenci adminProjeOgrenci = new AdminProjeOgrenci();
                adminProjeOgrenci.setProje(_Proje);
                adminProjeOgrenci.setOgrenci(_Ogrenci);
                Application.Current.MainPage = new NavigationPage(adminProjeOgrenci);
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            AdminGrupOgrenciCikar adminGrupOgrenciCikar = new AdminGrupOgrenciCikar();
            adminGrupOgrenciCikar.setGrup(_Grup);
            adminGrupOgrenciCikar.setOgrenci(_Ogrenci);
            await Navigation.PushAsync(adminGrupOgrenciCikar);
        }
    }
}
