using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;
using System.Linq;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminKategoriEkrani : ContentPage
{
    private readonly ISoruServices _soruServices;
    private readonly IKategoriServices _kategoriServices;
    private List<object> _tumTestler = new();
    private Ogrenci _ogrenci;

    public void setOgrenci(Ogrenci ogrenci)
    {
        _ogrenci = ogrenci;
    }

    public AdminKategoriEkrani()
    {
        InitializeComponent();
        _soruServices = new SoruServices();
        _kategoriServices = new KategoriServices();
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        var tumKategoriler = await _kategoriServices.GetKategoris();
        var tumSorular = await _soruServices.GetSorus();

        _tumTestler = tumKategoriler
            .Select(kat => new
            {
                Id = kat.Id,
                Kategori = kat,
                KategoriAdi = kat.Ad,
                IlgiliBolum = kat.IlgiliBolum,
                SoruSayisi = tumSorular.Count(s => s.KategoriId == kat.Id)
            })
            .Cast<object>()
            .ToList();

        CollectionViewTestler.ItemsSource = _tumTestler;
        testSayisi.Text = _tumTestler.Count.ToString();
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

    private void SearchBarTest_TextChanged(object sender, TextChangedEventArgs e)
    {
        string query = e.NewTextValue?.ToLower() ?? "";

        var filtrelenmis = _tumTestler
            .Where(t =>
                t.GetType().GetProperty("KategoriAdi")?.GetValue(t)?.ToString()?.ToLower().Contains(query) == true
                || t.GetType().GetProperty("SoruSayisi")?.GetValue(t)?.ToString()?.Contains(query) == true
                || t.GetType().GetProperty("IlgiliBolum")?.GetValue(t)?.ToString()?.ToLower().Contains(query) == true
            )
            .ToList();

        CollectionViewTestler.ItemsSource = filtrelenmis;
    }

    private async void CollectionViewTestler_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var secilenItem = e.CurrentSelection.FirstOrDefault();
        if (secilenItem == null) return;

        var kategori = (secilenItem as dynamic).Kategori as Kategori;
        if (kategori == null) return;

        bool secim = await DisplayAlert("Seçilen Kategori", $"{kategori.Id} numaralý {kategori.Ad} isimli Kategoriyi Güncellemek Ýster Misiniz?", "Evet", "Hayýr");
        if (secim)
        {
            var adminTestEkrani = new AdminTestEkrani();
            adminTestEkrani.setOgrenci(_ogrenci);
            adminTestEkrani.setKategori(kategori);
            await Navigation.PushAsync(adminTestEkrani);
        }
    }

    private async void Button_TestEkle_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100);
        await button.ScaleTo(1, 100);

        AdminKategoriEkle adminKategoriEkle = new AdminKategoriEkle();
        adminKategoriEkle.setOgrenci(_ogrenci);
        await Navigation.PushAsync(adminKategoriEkle);
    }
}
