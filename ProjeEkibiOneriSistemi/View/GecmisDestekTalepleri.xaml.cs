using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.View;

public partial class GecmisDestekTalepleri : ContentPage
{
    private readonly IDestekServices _destekServices;
	Ogrenci _ogrenci;
	public void setOgrenci(Ogrenci ogrenci)
	{
		_ogrenci = ogrenci;

    }
	public GecmisDestekTalepleri()
	{
		InitializeComponent();
        _destekServices = new DestekServices();

    }

    private async void CollectionViewDestek_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        if (e.CurrentSelection.Count > 0)
        {
            var secilenDestek = e.CurrentSelection[0] as Destek; 
            if (secilenDestek != null)
            {
                GecmisDestekTalepleriBilgiEkrani gecmisDestekTalepleriBilgiEkrani = new GecmisDestekTalepleriBilgiEkrani();
                gecmisDestekTalepleriBilgiEkrani.setDestek(secilenDestek);
                gecmisDestekTalepleriBilgiEkrani.setOgrenci(_ogrenci);
                Application.Current.MainPage = new NavigationPage(gecmisDestekTalepleriBilgiEkrani);

            }
        }
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = (Button)sender;
        await button.ScaleTo(0.9, 100); // Küçültme efekti
        await button.ScaleTo(1, 100); // Eski haline getirme
        OgrenciEkran ogrenciEkran = new OgrenciEkran();
        ogrenciEkran.setOgrenci(_ogrenci);
        Application.Current.MainPage = new NavigationPage(ogrenciEkran);
    }

    private async void ContentPage_Loaded_1(object sender, EventArgs e)
    {
        await getDestekler();
    }
    private async Task getDestekler()
    {
        var destekler = await _destekServices.GetAllDestek();
        var ogrenciDestekTalepleri = destekler.Where(x => x.OgrenciId == _ogrenci.Id);
        if (ogrenciDestekTalepleri != null)
        {
            CollectionViewDestek.ItemsSource = ogrenciDestekTalepleri;
        }
    }
}