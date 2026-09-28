# Practical 3: CRUD Operations Using ADO.NET (Insert and Display)

## Aim

To develop an ASP.NET Web Forms application that performs:

- **Insert** — Add a new student record to the database
- **Display** — Show all student records in a GridView

using **ADO.NET** with a SQL Server **Student** table.

---

# 1. Introduction

**ADO.NET** (ActiveX Data Objects .NET) is a data access technology used to connect ASP.NET applications with databases like SQL Server.

**CRUD** stands for:

```text
C  →  Create  (Insert)
R  →  Read    (Display / Select)
U  →  Update
D  →  Delete
```

In this practical, only **Insert** and **Display** operations are implemented.

The overall flow is:

```text
User fills Student Form
          ↓
Clicks Insert Button
          ↓
ADO.NET sends INSERT query
          ↓
SQL Server stores the record
          ↓
GridView reloads
          ↓
New record visible in table
```

---

# 2. Technology Used

- C#
- ASP.NET Web Forms
- ADO.NET
- SQL Server
- Visual Studio

> **Note:** This practical uses a **.NET Framework ASP.NET Web Forms project**, not the modern ASP.NET Core templates.

---

# 3. Database Setup

Open **SQL Server Management Studio (SSMS)** and execute the following:

### Create the Database

```sql
CREATE DATABASE CollegeDB;
```

### Select the Database

```sql
USE CollegeDB;
```

### Create the Student Table

```sql
CREATE TABLE Student
(
    StudentID   INT PRIMARY KEY IDENTITY(1,1),
    StudentName VARCHAR(100) NOT NULL,
    Age         INT,
    City        VARCHAR(100)
);
```

### Insert Sample Data

```sql
INSERT INTO Student (StudentName, Age, City)
VALUES
('Rahul',  20, 'Vadodara'),
('Priya',  21, 'Ahmedabad'),
('Amit',   20, 'Surat'),
('Neha',   22, 'Rajkot');
```

### Verify the Records

```sql
SELECT * FROM Student;
```

Expected result:

```text
StudentID   StudentName   Age   City
---------------------------------------
1           Rahul         20    Vadodara
2           Priya         21    Ahmedabad
3           Amit          20    Surat
4           Neha          22    Rajkot
```

---

# 4. Create ASP.NET Web Forms Application

## Using Visual Studio

Open **Visual Studio**.

Select:

```text
Create a new project
```

Search for:

```text
ASP.NET Web Application (.NET Framework)
```

Select and click **Next**.

Enter:

```text
Project Name: StudentCRUD
```

Select the required location and click **Create**.

In the next window select:

```text
Web Forms
```

Click **Create**.

---

# 5. Project Folder Structure

After creating the project, the structure will be:

```text
StudentCRUD/
│
├── App_Data/
│
├── Content/
│
├── Scripts/
│
├── Default.aspx
├── Default.aspx.cs
├── Default.aspx.designer.cs
│
├── Global.asax
├── Site.Master
│
├── Web.config
│
└── StudentCRUD.csproj
```

The important files for this practical are:

| File                       | Purpose                                              |
| -------------------------- | ---------------------------------------------------- |
| `Default.aspx`             | Insert form UI and GridView for display              |
| `Default.aspx.cs`          | C# code for Insert and Display logic                 |
| `Default.aspx.designer.cs` | Auto-generated control declarations                  |
| `Web.config`               | Database connection string configuration             |

---

# 6. Page Structure

```text
Default.aspx
     ↓
  ┌─────────────────────────────┐
  │      INSERT STUDENT         │
  │  Name: [TextBox]            │
  │  Age:  [TextBox]            │
  │  City: [TextBox]            │
  │  [Insert Button]            │
  │  [Message Label]            │
  └─────────────────────────────┘
  ┌─────────────────────────────┐
  │    STUDENT RECORDS          │
  │  GridView → all students    │
  └─────────────────────────────┘

Default.aspx.cs
     ↓
  ├── Page_Load  → calls LoadStudents()
  ├── InsertStudent() → INSERT INTO Student
  └── LoadStudents()  → SELECT * FROM Student
                              ↓
                         DataBind to GridView
```

