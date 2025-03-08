using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Dtos
{
    public class Soru
    {
        public int Id { get; set; }
        public int KategoriId { get; set; } //İlgili Kategorinin Id'si
        public string Metin { get; set; } // Sorunun içeriği
        public string Cevap { get; set; }
        public string OnemDerecesi { get; set; } // 1-5 arasında bir OnemDerecesi

    }
}
