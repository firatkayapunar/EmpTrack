<h1 align="center">👥 EmpTrack - Employee Management System</h1>

**EmpTrack**, kurumsal **Employee Management System (Personel Yönetim Sistemi)** ihtiyaçlarını dijitalleştirmek amacıyla geliştirilmiş; React, modern yazılım mimarileri ve güncel .NET teknolojileri kullanılarak inşa edilmiş bir uygulamadır.

Bu proje, yazılım geliştirme becerilerini değerlendirmek amacıyla hazırlanmış kapsamlı bir Case Study çalışması olup; uçtan uca **backend & frontend mimari tasarımı**, **API geliştirme**, **state yönetimli SPA tasarımı** ve **veritabanı entegrasyonu** süreçlerini tek bir bütün olarak ele almaktadır.

---

## 🚀 **Proje Özeti**

Uygulama; **Department**, **Title** ve **Employee** verilerinin tek bir panel altında toplanmasını, kayıtların oluşturulmasını, güncellenmesini, silinmesini ve raporlanmasını mümkün kılar. Tüm işlemler, güvenli kimlik doğrulama mekanizmasıyla yetkilendirilmiş kullanıcılara açık olarak gerçekleştirilir.

Sistem aşağıdaki temel fonksiyonları eksiksiz biçimde sunmaktadır:

- Kullanıcı girişi (JWT + Refresh Token bazlı authentication)
- Department yönetimi (listeleme, ekleme, düzenleme)
- Title yönetimi (listeleme, ekleme, düzenleme)
- Employee kayıt işlemleri (CRUD operasyonları)
- Employee profil fotoğrafı yükleme
- Aktif / Pasif Employee yönetimi
- Pagination ve Search Filter desteği
- Dashboard ekranı üzerinden canlı istatistik takibi

Uygulama iki ana bileşenden oluşmaktadır:

- **Backend (API)**: ASP.NET Core Web API altyapısında geliştirilmiş; **Clean Architecture** prensiplerine uygun şekilde ayrıştırılmış, **Screaming Architecture** yaklaşımıyla feature-based olarak kurgulanmış, **CQRS + MediatR** pattern kullanan ve **JWT** tabanlı güvenlik mimarisiyle desteklenen bir yapıdadır.
- **Frontend (UI)**: React tabanlı modern bir **Single Page Application (SPA)** olarak tasarlanmış; **component-based** mimari yaklaşımla yapılandırılmış, merkezi bir **API service layer** üzerinden backend ile entegre çalışan ve **Context API** tabanlı global state & authentication yönetimi ile desteklenen bir arayüz yapısına sahiptir.

## 🔧 **Kurulum & Veritabanı Ayarları**

<ol>

  <li>
    <strong>SQLite DB Browser uygulamasını indirin:</strong>
    <p>
      Veritabanını görüntülemek ve kontrol etmek için 
      <a href="https://sqlitebrowser.org/" target="_blank">DB Browser for SQLite</a> uygulamasını indirip kurmanız gerekmektedir.
    </p>
  </li>

  <li>
    <strong>Veritabanı migration işlemini başlatın:</strong>
    <pre><code>Update-Database</code></pre>
    <p>
      Bu komut <strong>Visual Studio</strong> üzerinde <strong>Package Manager Console</strong> aracılığıyla çalıştırılmalıdır. 
      Komutu çalıştırmadan önce, <strong>Startup Project</strong> olarak <code>EmpTrack.API</code> projesinin, 
      <strong>Default Project</strong> olarak ise <code>EmpTrack.Infrastructure</code> katmanının seçili olduğundan emin olmalısınız.
      Migration işlemi tamamlandığında, API projesinin <code>bin</code> klasörü altında 
      <code>EmpTrack.db</code> isimli SQLite veritabanı dosyası otomatik olarak oluşturulacaktır.
    </p>
  </li>

  <li>
    <strong>Veritabanını inceleyin:</strong>
    <p>
      Oluşan <code>EmpTrack.db</code> dosyasını DB Browser for SQLite uygulaması ile açarak 
      tablo yapısını ve kayıtları görüntüleyebilirsiniz.
    </p>
  </li>

  <li>
    <strong>Backend (API) uygulamasını başlatın:</strong>
    <pre><code>dotnet run --project EmpTrack.API --launch-profile https</code></pre>
    <p>
      Alternatif olarak Visual Studio içerisinde <code>EmpTrack.API</code> projesini seçerek <strong>F5</strong> tuşu ile de API’yı çalıştırabilirsiniz.<br>
      API varsayılan olarak <code>https://localhost:7295</code> adresi üzerinden yayınlanacaktır.
    </p>
  </li>

  <li>
    <strong>Frontend (UI) uygulamasını başlatın:</strong>
    <pre><code>cd frontend
npm install
npm run dev</code></pre>
    <p>
      React tabanlı arayüze tarayıcı üzerinden <code>http://localhost:5173</code> adresinden erişebilirsiniz.
    </p>
  </li>

  <li>
    <strong>Örnek kullanıcı ile giriş yapın:</strong>
    <p>
      Uygulama çalıştığında giriş ekranında aşağıdaki test kullanıcı bilgileri ile sisteme giriş yapabilirsiniz:
    </p>
    <ul>
      <li><strong>Username:</strong> zeynep</li>
      <li><strong>Password:</strong> EmpTrack!2025Strong</li>
    </ul>
  </li>

</ol>

---

## <h2>📂 Proje Yapısı</h2>

