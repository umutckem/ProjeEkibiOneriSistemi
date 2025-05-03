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
        public int KategoriId { get; set; } 
        public string Metin { get; set; } 
        public string Cevap { get; set; }
        public string OnemDerecesi { get; set; } 

    }
}
