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
    internal class GrupServices : BaseService, IGrupServices
    {
        public async Task ekleGrup(Grup grup)
        {
            Uri uri = new Uri(UrlHelper.GrupUrl);
            string jsonContent = JsonSerializer.Serialize(grup, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Grup başarıyla eklendi!");
            }
            else
            {
                Console.WriteLine($"Grup eklenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task<List<Grup>> getGrups()
        {
            Uri uri = new Uri(UrlHelper.GrupUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Grup>>(content, _serializerOptions) ?? new List<Grup>();
            }
            return new List<Grup>();
        }

        public async Task guncelleGrup(Grup grup)
        {
            Uri uri = new Uri($"{UrlHelper.GrupUrl}/{grup.Id}");
            string jsonContent = JsonSerializer.Serialize(grup, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Grup başarıyla güncellendi!");
            }
            else
            {
                Console.WriteLine($"Grup güncellenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task silGrup(Guid id)
        {
            Uri uri = new Uri($"{UrlHelper.GrupUrl}/{id}");
            HttpResponseMessage response = await _client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"ID {id} olan grup başarıyla silindi!");
            }
            else
            {
                Console.WriteLine($"Grup silinirken hata oluştu: {response.StatusCode}");
            }
        }
    }
}
