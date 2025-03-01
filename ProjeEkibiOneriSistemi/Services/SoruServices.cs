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
    public class SoruServices : BaseService, ISoruServices
    {
        public async Task ekleSoru(Soru soru)
        {
            Uri uri = new Uri(UrlHelper.SoruUrl);
            string jsonContent = JsonSerializer.Serialize(soru, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Soru başarıyla eklendi!");
            }
            else
            {
                Console.WriteLine($"Soru eklenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task<List<Soru>> GetSorus()
        {
            Uri uri = new Uri(UrlHelper.SoruUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Soru>>(content, _serializerOptions) ?? new List<Soru>();
            }
            return new List<Soru>();
        }

        public async Task guncelleSoru(Soru soru)
        {
            Uri uri = new Uri($"{UrlHelper.SoruUrl}/{soru.Id}");
            string jsonContent = JsonSerializer.Serialize(soru, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Soru başarıyla güncellendi!");
            }
            else
            {
                Console.WriteLine($"Soru güncellenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task silSoru(int id)
        {
            Uri uri = new Uri($"{UrlHelper.SoruUrl}/{id}");
            HttpResponseMessage response = await _client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"ID {id} olan soru başarıyla silindi!");
            }
            else
            {
                Console.WriteLine($"Soru silinirken hata oluştu: {response.StatusCode}");
            }
        }
    }
}
