# Practical 1: Display Entered Name Using ASP.NET Web Controls

## Aim

To design an ASP.NET webpage that contains:

- Label
- TextBox
- Button

And displays the name entered by the user when the Button is clicked.

---

# 1. Introduction

**ASP.NET Web Forms** provides server-side controls that interact with C# code through events.

In this practical:

```text
User types name in TextBox
          ↓
     Button Click
          ↓
  C# Code-Behind fires
          ↓
   Label.Text is updated
          ↓
  Name displayed on webpage
```

When the user types a name in the TextBox and clicks the Button, the `btnSubmit_Click` event fires in the code-behind, which reads the TextBox value and sets the Label text to display the name.

---

# 2. Technology Used

- C#
- ASP.NET Web Forms
- Visual Studio

> **Note:** This practical uses a **.NET Framework ASP.NET Web Forms project**, not the modern ASP.NET Core templates.

---

# 3. Database Setup

No database is required for this practical.

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

Select:

```text
ASP.NET Web Application (.NET Framework)
```

Click **Next**.

Enter:

```text
Project Name: NameDisplayApp
```

Select the required location and click **Create**.

In the next window, select:

```text
Web Forms
```

Click **Create**.

---

# 5. Project Folder Structure

After creating the project, the structure will be:

```text
NameDisplayApp/
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
└── NameDisplayApp.csproj
```

The important files for this practical are:

| File                       | Purpose                                      |
| -------------------------- | -------------------------------------------- |
| `Default.aspx`             | Webpage design and ASP.NET controls          |
| `Default.aspx.cs`          | C# code-behind for button click logic        |
| `Default.aspx.designer.cs` | Auto-generated control declarations          |
| `Web.config`               | Application configuration                   |

---

# 6. Page Structure

ASP.NET Web Forms uses two connected files:

```text
Default.aspx
     ↓
  UI Controls
  ├── Label      → page heading / input label
  ├── TextBox    → user input field
  ├── Button     → triggers the event
  └── Label      → displays the result

Default.aspx.cs
     ↓
  C# Logic
  ├── Page_Load event
  └── btnSubmit_Click event
          ↓
      Reads TextBox value
          ↓
      Sets Label text
```

---

# 7. Design the Webpage

Open:

```text
Default.aspx
```

The page will look like:

```text
┌──────────────────────────────────────────┐
│                                          │
│           Enter Your Name                │
│                                          │
│   Name :  [________________________]     │
│                                          │
│              [ Submit ]                  │
│                                          │
│   Hello, Rahul!                          │
│                                          │
└──────────────────────────────────────────┘
```

Controls used on the page:

| Control ID    | Control Type    | Purpose                        |
| ------------- | --------------- | ------------------------------ |
| `lblName`     | `asp:Label`     | Label shown next to TextBox    |
| `txtName`     | `asp:TextBox`   | Accepts user name input        |
| `btnSubmit`   | `asp:Button`    | Triggers the click event       |
| `lblResult`   | `asp:Label`     | Displays the result message    |

---

# 8. Complete Default.aspx

Replace the contents of `Default.aspx` with:

```aspx
<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="Default.aspx.cs"
    Inherits="NameDisplayApp.Default" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <title>Name Display App</title>

    <style>

        body
        {
            font-family: Arial;
            margin: 60px;
            background-color: #f0f4f8;
        }

        .container
        {
            background-color: white;
            padding: 40px;
            width: 420px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.12);
        }

        h2
        {
            margin-bottom: 24px;
            color: #333;
        }

        .field
        {
            margin-bottom: 16px;
        }

        .field label
        {
            display: inline-block;
            width: 60px;
            font-weight: bold;
            color: #555;
        }

        .field input[type=text]
        {
            padding: 8px 10px;
            width: 240px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 14px;
        }

        input[type=submit]
        {
            margin-top: 8px;
            padding: 9px 24px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 4px;
            font-size: 14px;
            cursor: pointer;
        }

        input[type=submit]:hover
        {
            background-color: #0056b3;
        }

        .result
        {
            margin-top: 24px;
            font-size: 18px;
            color: #2c7a2c;
            font-weight: bold;
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="container">

        <h2>Enter Your Name</h2>

        <div class="field">

            <asp:Label
                ID="lblName"
                runat="server"
                Text="Name : " />

            <asp:TextBox
                ID="txtName"
                runat="server" />

        </div>

        <asp:Button
            ID="btnSubmit"
            runat="server"
            Text="Submit"
            OnClick="btnSubmit_Click" />

        <div class="result">

            <asp:Label
                ID="lblResult"
                runat="server"
                Text="" />

        </div>

    </div>

</form>

</body>
</html>
```

