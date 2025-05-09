using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.View
{
    public partial class AdminGrupBilgisi : ContentPage
    {
        private readonly IGrupServices _grupServices;
        private readonly IOgrenciServices _ogrenciServices;
        Ogrenci _Ogrenci;
        Grup _Grup;
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

        public void setGrup(Grup grup)
        {
            _Grup = grup;
            LoadOgrenciler();
        }

        public void setOgrenci(Ogrenci ogrenci)
        {
            _Ogrenci = ogrenci;
        }

        private async void LoadOgrenciler()
        {
            try
            {
                // Tüm öðrencileri alýyoruz
                var tumOgrenciler = await _ogrenciServices.GetOgrencis();

                // Gruba ait öðrencileri filtreliyoruz
                var grupOgrencileri = tumOgrenciler
                    .Where(o => _tumGruplar.Any(g => g.GrupNo == _Grup.GrupNo && g.ProjeId == _Grup.ProjeId && g.OgrenciId == o.Id))
                    .ToList();

                // Grup bilgilerini ekrana yazdýrýyoruz
                GrupNoLabel.Text = $"Grup No: {_Grup.GrupNo}";
                ProjeAdLabel.Text = $"Proje Adý: {_Grup.ProjeId}"; // Proje adý almak için baþka bir servis gerekebilir.

                // Öðrencileri listeye ekliyoruz
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
            await button.ScaleTo(0.9, 100); // Küçültme efekti
            await button.ScaleTo(1, 100); // Eski haline getirme

            await DisplayAlert("Bilgi", "Öðrenci ekleme iþlemi burada yapýlacak.", "Tamam");
            // Gruba öðrenci ekleme sayfasýna yönlendirme yapýlabilir
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await button.ScaleTo(0.9, 100); // Küçültme efekti
            await button.ScaleTo(1, 100); // Eski haline getirme

            AdminEkran adminEkran = new AdminEkran();
            adminEkran.setAdmin(_Ogrenci);
            Application.Current.MainPage = new NavigationPage(adminEkran);
        }

        // "Grubu Sil" butonuna týklanýnca çalýþacak metod
        private async void GrubuSilButton_Clicked(object sender, EventArgs e)
        {
            if (_Grup == null)
            {
                await DisplayAlert("Hata", "Silinecek grup bilgisi alýnamadý.", "Tamam");
                return;
            }

            // Silme iþlemini gerçekleþtirme
            bool silinsinMi = await DisplayAlert("Uyarý", $"Grup {_Grup.GrupNo} silinecek. Onaylýyor musunuz?", "Evet", "Hayýr");
            if (silinsinMi)
            {
                await _grupServices.silGrup(_Grup.Id);
                await DisplayAlert("Baþarýlý", $"Grup {_Grup.GrupNo} baþarýyla silindi.", "Tamam");
                // Grubu sildikten sonra, sayfayý veya listeyi güncelleyebilirsiniz.
                // Örneðin, admin ekranýna geri yönlendirme yapýlabilir.
                AdminEkran adminEkran = new AdminEkran();
                adminEkran.setAdmin(_Ogrenci);
                Application.Current.MainPage = new NavigationPage(adminEkran);
            }
        }
    }
}
