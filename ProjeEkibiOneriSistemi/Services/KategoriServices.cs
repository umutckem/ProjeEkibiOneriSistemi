using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.EfCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Services
{
    public class KategoriServices : BaseService, IKategoriServices
    {
        public async Task ekleKategori(Kategori kategori)
        {
            Uri uri = new Uri(UrlHelper.KategoriUrl);
            string jsonContent = JsonSerializer.Serialize(kategori, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Kategori başarıyla eklendi!");
            }
            else
            {
                Console.WriteLine($"Kategori eklenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task<List<Kategori>> GetKategoris()
        {
            Uri uri = new Uri(UrlHelper.KategoriUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Kategori>>(content, _serializerOptions) ?? new List<Kategori>();
            }
            return new List<Kategori>();
        }

        public async Task removeKategori(int id)
        {
            Uri uri = new Uri($"{UrlHelper.KategoriUrl}/{id}");
            HttpResponseMessage response = await _client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"ID {id} olan kategori başarıyla silindi!");
            }
            else
            {
                Console.WriteLine($"Kategori silinirken hata oluştu: {response.StatusCode}");
            }
        }
    }
}
