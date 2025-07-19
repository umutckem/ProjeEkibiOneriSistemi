## 🤖 ProjeEkibiOneriSistemi

ProjeEkibiOneriSistemi, veri odaklı analiz ile öğrenci profillerini değerlendirerek dinamik ve optimum proje ekipleri öneren bir sistemdir.
Arka planda ASP.NET Core Web API kullanılarak geliştirilmiş, istemci tarafında ise .NET MAUI ile platformlar arası çalışabilen bir uygulama ile desteklenmiştir.

## 📌 Proje Açıklaması

Bu sistemin amacı:

- Öğrenci profillerini değerlendirmek

- Yetenek ve bilgi düzeylerine göre analiz yapmak

- Proje gereksinimlerine uygun en ideal ekipleri otomatik olarak önermek

- Geliştirici ve yönetici arayüzleri sayesinde kullanıcı yönetimi, veri girişi, analiz ve öneri işlemleri kolaylıkla gerçekleştirilebilir.

## ⚙️ Kurulum ve Yapı (MAUI Uygulaması)

- .NET MAUI tabanlı istemci uygulama, ASP.NET API ile doğrudan iletişim kurar. Bu bağlantı, özel olarak geliştirilen BaseService ve UrlHelper sınıflarıyla gerçekleştirilir.

- 🔗 UrlHelper

API'nin temel adresi burada tanımlanır. Platforma göre ayarlanmalıdır:

Windows için:
https://localhost:5001

Android Emülatörü için:
http://10.0.2.2

Gerçek Cihaz için:
API'nin çalıştığı bilgisayarın yerel IP adresi

Bu adres yalnızca UrlHelper.cs üzerinden merkezi olarak değiştirilerek yönetilir.

- 🔁 BaseService

Tüm HTTP isteklerini (GET, POST, PUT, DELETE) ortaklaştırır.

Kod tekrarını engeller.

Servis sınıfları BaseService'i kalıtım alarak API ile iletişim kurar.

## 🛠️ Kullanılan Teknolojiler

- Backend (API):

- ASP.NET Core Web API (.NET 6)

- Entity Framework Core

- SQL Server

- JWT Authentication

- Swagger (API test ve dokümantasyon)

- Frontend (Client):

- .NET MAUI

- C#

- MVVM Mimari

- HttpClient (BaseService ile özelleştirilmiş)

- SQLite (İsteğe bağlı yerel veri depolama)

## 📬 İletişim

Bu projeyle ilgili görüş, öneri veya katkı sağlamak isterseniz lütfen bizimle iletişime geçin.

## Uyarı

Bu görseller örnek amaçlıdır ve gerçek verileri yansıtmayabilir.

## Giriş Ekranı

<img width="1919" height="1018" alt="image" src="https://github.com/user-attachments/assets/2c553067-033b-4adb-956b-8145079b25d6" />

## 🧑‍💻 Admin Paneli

<img width="1919" height="1012" alt="image" src="https://github.com/user-attachments/assets/28aeeb69-a145-4476-837e-7e92aa044ec1" />

<img width="1919" height="1011" alt="image" src="https://github.com/user-attachments/assets/96dfe71a-ea39-4a28-b24f-38698115531c" />

<img width="1919" height="1016" alt="image" src="https://github.com/user-attachments/assets/e5f3b185-c85b-45f7-bb28-79d07343d70d" />

<img width="1919" height="1022" alt="image" src="https://github.com/user-attachments/assets/e7c56245-50b2-415b-8b88-50f0b8c1366c" />

<img width="1919" height="1017" alt="image" src="https://github.com/user-attachments/assets/c3417f4a-f5cf-43cd-b047-63c0a9076896" />

<img width="1919" height="1020" alt="image" src="https://github.com/user-attachments/assets/0ebc6e68-2a65-4c73-99fc-08502c91defa" />

<img width="1919" height="1015" alt="image" src="https://github.com/user-attachments/assets/e754fa5f-da70-4269-8d4c-96b2b57c9823" />

<img width="1918" height="1020" alt="image" src="https://github.com/user-attachments/assets/5f466208-da8b-40c9-ba5d-0b14ccdfe508" />

<img width="1919" height="1020" alt="image" src="https://github.com/user-attachments/assets/3c917873-6b01-432d-ae39-1a353aad9556" />

<img width="1919" height="1019" alt="image" src="https://github.com/user-attachments/assets/182e8ed4-b760-4f9d-8ef6-114165708c0a" />

<img width="1919" height="1018" alt="image" src="https://github.com/user-attachments/assets/426c0e23-7967-4ea8-b852-7d8ee9677f6b" />

<img width="1919" height="1018" alt="image" src="https://github.com/user-attachments/assets/23ce1aec-218e-4dc6-a5b3-bcbbf04ce2b6" />

<img width="1919" height="1022" alt="image" src="https://github.com/user-attachments/assets/de154b27-c784-40e0-8e79-f896eb09c922" />

<img width="1919" height="990" alt="image" src="https://github.com/user-attachments/assets/7af51ef9-5914-4ff1-845f-d14db988e85b" />

<img width="1917" height="1017" alt="image" src="https://github.com/user-attachments/assets/c9aff3e1-86e2-47ae-8041-cba5c3922d65" />

<img width="1919" height="1019" alt="image" src="https://github.com/user-attachments/assets/9269d41c-d90b-46c0-948b-50ff3d3e1c89" />

<img width="1919" height="1010" alt="image" src="https://github.com/user-attachments/assets/b32c3dc1-f440-478b-9513-7a7c6ded9a78" />

<img width="1919" height="1019" alt="image" src="https://github.com/user-attachments/assets/6a1a1c9d-89c7-49ca-8d74-01c6da3be3c0" />

<img width="1919" height="1016" alt="image" src="https://github.com/user-attachments/assets/64264ea3-543a-4d8d-8f67-083b69457ac0" />

<img width="1919" height="1019" alt="image" src="https://github.com/user-attachments/assets/834828d8-4265-4c93-90c3-cd0eb606a67b" />

## 🎓 Öğrenci Paneli

<img width="1919" height="1014" alt="image" src="https://github.com/user-attachments/assets/f9f3a137-5e7f-4491-b57f-4cdc2c842ad4" />

<img width="1919" height="1020" alt="image" src="https://github.com/user-attachments/assets/1843981c-a6cc-4fc4-9e98-dbb840ab5b5a" />

<img width="1919" height="1018" alt="image" src="https://github.com/user-attachments/assets/7bfbab16-4471-4539-9b58-77d5e40613ef" />

<img width="1919" height="1010" alt="image" src="https://github.com/user-attachments/assets/9d003131-11d9-4c87-a98c-0a9aa174a7b1" />

<img width="1919" height="1019" alt="image" src="https://github.com/user-attachments/assets/8c9d504a-7791-41c5-ae2d-130f7523c642" />

<img width="1919" height="1022" alt="image" src="https://github.com/user-attachments/assets/ea12f977-1027-4338-bef4-f761fec94798" />

<img width="1919" height="1021" alt="image" src="https://github.com/user-attachments/assets/b728836c-8f73-410b-b367-e5f74b60d839" />

<img width="1919" height="1015" alt="image" src="https://github.com/user-attachments/assets/c9217d2a-dc06-4f4c-961c-906f063e3069" />
