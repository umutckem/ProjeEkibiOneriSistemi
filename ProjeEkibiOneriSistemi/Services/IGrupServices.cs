using ProjeEkibiOneriSistemi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Services
{
    internal interface IGrupServices
    {
        Task<List<Grup>> getGrups();
        Task ekleGrup(Grup grup);
        Task guncelleGrup(Grup grup);
        Task silGrup(Guid id);

    }
}
