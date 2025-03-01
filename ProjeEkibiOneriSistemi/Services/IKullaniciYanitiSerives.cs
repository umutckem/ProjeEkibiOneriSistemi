using ProjeEkibiOneriSistemi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Services
{
    internal interface IKullaniciYanitiSerives
    {
        Task<List<KullaniciYaniti>> GetKullaniciYanitis();
        Task ekleKullaniciYaniti(KullaniciYaniti kullaniciYaniti);
        Task silKullaniciYaniti(int id);

    }
}
