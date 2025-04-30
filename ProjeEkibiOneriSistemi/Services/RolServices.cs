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
    public class RolServices : BaseService, IRolServices
    {
        public async Task ekleRol(Rol rol)
        {
            Uri uri = new Uri(UrlHelper.RolUrl); // UrlHelper.RolUrl tanımlı olmalı
            string jsonContent = JsonSerializer.Serialize(rol, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Rol başarıyla eklendi!");
            }
            else
            {
                Console.WriteLine($"Rol eklenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task<List<Rol>> GetAllRol()
        {
            Uri uri = new Uri(UrlHelper.RolUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Rol>>(content, _serializerOptions) ?? new List<Rol>();
            }
            return new List<Rol>();
        }

        public async Task silRol(Guid id)
        {
            Uri uri = new Uri($"{UrlHelper.RolUrl}/{id}");
            HttpResponseMessage response = await _client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"ID {id} olan rol başarıyla silindi!");
            }
            else
            {
                Console.WriteLine($"Rol silinirken hata oluştu: {response.StatusCode}");
            }
        }
    }
}
