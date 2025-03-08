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
    public class ProjeServices : BaseService, IProjeServices
    {
        public async Task<List<Proje>> GetProjes()
        {
            Uri uri = new Uri(UrlHelper.ProjeUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Proje>>(content, _serializerOptions) ?? new List<Proje>();
            }
            return new List<Proje>();
        }

        public async Task projeEkle(Proje proje)
        {
            Uri uri = new Uri(UrlHelper.ProjeUrl);
            string jsonContent = JsonSerializer.Serialize(proje, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Proje başarıyla eklendi!");
            }
            else
            {
                Console.WriteLine($"Proje eklenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task projeGuncelle(Proje proje)
        {
            Uri uri = new Uri($"{UrlHelper.ProjeUrl}/{proje.Id}");
            string jsonContent = JsonSerializer.Serialize(proje, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Proje başarıyla güncellendi!");
            }
            else
            {
                Console.WriteLine($"Proje güncellenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task projeSil(Guid id)
        {
            Uri uri = new Uri($"{UrlHelper.ProjeUrl}/{id}");
            HttpResponseMessage response = await _client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"ID {id} olan proje başarıyla silindi!");
            }
            else
            {
                Console.WriteLine($"Proje silinirken hata oluştu: {response.StatusCode}");
            }
        }
    }
}
