using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class TestEkrani : ContentPage
{
    private readonly ISoruServices _soruServices;
    private readonly IKullaniciYanitiSerives _yanitServices;
    private readonly IOgrenciServices _ogrenciServices;

    public Kategori Kategori;
    public Ogrenci ogrenci;

    private List<Soru> Sorular;
    private int MevcutSoruIndex = 0;
    private int SecilenPuan = 0;

    public TestEkrani()
    {
        InitializeComponent();
        _soruServices = new SoruServices();
        _yanitServices = new KullaniciYanitiServices();
        _ogrenciServices = new OgrenciServices();
    }

    public void setKategori(Kategori _kategori)
    {
        Kategori = _kategori;
    }

    public void setOgrenci(Ogrenci _ogrenci)
    {
        ogrenci = _ogrenci;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await GetSorular();
    }

    public async Task GetSorular()
    {
        if (Kategori == null)
        {
            await DisplayAlert("Hata", "Kategori bilgisi alýnamadý!", "Tamam");
            await Navigation.PopAsync();
            return;
        }

        var testler = await _soruServices.GetSorus();
        Sorular = testler.Where(x => x.KategoriId == Kategori.Id).ToList();

        if (Sorular.Count == 0)
        {
            await DisplayAlert("Bilgi", "Bu kategori için soru bulunamadý.", "Tamam");
            await Navigation.PopAsync();
            return;
        }

        GosterSoru();
    }

    private void GosterSoru()
    {
        if (MevcutSoruIndex < Sorular.Count)
        {
            var soru = Sorular[MevcutSoruIndex];
            lblSoruMetni.Text = $"Soru {MevcutSoruIndex + 1}/{Sorular.Count}:\n\n{soru.Metin}";
            SecilenPuan = 0;
            TemizleRadioButtonSecimi();
        }
    }

    private void TemizleRadioButtonSecimi()
    {
        foreach (var view in RadioGroup.Children)
        {
            if (view is RadioButton rb)
                rb.IsChecked = false;
        }
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton rb && rb.IsChecked)
        {
            SecilenPuan = int.Parse(rb.Value.ToString());
        }
    }

    private async void BtnSonrakiSoru_Clicked(object sender, EventArgs e)
    {
        btnSonrakiSoru.IsEnabled = false;
        await btnSonrakiSoru.ScaleTo(0.9, 100);
        await btnSonrakiSoru.ScaleTo(1, 100);

        if (SecilenPuan == 0)
        {
            await DisplayAlert("Uyarý", "Lütfen bir seçenek seçiniz.", "Tamam");
            btnSonrakiSoru.IsEnabled = true;
            return;
        }

        if (ogrenci == null)
        {
            await DisplayAlert("Hata", "Öðrenci bilgisi bulunamadý!", "Tamam");
            btnSonrakiSoru.IsEnabled = true;
            return;
        }

        var seciliSoru = Sorular[MevcutSoruIndex];

        var yanit = new KullaniciYaniti
        {
            OgrenciId = ogrenci.Id,
            SoruId = seciliSoru.Id,
            KategoriId = seciliSoru.KategoriId,
            Puan = SecilenPuan
        };

        await _yanitServices.ekleKullaniciYaniti(yanit);

        MevcutSoruIndex++;

        if (MevcutSoruIndex >= Sorular.Count)
        {
            await DisplayAlert("Tamamlandý", "Tüm sorular cevaplandý!", "Tamam");
            await Navigation.PopAsync();
        }
        else
        {
            GosterSoru();
        }

        btnSonrakiSoru.IsEnabled = true;
    }
}
