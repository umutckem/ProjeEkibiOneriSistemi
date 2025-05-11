using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class ProjeBilgiEkrani : ContentPage
{
    private readonly IKatilimciServices _katilimciServices;
    private readonly IProjeServices _projeServices;
    private readonly IKullaniciYanitiSerives _kullaniciYanitiSerives;
    private readonly IOgrenciServices _ogrenciServices;
    Proje _Proje;
    Ogrenci _Ogrenci;

    public void setProje(Proje proje)
    {
        _Proje = proje;
        BindingContext = null;
        BindingContext = _Proje;
    }

    public void setOgrenci(Ogrenci ogrenci)
    {
        _Ogrenci = ogrenci;
    }

    public ProjeBilgiEkrani()
    {
        InitializeComponent();
        _ogrenciServices = new OgrenciServices();
        _kullaniciYanitiSerives = new KullaniciYanitiServices();
        _projeServices = new ProjeServices();
        _katilimciServices = new KatilimciServices();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        
        var ogrenciBilgileri = await _ogrenciServices.GetOgrencis();
        var ogrenci = ogrenciBilgileri.FirstOrDefault(x => x.Id == _Ogrenci.Id);

        if (ogrenci is not null)
        {
            var projeler = await _projeServices.GetProjes();
            var proje = projeler.FirstOrDefault(x => x.Id == _Proje.Id);

            if (proje is not null)
            {
                var kullaniciYanitiListesi = await _kullaniciYanitiSerives.GetKullaniciYanitis();
                var kullaniciYaniti = kullaniciYanitiListesi
                    .Where(x => x.OgrenciId == ogrenci.Id)
                    .Select(x => x.KategoriId) 
                    .Distinct() 
                    .ToList();

                
                var ogrenciYetkinlikleri = string.Join(", ", kullaniciYaniti);
                OgrenciYetkinlikleriLabel.Text = ogrenciYetkinlikleri;

                
                var projeGereksinimleri = string.Join(", ", proje.GerekenKategoriIdler);
                ProjeGereksinimleriLabel.Text = projeGereksinimleri;

                
                bool uygunMu = proje.GerekenKategoriIdler.All(kategoriId => kullaniciYaniti.Contains(kategoriId));

                if (uygunMu)
                {
                    KatilimButton.IsEnabled = true; 
                    KatilimButton.Text = "Katýl";
                }
                else
                {
                    KatilimButton.IsEnabled = false; 
                    KatilimButton.Text = "Uygun Deðil"; 
                }
            }
        }
        else
        {
            await DisplayAlert("", "Öðrenci Bilgileri Bulunamadý!", "Tamam");
        }
    }


    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        var ogrenciBilgileri = await _ogrenciServices.GetOgrencis();
        var ogrenci = ogrenciBilgileri.FirstOrDefault(x => x.Id == _Ogrenci.Id);

        if (ogrenci is not null)
        {
            var projeler = await _projeServices.GetProjes();
            var proje = projeler.FirstOrDefault(x => x.Id == _Proje.Id);

            if (proje is not null)
            {
                var kullaniciYanitiListesi = await _kullaniciYanitiSerives.GetKullaniciYanitis();
                var kullaniciYaniti = kullaniciYanitiListesi
                    .Where(x => x.OgrenciId == ogrenci.Id)
                    .Select(x => x.KategoriId) 
                    .ToList();

                bool uygunMu = proje.GerekenKategoriIdler.All(kategoriId => kullaniciYaniti.Contains(kategoriId));

                if (uygunMu)
                {
                    var Katilimcilar = await _katilimciServices.GetKatilimcis();
                    var katilimci = Katilimcilar.FirstOrDefault(x => x.OgrenciId == ogrenci.Id && x.ProjeId == _Proje.Id);
                    if (katilimci == null)
                    {
                        await _katilimciServices.KatilimciEkle(new Katilimci
                        {
                            Id = new Guid(),
                            OgrenciId = ogrenci.Id,
                            ProjeId = proje.Id
                        });

                        await DisplayAlert("Baþarýlý", "Öðrenci projeye katýldý.", "Tamam");
                    }
                    else
                    {
                        await DisplayAlert("", "Öðrenci projeye zaten katýldý.", "Tamam");
                    }
                }
                else
                {
                    await DisplayAlert("Uygun Deðil", "Öðrencinin yetkinliði bu projeye uygun deðil!", "Tamam");
                }
            }
        }
        else
        {
            await DisplayAlert("", "Öðrenci Bilgileri Bulunamadý!", "Tamam");
        }
    }
}
