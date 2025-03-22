using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class ProjeEkrani : ContentPage
{
	private readonly IProjeServices _projeServices;
	Ogrenci _ogrenci;

	public void setOgrenci(Ogrenci ogrenci)
	{
		_ogrenci = ogrenci;

    }
	public ProjeEkrani()
	{
		InitializeComponent();
		_projeServices = new ProjeServices();
	}



    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
		await GetProjeler();
    }
	private async Task GetProjeler()
	{
		var Projeler = await _projeServices.GetProjes();
        CollectionViewProje.ItemsSource = Projeler;

    }

    private void CollectionViewProje_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var secilenProje = e.CurrentSelection[0] as Proje; // Seçilen kategoriyi al
    }
}