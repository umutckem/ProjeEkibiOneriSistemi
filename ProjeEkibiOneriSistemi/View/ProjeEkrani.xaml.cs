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

    private async void CollectionViewProje_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		if (e.CurrentSelection.Count > 0)
		{
            var secilenProje = e.CurrentSelection[0] as Proje; // Seçilen kategoriyi al
			if (secilenProje != null) 
			{

                bool secim = await DisplayAlert("Seçilen Proje", $"Ad: {secilenProje.Ad}\nAçýklama: {secilenProje.Aciklama}", "Tamam", "Hayýr");

				if (secim) 
				{
                    if (_ogrenci == null)
                    {
                        await DisplayAlert("Hata", "Öðrenci bilgisi bulunamadý!", "Tamam");
                        return;
                    }

                    ProjeBilgiEkrani projeBilgiEkrani = new ProjeBilgiEkrani();
                    projeBilgiEkrani.setOgrenci(_ogrenci);
                    projeBilgiEkrani.setProje(secilenProje);
                    await Navigation.PushAsync(projeBilgiEkrani);

                }


            }

        }

            
    }
}