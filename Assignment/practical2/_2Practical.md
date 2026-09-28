# Practical 2: Registration Form with Validation Controls in ASP.NET

## Aim

To create a Registration Form in ASP.NET using:

- RequiredFieldValidator
- RangeValidator
- CompareValidator

---

# 1. Introduction

**Validation Controls** in ASP.NET automatically validate form input before the data is processed on the server. They display error messages on the webpage without writing manual C# validation code.

In this practical:

```text
User fills Registration Form
          ↓
    Clicks Register Button
          ↓
  Validation Controls check input
          ↓
      ┌─────────────┐
      │  All Valid? │
      └─────────────┘
        ↓         ↓
       Yes        No
        ↓         ↓
   Success     Error messages
   Message     shown on page
```

Three validators are used:

```text
RequiredFieldValidator  →  Ensures a field is not left empty
RangeValidator          →  Ensures a value falls within a range
CompareValidator        →  Ensures two field values match
```

---

# 2. Technology Used

- C#
- ASP.NET Web Forms
- ASP.NET Validation Controls
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
Project Name: RegistrationFormApp
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
RegistrationFormApp/
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
└── RegistrationFormApp.csproj
```

The important files for this practical are:

| File                       | Purpose                                              |
| -------------------------- | ---------------------------------------------------- |
| `Default.aspx`             | Registration form UI and all validator controls      |
| `Default.aspx.cs`          | C# code-behind for form submission logic             |
| `Default.aspx.designer.cs` | Auto-generated control declarations                  |
| `Web.config`               | Application configuration                           |

---

# 6. Page Structure

```text
Default.aspx
     ↓
  Form Controls
  ├── TextBox (Full Name)       + RequiredFieldValidator
  ├── TextBox (Age)             + RequiredFieldValidator
  │                             + RangeValidator (18–100)
  ├── TextBox (Password)        + RequiredFieldValidator
  ├── TextBox (Confirm Password)+ RequiredFieldValidator
  │                             + CompareValidator
  ├── Button  (Register)
  ├── ValidationSummary         → lists all errors at top
  └── Label   (Success message)

Default.aspx.cs
     ↓
  btnRegister_Click
     ↓
  Page.IsValid check
     ↓
  Show success message
```

---

# 7. Design the Webpage

Open:

```text
Default.aspx
```

The page will look like:

```text
┌────────────────────────────────────────────────────┐
│                                                    │
│              REGISTRATION FORM                     │
│                                                    │
│  • Full Name is required.                          │
│  • Age must be between 18 and 100.                 │
│  • Passwords do not match.                         │
│                                                    │
│  Full Name  :  [__________________________]        │
│                * Full Name is required.            │
│                                                    │
│  Age (18–100): [__________________________]        │
│                * Age must be between 18 and 100.   │
│                                                    │
│  Password   :  [__________________________]        │
│                                                    │
│  Confirm Pwd:  [__________________________]        │
│                * Passwords do not match.           │
│                                                    │
│                   [ Register ]                     │
│                                                    │
└────────────────────────────────────────────────────┘
```

---

# 8. Understanding the Validators

## RequiredFieldValidator

Ensures a field is not submitted empty.

```text
TextBox is empty
     ↓
RequiredFieldValidator fires
     ↓
Error message shown
     ↓
Form is NOT submitted
```

Key properties:

| Property            | Purpose                                      |
| ------------------- | -------------------------------------------- |
| `ControlToValidate` | ID of the TextBox to validate                |
| `ErrorMessage`      | Message shown in ValidationSummary           |
| `Text`              | Message shown next to the field              |

---

## RangeValidator

Ensures a numeric (or date) value falls within a minimum and maximum.

```text
Age TextBox value = 200
     ↓
RangeValidator checks: 18 ≤ value ≤ 100?
     ↓
No → Error message shown
     ↓
Form is NOT submitted
```

Key properties:

| Property            | Purpose                                      |
| ------------------- | -------------------------------------------- |
| `ControlToValidate` | ID of the TextBox to validate                |
| `MinimumValue`      | Lower bound of the allowed range             |
| `MaximumValue`      | Upper bound of the allowed range             |
| `Type`              | Data type: Integer, String, Date, etc.       |
| `ErrorMessage`      | Message shown in ValidationSummary           |

---

## CompareValidator

Compares the value of one field with another.

```text
Password TextBox     →  "abc123"
Confirm Password     →  "xyz999"
     ↓
CompareValidator checks: do they match?
     ↓
No → Error message shown
     ↓
