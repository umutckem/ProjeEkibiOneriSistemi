using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;
using System.Security.Cryptography.X509Certificates;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminProjeErkani : ContentPage
{
    private readonly IProjeServices _projeservices;
    private List<Proje> _tumProjeler;
    Ogrenci _ogrenci;

    public void setOgrenci(Ogrenci ogrenci)
    {
        _ogrenci = ogrenci;
    }

    public AdminProjeErkani()
    {
        InitializeComponent();
        _projeservices = new ProjeServices();
    }

    private async Task getProjeler()
    {
        _tumProjeler = await _projeservices.GetProjes();
        CollectionViewProje.ItemsSource = _tumProjeler;
    }

    private async Task toplamProjeSayisi()
    {
        var projeler = await _projeservices.GetProjes();
        var projeSayisi = projeler.Count();
        ogrenciProje.Text = projeSayisi.ToString();
    }

    private void SearchBarProje_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_tumProjeler == null)
            return;

        string arama = e.NewTextValue?.ToLower() ?? "";
        var filtrelenmisListe = _tumProjeler
            .Where(p => p.Ad.ToLower().Contains(arama) || p.Aciklama.ToLower().Contains(arama))
            .ToList();

        CollectionViewProje.ItemsSource = filtrelenmisListe;
    }

    private async void CollectionViewProje_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Proje secilenProje)
        {
            if (secilenProje is not null)
            {
                bool secim = await DisplayAlert("","Secilen Projeyi Görüntülemek Ýster Misiniz ?","Evet","Hayir");
                if(secim == true)
                {
                    AdminProjeOgrenci adminProjeOgrenci = new AdminProjeOgrenci();
                    adminProjeOgrenci.setOgrenci(_ogrenci);
                    adminProjeOgrenci.setProje(secilenProje);
                    await Navigation.PushAsync(adminProjeOgrenci);
                }
            }
        }
    }

    

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await getProjeler();
        await toplamProjeSayisi();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100);   

        AdminEkran adminEkran = new AdminEkran();
        adminEkran.setAdmin(_ogrenci);
        Application.Current.MainPage = new NavigationPage(adminEkran);
    }


    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100);   

        AdminProjeOlustur adminProjeOlustur = new AdminProjeOlustur();
        adminProjeOlustur.setOgrenci(_ogrenci);
        await Navigation.PushAsync(adminProjeOlustur);
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100);   

        AdminProjeDuzenleme adminProjeDuzenleme = new AdminProjeDuzenleme();
        adminProjeDuzenleme.setOgrenci(_ogrenci);
        Application.Current.MainPage = new NavigationPage(adminProjeDuzenleme);
    }
}
