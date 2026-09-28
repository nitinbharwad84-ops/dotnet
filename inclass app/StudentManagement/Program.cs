using Microsoft.Data.SqlClient;

class Program
{
    static string connectionString = GetConnectionString();

    static string GetConnectionString()
    {
        string[] candidates =
        {
            @"Server=(localdb)\MSSQLLocalDB;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;",
            @"Server=.\SQLEXPRESS;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;",
            @"Server=.;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;"
        };

        foreach (string candidate in candidates)
        {
            try
            {
                using SqlConnection testConnection = new SqlConnection(candidate);
                testConnection.Open();
                return candidate;
            }
            catch (SqlException)
            {
                // Try the next server option.
            }
        }

        return candidates[0];
    }

    static void EnsureDatabaseAndTableExists()
    {
        string masterConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";

        try
        {
            using SqlConnection masterConnection = new SqlConnection(masterConnectionString);
            masterConnection.Open();

            string createDatabaseQuery = @"
                IF DB_ID('CollegeDB') IS NULL
                    CREATE DATABASE CollegeDB;";

            using SqlCommand createDatabaseCommand = new SqlCommand(createDatabaseQuery, masterConnection);
            createDatabaseCommand.ExecuteNonQuery();
        }
        catch (SqlException)
        {
            // If LocalDB is not available on the machine, try the other server options.
            foreach (string candidate in new[]
            {
                @"Server=.\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;",
                @"Server=.;Database=master;Trusted_Connection=True;TrustServerCertificate=True;"
            })
            {
                try
                {
                    using SqlConnection fallbackConnection = new SqlConnection(candidate);
                    fallbackConnection.Open();

                    string createDatabaseQuery = @"
                        IF DB_ID('CollegeDB') IS NULL
                            CREATE DATABASE CollegeDB;";

                    using SqlCommand createDatabaseCommand = new SqlCommand(createDatabaseQuery, fallbackConnection);
                    createDatabaseCommand.ExecuteNonQuery();

                    connectionString = candidate.Replace("Database=master", "Database=CollegeDB");
                    return;
                }
                catch (SqlException)
                {
                    // Try next option.
                }
            }

            Console.WriteLine("Unable to connect to SQL Server. Please install SQL Server or LocalDB and make sure the CollegeDB database exists.");
            Environment.Exit(1);
        }

        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string createTableQuery = @"
            IF OBJECT_ID('dbo.Student', 'U') IS NULL
            BEGIN
                CREATE TABLE Student (
                    StudentID INT IDENTITY(1,1) PRIMARY KEY,
                    StudentName NVARCHAR(100) NOT NULL,
                    Location NVARCHAR(100) NOT NULL,
                    Age INT NOT NULL,
                    Gender NVARCHAR(20) NOT NULL
                );
            END;";

        using SqlCommand createTableCommand = new SqlCommand(createTableQuery, connection);
        createTableCommand.ExecuteNonQuery();
    }

