using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;
using System;

namespace ProjeEkibiOneriSistemi.View
{
    public partial class AdminGrupEkle : ContentPage
    {
        private readonly IGrupServices _grupServices;
        Ogrenci _ogrenci;
        private Proje _proje;

        public AdminGrupEkle()
        {
            InitializeComponent();
            _grupServices = new GrupServices();
        }

        public void setProje(Proje proje)
        {
            _proje = proje;
        }

        public void setOgrenci(Ogrenci ogrenci)
        {
            _ogrenci = ogrenci;
        }

        private async void GrubuEkle_Clicked(object sender, EventArgs e)
        {
            if (_proje == null)
            {
                await DisplayAlert("Hata", "Proje bilgisi alýnamadý.", "Tamam");
                return;
            }

            if (int.TryParse(grupNoEntry.Text, out int grupNo))
            {
                // Mevcut gruplarý al
                List<Grup> gruplar = await _grupServices.getGrups();

                // Grup numarasýnýn bu projede daha önce olup olmadýðýný kontrol et
                bool grupVarMi = gruplar.Any(grup => grup.GrupNo == grupNo && grup.ProjeId == _proje.Id);

                if (grupVarMi)
                {
                    await DisplayAlert("Uyarý", "Bu grup numarasý zaten mevcut. Lütfen baþka bir numara girin.", "Tamam");
                    return;
                }

                Grup yeniGrup = new Grup
                {
                    Id = Guid.NewGuid(),
                    ProjeId = _proje.Id,
                    OgrenciId = Guid.Empty, // Ýlgili öðrenci daha sonra atanabilir
                    GrupNo = grupNo
                };

                await _grupServices.ekleGrup(yeniGrup);
                await DisplayAlert("Baþarýlý", $"Grup {grupNo} baþarýyla eklendi.", "Tamam");
                grupNoEntry.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Uyarý", "Geçerli bir grup numarasý girin.", "Tamam");
            }
        }

        private async void GeriDon_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await button.ScaleTo(0.9, 100);
            await button.ScaleTo(1, 100);

            AdminEkran adminEkran = new AdminEkran();
            adminEkran.setAdmin(_ogrenci);
            Application.Current.MainPage = new NavigationPage(adminEkran);
        }
    }
}