---

# 7. Design the Webpage

Open:

```text
Default.aspx
```

The page will look like:

```text
┌──────────────────────────────────────────────────┐
│                                                  │
│               ADD NEW STUDENT                    │
│                                                  │
│   Student Name : [_______________________]       │
│   Age          : [_______________________]       │
│   City         : [_______________________]       │
│                                                  │
│                 [ Insert ]                       │
│                                                  │
│   ✔ Student inserted successfully!               │
│                                                  │
├──────────────────────────────────────────────────┤
│                STUDENT RECORDS                   │
│                                                  │
│  ID | Student Name | Age | City                  │
│  ---+-------------+-----+-----------            │
│  1  | Rahul       |  20 | Vadodara              │
│  2  | Priya       |  21 | Ahmedabad             │
│  3  | Amit        |  20 | Surat                 │
│  4  | Neha        |  22 | Rajkot                │
│                                                  │
└──────────────────────────────────────────────────┘
```

---

# 8. Configure Database Connection

Open:

```text
Web.config
```

Inside the `<configuration>` element, add the `<connectionStrings>` section:

```xml
<connectionStrings>

    <add name="CollegeDBConnection"
         connectionString="Server=.\SQLEXPRESS;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;"
         providerName="System.Data.SqlClient" />

</connectionStrings>
```

The full structure will be:

```xml
<configuration>

    <connectionStrings>

        <add name="CollegeDBConnection"
             connectionString="Server=.\SQLEXPRESS;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;"
             providerName="System.Data.SqlClient" />

    </connectionStrings>

</configuration>
```

> **Note:** If your SQL Server instance is different, change `.\SQLEXPRESS` to the appropriate server name (e.g., `localhost` or `(local)`).

Why store it in `Web.config`?

```text
Web.config
     ↓
Connection String stored once
     ↓
C# reads it using ConfigurationManager
     ↓
No hardcoded strings in code
     ↓
Easy to change without modifying C# files
```

---

# 9. Complete Default.aspx

Replace the contents of `Default.aspx` with:

```aspx
<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="Default.aspx.cs"
    Inherits="StudentCRUD.Default" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <title>Student CRUD</title>

    <style>

        body
        {
            font-family: Arial;
            margin: 40px;
            background-color: #f0f4f8;
        }

        h2
        {
            color: #333;
            margin-bottom: 16px;
        }

        .form-box
        {
            background-color: white;
            padding: 30px;
            width: 460px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            margin-bottom: 36px;
        }

        .field
        {
            margin-bottom: 14px;
        }

        .field label
        {
            display: inline-block;
            width: 130px;
            font-weight: bold;
            color: #444;
            font-size: 14px;
        }

        .field input[type=text]
        {
            padding: 8px 10px;
            width: 200px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 14px;
        }

        input[type=submit]
        {
            margin-top: 10px;
            padding: 9px 24px;
            background-color: #28a745;
            color: white;
            border: none;
            border-radius: 4px;
            font-size: 14px;
            cursor: pointer;
        }

        input[type=submit]:hover
        {
            background-color: #1e7e34;
        }

        .message
        {
            margin-top: 12px;
            font-size: 14px;
            color: #28a745;
            font-weight: bold;
        }

        .grid-box
        {
            background-color: white;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }

        .grid-box table
        {
            width: 100%;
            border-collapse: collapse;
        }

        .grid-box th
        {
            background-color: #343a40;
            color: white;
            padding: 10px 14px;
            text-align: left;
        }

        .grid-box td
        {
            padding: 9px 14px;
            border-bottom: 1px solid #dee2e6;
            color: #333;
        }

        .grid-box tr:hover td
        {
            background-color: #f8f9fa;
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <!-- Insert Form -->
    <div class="form-box">

        <h2>Add New Student</h2>

        <div class="field">
            <label>Student Name :</label>
            <asp:TextBox ID="txtName" runat="server" />
        </div>

        <div class="field">
            <label>Age :</label>
            <asp:TextBox ID="txtAge" runat="server" />
        </div>

        <div class="field">
            <label>City :</label>
            <asp:TextBox ID="txtCity" runat="server" />
        </div>

        <asp:Button
            ID="btnInsert"
            runat="server"
            Text="Insert"
            OnClick="btnInsert_Click" />

        <div class="message">
            <asp:Label ID="lblMessage" runat="server" Text="" />
        </div>

    </div>

    <!-- Display GridView -->
    <div class="grid-box">

        <h2>Student Records</h2>

        <asp:GridView
            ID="gvStudents"
            runat="server"
            AutoGenerateColumns="False">

            <Columns>

                <asp:BoundField
                    DataField="StudentID"
                    HeaderText="ID" />

                <asp:BoundField
                    DataField="StudentName"
                    HeaderText="Student Name" />

                <asp:BoundField
                    DataField="Age"
                    HeaderText="Age" />

                <asp:BoundField
                    DataField="City"
                    HeaderText="City" />

            </Columns>

        </asp:GridView>

    </div>

</form>

</body>
</html>
```

