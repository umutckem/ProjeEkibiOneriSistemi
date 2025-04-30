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
    public class YetkiServices : BaseService, IYetkiServices
    {
        public async Task ekleYetki(Yetki yetki)
        {
            Uri uri = new Uri(UrlHelper.YetkiUrl);
            string jsonContent = JsonSerializer.Serialize(yetki, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Yetki başarıyla eklendi!");
            }
            else
            {
                Console.WriteLine($"Yetki eklenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task<List<Yetki>> GetYetkis()
        {
            Uri uri = new Uri(UrlHelper.YetkiUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Yetki>>(content, _serializerOptions) ?? new List<Yetki>();
            }
            return new List<Yetki>();
        }

        public async Task guncelleYetki(Yetki yetki)
        {
            Uri uri = new Uri($"{UrlHelper.YetkiUrl}/{yetki.Id}");
            string jsonContent = JsonSerializer.Serialize(yetki, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Yetki başarıyla güncellendi!");
            }
            else
            {
                Console.WriteLine($"Yetki güncellenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task silYetki(Guid id)
        {
            Uri uri = new Uri($"{UrlHelper.YetkiUrl}/{id}");
            HttpResponseMessage response = await _client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"ID {id} olan yetki başarıyla silindi!");
            }
            else
            {
                Console.WriteLine($"Yetki silinirken hata oluştu: {response.StatusCode}");
            }
        }
    }
}
