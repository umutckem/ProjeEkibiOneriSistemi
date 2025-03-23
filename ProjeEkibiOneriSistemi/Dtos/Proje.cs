using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Dtos
{
    public class Proje
    {
        public Guid Id { get; set; }
        public string Ad { get; set; } // Proje adı
        public string Aciklama { get; set; } // Proje hakkında açıklama
        public string Bolum { get; set; }
        public List<int> GerekenKategoriIdler { get; set; } // Projede ihtiyaç duyulan yetenekler (C#, SQL vb.)
        public int ZorlukSeviyesi { get; set; } // 1-5 arasında zorluk derecesi
        public int projeyeKatilimSayisi { get; set; }
        public DateTime BaslangicTarihi { get; set; } // Projenin başlangıç tarihi
        public DateTime? BitisTarihi { get; set; } // Proje bitiş tarihi (null ise devam ediyor)
        public bool AktifMi { get; set; } // Proje devam ediyor mu?
    }
}
