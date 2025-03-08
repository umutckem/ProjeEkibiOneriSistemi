using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Dtos
{
    public class OgrenciProje
    {
        public int Id { get; set; }
        public Guid OgrenciId { get; set; }
        public int ProjeId { get; set; }
        public string Rol { get; set; } // Yazılım Geliştirici, Veri Analisti, Tester gibi roller
        public string Durum { get; set; } // Tamamlandı, Devam Ediyor, Başarısız vb.
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; } // Proje devam ediyorsa null olabilir
    }
}
