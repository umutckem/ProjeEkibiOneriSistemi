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
    public class KatilimciServices : BaseService, IKatilimciServices
    {
        public async Task<List<Katilimci>> GetKatilimcis()
        {
            Uri uri = new Uri(UrlHelper.KatilimciUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Katilimci>>(content, _serializerOptions) ?? new List<Katilimci>();
            }
            return new List<Katilimci>();
        }

        public async Task KatilimciEkle(Katilimci katilimci)
        {
            Uri uri = new Uri(UrlHelper.KatilimciUrl);
            string jsonContent = JsonSerializer.Serialize(katilimci, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Katılımcı başarıyla eklendi!");
            }
            else
            {
                Console.WriteLine($"Katılımcı eklenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task KatilimciSil(Guid id)
        {
            Uri uri = new Uri($"{UrlHelper.KatilimciUrl}/{id}");
            HttpResponseMessage response = await _client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"ID {id} olan katılımcı başarıyla silindi!");
            }
            else
            {
                Console.WriteLine($"Katılımcı silinirken hata oluştu: {response.StatusCode}");
            }
        }
    }
}
