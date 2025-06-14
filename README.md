# Stefanini Project

![.NET Core](https://img.shields.io/badge/.NET%20Core-512BD4?style=for-the-badge\&logo=dotnet\&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge\&logo=microsoft-sql-server\&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge\&logo=docker\&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91?style=for-the-badge\&logo=visual-studio\&logoColor=white)
![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)

---

## 🛠️ Tools & Database

To use and test the application, clone the repository:

```bash
git clone https://github.com/leandrokuranaga/Stefanini.git
```

* Developed on **Windows 11**
* **SQL Server Express 2019** as the database
* IDE: **Visual Studio 2022**
* Database name: `StefaniniLeandroKuranaga` (configurable via `appsettings.json`)

---

## 🧱 Project Structure

This project follows **Onion Architecture**, and uses:

* **Entity Framework Core** (`Design`, `Relational`, `SqlServer`, `Tools`)
* Migration created with:

```bash
add-migration Initial
```

### 🗂️ Layers

* 📡 **Stefanini.API** – Controllers (Application layer)
* 🧠 **Stefanini.Domain** – Business rules, interfaces, models (Domain layer)
* 💾 **Stefanini.Infra** – DB context, mappings, migrations, repositories (Infrastructure layer)

### 🧩 Dependencies

* `Stefanini.Infra` depends on `Stefanini.Domain`
* `Stefanini.API` depends on both `Stefanini.Domain` and `Stefanini.Infra`

---

## 🗃️ Database

* Connection string: `(localdb)\MSSQLLocalDB` (Windows Authentication)
* Compatible with SQL Server 2017 and 2019

### 📊 UML Diagram

![UML Diagram](https://user-images.githubusercontent.com/29407031/156945886-01367072-7991-4f13-b54a-f7be7812a393.png)

### 📫 Postman Collection

![Collections postman](Stefanini/Stefanini/Assets/Stefanini%20-%20Localhost.postman_collection.json)

---

## 📜 License

This project is licensed under the MIT License.
