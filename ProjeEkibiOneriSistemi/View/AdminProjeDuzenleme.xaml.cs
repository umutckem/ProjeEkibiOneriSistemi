using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminProjeDuzenleme : ContentPage
{
	private readonly IProjeServices _projeServices;
    private List<Proje> _tumProjeler;
    Ogrenci _ogrenci;

	public void setOgrenci(Ogrenci ogrenci) {

		_ogrenci = ogrenci;
	}
	public AdminProjeDuzenleme()
	{
		InitializeComponent();
        _projeServices = new ProjeServices();

    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await getProjeler();
        await toplamProjeSayisi();

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); // Küçültme efekti
        await button.ScaleTo(1, 100);   // Eski haline getirme

        AdminEkran adminEkran = new AdminEkran();
        adminEkran.setAdmin(_ogrenci);
        Application.Current.MainPage = new NavigationPage(adminEkran);
    }

    private async void CollectionViewProje_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Proje secilenProje)
        {
            if (secilenProje is not null)
            {
                bool secim = await DisplayAlert("Proje Seçildi", $"{secilenProje.Ad} projesini güncellemek ister misiniz?", "Evet", "Hayýr");
                if (secim == true)
                {
                    AdminProjeGuncelleme adminProjeGuncelleme = new AdminProjeGuncelleme();
                    adminProjeGuncelleme.SetOgrenci(_ogrenci);
                    adminProjeGuncelleme.SetProje(secilenProje);
                    Application.Current.MainPage = new NavigationPage(adminProjeGuncelleme);

                }
                else
                {
                    CollectionViewProje.SelectedItem = null; // Seçimi kaldýr
                }
            }
        }

    }

    private async void SearchBarProje_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_tumProjeler == null)
            return;

        string arama = e.NewTextValue?.ToLower() ?? "";
        var filtrelenmisListe = _tumProjeler
            .Where(p => p.Ad.ToLower().Contains(arama) || p.Aciklama.ToLower().Contains(arama))
            .ToList();

        CollectionViewProje.ItemsSource = filtrelenmisListe;
    }
    private async Task getProjeler()
    {
        _tumProjeler = await _projeServices.GetProjes();
        CollectionViewProje.ItemsSource = _tumProjeler;
    }

    private async Task toplamProjeSayisi()
    {
        var projeler = await _projeServices.GetProjes();
        var projeSayisi = projeler.Count();
        ogrenciProje.Text = projeSayisi.ToString();
    }
}