---

# 10. Complete Default.aspx.cs

Open:

```text
Default.aspx.cs
```

Replace the contents with:

```csharp
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StudentCRUD
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStudents();
            }
        }

        private void LoadStudents()
        {
            string conStr =
                ConfigurationManager
                .ConnectionStrings["CollegeDBConnection"]
                .ConnectionString;

            using (SqlConnection con =
                new SqlConnection(conStr))
            {
                string query = "SELECT * FROM Student";

                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    con.Open();

                    using (SqlDataReader reader =
                        cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();

                        dt.Load(reader);

                        gvStudents.DataSource = dt;

                        gvStudents.DataBind();
                    }
                }
            }
        }

        protected void btnInsert_Click(
            object sender,
            EventArgs e)
        {
            string conStr =
                ConfigurationManager
                .ConnectionStrings["CollegeDBConnection"]
                .ConnectionString;

            using (SqlConnection con =
                new SqlConnection(conStr))
            {
                string query =
                    "INSERT INTO Student " +
                    "(StudentName, Age, City) " +
                    "VALUES (@Name, @Age, @City)";

                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@Name", txtName.Text.Trim());

                    cmd.Parameters.AddWithValue(
                        "@Age", int.Parse(txtAge.Text.Trim()));

                    cmd.Parameters.AddWithValue(
                        "@City", txtCity.Text.Trim());

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }

            lblMessage.Text =
                "Student inserted successfully!";

            txtName.Text = "";
            txtAge.Text  = "";
            txtCity.Text = "";

            LoadStudents();
        }
    }
}
```

---

# 11. Understanding the Key Methods

## LoadStudents()

Retrieves all student records and displays them in the GridView.

```text
LoadStudents()
     ↓
Open SqlConnection
     ↓
Execute: SELECT * FROM Student
     ↓
SqlDataReader reads rows
     ↓
DataTable stores the rows
     ↓
gvStudents.DataSource = dt
     ↓
gvStudents.DataBind()
     ↓
GridView displays all records
```

## btnInsert_Click()

Inserts a new student record when the Insert button is clicked.

```text
btnInsert_Click()
     ↓
Read txtName, txtAge, txtCity values
     ↓
Build parameterized INSERT query
     ↓
cmd.Parameters.AddWithValue(...)
     ↓
con.Open()
     ↓
cmd.ExecuteNonQuery()
     ↓
Record saved to SQL Server
     ↓
Clear TextBoxes
     ↓
Call LoadStudents()
     ↓
GridView refreshed
```

## Why Use Parameterized Queries?

```text
Without Parameters:
"INSERT INTO Student VALUES('" + txtName.Text + "')"
→ SQL Injection risk ❌

With Parameters:
cmd.Parameters.AddWithValue("@Name", txtName.Text)
→ Safe and secure ✔
```