Form is NOT submitted
```

Key properties:

| Property            | Purpose                                      |
| ------------------- | -------------------------------------------- |
| `ControlToValidate` | The field being validated (Confirm Password) |
| `ControlToCompare`  | The field to compare against (Password)      |
| `Operator`          | Comparison type: Equal, NotEqual, etc.       |
| `ErrorMessage`      | Message shown in ValidationSummary           |

---

# 9. Complete Default.aspx

Replace the contents of `Default.aspx` with:

```aspx
<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="Default.aspx.cs"
    Inherits="RegistrationFormApp.Default" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <title>Registration Form</title>

    <style>

        body
        {
            font-family: Arial;
            margin: 50px;
            background-color: #f0f4f8;
        }

        .container
        {
            background-color: white;
            padding: 40px;
            width: 480px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.12);
        }

        h2
        {
            margin-bottom: 24px;
            color: #333;
            text-align: center;
        }

        .field
        {
            margin-bottom: 18px;
        }

        .field label
        {
            display: block;
            font-weight: bold;
            color: #444;
            margin-bottom: 5px;
            font-size: 14px;
        }

        .field input[type=text],
        .field input[type=password]
        {
            width: 100%;
            padding: 9px 12px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 14px;
        }

        input[type=submit]
        {
            width: 100%;
            padding: 11px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 4px;
            font-size: 15px;
            cursor: pointer;
            margin-top: 8px;
        }

        input[type=submit]:hover
        {
            background-color: #0056b3;
        }

        .validator
        {
            color: #cc0000;
            font-size: 12px;
            display: block;
            margin-top: 4px;
        }

        .summary
        {
            background-color: #fff3f3;
            border: 1px solid #f5c6c6;
            border-radius: 4px;
            padding: 12px 16px;
            margin-bottom: 16px;
            color: #cc0000;
            font-size: 13px;
        }

        .success
        {
            margin-top: 20px;
            color: #2c7a2c;
            font-size: 16px;
            font-weight: bold;
            text-align: center;
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="container">

        <h2>Registration Form</h2>

        <asp:ValidationSummary
            ID="valSummary"
            runat="server"
            CssClass="summary"
            HeaderText="Please fix the following errors:" />

        <!-- Full Name -->
        <div class="field">

            <label>Full Name</label>

            <asp:TextBox
                ID="txtName"
                runat="server" />

            <asp:RequiredFieldValidator
                ID="rfvName"
                runat="server"
                ControlToValidate="txtName"
                ErrorMessage="Full Name is required."
                Text="* Full Name is required."
                CssClass="validator" />

        </div>

        <!-- Age -->
        <div class="field">

            <label>Age (18 – 100)</label>

            <asp:TextBox
                ID="txtAge"
                runat="server" />

            <asp:RequiredFieldValidator
                ID="rfvAge"
                runat="server"
                ControlToValidate="txtAge"
                ErrorMessage="Age is required."
                Text="* Age is required."
                CssClass="validator" />

            <asp:RangeValidator
                ID="rvAge"
                runat="server"
                ControlToValidate="txtAge"
                MinimumValue="18"
                MaximumValue="100"
                Type="Integer"
                ErrorMessage="Age must be between 18 and 100."
                Text="* Age must be between 18 and 100."
                CssClass="validator" />

        </div>

        <!-- Password -->
        <div class="field">

            <label>Password</label>

            <asp:TextBox
                ID="txtPassword"
                runat="server"
                TextMode="Password" />

            <asp:RequiredFieldValidator
                ID="rfvPassword"
                runat="server"
                ControlToValidate="txtPassword"
                ErrorMessage="Password is required."
                Text="* Password is required."
                CssClass="validator" />

        </div>

        <!-- Confirm Password -->
        <div class="field">

            <label>Confirm Password</label>

            <asp:TextBox
                ID="txtConfirmPassword"
                runat="server"
                TextMode="Password" />

            <asp:RequiredFieldValidator
                ID="rfvConfirmPassword"
                runat="server"
                ControlToValidate="txtConfirmPassword"
                ErrorMessage="Confirm Password is required."
                Text="* Confirm Password is required."
                CssClass="validator" />

            <asp:CompareValidator
                ID="cvPassword"
                runat="server"
                ControlToValidate="txtConfirmPassword"
                ControlToCompare="txtPassword"
                Operator="Equal"
                ErrorMessage="Passwords do not match."
                Text="* Passwords do not match."
                CssClass="validator" />

        </div>

        <!-- Register Button -->
        <asp:Button
            ID="btnRegister"
            runat="server"
            Text="Register"
            OnClick="btnRegister_Click" />

        <!-- Success Message -->
        <div class="success">
            <asp:Label
                ID="lblSuccess"
                runat="server"
                Text="" />
        </div>

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

namespace RegistrationFormApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
        }

        protected void btnRegister_Click(
            object sender,
            EventArgs e)
        {
            if (Page.IsValid)
            {
                lblSuccess.Text =
                    "Registration successful! Welcome, "
                    + txtName.Text.Trim()
                    + ".";
            }
        }
    }
}
```

---

# 11. Understanding Page.IsValid

```csharp
if (Page.IsValid)
```

```text
All validation controls pass?
          ↓
     Page.IsValid = true
          ↓
   Execute success logic
