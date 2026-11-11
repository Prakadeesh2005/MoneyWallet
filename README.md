# MoneyWallet

A desktop expense-tracking application built with **C# and WPF**. MoneyWallet helps users manage personal expenses through individual accounts, record transactions, and review spending over weekly, monthly, and yearly periods.

## Features

- **User registration and login** – users can create an account and sign in.
- **Individual expense records** – each user's expense data is kept separate.
- **Add expenses** – record an expense with its reason, date, and amount.
- **Expense history** – view recorded transactions in a table with an Expense ID (EID).
- **Spending dashboard** – review weekly, monthly, and yearly expense summaries.
- **Local JSON storage** – application data is stored locally in JSON files.
- **Password protection** – passwords are stored as salted PBKDF2 hashes rather than plain text.
- **Input validation** – registration and expense forms validate user input.

## Technologies Used

| Technology | Purpose |
|---|---|
| C# | Application logic |
| .NET / WPF | Windows desktop application |
| XAML | User interface design |
| `System.Text.Json` | Reading and writing JSON data |
| LINQ | Filtering and summarising expense records |
| PBKDF2 | Password hashing |

## Application Data

MoneyWallet stores its data in the current Windows user's local application data folder:

```text
%LOCALAPPDATA%\MoneyWallet\
```

The application uses JSON files such as:

```text
MoneyWallet/
├── users.json
└── expenses.json
```

The exact files created may depend on the application's implementation and whether data has been saved yet.

## Getting Started

### Prerequisites

- Windows
- Visual Studio with the **.NET desktop development** workload
- A .NET SDK/runtime version compatible with the project

### Run the project

1. Open the repository on GitHub:  
   https://github.com/Prakadeesh2005/MoneyWallet
2. Click **Code → Download ZIP**, then extract the ZIP file.
3. Open the solution (`.sln`) or project (`.csproj`) file in Visual Studio.
4. Allow Visual Studio to restore the project dependencies.
5. Select the project as the startup project.
6. Build the solution, then run it using **F5** or **Start**.

## Project Structure

The application is organised around user management, JSON storage, and WPF windows. Key files include:

```text
MoneyWallet/
├── User.cs
├── JsonFileService.cs
├── UserService.cs
├── LoginWindow.xaml
├── RegisterWindow.xaml
├── App.xaml
└── ...
```

- `User.cs` – represents a user account and its stored authentication information.
- `JsonFileService.cs` – handles JSON file storage operations.
- `UserService.cs` – contains user registration and account-related logic.
- `LoginWindow.xaml` – login screen UI.
- `RegisterWindow.xaml` – registration screen UI.
- `App.xaml` – WPF application entry and resources.

## How It Works

1. A new user registers with the required account details.
2. The application validates the input and checks for an existing account.
3. The password is salted and hashed using PBKDF2 before it is stored.
4. After login, the user can add and view their own expenses.
5. Expense records can be reviewed through the dashboard's weekly, monthly, and yearly summaries.

## Security Note

MoneyWallet uses password hashing with a salt. This is safer than storing passwords as readable text. The application stores its JSON data locally, so users should protect access to their Windows account and keep backups of important data.

## Future Improvements

- Export expense reports to CSV or PDF.
- Add expense categories and budget limits.
- Add charts for spending trends.
- Add optional data backup and restore.

## Author

**Prakadeeshwaran**

GitHub: [@Prakadeesh2005](https://github.com/Prakadeesh2005)

---

If you find this project useful, feel free to ⭐ the repository.
