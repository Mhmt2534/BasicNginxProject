
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

### Run

```bash
docker compose up --build
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

### Çalıştırma

```bash
docker compose up --build
```

### Amaç

* Nginx reverse proxy mantığını öğrenmek
* Docker container networking’i anlamak
* Load balancing öncesi temel oluşturmak


