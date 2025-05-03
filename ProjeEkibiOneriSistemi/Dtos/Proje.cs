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
        public string Ad { get; set; } 
        public string Aciklama { get; set; } 
        public string Bolum { get; set; }
        public List<int> GerekenKategoriIdler { get; set; } 
        public int ZorlukSeviyesi { get; set; } 
        public int projeyeKatilimSayisi { get; set; }
        public DateTime BaslangicTarihi { get; set; } 
        public DateTime? BitisTarihi { get; set; } 
        public bool AktifMi { get; set; } 
    }
}