    static string ReadRequiredInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? value = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            Console.WriteLine("Input cannot be empty. Please try again.");
        }
    }

    static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? value = Console.ReadLine();

            if (int.TryParse(value, out int result))
            {
                return result;
            }

            Console.WriteLine("Invalid number. Please try again.");
        }
    }

    static void Main()
    {
        EnsureDatabaseAndTableExists();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("================================");
            Console.WriteLine("       STUDENT MANAGEMENT");
            Console.WriteLine("================================");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View Students");
            Console.WriteLine("3. Search Student");
            Console.WriteLine("4. Update Student");
            Console.WriteLine("5. Delete Student");
            Console.WriteLine("6. Exit");

            string choice = ReadRequiredInput("Enter your choice: ");

            switch (choice)
            {
                case "1":
                    AddStudent();
                    break;

                case "2":
                    ViewStudents();
                    break;

                case "3":
                    SearchStudent();
                    break;

                case "4":
                    UpdateStudent();
                    break;

                case "5":
                    DeleteStudent();
                    break;

                case "6":
                    Console.WriteLine("Application closed.");
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void AddStudent()
    {
        string name = ReadRequiredInput("Enter Student Name: ");
        string location = ReadRequiredInput("Enter Location: ");
        int age = ReadInt("Enter Age: ");
        string gender = ReadRequiredInput("Enter Gender: ");

        string query = @"INSERT INTO Student
                         (StudentName, Location, Age, Gender)
                         VALUES
                         (@StudentName, @Location, @Age, @Gender)";

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(query, connection);

        command.Parameters.AddWithValue(
            "@StudentName", name);

        command.Parameters.AddWithValue(
            "@Location", location);

        command.Parameters.AddWithValue(
            "@Age", age);

        command.Parameters.AddWithValue(
            "@Gender", gender);

        connection.Open();

        command.ExecuteNonQuery();

        Console.WriteLine("Student added successfully.");
    }

    static void ViewStudents()
    {
        string query = "SELECT * FROM Student";

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(query, connection);

        connection.Open();

        using SqlDataReader reader =
            command.ExecuteReader();

        Console.WriteLine();
        Console.WriteLine("ID\tName\tLocation\tAge\tGender");
        Console.WriteLine("------------------------------------------------");

        while (reader.Read())
        {
            Console.WriteLine(
                $"{reader["StudentID"]}\t" +
                $"{reader["StudentName"]}\t" +
                $"{reader["Location"]}\t" +
                $"{reader["Age"]}\t" +
                $"{reader["Gender"]}");
        }
    }

    static void SearchStudent()
    {
        int id = ReadInt("Enter Student ID: ");

        string query =
            "SELECT * FROM Student " +
            "WHERE StudentID = @StudentID";

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(query, connection);

        command.Parameters.AddWithValue(
            "@StudentID", id);

        connection.Open();

        using SqlDataReader reader =
            command.ExecuteReader();

        if (reader.Read())
        {
            Console.WriteLine();
            Console.WriteLine("Student Found");
            Console.WriteLine("-------------------------");

            Console.WriteLine("Student ID: " + reader["StudentID"]);
            Console.WriteLine("Student Name: " + reader["StudentName"]);
            Console.WriteLine("Location: " + reader["Location"]);
            Console.WriteLine("Age: " + reader["Age"]);
            Console.WriteLine("Gender: " + reader["Gender"]);
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    static void UpdateStudent()
    {
        int id = ReadInt("Enter Student ID: ");
        string name = ReadRequiredInput("Enter New Name: ");
        string location = ReadRequiredInput("Enter New Location: ");
        int age = ReadInt("Enter New Age: ");
        string gender = ReadRequiredInput("Enter New Gender: ");

        string query = @"UPDATE Student
                         SET StudentName = @StudentName,
                             Location = @Location,
                             Age = @Age,
                             Gender = @Gender
                         WHERE StudentID = @StudentID";

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(query, connection);

        command.Parameters.AddWithValue(
            "@StudentID", id);

        command.Parameters.AddWithValue(
            "@StudentName", name);

        command.Parameters.AddWithValue(
            "@Location", location);

        command.Parameters.AddWithValue(
            "@Age", age);

        command.Parameters.AddWithValue(
            "@Gender", gender);

        connection.Open();

        int rowsAffected =
            command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            Console.WriteLine("Student updated successfully.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    static void DeleteStudent()
    {
        int id = ReadInt("Enter Student ID: ");

        string query =
            "DELETE FROM Student " +
            "WHERE StudentID = @StudentID";

        using SqlConnection connection =
            new SqlConnection(connectionString);

        using SqlCommand command =
            new SqlCommand(query, connection);

        command.Parameters.AddWithValue(
            "@StudentID", id);

        connection.Open();

        int rowsAffected =
            command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            Console.WriteLine("Student deleted successfully.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }
}

