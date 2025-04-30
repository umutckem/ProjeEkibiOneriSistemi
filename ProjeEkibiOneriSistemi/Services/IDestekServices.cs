using ProjeEkibiOneriSistemi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Services
{
    internal interface IDestekServices
    {
        Task<List<Destek>> GetAllDestek();
        Task ekleDestek(Destek destek);
        Task silDestek(int id);

    }
}
