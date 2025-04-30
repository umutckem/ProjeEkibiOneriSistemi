using ProjeEkibiOneriSistemi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Services
{
    public interface IYetkiServices
    {
        Task<List<Yetki>> GetYetkis();
        Task ekleYetki(Yetki yetki);
        Task guncelleYetki(Yetki yetki);
        Task silYetki(Guid id);
    }
}
