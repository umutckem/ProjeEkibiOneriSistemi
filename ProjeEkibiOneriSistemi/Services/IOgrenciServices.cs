using ProjeEkibiOneriSistemi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Services
{
    internal interface IOgrenciServices
    {
        Task<List<Ogrenci>> GetOgrencis();

        Task ekleOgrenci(Ogrenci ogrenci);

        Task guncelleOgrenci(Ogrenci ogrenci);

        Task silOgrenci(Guid id);

    }
}
