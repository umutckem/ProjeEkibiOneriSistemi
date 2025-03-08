using ProjeEkibiOneriSistemi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Services
{
    internal interface IOgrenciProjeServices
    {
        Task<List<OgrenciProje>> GetProjes();
        Task ekleOgrenciProje(OgrenciProje ogrenciProje);
        Task guncelleOgrenciProje(OgrenciProje ogrenciProje);
        Task silOgrenciProje(int id);
    }
}
