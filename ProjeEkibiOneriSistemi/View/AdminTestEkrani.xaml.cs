using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminTestEkrani : ContentPage
{
    private readonly ISoruServices _soruServices;
    private readonly IKategoriServices _kategoriServices;
    private Ogrenci _ogrenci;
    private Kategori _kategori;

    public AdminTestEkrani()
    {
        InitializeComponent();
        _soruServices = new SoruServices();
        _kategoriServices = new KategoriServices();
    }

    public void setKategori(Kategori kategori)
    {
        _kategori = kategori;
    }

    public void setOgrenci(Ogrenci ogrenci)
    {
        _ogrenci = ogrenci;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (_kategori == null)
        {
            await DisplayAlert("Uyarý", "Kategori bilgisi eksik.", "Tamam");
            return;
        }

        try
        {
            var tumSorular = await _soruServices.GetSorus();
            var filtrelenmis = tumSorular.Where(s => s.KategoriId == _kategori.Id).ToList();

            SoruCollection.ItemsSource = filtrelenmis;

            if (filtrelenmis.Count == 0)
            {
                await DisplayAlert("Bilgi", "Bu kategoriye ait hiç soru yok.", "Tamam");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Sorular yüklenemedi: {ex.Message}", "Tamam");
        }
    }


    private async void SoruEkle_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); 
        await button.ScaleTo(1, 100); 

        AdminTestOlustur adminTestOlustur = new AdminTestOlustur();
        adminTestOlustur.setKategori(_kategori);
        adminTestOlustur.setOgrenci(_ogrenci);
        await Navigation.PushAsync(adminTestOlustur);

    }

    private async void KategoriSil_Clicked(object sender, EventArgs e)
    {
        
        bool secim = await DisplayAlert("Kategori Silme", "Bu kategoriyi ve o kategoriye ait tüm sorularý silmek istediðinizden emin misiniz?", "Evet", "Hayýr");

        if (secim)
        {
            try
            {
                
                var tumSorular = await _soruServices.GetSorus();
                var kategoriyeAitSorular = tumSorular.Where(s => s.KategoriId == _kategori.Id).ToList();

                
                foreach (var soru in kategoriyeAitSorular)
                {
                    await _soruServices.silSoru(soru.Id);
                }
                await _kategoriServices.removeKategori(_kategori.Id);
                await DisplayAlert("Baþarýlý", "Kategori ve ilgili sorular baþarýyla silindi.", "Tamam");

                var adminKategoriEkrani = new AdminKategoriEkrani();
                adminKategoriEkrani.setOgrenci(_ogrenci);
                Application.Current.MainPage = new NavigationPage(adminKategoriEkrani);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Kategori ve sorular silinirken hata oluþtu: {ex.Message}", "Tamam");
            }
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var secilenSoru = SoruCollection.SelectedItem as Soru;

        if (secilenSoru == null)
        {
            await DisplayAlert("Uyarý", "Lütfen silmek istediðiniz bir soruyu seçin.", "Tamam");
            return;
        }

        bool onay = await DisplayAlert("Soru Sil", "Seçilen soruyu silmek istediðinize emin misiniz?", "Evet", "Hayýr");

        if (onay)
        {
            try
            {
                await _soruServices.silSoru(secilenSoru.Id);

                
                var guncelListe = (SoruCollection.ItemsSource as List<Soru>).ToList();
                guncelListe.Remove(secilenSoru);
                SoruCollection.ItemsSource = null; 
                SoruCollection.ItemsSource = guncelListe;

                await DisplayAlert("Baþarýlý", "Soru baþarýyla silindi.", "Tamam");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Soru silinirken hata oluþtu: {ex.Message}", "Tamam");
            }
        }
    }

}
