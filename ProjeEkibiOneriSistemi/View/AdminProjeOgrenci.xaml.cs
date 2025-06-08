using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;
using System.Collections.ObjectModel;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminProjeOgrenci : ContentPage
{
    private readonly IGrupServices _grupServices;
    Ogrenci _Ogrenci;
    private Proje _proje;
    private List<Grup> _tumGruplar;

    public void setOgrenci(Ogrenci ogrenci)
    {
        _Ogrenci = ogrenci;
    }

    public AdminProjeOgrenci()
    {
        InitializeComponent();
        _grupServices = new GrupServices();
    }

    public void setProje(Proje proje)
    {
        _proje = proje;
        LoadGruplar();
    }

    private async void LoadGruplar()
    {
        try
        {
            _tumGruplar = await _grupServices.getGrups();

            
            var projeGruplari = _tumGruplar
                .Where(g => g.ProjeId == _proje.Id)
                .GroupBy(g => g.GrupNo)
                .Select(grup => grup.First())
                .OrderBy(g => g.GrupNo)
                .ToList();

            GrupListView.ItemsSource = projeGruplari;

        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Gruplar yüklenirken sorun oluþtu: {ex.Message}", "Tamam");
        }
    }

    private async void GrupListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var secilenGrup = e.CurrentSelection.FirstOrDefault() as Grup;
        if (secilenGrup != null)
        {
            AdminGrupBilgisi adminGrupBilgisi = new AdminGrupBilgisi();
            adminGrupBilgisi.setGrup(secilenGrup);
            adminGrupBilgisi.setOgrenci(_Ogrenci);
            adminGrupBilgisi.setProje(_proje);
            adminGrupBilgisi.setTumGruplar(_tumGruplar); 
            await Navigation.PushAsync(adminGrupBilgisi);   
        }
    }

    private async void AnaMenuButton_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100);
        await button.ScaleTo(1, 100);

        AdminEkran adminEkran = new AdminEkran();
        adminEkran.setAdmin(_Ogrenci);
        Application.Current.MainPage = new NavigationPage(adminEkran);
    }

    private async void GrupEkleButton_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100);
        await button.ScaleTo(1, 100);

        AdminGrupEkle adminGrupEkle = new AdminGrupEkle();
        adminGrupEkle.setProje(_proje);
        adminGrupEkle.setOgrenci(_Ogrenci);
        await Navigation.PushAsync(adminGrupEkle);
    }

    
    public int GetGrupOgrenciSayisi(int grupNo)
    {
        return _tumGruplar
            .Count(g => g.ProjeId == _proje.Id && g.GrupNo == grupNo);
    }
}
