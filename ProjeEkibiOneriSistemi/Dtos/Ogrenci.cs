using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Dtos
{
    public class Ogrenci
    {
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Bolum { get; set; }
        public int Sinif { get; set; }
        public string ogrenciNo { get; set; }
        public string TC { get; set; }
        public string ogrenciResmi { get; set; }
        public int ToplamCevaplananSoruSayisi { get; set; }
        public float OrtalamaPuan { get; set; }

    }
}