---

# 12. How the Application Works

### On Page Load:

```text
Page opens
     ↓
Page_Load fires
     ↓
!IsPostBack → true
     ↓
LoadStudents()
     ↓
All students shown in GridView
```

### On Insert:

```text
User fills Name, Age, City
     ↓
Clicks Insert
     ↓
btnInsert_Click fires
     ↓
Parameterized INSERT query runs
     ↓
New record saved in SQL Server
     ↓
TextBoxes cleared
     ↓
LoadStudents() called
     ↓
GridView refreshed with new record
```

---

# 13. Expected Output

After inserting **"Karan, 23, Surat"**:

```text
┌──────────────────────────────────────────────────┐
│              ADD NEW STUDENT                     │
│                                                  │
│   Student Name : [_______________________]       │
│   Age          : [_______________________]       │
│   City         : [_______________________]       │
│                                                  │
│                 [ Insert ]                       │
│                                                  │
│   ✔ Student inserted successfully!               │
│                                                  │
├──────────────────────────────────────────────────┤
│               STUDENT RECORDS                    │
│                                                  │
│  ID | Student Name | Age | City                  │
│  ---+-------------+-----+------------           │
│  1  | Rahul       |  20 | Vadodara              │
│  2  | Priya       |  21 | Ahmedabad             │
│  3  | Amit        |  20 | Surat                 │
│  4  | Neha        |  22 | Rajkot                │
│  5  | Karan       |  23 | Surat                 │
│                                                  │
└──────────────────────────────────────────────────┘
```

---

# 14. Important Classes and Methods

| Class / Method                  | Purpose                                          |
| ------------------------------- | ------------------------------------------------ |
| `SqlConnection`                 | Opens a connection to SQL Server                 |
| `SqlCommand`                    | Executes a SQL query                             |
| `SqlDataReader`                 | Reads rows returned by SELECT query              |
| `DataTable`                     | Stores retrieved rows in memory                  |
| `ExecuteNonQuery()`             | Executes INSERT (returns rows affected)          |
| `ExecuteReader()`               | Executes SELECT (returns SqlDataReader)          |
| `Parameters.AddWithValue()`     | Adds a parameterized value to the query          |
| `DataBind()`                    | Binds the DataTable to the GridView              |
| `DataSource`                    | Sets the data source of the GridView             |
| `ConfigurationManager`          | Reads connection string from Web.config          |
| `!IsPostBack`                   | Ensures LoadStudents runs only on first load     |

---

# 15. Troubleshooting

## Error: "Cannot open database CollegeDB"

Check:
1. SQL Server service is running.
2. `CollegeDB` database exists in SQL Server.
3. The server name in Web.config is correct (`.\SQLEXPRESS` or `localhost`).

---

## Error: "Invalid object name 'Student'"

The table does not exist. Run:

```sql
USE CollegeDB;

CREATE TABLE Student
(
    StudentID   INT PRIMARY KEY IDENTITY(1,1),
    StudentName VARCHAR(100) NOT NULL,
    Age         INT,
    City        VARCHAR(100)
);
```

---

## Error: "The name 'ConfigurationManager' does not exist"

Add the namespace at the top of `Default.aspx.cs`:

```csharp
using System.Configuration;
```

---

## Error: "Input string was not in a correct format"

This happens when the Age TextBox contains non-numeric text. Make sure the user enters a number in the Age field. The code uses:

```csharp
int.Parse(txtAge.Text.Trim())
```

If the value is not a valid integer, this will throw an error.

---

## Error: GridView shows empty after Insert

Check:
1. `LoadStudents()` is called at the end of `btnInsert_Click`.
2. The INSERT query ran successfully.
3. `gvStudents.DataBind()` is called inside `LoadStudents()`.

---

# 16. Result

A **Student CRUD application** using **ADO.NET** is successfully developed in ASP.NET Web Forms. The application connects to a SQL Server database, inserts new student records using parameterized queries, and displays all records in a GridView using Data Binding.
