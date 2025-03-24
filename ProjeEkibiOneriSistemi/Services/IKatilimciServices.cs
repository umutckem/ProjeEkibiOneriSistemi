using ProjeEkibiOneriSistemi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Services
{
    internal interface IKatilimciServices
    {
        Task<List<Katilimci>> GetKatilimcis();
        Task KatilimciEkle(Katilimci katilimci);
        Task KatilimciSil(Guid id);
    }
}
