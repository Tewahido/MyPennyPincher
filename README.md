# MyPennyPincher

**MyPennyPincher** is a full-stack personal finance management application designed to help users gain better control over their money. Track your income, expenses, categorize transactions with pre-determined tags, and analyze your spending patterns with rich visual dashboards.

---

## ✨ Features

- ✅ Add, update, and delete **income** and **expenses**
- 🔁 Track **recurring** and **one-time** transactions
- 🏷️ **Tag expenses** with categories like `Groceries`, `Transport`, `Rent`, etc.
- 👥 Support for **multiple users and accounts**
- 📊 Real-time **analytics dashboard** with charts and insights
- 🔐 Secure, authenticated access

---

## 🛠️ Tech Stack

| Layer        | Technology              |
|--------------|--------------------------|
| Frontend     | React, Tailwind CSS       |
| Backend      | C# Web API (not using .NET 8) |
| Database     | SQL Server                |
| ORM          | Entity Framework Core     |
| Analytics    | Chart.js      |
| Authentication | JWT / Identity           |

---

## 📦 Project Structure
mypennypincher/
├── backend/
│ ├── Controllers/
│ ├── Models/
│ ├── Services/
│ └── Data/
├── frontend/
│ ├── src/
│ │ ├── components/
└── README.md


---

## 🚀 Getting Started

### Prerequisites

- Node.js and npm
- C# and .NET SDK (compatible version with your project)
- SQL Server
- Visual Studio Code or preferred IDE

### 1. Clone the Repository


`git clone https://github.com/your-username/mypennypincher.git
cd mypennypincher`
2. Setup the Database
Create a new SQL Server database (e.g. MyPennyPincherDb)

Update the connection string in backend/appsettings.json

Run database migrations:

bash
Copy
Edit
dotnet ef database update
3. Run the Backend
bash
Copy
Edit
cd backend
dotnet run
4. Run the Frontend
bash
Copy
Edit
cd frontend
npm install
npm run dev
