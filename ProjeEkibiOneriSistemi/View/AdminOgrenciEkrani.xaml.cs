using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminOgrenciEkrani : ContentPage
{
    private readonly IOgrenciServices _ogrenciServices;
    private List<Ogrenci> _tumOgrenciler; // Tüm öðrenciler burada tutulur
    Ogrenci _ogrenci;

    public void setOgrenci(Ogrenci ogrenci)
    {
        _ogrenci = ogrenci;
    }

    public AdminOgrenciEkrani()
    {
        InitializeComponent();
        _ogrenciServices = new OgrenciServices();
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await getOgrenciler();
        await getKayitliOgrenciSayisi();
    }

    private async Task getOgrenciler()
    {
        _tumOgrenciler = await _ogrenciServices.GetOgrencis();
        CollectionViewOgrenci.ItemsSource = _tumOgrenciler;
    }

    private void SearchBarOgrenci_TextChanged(object sender, TextChangedEventArgs e)
    {
        string arama = e.NewTextValue?.ToLower() ?? "";

        var filtrelenmis = _tumOgrenciler
            .Where(o =>
                (!string.IsNullOrWhiteSpace(o.Ad) && o.Ad.ToLower().Contains(arama)) ||
                (!string.IsNullOrWhiteSpace(o.Soyad) && o.Soyad.ToLower().Contains(arama)) ||
                (!string.IsNullOrWhiteSpace(o.ogrenciNo) && o.ogrenciNo.ToLower().Contains(arama)) || // düzeltildi
                (!string.IsNullOrWhiteSpace(o.Telefon) && o.Telefon.Contains(arama)) // sadece burada olmalý
            )
            .ToList();

        CollectionViewOgrenci.ItemsSource = filtrelenmis;
    }


    private async void CollectionViewOgrenci_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Ýstersen seçilen öðrenci ile iþlem yapabilirsin.
        var secilenOgrenci = e.CurrentSelection.FirstOrDefault() as Ogrenci;
        if (secilenOgrenci == null) return;

        bool secim = await DisplayAlert("Seçilen Öðrenci", $"{secilenOgrenci.Ad} {secilenOgrenci.Soyad}", "Evet","Hayýr");
        if(secim == true) { 
        AdminOgrenciDuzenlemeEkrani adminOgrenciDuzenlemeEkrani = new AdminOgrenciDuzenlemeEkrani();
        adminOgrenciDuzenlemeEkrani.setOgrenci(_ogrenci);
        adminOgrenciDuzenlemeEkrani.setDuzenlenecekOgrenci(secilenOgrenci);
        Application.Current.MainPage = new NavigationPage(adminOgrenciDuzenlemeEkrani);
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); // Küçültme efekti
        await button.ScaleTo(1, 100); // Eski haline getirme

        AdminEkran adminEkran = new AdminEkran();
        adminEkran.setAdmin(_ogrenci);
        Application.Current.MainPage = new NavigationPage(adminEkran);
    }
    private async Task getKayitliOgrenciSayisi()
    {
        if(_ogrenci is not null)
        {
            var kayitliOgrenciler = await _ogrenciServices.GetOgrencis();
            var kayitliOgrencilerSayisi = kayitliOgrenciler.Count();
            ogrenciSayisi.Text = Convert.ToString(kayitliOgrencilerSayisi);
        }
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); // Küçültme efekti
        await button.ScaleTo(1, 100); // Eski haline getirme

        AdminOgrenciOlustur adminOgrenciOlustur = new AdminOgrenciOlustur();
        adminOgrenciOlustur.setOgrenci(_ogrenci);
        Application.Current.MainPage = new NavigationPage(adminOgrenciOlustur);

    }
}
