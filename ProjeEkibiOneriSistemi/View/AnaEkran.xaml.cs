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
            var OgrenciBilgileri = await _ogrenciServices.GetOgrencis();

            await DisplayAlert("Baþarýlý","","Tamam");

        }
    }
}
