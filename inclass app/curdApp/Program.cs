using Microsoft.Data.SqlClient;

class Program
{
    static string connectionString =
        @"Server=.\SQLEXPRESS;
          Database=CollegeDB;
          Trusted_Connection=True;
          TrustServerCertificate=True;";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("================================");
            Console.WriteLine("       STUDENT MANAGEMENT");
            Console.WriteLine("================================");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View Students");
            Console.WriteLine("3. Update Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudent();
                    break;

                case "2":
                    ViewStudents();
                    break;

                case "3":
                    UpdateStudent();
                    break;

                case "4":
                    DeleteStudent();
                    break;

                case "5":
                    Console.WriteLine(
                        "Application closed.");
                    return;

                default:
                    Console.WriteLine(
                        "Invalid choice.");
                    break;
            }
        }
    }

    static void AddStudent()
    {
        Console.Write("Enter Student Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Location: ");
        string location = Console.ReadLine();

        Console.Write("Enter Age: ");
        int age = Convert.ToInt32(
            Console.ReadLine());

        Console.Write("Enter Gender: ");
        string gender = Console.ReadLine();

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

        Console.WriteLine(
            "Student added successfully.");
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
        Console.WriteLine(
            "ID\tName\tLocation\tAge\tGender");

        Console.WriteLine(
            "------------------------------------------------");

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

    static void UpdateStudent()
    {
        Console.Write("Enter Student ID: ");
        int id = Convert.ToInt32(
            Console.ReadLine());

        Console.Write("Enter New Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter New Location: ");
        string location = Console.ReadLine();

        Console.Write("Enter New Age: ");
        int age = Convert.ToInt32(
            Console.ReadLine());

        Console.Write("Enter New Gender: ");
        string gender = Console.ReadLine();

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
            Console.WriteLine(
                "Student updated successfully.");
        else
            Console.WriteLine(
                "Student not found.");
    }

    static void DeleteStudent()
    {
        Console.Write("Enter Student ID: ");
        int id = Convert.ToInt32(
            Console.ReadLine());

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
            Console.WriteLine(
                "Student deleted successfully.");
        else
            Console.WriteLine(
                "Student not found.");
    }
}