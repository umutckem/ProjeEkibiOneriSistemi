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

        private Ogrenci _Ogrenci;
        private Grup _grup;
        private Proje _proje;

        public AdminGrupOgrenciEkle()
        {
            InitializeComponent();
            _grupServices = new GrupServices();
            _ogrenciServices = new OgrenciServices();
            _katilimciServices = new KatilimciServices();
        }

        public void setGrup(Grup grup) => _grup = grup;
        public void setProje(Proje proje) => _proje = proje;
        public void setOgrenci(Ogrenci ogrenci) => _Ogrenci = ogrenci;

        private class EtiketliOgrenci : Ogrenci
        {
            public string Etiket { get; set; }
            public Color EtiketRenk { get; set; }
            public bool OneriMi { get; set; }
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

                var projeOgrenciIdleri = tumKatilimcilar
                    .Where(k => k.ProjeId == _grup.ProjeId)
                    .Select(k => k.OgrenciId)
                    .ToList();

                var projeOgrencileri = tumOgrenciler
                    .Where(o => projeOgrenciIdleri.Contains(o.Id))
                    .ToList();

                var mevcutGrupOgrenciIdleri = tumGruplar
                    .Where(g => g.ProjeId == _grup.ProjeId && g.GrupNo == _grup.GrupNo)
                    .Select(g => g.OgrenciId)
                    .ToList();

                var digerGrupOgrenciIdleri = tumGruplar
                    .Where(g => g.ProjeId == _grup.ProjeId && g.GrupNo != _grup.GrupNo)
                    .Select(g => g.OgrenciId)
                    .ToList();

                float mevcutGrupToplam = projeOgrencileri
                    .Where(o => mevcutGrupOgrenciIdleri.Contains(o.Id))
                    .Sum(o => o.OrtalamaPuan);

                int mevcutGrupSayisi = mevcutGrupOgrenciIdleri.Count;

                float digerGrupOrtalama = projeOgrencileri
                    .Where(o => digerGrupOgrenciIdleri.Contains(o.Id))
                    .Select(o => o.OrtalamaPuan)
                    .DefaultIfEmpty(0)
                    .Average();

                var gruptaOlmayanOgrenciler = projeOgrencileri
                    .Where(o => !mevcutGrupOgrenciIdleri.Contains(o.Id) && !digerGrupOgrenciIdleri.Contains(o.Id))
                    .ToList();

                var etiketeGoreOgrenciler = gruptaOlmayanOgrenciler.Select(o =>
                {
                    float yeniOrtalama = (mevcutGrupToplam + o.OrtalamaPuan) / (mevcutGrupSayisi + 1);
                    float fark = yeniOrtalama - digerGrupOrtalama;

                    string etiket;
                    Color renk;

                    if (digerGrupOrtalama == 0)
                    {
                        etiket = $"Yeni Ortalama: {yeniOrtalama:F2}";
                        renk = Colors.SlateGray;
                    }
                    else if (Math.Abs(fark) <= 0.5f)
                    {
                        etiket = $"Dengeli (+{fark:F2})";
                        renk = Colors.Goldenrod;
                    }
                    else if (fark < -0.5f)
                    {
                        etiket = $"Grubu Yükseltir ({fark:F2})";
                        renk = Colors.LimeGreen;
                    }
                    else
                    {
                        etiket = $"Aðýrlaþýr (+{fark:F2})";
                        renk = Colors.OrangeRed;
                    }

                    return new EtiketliOgrenci
                    {
                        Id = o.Id,
                        Ad = o.Ad,
                        Soyad = o.Soyad,
                        OrtalamaPuan = o.OrtalamaPuan,
                        ogrenciNo = o.ogrenciNo,
                        Sinif = o.Sinif,
                        Etiket = etiket,
                        EtiketRenk = renk,
                        OneriMi = false
                    };
                }).ToList();

                // Öneri yapýlabilecek (Aðýrlaþýr olmayan) öðrenciler içinden en uygun olaný bul
                var onerilebilirler = etiketeGoreOgrenciler
                    .Where(e => !e.Etiket.Contains("Aðýrlaþýr"))
                    .OrderBy(e => Math.Abs(((mevcutGrupToplam + e.OrtalamaPuan) / (mevcutGrupSayisi + 1)) - digerGrupOrtalama))
                    .ToList();

                if (onerilebilirler.Any())
                {
                    onerilebilirler.First().OneriMi = true;
                }

                OgrenciListView.ItemsSource = etiketeGoreOgrenciler;
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
                await _grupServices.ekleGrup(yeniGrupKaydi);
                await DisplayAlert("Baþarýlý", $"{selectedOgrenci.Ad} gruba eklendi.", "Tamam");
                await LoadOgrenciler();
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
