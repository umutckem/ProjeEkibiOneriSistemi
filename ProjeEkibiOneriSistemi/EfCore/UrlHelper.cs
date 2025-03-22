using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.EfCore
{
    public static class UrlHelper
    {
        private static string BaseUrl = "https://localhost:7272";

        // Öğrenci API URL
        public static string OgrenciUrl = $"{BaseUrl}/Ogrenci";

        // Kategori API URL
        public static string KategoriUrl = $"{BaseUrl}/Kategori";

        // Soru API URL
        public static string SoruUrl = $"{BaseUrl}/Soru";

        // Kullanıcı Yanıtları API URL
        public static string YanitlarUrl = $"{BaseUrl}/Yanitlar";

        // Proje API URL (Eksik olan kısım tamamlandı)
        public static string ProjeUrl = $"{BaseUrl}/Projeler";

        // Öğrenci Proje API URL (Öğrencinin geçmiş projelerini takip etmek için)
        public static string OgrenciProjeUrl = $"{BaseUrl}/OgrenciProje";
    }
}
