using Microsoft.Maui.Controls;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View
{
    public partial class AnaEkran : ContentPage
    {
        private readonly IOgrenciServices _ogrenciServices;
        private readonly IYetkiServices _yetkiServices;
        private readonly IRolServices _rolServices;
        
        public AnaEkran()
        {
            InitializeComponent();
            _ogrenciServices = new OgrenciServices();
            _yetkiServices = new YetkiServices();
            _rolServices = new RolServices();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await button.ScaleTo(0.9, 100); 
            await button.ScaleTo(1, 100); 

            if (string.IsNullOrWhiteSpace(ogrenciNo.Text) || string.IsNullOrWhiteSpace(Sifre.Text))
            {
                await DisplayAlert("Hata", "Lütfen tüm alanlarý doldurunuz!", "Tamam");
                
            }
            else
            {
                var OgrenciBilgileri = await _ogrenciServices.GetOgrencis();
                var Yetkiler = await _yetkiServices.GetYetkis();
                var roller = await _rolServices.GetAllRol();


                var ogrenci =  OgrenciBilgileri.FirstOrDefault(x => x.ogrenciNo == ogrenciNo.Text && x.Sifre == Sifre.Text);
                if (ogrenci is not null)
                {
                    var ogrenciYetkisi = Yetkiler.FirstOrDefault(x => x.OgrenciId == ogrenci.Id);
                    if(ogrenciYetkisi != null)
                    {
                        var ogrencirolu = roller.FirstOrDefault(x => x.Id == ogrenciYetkisi.RolId);
                        if (ogrencirolu != null) 
                        {
                            if(ogrencirolu.KullaniciRol == "OGRENCI")
                            {
                                OgrenciEkran ogrenciEkran = new OgrenciEkran();
                                ogrenciEkran.setOgrenci(ogrenci);
                                Application.Current.MainPage = new NavigationPage(ogrenciEkran);

                            }
                            else if(ogrencirolu.KullaniciRol == "ADMIN")
                            {
                                AdminEkran adminEkran = new AdminEkran();
                                adminEkran.setAdmin(ogrenci);
                                Application.Current.MainPage = new NavigationPage(adminEkran);
                            }
                            else
                            {
                                await DisplayAlert("", "Giriþ Yapýlýrken Bir Sorun Oluþtu ", "Tamam");
                            }
                               
                        }
                        else
                        {
                            await DisplayAlert("", "Giriþ Yapýlýrken Bir Sorun Oluþtu ", "Tamam");
                        }
                    }
                    else
                    {
                        await DisplayAlert("","Giriþ Yapýlýrken Bir Sorun Oluþtu","Tamam");
                    }

                }
                else
                {
                    await DisplayAlert("","Ogrenci Bilgisi Bulunamadý","Tamam");
                }
                
            }
            

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var button = (Button)sender;
            await button.ScaleTo(0.9, 100); 
            await button.ScaleTo(1, 100); 

            SifreUnuttumEkrani sifreUnuttumEkrani = new SifreUnuttumEkrani();
            await Navigation.PushAsync(sifreUnuttumEkrani);

        }
    }
}
