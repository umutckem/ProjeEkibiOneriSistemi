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
    public class OgrenciProjeServices : BaseService, IOgrenciProjeServices
    {
        public async Task ekleOgrenciProje(OgrenciProje ogrenciProje)
        {
            Uri uri = new Uri(UrlHelper.OgrenciProjeUrl);
            string jsonContent = JsonSerializer.Serialize(ogrenciProje, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Öğrenci projeye başarıyla eklendi!");
            }
            else
            {
                Console.WriteLine($"Öğrenci projeye eklenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task<List<OgrenciProje>> GetProjes()
        {
            Uri uri = new Uri(UrlHelper.OgrenciProjeUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<OgrenciProje>>(content, _serializerOptions) ?? new List<OgrenciProje>();
            }
            return new List<OgrenciProje>();
        }

        public async Task guncelleOgrenciProje(OgrenciProje ogrenciProje)
        {
            Uri uri = new Uri($"{UrlHelper.OgrenciProjeUrl}/{ogrenciProje.Id}");
            string jsonContent = JsonSerializer.Serialize(ogrenciProje, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Öğrencinin proje bilgisi başarıyla güncellendi!");
            }
            else
            {
                Console.WriteLine($"Öğrenci proje bilgisi güncellenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task silOgrenciProje(int id)
        {
            Uri uri = new Uri($"{UrlHelper.OgrenciProjeUrl}/{id}");
            HttpResponseMessage response = await _client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"ID {id} olan öğrenci-proje ilişkisi başarıyla silindi!");
            }
            else
            {
                Console.WriteLine($"Öğrenci-proje ilişkisi silinirken hata oluştu: {response.StatusCode}");
            }
        }
    }
}
