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

        public static string OgrenciUrl = $"{BaseUrl}/Ogrenci";

        public static string KategoriUrl = $"{BaseUrl}/Kategori";

        public static string SoruUrl = $"{BaseUrl}/Soru";

        public static string YanitlarUrl = $"{BaseUrl}/Yanitlar";

        public static string ProjeUrl = $"{BaseUrl}/Projeler";

        public static string OgrenciProjeUrl = $"{BaseUrl}/OgrenciProje";

        public static string KatilimciUrl = $"{BaseUrl}/Katilimcilar";

        public static string GrupUrl = $"{BaseUrl}/grup";

        public static string DestekUrl = $"{BaseUrl}/Destek";

        public static string RolUrl = $"{BaseUrl}/Rol";

        public static string YetkiUrl = $"{BaseUrl}/Yetki";

    }
}
