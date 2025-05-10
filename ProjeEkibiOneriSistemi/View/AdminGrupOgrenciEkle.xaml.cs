using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace ProjeEkibiOneriSistemi.View
{
    public partial class AdminGrupOgrenciEkle : ContentPage
    {
        private readonly IGrupServices _grupServices;
        private readonly IOgrenciServices _ogrenciServices;
        private readonly IKatilimciServices _katilimciServices;

        private Grup _grup;

        public AdminGrupOgrenciEkle()
        {
            InitializeComponent();
            _grupServices = new GrupServices();
            _ogrenciServices = new OgrenciServices();
            _katilimciServices = new KatilimciServices(); // Katýlýmcý servisi
        }

        public void setGrup(Grup grup)
        {
            _grup = grup;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadOgrenciler();
        }

        private async Task LoadOgrenciler()
        {
            try
            {
                var tumOgrenciler = await _ogrenciServices.GetOgrencis();
                var tumKatilimcilar = await _katilimciServices.GetKatilimcis();
                var tumGruplar = await _grupServices.getGrups();

                // Bu projeye ait gruba eklenmiþ öðrenci ID'leri
                var grubaEklenenOgrenciIdleri = tumGruplar
                    .Where(g => g.ProjeId == _grup.ProjeId)
                    .Select(g => g.OgrenciId)
                    .ToList();

                // Katýlýmcýlardaki öðrenci ID'leri (bu projeye baþvuran öðrenciler)
                var katilimciOgrenciIdleri = tumKatilimcilar
                    .Where(k => k.ProjeId == _grup.ProjeId)
                    .Select(k => k.OgrenciId)
                    .ToList();

                // Sadece bu projeye baþvuran ve grupta olmayan öðrencileri filtrele
                var gruptaOlmayanOgrenciler = tumOgrenciler
                    .Where(o => katilimciOgrenciIdleri.Contains(o.Id) && !grubaEklenenOgrenciIdleri.Contains(o.Id))
                    .ToList();

                // Öðrencileri ListView'e baðla
                OgrenciListView.ItemsSource = gruptaOlmayanOgrenciler;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", "Öðrenciler yüklenemedi: " + ex.Message, "Tamam");
            }
        }

        private async void OgrenciListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedOgrenci = e.Item as Ogrenci;
            if (selectedOgrenci == null) return;

            bool onay = await DisplayAlert("Onay", $"{selectedOgrenci.Ad} öðrencisini gruba eklemek istiyor musunuz?", "Evet", "Hayýr");
            if (!onay) return;

            var yeniGrupKaydi = new Grup
            {
                Id = Guid.NewGuid(),
                OgrenciId = selectedOgrenci.Id,
                ProjeId = _grup.ProjeId,
                GrupNo = _grup.GrupNo
            };

            try
            {
                // Öðrenciyi gruba ekle
                await _grupServices.ekleGrup(yeniGrupKaydi);
                await DisplayAlert("Baþarýlý", $"{selectedOgrenci.Ad} gruba eklendi.", "Tamam");
                await LoadOgrenciler(); // Listeyi güncelle
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", "Öðrenci eklenemedi: " + ex.Message, "Tamam");
            }
        }

        private async void GeriDon_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
