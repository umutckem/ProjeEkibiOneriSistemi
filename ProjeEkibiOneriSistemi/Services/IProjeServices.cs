using ProjeEkibiOneriSistemi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Services
{
    internal interface IProjeServices
    {
        Task<List<Proje>> GetProjes();
        Task projeEkle(Proje proje);
        Task projeGuncelle(Proje proje);
        Task projeSil(Guid id);
    }
}
