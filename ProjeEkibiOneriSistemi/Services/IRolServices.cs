using ProjeEkibiOneriSistemi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Services
{
    internal interface IRolServices
    {
        Task<List<Rol>> GetAllRol();
        Task ekleRol(Rol rol);
        Task silRol(Guid id);
    }
}
