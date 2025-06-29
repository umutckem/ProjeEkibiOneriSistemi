# ProjeEkibiOneriSistemi



📌 Proje Açıklaması
Bu proje, veri odaklı analiz ile öğrenci profillerini değerlendirerek, dinamik ve optimum proje ekibi önerileri sunan bir sistemdir.
ASP.NET API kullanılarak geliştirilmiştir.
#Admin Paneli

# Admin Paneli

![Ekran görüntüsü 2025-06-09 163618](https://github.com/user-attachments/assets/d16ad06e-f11f-4e2c-b12a-d3d2a30ed6d8)


# Öğrenci Paneli

![Ekran görüntüsü 2025-06-09 163632](https://github.com/user-attachments/assets/35c39676-caf9-4302-a2d3-176ed68bdecb)

# Kurulum

ProjeEkibiOneriSistemi projesinin .NET MAUI uygulaması, API ile doğrudan bağlantı kurarak veri alışverişi yapmaktadır. Bu bağlantı işlemleri herhangi bir harici NuGet paketi kullanılmadan, projeye özel geliştirilen BaseService ve UrlHelper sınıfları yardımıyla gerçekleştirilmektedir. UrlHelper sınıfı, API’nin temel adresini merkezi olarak tanımlamak amacıyla kullanılmaktadır. Burada genellikle "https://localhost:5001" gibi bir URL sabit olarak tutulur. Bu adres, uygulamanın çalışacağı platforma göre gerektiğinde değiştirilmelidir. Örneğin Windows platformu için localhost kullanılabilirken, Android emülatörü kullanılıyorsa bu adres "http://10.0.2.2" olarak güncellenmelidir. Gerçek bir Android cihazdan bağlantı kurulacaksa, API’nin çalıştığı bilgisayarın yerel IP adresi kullanılmalıdır. BaseService sınıfı ise API ile yapılan tüm HTTP isteklerini (GET, POST, PUT, DELETE) ortaklaştırarak yönetir. Bu yapı sayesinde, farklı sayfalardan API’ye bağlanmak isteyen servisler, BaseService üzerinden tek tip bağlantı yaparak kod tekrarını önler. Uygulama açıldığında servisler BaseService üzerinden UrlHelper aracılığıyla belirlenen API adresine bağlanır, veri alımı veya gönderimi gerçekleştirilir. Visual Studio üzerinden .NET MAUI projesi açılıp çalıştırıldığında (Android, Windows, iOS fark etmeksizin), uygulama API’den gelen yanıtlarla dinamik olarak çalışır. Herhangi bir API değişikliği durumunda sadece UrlHelper sınıfı güncellenerek sistemin geri kalanı aynı şekilde kullanılmaya devam eder. Bu yapı, geliştirilebilirlik ve sürdürülebilirlik açısından büyük kolaylık sağlar. Uygulama başlatılmadan önce API’nin mutlaka çalışır durumda olduğundan emin olunmalıdır.

![image](https://github.com/user-attachments/assets/b85dc08d-6d2b-4eb3-aede-b2f6426b1b06)

