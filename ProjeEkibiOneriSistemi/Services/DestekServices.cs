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
    public class DestekServices : BaseService, IDestekServices
    {
        public async Task ekleDestek(Destek destek)
        {
            Uri uri = new Uri(UrlHelper.DestekUrl); 
            string jsonContent = JsonSerializer.Serialize(destek, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Destek başarıyla eklendi!");
            }
            else
            {
                Console.WriteLine($"Destek eklenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task<List<Destek>> GetAllDestek()
        {
            Uri uri = new Uri(UrlHelper.DestekUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Destek>>(content, _serializerOptions) ?? new List<Destek>();
            }
            return new List<Destek>();
        }

        public async Task silDestek(int id)
        {
            Uri uri = new Uri($"{UrlHelper.DestekUrl}/{id}");
            HttpResponseMessage response = await _client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"ID {id} olan destek başarıyla silindi!");
            }
            else
            {
                Console.WriteLine($"Destek silinirken hata oluştu: {response.StatusCode}");
            }
        }
    }
}
