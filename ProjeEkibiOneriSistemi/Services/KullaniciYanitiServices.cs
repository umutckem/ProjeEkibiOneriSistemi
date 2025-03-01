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
    public class KullaniciYanitiServices : BaseService , IKullaniciYanitiSerives
    {


        public async Task ekleKullaniciYaniti(KullaniciYaniti kullaniciYaniti)
        {
            Uri uri = new Uri(UrlHelper.YanitlarUrl);
            string jsonContent = JsonSerializer.Serialize(kullaniciYaniti, _serializerOptions);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Kullanıcı yanıtı başarıyla eklendi!");
            }
            else
            {
                Console.WriteLine($"Kullanıcı yanıtı eklenirken hata oluştu: {response.StatusCode}");
            }
        }

        public async Task<List<KullaniciYaniti>> GetKullaniciYanitis()
        {
            Uri uri = new Uri(UrlHelper.YanitlarUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<KullaniciYaniti>>(content, _serializerOptions) ?? new List<KullaniciYaniti>();
            }
            return new List<KullaniciYaniti>();
        }


        public async Task silKullaniciYaniti(int id)
        {
            Uri uri = new Uri($"{UrlHelper.YanitlarUrl}/{id}");
            HttpResponseMessage response = await _client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"ID {id} olan kullanıcı yanıtı başarıyla silindi!");
            }
            else
            {
                Console.WriteLine($"Kullanıcı yanıtı silinirken hata oluştu: {response.StatusCode}");
            }
        }

      
    }
}
