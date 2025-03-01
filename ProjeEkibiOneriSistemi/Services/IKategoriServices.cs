using ProjeEkibiOneriSistemi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Services
{
    internal interface IKategoriServices
    {
        Task<List<Kategori>> GetKategoris();

        Task ekleKategori(Kategori kategori);

        Task removeKategori(int id);
    }
}
