using ProjeEkibiOneriSistemi.View;

namespace ProjeEkibiOneriSistemi
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AnaEkran());

        }
    }
}