<pre>
📦 EmpTrack
│
├── 📂 EmpTrack.API                --> ASP.NET Core Web API (Controllers, Filters, Exception Handlers)
│
├── 📂 EmpTrack.Domain             --> Core domain entities ve auditing altyapısı
│                                     (BaseEntity, AuditableEntity, FullAuditableEntity, Employee, Department, Title, AppUser)
│
├── 📂 EmpTrack.Application        --> Business logic & orchestration layer
│                                     (CQRS Commands/Queries/Handlers, Validators, DTOs, AutoMapper Profiles, Interfaces)
│
├── 📂 EmpTrack.Infrastructure     --> EF Core persistence, repositories & JWT services
│                                     (DbContext, Entity configurations, GenericRepository pattern, Migrations, Seed, Token services)
│
├── 📂 frontend (React SPA)        --> Modern UI katmanı
                                      (Component library, Pages, Services, Auth Context, Routing, Hooks)
</pre>

---

## <h2>🛠️ Kullanılan Teknolojiler ve Mimariler</h2>

<ul> 
  <li><strong>.NET 8</strong> – ASP.NET Core Web API altyapısı ile RESTful servis geliştirme</li> 
  <li><strong>Entity Framework Core</strong> – ORM katmanı (Code First yaklaşımı ile SQLite veritabanı entegrasyonu)</li> 
  <li><strong>SQLite</strong> – Hafif, dosya tabanlı veritabanı çözümü</li>
  <li><strong>MediatR</strong> – CQRS pattern implementasyonu için request/handler orchestration altyapısı</li> 
  <li><strong>FluentValidation</strong> – Tüm request modelleri için merkezi ve katmanlı input validation sistemi</li> 
  <li><strong>AutoMapper</strong> – Domain Entity ↔ DTO dönüşümlerinde otomatik mapping altyapısı</li> 
  <li><strong>JWT + Refresh Token</strong> – Stateless authentication ve token lifecycle yönetimi</li> 
  <li><strong>Bcrypt</strong> – Password hashing için güvenli kriptografik algoritma</li>
  <li><strong>Clean Architecture</strong> – Core domain merkezli katmanlı mimari tasarım prensibi</li>
  <li><strong>Screaming Architecture</strong> – Feature-based klasör organizasyonu (Auth, Employees, Departments vb.)</li>
  <li><strong>CQRS</strong> – Read / Write operasyonlarının Command ve Query olarak ayrıştırılması</li>
  <li><strong>Result Pattern</strong> – API response standardizasyonu ve business hata yönetimi</li>
  <li><strong>Global Exception Handling</strong> – SQL, timeout ve runtime hataları için merkezi exception pipeline</li>

  <li><strong>React</strong> – Modern SPA (Single Page Application) UI framework</li>
  <li><strong>Vite</strong> – Hızlı frontend build ve dev server altyapısı</li>
  <li><strong>Axios</strong> – HTTP client ve JWT interceptor yönetimi</li>
  <li><strong>Context API</strong> – Authentication ve global session state yönetimi</li>
  <li><strong>React Router</strong> – SPA route ve protected-page navigation altyapısı</li>
</ul>

---

## 📌 **Proje Ekran Görüntüleri**

> Aşağıda EmpTrack uygulamasına ait kullanıcı arayüzü ekranlarından ve API tarafı örneklerinden seçilmiş görseller yer almaktadır.  

### 🔐 Login Ekranı
<img width="1882" height="942" alt="1" src="https://github.com/user-attachments/assets/dab49ce4-2a07-4608-9e92-b53bbe6b3c9a" />

### 📊 Dashboard
<img width="1900" height="838" alt="1" src="https://github.com/user-attachments/assets/97927c5a-d6f3-44eb-b5b9-5e6979af7e02" />

### 🏢 Department Management
<img width="1896" height="916" alt="1" src="https://github.com/user-attachments/assets/373c397b-814d-47c0-9657-31e3b4121712" />
<img width="1909" height="786" alt="2" src="https://github.com/user-attachments/assets/63e274cc-07d6-4312-9b72-1eec035b5969" />
<img width="1898" height="870" alt="3" src="https://github.com/user-attachments/assets/28a08024-91d3-495e-8093-1ec1e9b03087" />

### 🎖 Title Management
<img width="1892" height="796" alt="4" src="https://github.com/user-attachments/assets/30aea22e-79c0-452e-8bb3-4c263a9c2711" />

### 👥 Employee Listesi
<img width="1907" height="908" alt="5" src="https://github.com/user-attachments/assets/2c170547-4346-4a35-a136-e23229fad4ff" />

### ➕ Employee Create Modal ( + 📎 Photo Upload)
<img width="1897" height="942" alt="1" src="https://github.com/user-attachments/assets/0a169f0b-5efe-4989-977e-5827a602194e" />

### ✏ Employee Update Modal ( + 📎 Photo Upload)
<img width="1900" height="936" alt="1" src="https://github.com/user-attachments/assets/3868f267-3062-4474-8730-a3bd7de53da2" />

### ❌ Employee Delete
<img width="1898" height="818" alt="1" src="https://github.com/user-attachments/assets/dcdb5027-6c2a-4d3b-bb5e-0843499c3ab9" />

### 🔗 API Endpoint Koleksiyonu
<img width="1143" height="929" alt="API Endpoint Listesi" src="https://github.com/user-attachments/assets/0c306981-6bae-45e3-abd6-d7eb9317f5ad" />

---

## 📌 **Örnek Resimler**
![c2](https://github.com/user-attachments/assets/00bc50a4-3db2-4025-bfaa-79290477f82b)
![c1](https://github.com/user-attachments/assets/5d2c7c60-a280-4057-ad9b-5051d3de3bdd)
![c3](https://github.com/user-attachments/assets/a15bcdab-eb0a-439f-b075-7e1e8715167d)
