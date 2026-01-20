## Investment Simulator

A full-stack investment management application with real-time updates and background processing.

---

## 🛠 Tech Stack

* **Frontend:** React 18, TypeScript, Vite, Material UI
* **Backend:** ASP.NET Core 8, SignalR, Entity Framework Core (SQLite)

---

## ⚙️ Installation & Setup

### Steps

**1. Clone the repository**
```bash
git clone https://github.com/Tehila6564/Investment-Simulator.git
cd Investment-Simulator
```

**2. Backend Setup**
```bash
cd Server
dotnet restore
dotnet ef database update
dotnet run
```
Server runs on `https://localhost:7049/`

**3. Frontend Setup**
```bash
cd Client
npm install
npm run dev
```
UI runs on `http://localhost:5173`

---
