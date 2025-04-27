using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.Services;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.View;

public partial class ProjelerimBilgiEkrani : ContentPage
{
    private readonly IOgrenciServices _ogrenciServices;
    private readonly IGrupServices _grupServices;
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
    }



    private async void ContentPage_Loaded_1(object sender, EventArgs e)
    {
        var gruplar = await _grupServices.getGrups();
        var ogrenciGrup = gruplar.FirstOrDefault(x => x.OgrenciId == _ogrenci.Id);

        if (ogrenciGrup != null)
        {
            var ogrenciler = await _ogrenciServices.GetOgrencis();
            var ayniGrupUyeleri = gruplar.Where(x => x.GrupNo == ogrenciGrup.GrupNo).ToList();

            // Grup üyelerinin öðrenci bilgilerini çekiyoruz
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

            // CollectionView'a gönderiyoruz
            grupUyeleriCollectionView.ItemsSource = ayniGrupUyeleriOgrenciler;
        }
    }
}