---

# 9. Complete Default.aspx.cs

Open:

```text
Default.aspx.cs
```

Replace the contents with:

```csharp
using System;

namespace NameDisplayApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
        }

        protected void btnSubmit_Click(
            object sender,
            EventArgs e)
        {
            string name = txtName.Text.Trim();

            if (name != "")
            {
                lblResult.Text = "Hello, " + name + "!";
            }
            else
            {
                lblResult.Text = "Please enter a name.";
            }
        }
    }
}
```

---

# 10. Understanding the Key Code

### Reading TextBox Input

```csharp
string name = txtName.Text.Trim();
```

```text
txtName.Text
     ↓
Gets the value typed in the TextBox
     ↓
.Trim()
     ↓
Removes extra spaces from input
```

### Setting Label Output

```csharp
lblResult.Text = "Hello, " + name + "!";
```

```text
lblResult.Text
     ↓
Sets the displayed text of the Label
     ↓
Page re-renders
     ↓
Result visible to user
```

### Button Click Event Connection

```aspx
OnClick="btnSubmit_Click"
```

```text
User clicks Button
     ↓
ASP.NET sends POST request to server
     ↓
btnSubmit_Click method runs
     ↓
Label is updated
```

---

# 11. How the Application Works

```text
User opens webpage
       ↓
Default.aspx loads
       ↓
User types name in TextBox
       ↓
Clicks Submit Button
       ↓
HTTP POST sent to server
       ↓
btnSubmit_Click fires
       ↓
txtName.Text is read
       ↓
Validation check (empty or not)
       ↓
lblResult.Text is set
       ↓
Page re-renders with updated Label
       ↓
Name displayed on webpage
```

---

# 12. Expected Output

After typing **"Rahul"** and clicking Submit:

```text
┌──────────────────────────────────────────┐
│                                          │
│           Enter Your Name                │
│                                          │
│   Name :  [ Rahul                   ]   │
│                                          │
│              [ Submit ]                  │
│                                          │
│   Hello, Rahul!                          │
│                                          │
└──────────────────────────────────────────┘
```

If the TextBox is left **empty** and Submit is clicked:

```text
   Please enter a name.
```

---

# 13. Important Controls and Properties

| Control / Property    | Purpose                                            |
| --------------------- | -------------------------------------------------- |
| `asp:Label`           | Displays text on the webpage                       |
| `asp:TextBox`         | Input field for the user                           |
| `asp:Button`          | Triggers a server-side click event                 |
| `Text` property       | Gets or sets the displayed text of a control       |
| `OnClick`             | Links the Button to its C# event handler           |
| `runat="server"`      | Makes the control accessible from C# code-behind  |
| `txtName.Text`        | Reads the value entered in the TextBox             |
| `lblResult.Text`      | Sets the output text of the result Label           |
| `.Trim()`             | Removes leading and trailing whitespace            |

---

# 14. Troubleshooting

## Error: "btnSubmit_Click does not exist in the current context"

Make sure the method name in `Default.aspx`:

```aspx
OnClick="btnSubmit_Click"
```

exactly matches the method in `Default.aspx.cs`:

```csharp
protected void btnSubmit_Click(object sender, EventArgs e)
```

---

## Error: Label shows nothing after clicking Submit

Check the following:

1. `runat="server"` is present on the `asp:Label` control.
2. The `ID` in `Default.aspx` (`lblResult`) matches exactly in `Default.aspx.cs`.
3. `lblResult.Text` is being assigned inside `btnSubmit_Click`.

---

## Error: TextBox value is always empty

Make sure you are reading `txtName.Text` inside the button click event handler, not inside `Page_Load`.

---

## Error: Page reloads but no output appears

Verify that the Button's `OnClick` attribute is correctly set:

```aspx
<asp:Button
    ID="btnSubmit"
    runat="server"
    Text="Submit"
    OnClick="btnSubmit_Click" />
```

If `OnClick` is missing, the event will not be connected.

---

# 15. Result

A simple **ASP.NET Web Forms** application is successfully created using **Label**, **TextBox**, and **Button** controls. When the user enters a name and clicks the Submit button, the `btnSubmit_Click` event reads the TextBox input and displays a greeting message using the Label control.
