using ProjeEkibiOneriSistemi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Services
{
    internal interface ISoruServices
    {
        Task<List<Soru>> GetSorus();

        Task ekleSoru(Soru soru);

        Task guncelleSoru(Soru soru);

        Task silSoru(int id);

    }
}