```

```text
Any validation fails?
          ↓
     Page.IsValid = false
          ↓
   Success logic is skipped
          ↓
   Error messages remain on page
```

`Page.IsValid` is automatically set by ASP.NET based on all validator results.

---

# 12. How the Application Works

```text
User fills Registration Form
       ↓
Clicks Register Button
       ↓
Client-side validation runs first
       ↓
   ┌────────────────────────────────┐
   │  RequiredFieldValidator check  │
   │  RangeValidator check          │
   │  CompareValidator check        │
   └────────────────────────────────┘
              ↓            ↓
          All pass       Any fail
              ↓            ↓
         Page.IsValid  Error messages
          = true        on page
              ↓
   btnRegister_Click executes
              ↓
   Success message displayed
```

---

# 13. Expected Output

### On Invalid Submission:

```text
┌────────────────────────────────────────────────────┐
│  Please fix the following errors:                  │
│  • Full Name is required.                          │
│  • Age must be between 18 and 100.                 │
│  • Passwords do not match.                         │
│                                                    │
│  Full Name :  [                    ]               │
│               * Full Name is required.             │
│                                                    │
│  Age       :  [ 200                ]               │
│               * Age must be between 18 and 100.    │
└────────────────────────────────────────────────────┘
```

### On Valid Submission:

```text
┌────────────────────────────────────────────────────┐
│  Full Name :  [ Rahul              ]               │
│  Age       :  [ 21                 ]               │
│  Password  :  [ ********           ]               │
│  Confirm   :  [ ********           ]               │
│                                                    │
│                 [ Register ]                       │
│                                                    │
│     Registration successful! Welcome, Rahul.       │
└────────────────────────────────────────────────────┘
```

---

# 14. Important Controls and Properties

| Control / Property           | Purpose                                             |
| ---------------------------- | --------------------------------------------------- |
| `RequiredFieldValidator`     | Ensures the field is not left empty                 |
| `RangeValidator`             | Ensures value is within a min–max range             |
| `CompareValidator`           | Compares two field values                           |
| `ValidationSummary`          | Displays all error messages in one place            |
| `ControlToValidate`          | Links the validator to a specific TextBox           |
| `ErrorMessage`               | Message shown in ValidationSummary                  |
| `Text`                       | Message shown inline next to the field              |
| `MinimumValue`               | Lower bound for RangeValidator                      |
| `MaximumValue`               | Upper bound for RangeValidator                      |
| `Type`                       | Data type for RangeValidator (Integer, Date, etc.)  |
| `ControlToCompare`           | Field to compare against in CompareValidator        |
| `Operator`                   | Comparison type: Equal, NotEqual, GreaterThan, etc. |
| `Page.IsValid`               | True only if all validators pass                    |
| `TextMode="Password"`        | Hides password input characters                     |

---

# 15. Troubleshooting

## Error: Form submits even with empty fields

Check:
1. `ControlToValidate` is set to the correct TextBox ID.
2. `runat="server"` is present on both the TextBox and Validator.
3. The TextBox `ID` matches exactly with `ControlToValidate`.

---

## Error: RangeValidator always shows error

Check:
1. `Type="Integer"` is set (not `Type="String"`).
2. `MinimumValue` and `MaximumValue` are correct numbers.
3. The user is entering a numeric value, not text.

---

## Error: CompareValidator not detecting mismatch

Check:
1. `ControlToValidate` is set to the **Confirm Password** field.
2. `ControlToCompare` is set to the original **Password** field.
3. `Operator="Equal"` is set.
4. Both TextBoxes have `TextMode="Password"`.

---

## Error: Success message shows even when form is invalid

Make sure `Page.IsValid` is checked inside the button click event:

```csharp
if (Page.IsValid)
{
    lblSuccess.Text = "Registration successful! ...";
}
```

---

# 16. Result

A **Registration Form** is successfully created in ASP.NET Web Forms using **RequiredFieldValidator**, **RangeValidator**, and **CompareValidator**. The form validates all input fields automatically before submission and displays a success message only when all validations pass.
