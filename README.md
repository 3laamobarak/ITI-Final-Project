# 🛒 iHerb Backend — E-Commerce Platform

> **Production-ready .NET backend for a health & fitness e-commerce system**
> Covers supplements, vitamins, gym equipment, and lifestyle products.

Built as a **Final Project during 6-month ITI Internship (Qena)** using **Clean Architecture** and modern backend practices.

---

## 🚀 Overview

This project simulates a **real-world scalable e-commerce system**, including:

* Customer shopping experience (browse, cart, checkout)
* Secure authentication & authorization
* Online payments
* Admin dashboard for full control
* Real-time features

---

## 🎨 Frontend Application

This backend is part of a full-stack solution.

👉 **Angular Frontend Repository:**  
https://github.com/3laamobarak/ITI.Final.Front

- Built with Angular
- Consumes the backend APIs
- Provides a responsive e-commerce UI (products, cart, checkout)

> Note: The frontend is maintained in a separate repository for better scalability and separation of concerns.

## ✨ Key Features

### 🔐 Authentication & Security

* JWT Authentication & Authorization
* OTP Email Verification (Gmail SMTP)
* ASP.NET Identity integration

### 🛍️ E-Commerce Core

* Product catalog (categories, brands, images)
* Shopping cart & order management
* Checkout system with payment integration

### 💳 Payments

* Stripe Payment Gateway integration

### ⚡ Real-Time

* SignalR for live notifications

### 🧠 Architecture & Design

* Clean Architecture (Separation of Concerns)
* Unit of Work + Repository Pattern
* DTOs + FluentValidation
* AutoMapper for mapping

### 🧑‍💼 Admin Dashboard

* Separate MVC project
* Manage products, orders, users

---

## 🏗️ Project Structure

```
Company.Project/

├── Domain/          # Core entities, enums, interfaces
├── DTO/             # Data transfer objects + validation
├── Application/     # Business logic & services
├── Infrastructure/  # Repositories, UnitOfWork, configs
├── DbContext/       # EF Core + migrations + seeding
├── PL API/          # Swagger Api
├── PL MVC/          # Admin dashboard
└── Company.Project.sln
```

---

## 🛠️ Tech Stack

| Technology            | Usage                   |
| --------------------- | ----------------------- |
| .NET 8                | Backend framework       |
| ASP.NET Core MVC      | Web applications        |
| Entity Framework Core | ORM & database access   |
| SQL Server            | Database                |
| JWT + Identity        | Authentication          |
| Stripe.NET            | Payments                |
| SignalR               | Real-time communication |
| AutoMapper            | Object mapping          |
| FluentValidation      | Input validation        |
| Swagger               | API documentation       |

---

## ⚙️ Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/3laamobarak/ITI-Final-Project.git
cd ITI-Final-Project/Company.Project
```

### 2. Open Solution

Open `Company.Project.sln` using **Visual Studio 2022+**

---

### 3. Configure Settings ⚠️

Update both:

* `PL/appsettings.json`
* `MVC/appsettings.json`

Required:

* ConnectionStrings:DefaultConnection
* JWT:SecretKey
* Stripe:SecretKey
* EmailSettings

---

### 4. Apply Database

```powershell
Update-Database
```

(Default project: `Company.Project.DbContext`)

✔️ Database comes pre-seeded with products & categories

---

### 5. Run the Application

* **Customer App**
  Set `PL` as startup → Run

* **Admin Dashboard**
  Set `MVC` as startup → Run

---

## 📌 Notes

* `PL` → Customer-facing system
* `MVC` → Admin dashboard
* Designed as backend-first (Angular can be added later)
* Demo connection string is public → **Use your own in production**

---

## 👨‍💻 Author

**Alaa Mobarak**
.NET Full-Stack Developer (ASP.NET Core & Angular)
ITI Graduate — Qena

---

## ⭐ Support

If you like this project:

* ⭐ Star the repository
* 🍴 Fork it
* 🧠 Suggest improvements

---

## 📬 Feedback

Open an issue or reach out — contributions are welcome!
