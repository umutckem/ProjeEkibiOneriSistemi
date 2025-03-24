using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;

namespace ProjeEkibiOneriSistemi.View;

public partial class ProjelerimEkrani : ContentPage
{
	private readonly IProjeServices _projeServices;
	private readonly IKatilimciServices _katilimciServices;
	Ogrenci _Ogrenci;
	public void setOgrenci(Ogrenci ogrenci)
	{
		_Ogrenci = ogrenci;
    }
	public ProjelerimEkrani()
	{
		InitializeComponent();
		_katilimciServices = new KatilimciServices();
		_projeServices = new ProjeServices();

    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
		await getKatilimcilar();
    }
    public async Task getKatilimcilar()
    {
        var katilimcilar = await _katilimciServices.GetKatilimcis();
        var katilimciList = katilimcilar.Where(x => x.OgrenciId == _Ogrenci.Id).ToList();

        if (katilimciList.Any()) // Koleksiyon boþ mu kontrolü
        {
            var projeler = await _projeServices.GetProjes();

            // Sadece ilgili öðrencinin katýldýðý projeleri listele
            var ogrenciProjeleri = projeler
                .Where(p => katilimciList.Any(k => k.ProjeId == p.Id))
                .ToList();

            CollectionViewKatilimcilar.ItemsSource = ogrenciProjeleri;
        }
    }



    private async void CollectionViewKatilimcilar_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            var secilenProje = e.CurrentSelection[0] as Proje;

            if(secilenProje is not null)
            {
                ProjelerimBilgiEkrani projelerimBilgiEkrani = new ProjelerimBilgiEkrani();
                projelerimBilgiEkrani.SetProje(secilenProje);
                projelerimBilgiEkrani.SetOgrenci(_Ogrenci);
                await Navigation.PushAsync(projelerimBilgiEkrani);
            }

        }
    }
}