using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Dtos
{
    public class Grup
    {
        public Guid Id { get; set; }
        public Guid OgrenciId { get; set; }
        public Guid ProjeId { get; set; }
        public int GrupNo { get; set; }

    }
}
