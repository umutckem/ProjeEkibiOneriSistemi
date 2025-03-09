using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Dtos
{
    public class KullaniciYaniti
    {
        public int Id { get; set; }
        public Guid OgrenciId { get; set; }
        public int KategoriId { get; set; }
        public int SoruId { get; set; }
        public int Puan { get; set; } // 1-10 arasında verilen puan

    }
}
