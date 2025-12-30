
# Nginx Basic Reverse Proxy with .NET Minimal API


## English

This project is a **learning-focused example** demonstrating how to use **Nginx as a reverse proxy** in front of a **.NET Minimal API**, running with Docker.

### Tech Stack

* .NET 9 Minimal API
* Nginx (Alpine)
* Docker & Docker Compose

### API Endpoint

```http
GET /whoami
```

Response:

```
Hello from API
```

Accessed via Nginx:

```
http://localhost/whoami
```

### Architecture

* Nginx listens on port 80
* .NET API runs on port 8080
* Containers communicate via Docker bridge network
* Nginx connects using the **service name (`api`)**

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

### API Endpoint
```http
GET /whoami
````

Yanıt:

```
Hello from API
```

Endpoint, Nginx üzerinden erişilir:

```
http://localhost/whoami
```

### Mimari

* Nginx → 80
* .NET API → 8080
* Servisler Docker bridge network üzerinden haberleşir
* Nginx, backend’e **service name (`api`)** ile bağlanır

### Passive Health Check & Failover

Bu projede Nginx, **passive health check** mekanizması ile yapılandırılmıştır.

Bir API instance’ı erişilemez hale geldiğinde:
- Nginx, isteğe cevap alamadığı backend’i tespit eder
- Belirlenen hata sayısından sonra (`max_fails`)
- Bu instance geçici olarak trafikten çıkarılır (`fail_timeout`)
- İstekler otomatik olarak sağlıklı instance’a yönlendirilir

Bu davranış, bir API container’ı manuel olarak durdurularak test edilebilir.
Sistem kesintiye uğramadan çalışmaya devam eder.

> Not: Bu proje öğrenme amaçlıdır ve passive health check kullanır.
> Active health check için Nginx Plus veya Kubernetes gibi çözümler gereklidir.

### Çalıştırma

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
* Docker container networking’i anlamak
* Load balancing öncesi temel oluşturmak


