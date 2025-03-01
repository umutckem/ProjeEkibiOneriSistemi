using ProjeEkibiOneriSistemi.Dtos;
using ProjeEkibiOneriSistemi.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjeEkibiOneriSistemi.Services
{
    public class OgrenciServices : BaseService, IOgrenciServices
    {
        public async Task ekleOgrenci(Ogrenci ogrenci)
        {
            Uri uri = new Uri(UrlHelper.OgrenciUrl);
            string jsonContent = JsonSerializer.Serialize(ogrenci, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Öğrenci başarıyla eklendi!");
            }
            else
            {
                Console.WriteLine($"Öğrenci eklenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task<List<Ogrenci>> GetOgrencis()
        {
            Uri uri = new Uri(UrlHelper.OgrenciUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Ogrenci>>(content, _serializerOptions) ?? new List<Ogrenci>();
            }
            return new List<Ogrenci>();
        }

        public async Task guncelleOgrenci(Ogrenci ogrenci)
        {
            Uri uri = new Uri($"{UrlHelper.OgrenciUrl}/{ogrenci.Id}");
            string jsonContent = JsonSerializer.Serialize(ogrenci, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Öğrenci başarıyla güncellendi!");
            }
            else
            {
                Console.WriteLine($"Öğrenci güncellenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task silOgrenci(Guid id)
        {
            Uri uri = new Uri($"{UrlHelper.OgrenciUrl}/{id}");
            HttpResponseMessage response = await _client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"ID {id} olan öğrenci başarıyla silindi!");
            }
            else
            {
                Console.WriteLine($"Öğrenci silinirken hata oluştu: {response.StatusCode}");
            }
        }
    }
}
