using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.View;

public partial class ProjelerimBilgiEkrani : ContentPage
{
    private readonly IOgrenciServices _ogrenciServices;
    private readonly IGrupServices _grupServices;
    private readonly IProjeServices _projeServices;
	Ogrenci _ogrenci;
    Proje _Proje;
	public void SetProje(Proje proje)
    {
        _Proje = proje;
        projeAdi.Text = _Proje.Ad;
        projeAciklama.Text = _Proje.Aciklama;
        projeBaslangicTarihi.Text = Convert.ToString(_Proje.BaslangicTarihi);
        projeBitisTarihi.Text = Convert.ToString(_Proje.BitisTarihi);
        projeBitisAktifMi.Text = Convert.ToString(_Proje.AktifMi);
        projeyeKatilimSayisi.Text = Convert.ToString(_Proje.projeyeKatilimSayisi);
    }
    public void SetOgrenci(Ogrenci ogrenci)
    {
        _ogrenci = ogrenci;
    }
    public ProjelerimBilgiEkrani()
	{
		InitializeComponent();
        _ogrenciServices = new OgrenciServices();
        _grupServices = new GrupServices();
        _projeServices = new ProjeServices();
    }



    private async void ContentPage_Loaded_1(object sender, EventArgs e)
    {
        var gruplar = await _grupServices.getGrups();
        var ogrenciGrup = gruplar.FirstOrDefault(x => x.OgrenciId == _ogrenci.Id);

        if (ogrenciGrup != null && ogrenciGrup.ProjeId == _Proje.Id)
        {
            var ogrenciler = await _ogrenciServices.GetOgrencis();
            var ayniGrupUyeleri = gruplar
                .Where(x => x.GrupNo == ogrenciGrup.GrupNo && x.ProjeId == _Proje.Id)
                .ToList();

            var ayniGrupUyeleriOgrenciler = ayniGrupUyeleri
                .Join(
                    ogrenciler,
                    grup => grup.OgrenciId,
                    ogrenci => ogrenci.Id,
                    (grup, ogrenci) => new
                    {
                        Ad = ogrenci.Ad,
                        Soyad = ogrenci.Soyad,
                        GrupNo = grup.GrupNo
                    }
                ).ToList();

            grupUyeleriCollectionView.ItemsSource = ayniGrupUyeleriOgrenciler;
        }
    }
}
