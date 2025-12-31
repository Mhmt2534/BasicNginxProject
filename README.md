
# Nginx Basic Reverse Proxy with .NET Minimal API


## English

This project is a **learning-focused example** demonstrating how to use **Nginx as a reverse proxy** in front of a **.NET Minimal API**, running with Docker.

### Tech Stack

* .NET 9 Minimal API
* Nginx (Alpine)
* Docker & Docker Compose
* OpenSSL (for generating self-signed certificates)

### 🔐 SSL/HTTPS Setup (Required)

Before running the project, you need to generate a self-signed certificate. Nginx needs these files to serve traffic over HTTPS.

Run the following command in the root of the project to create a `certs` folder and generate the keys:

```bash
mkdir -p certs
openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
  -keyout certs/nginx.key \
  -out certs/nginx.crt \
  -subj "/C=US/ST=State/L=City/O=Organization/CN=localhost"
```

>Note: Since this is a self-signed certificate, your browser will show a "Not Secure" warning. You must manually accept the risk (e.g., click "Advanced" -> "Proceed to localhost") to test the site.


### API Endpoint

**1. Standard Identity Check:**

```http
GET /whoami
```
* Via HTTP: http://localhost/whoami (Redirects to HTTPS)
* Via HTTPS: https://localhost/whoami

**2. Scheme Check (SSL Verification):**
This endpoint helps verify that Nginx is correctly passing the X-Forwarded-Proto header to the backend.

```http
GET /scheme
```
* Test URL: https://localhost/scheme
* Expected Response: https (Even though the internal traffic is HTTP, the app knows the original request was secure).



### Architecture & SSL Termination

* **Nginx (Public Facing):**
  * Listens on **Port 80** (HTTP) → Redirects to HTTPS.
  * Listens on **Port 443** (HTTPS) → Decrypts the SSL traffic (**SSL Termination**).

* **.NET API (Internal):**
  * Runs on **Port 8080** (HTTP).

* **Communication:**
  * Nginx talks to the .NET API over plain **HTTP** within the secure Docker bridge network.
  * Nginx connects using the **service name** (`api`).

### Passive Health Check & Failover

Nginx is configured with **passive health checks** using the `upstream` directive.

If one API instance becomes unreachable:
- Nginx detects the failure during request handling
- After a defined number of failures (`max_fails`)
- The unhealthy instance is temporarily removed from the upstream pool (`fail_timeout`)
- Traffic is automatically routed to the remaining healthy instance

This behavior can be tested by stopping one API container manually and observing
that requests continue to succeed without downtime.

> Note: This project uses passive health checks for learning purposes.
> Active health checks require Nginx Plus or an external orchestration platform
> such as Kubernetes.

### Run

```bash
docker compose up --build
```

### ⚠️ Important: Scaling Containers

Docker Compose may not always support `replicas` depending on the Docker version
or the Compose implementation being used.

If multiple backend instances do not start correctly, use the following
**guaranteed approach**:

```bash
docker compose up --build --scale api=2
```

### Purpose

* Learn Nginx reverse proxy basics
* Understand Docker container networking
* Build a foundation before load balancing



---

## Türkçe

Bu proje, **Nginx’i öğrenme amacıyla** oluşturulmuş basit bir örnektir.  
Nginx, Docker üzerinde çalışan bir **.NET Minimal API**’ye **reverse proxy** olarak yapılandırılmıştır.

### Kullanılan Teknolojiler
- .NET 9 Minimal API
- Nginx (Alpine)
- Docker & Docker Compose
- OpenSSL (Self-signed sertifika oluşturmak için)

### 🔐 SSL/HTTPS Setup (Required)

Projeyi çalıştırmadan önce bir self-signed (kendinden imzalı) sertifika oluşturmanız gerekir. Nginx'in HTTPS üzerinden hizmet verebilmesi için bu dosyalara ihtiyacı vardır.

certs klasörü oluşturmak ve anahtarları üretmek için projenin ana dizininde şu komutu çalıştırın:

```bash
mkdir -p certs
openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
  -keyout certs/nginx.key \
  -out certs/nginx.crt \
  -subj "/C=TR/ST=Istanbul/L=Istanbul/O=Organization/CN=localhost"
```

>Not: Bu sertifika resmi bir otorite tarafından imzalanmadığı için, tarayıcınız "Güvenli Değil" uyarısı verecektir. Test etmek için "Gelişmiş" -> "localhost sitesine ilerle" (veya "Riski kabul et") seçeneklerini kullanmalısınız.


### Mimari ve SSL Termination

* **Nginx (Dışa Açık):**
  * **Port 80** (HTTP) dinler → HTTPS'e yönlendirir.
  * **Port 443** (HTTPS) dinler → Şifreli trafiği çözer (**SSL Termination**).

* **.NET API (Dahili):**
  * **Port 8080** (HTTP) üzerinde çalışır.

* **İletişim:**
  * Nginx, şifresi çözülmüş trafiği güvenli Docker bridge ağı üzerinden **HTTP** protokolü ile .NET API'ye iletir.
  * Nginx, backend’e **service name** (`api`) ile bağlanır.

### Passive Health Check & Failover

Bu projede Nginx, **passive health check** mekanizması ile yapılandırılmıştır.

Bir API instance’ı erişilemez hale geldiğinde:
- Nginx, isteğe cevap alamadığı backend’i tespit eder.
- Belirlenen hata sayısından sonra (`max_fails`).
- Bu instance geçici olarak trafikten çıkarılır (`fail_timeout`)
- İstekler otomatik olarak sağlıklı instance’a yönlendirilir.

> Note: This project uses passive health checks for learning purposes.
> Active health checks require Nginx Plus or an external orchestration platform
> such as Kubernetes.

### Çalıştırma

Önce sertifikaları oluşturduğunuzdan emin olun!

```bash
docker compose up --build
```

### ⚠️ Önemli: Container Ölçeklendirme (Scaling)

Docker Compose, kullanılan sürüme veya çalıştırma şekline bağlı olarak
`replicas` özelliğini her zaman desteklemeyebilir.

Eğer birden fazla backend instance çalışmazsa, aşağıdaki yöntem
**garantili bir çözümdür**:

```bash
docker compose up --build --scale api=2
```

### Amaç

* Nginx reverse proxy mantığını öğrenmek
* SSL Termination ve sertifika yönetimini anlamak.
* Docker container networking’i anlamak
* Load balancing öncesi temel oluşturmak


