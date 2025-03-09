using ProjeEkibiOneriSistemi.Dtos;

namespace ProjeEkibiOneriSistemi.View;

public partial class TestEkrani : ContentPage
{
	public Kategori Kategori;
	public TestEkrani()
	{

		InitializeComponent();
	}
	public void setKategori(Kategori _kategori)
	{
		Kategori = _kategori;
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {

    }
}