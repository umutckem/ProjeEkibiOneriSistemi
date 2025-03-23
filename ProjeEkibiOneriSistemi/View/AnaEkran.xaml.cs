using Microsoft.Maui.Controls;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View
{
    public partial class AnaEkran : ContentPage
    {
        private readonly IOgrenciServices _ogrenciServices;
        
        public AnaEkran()
        {
            _ogrenciServices = new OgrenciServices();
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await button.ScaleTo(0.9, 100); // Küçültme efekti
            await button.ScaleTo(1, 100); // Eski haline getirme

            if (string.IsNullOrWhiteSpace(ogrenciNo.Text) || string.IsNullOrWhiteSpace(Sifre.Text))
            {
                await DisplayAlert("Hata", "Lütfen tüm alanlarý doldurunuz!", "Tamam");
                return;
            }
            var OgrenciBilgileri = await _ogrenciServices.GetOgrencis();
            var ogrenci =  OgrenciBilgileri.FirstOrDefault(x => x.ogrenciNo == ogrenciNo.Text && x.Sifre == Sifre.Text);
            if (ogrenci is not null)
            {
                await DisplayAlert("Giriþ Baþarýlý", $"{ogrenci.Ad}, hoþ geldiniz!", "Tamam");
                OgrenciEkran ogrenciEkran = new OgrenciEkran();
                ogrenciEkran.setOgrenci(ogrenci);
                await Navigation.PushAsync(ogrenciEkran);
                return;
            }


            await DisplayAlert("Hata","Giriþ Yapýlamadý!","Tamam");

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await button.ScaleTo(0.9, 100); // Küçültme efekti
            await button.ScaleTo(1, 100); // Eski haline getirme

            SifreUnuttumEkrani sifreUnuttumEkrani = new SifreUnuttumEkrani();
            await Navigation.PushAsync(sifreUnuttumEkrani);

        }
    }
}
