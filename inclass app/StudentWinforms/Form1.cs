using Microsoft.Data.Sqlite;
using System.Data;

namespace StudentWinforms
{
    public partial class Form1 : Form
    {
        private readonly string connectionString =
            $"Data Source={Path.Combine(AppContext.BaseDirectory, "student.db")}";
        private int? selectedStudentId;

        public Form1()
        {
            InitializeComponent();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var con = new SqliteConnection(connectionString);
            con.Open();

            string query = @"
                CREATE TABLE IF NOT EXISTS Student (
                    StudentID INTEGER PRIMARY KEY AUTOINCREMENT,
                    StudentName TEXT NOT NULL,
                    Location TEXT,
                    Age INTEGER,
                    Gender TEXT
                );";

            using var cmd = new SqliteCommand(query, con);
            cmd.ExecuteNonQuery();
        }

        // Load students into DataGridView
        void LoadStudents()
        {
            using SqliteConnection con = new SqliteConnection(connectionString);
            string query = "SELECT * FROM Student";

            using SqliteCommand cmd = new SqliteCommand(query, con);
            con.Open();

            using SqliteDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            dgvStudents.DataSource = dt;
        }

        // Add Student
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using SqliteConnection con = new SqliteConnection(connectionString);

            string query = @"INSERT INTO Student
                            (StudentName, Location, Age, Gender)
                            VALUES
                            (@StudentName, @Location, @Age, @Gender)";

            using SqliteCommand cmd = new SqliteCommand(query, con);

            cmd.Parameters.AddWithValue("@StudentName", txtStudentName.Text);
            cmd.Parameters.AddWithValue("@Location", txtLocation.Text);
            cmd.Parameters.AddWithValue("@Age", int.Parse(txtAge.Text));
            cmd.Parameters.AddWithValue("@Gender", cmbGender.Text);

            con.Open();
            cmd.ExecuteNonQuery();

            MessageBox.Show("Student added successfully.");
            LoadStudents();
            ClearFields();
        }

        // Update Student
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedStudentId == null)
            {
                MessageBox.Show("Please select a student from the table first.");
                return;
            }

            using SqliteConnection con = new SqliteConnection(connectionString);

            string query = @"UPDATE Student
                             SET StudentName = @StudentName,
                                 Location = @Location,
                                 Age = @Age,
                                 Gender = @Gender
                             WHERE StudentID = @StudentID";

            using SqliteCommand cmd = new SqliteCommand(query, con);

            cmd.Parameters.AddWithValue("@StudentID", selectedStudentId.Value);
            cmd.Parameters.AddWithValue("@StudentName", txtStudentName.Text);
            cmd.Parameters.AddWithValue("@Location", txtLocation.Text);
            cmd.Parameters.AddWithValue("@Age", int.Parse(txtAge.Text));
            cmd.Parameters.AddWithValue("@Gender", cmbGender.Text);

            con.Open();
            cmd.ExecuteNonQuery();

            MessageBox.Show("Student updated successfully.");
            LoadStudents();
            ClearFields();
        }

        // Delete Student
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedStudentId == null)
            {
                MessageBox.Show("Please select a student from the table first.");
                return;
            }

            using SqliteConnection con = new SqliteConnection(connectionString);

            string query = "DELETE FROM Student WHERE StudentID = @StudentID";

            using SqliteCommand cmd = new SqliteCommand(query, con);
            cmd.Parameters.AddWithValue("@StudentID", selectedStudentId.Value);

            con.Open();
            cmd.ExecuteNonQuery();

            MessageBox.Show("Student deleted successfully.");
            LoadStudents();
            ClearFields();
        }

        // Show data from database
        private void btnShowData_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }

        // Clear fields
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        void ClearFields()
        {
            selectedStudentId = null;
            txtStudentName.Clear();
            txtLocation.Clear();
            txtAge.Clear();
            cmbGender.SelectedIndex = -1;
        }

        // Form Load
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }

        // Select DataGridView row
        private void dgvStudents_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStudents.Rows[e.RowIndex];

                selectedStudentId = row.Cells["StudentID"]?.Value is int id ? id : Convert.ToInt32(row.Cells["StudentID"]?.Value);
                txtStudentName.Text = row.Cells["StudentName"]?.Value?.ToString() ?? "";
                txtLocation.Text = row.Cells["Location"]?.Value?.ToString() ?? "";
                txtAge.Text = row.Cells["Age"]?.Value?.ToString() ?? "";
                cmbGender.Text = row.Cells["Gender"]?.Value?.ToString() ?? "";
            }
        }
    }
}