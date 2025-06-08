using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;
using System.Collections.ObjectModel;

namespace ProjeEkibiOneriSistemi.View;

public partial class AdminDestekEkrani : ContentPage
{
    private readonly IDestekServices _destekServices;
    private readonly IOgrenciServices _ogreciServices;

    private Ogrenci _Ogrenci;
    private ObservableCollection<Destek> _destekListesi;

    public AdminDestekEkrani()
    {
        InitializeComponent();
        _destekServices = new DestekServices();
        _ogreciServices = new OgrenciServices();
        YükleDestekListesi(); // Sayfa yüklenince veriler gelsin
    }

    public void setAdmin(Ogrenci ogrenci)
    {
        _Ogrenci = ogrenci;
    }

    private async void YükleDestekListesi()
    {
        var liste = await _destekServices.GetAllDestek();
        _destekListesi = new ObservableCollection<Destek>(liste);
        DestekListesi.ItemsSource = _destekListesi;
    }

    private async void Sil_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        int destekId = (int)button.CommandParameter;

        bool onay = await DisplayAlert("Sil", "Bu destek kaydý silinsin mi?", "Evet", "Hayýr");
        if (!onay) return;

        _destekServices.silDestek(destekId); // bool sonuc yok, sadece çaðrýlýr

        var silinecek = _destekListesi.FirstOrDefault(x => x.Id == destekId);
        if (silinecek != null)
            _destekListesi.Remove(silinecek);
    }


    private async void Cevapla_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        Destek secilenDestek = (Destek)button.CommandParameter;

        string cevap = await DisplayPromptAsync("Cevapla", $"Konu: {secilenDestek.Konu}\nAçýklama: {secilenDestek.Açýklama}", "Gönder", "Ýptal", "Yanýtýnýzý yazýn...");

        if (!string.IsNullOrWhiteSpace(cevap))
        {
            var Destekler = await _destekServices.GetAllDestek();

            await _destekServices.silDestek(secilenDestek.Id);
            var yeniDestek = new Destek
            {
                Id = 0, // Yeni ekleme için ID sýfýr olmalý
                AdminId = _Ogrenci.Id,
                AdminCevap = cevap,
                OgrenciId = secilenDestek.OgrenciId,
                Konu = secilenDestek.Konu,
                Açýklama = secilenDestek.Açýklama,
                OlusturmaTarihi = secilenDestek.OlusturmaTarihi
            };
            await _destekServices.ekleDestek(yeniDestek);

            await DisplayAlert("Baþarýlý", "Cevap gönderildi.", "Tamam");
        }
    }
}
