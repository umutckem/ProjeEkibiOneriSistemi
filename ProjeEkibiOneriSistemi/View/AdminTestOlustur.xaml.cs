using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminTestOlustur : ContentPage
{
	Kategori _kategori;
	Ogrenci _ogrenci;
    private readonly ISoruServices _soruServices;

    public void setKategori(Kategori kategori)
    {
        _kategori = kategori;
    }

    public void setOgrenci(Ogrenci ogrenci)
    {
        _ogrenci = ogrenci;
    }
    public AdminTestOlustur()
	{
		InitializeComponent();
        _soruServices = new SoruServices();

    }

    private async void OnSoruOlusturClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryMetin.Text) ||
            string.IsNullOrWhiteSpace(entryCevap.Text) ||
            pickerOnem.SelectedItem == null)
        {
            await DisplayAlert("Hata", "Lütfen tüm alanlarý doldurun.", "Tamam");
            return;
        }

        var yeniSoru = new Soru
        {
            Metin = entryMetin.Text,
            Cevap = entryCevap.Text,
            OnemDerecesi = pickerOnem.SelectedItem.ToString(),
            KategoriId = _kategori?.Id ?? 0 
        };

        try
        {
            await _soruServices.ekleSoru(yeniSoru); 
            await DisplayAlert("Baþarýlý", "Soru eklendi.", "Tamam");

            
            entryMetin.Text = "";
            entryCevap.Text = "";
            pickerOnem.SelectedItem = null;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Soru eklenirken hata oluþtu: {ex.Message}", "Tamam");
        }
    }

}