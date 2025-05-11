using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminGrupOgrenciCikar : ContentPage
{
    private readonly IProjeServices _projeServices;
	private readonly IOgrenciServices _ogrenciServices;
    private readonly IGrupServices _grupServices;
    Grup _grup;
    Ogrenci _ogrenci;

    public void setGrup(Grup grup)
    {
        _grup = grup;
    }
    public void setOgrenci(Ogrenci ogrenci)
    {
        _ogrenci = ogrenci;
    }
    public AdminGrupOgrenciCikar()
	{
		InitializeComponent();
        _projeServices = new ProjeServices();
        _ogrenciServices = new OgrenciServices();
        _grupServices = new GrupServices();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_grup == null)
            return;

        
        var tumGruplar = await _grupServices.getGrups();
        var ilgiliGrupKayitlari = tumGruplar
            .Where(g => g.GrupNo == _grup.GrupNo && g.ProjeId == _grup.ProjeId)
            .ToList();

        
        var tumOgrenciler = await _ogrenciServices.GetOgrencis();
        var ogrenciler = tumOgrenciler
            .Where(o => ilgiliGrupKayitlari.Any(g => g.OgrenciId == o.Id))
            .ToList();

        ogrenciListesi.ItemsSource = ogrenciler;
    }

    private async void OnOgrenciCikarClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var secilenOgrenci = (Ogrenci)button.CommandParameter;

        bool onay = await DisplayAlert("Onay",
            $"{secilenOgrenci.Ad} {secilenOgrenci.Soyad} öðrencisini bu gruptan çýkarmak istiyor musunuz?",
            "Evet", "Hayýr");

        if (onay)
        {
            
            var tumGruplar = await _grupServices.getGrups();
            var kayit = tumGruplar.FirstOrDefault(g =>
                g.ProjeId == _grup.ProjeId &&
                g.GrupNo == _grup.GrupNo &&
                g.OgrenciId == secilenOgrenci.Id);

            if (kayit != null)
            {
                await _grupServices.silGrup(kayit.Id); 
                await DisplayAlert("Baþarýlý", "Öðrenci gruptan çýkarýldý.", "Tamam");
                OnAppearing(); 
            }
            else
            {
                await DisplayAlert("Hata", "Grup kaydý bulunamadý.", "Tamam");
            }
        }
    }



}