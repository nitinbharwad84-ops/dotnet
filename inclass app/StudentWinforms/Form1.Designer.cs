namespace StudentWinforms;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.DataGridView dgvStudents;
    private System.Windows.Forms.Label lblStudentName;
    private System.Windows.Forms.Label lblLocation;
    private System.Windows.Forms.Label lblAge;
    private System.Windows.Forms.Label lblGender;
    private System.Windows.Forms.TextBox txtStudentName;
    private System.Windows.Forms.TextBox txtLocation;
    private System.Windows.Forms.TextBox txtAge;
    private System.Windows.Forms.ComboBox cmbGender;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.Button btnShowData;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();

        this.dgvStudents = new System.Windows.Forms.DataGridView();
        this.lblStudentName = new System.Windows.Forms.Label();
        this.lblLocation = new System.Windows.Forms.Label();
        this.lblAge = new System.Windows.Forms.Label();
        this.lblGender = new System.Windows.Forms.Label();
        this.txtStudentName = new System.Windows.Forms.TextBox();
        this.txtLocation = new System.Windows.Forms.TextBox();
        this.txtAge = new System.Windows.Forms.TextBox();
        this.cmbGender = new System.Windows.Forms.ComboBox();
        this.btnAdd = new System.Windows.Forms.Button();
        this.btnUpdate = new System.Windows.Forms.Button();
        this.btnDelete = new System.Windows.Forms.Button();
        this.btnClear = new System.Windows.Forms.Button();
        this.btnShowData = new System.Windows.Forms.Button();

        ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
        this.SuspendLayout();

        this.dgvStudents.AllowUserToAddRows = false;
        this.dgvStudents.AllowUserToDeleteRows = false;
        this.dgvStudents.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
        this.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvStudents.Location = new System.Drawing.Point(12, 12);
        this.dgvStudents.Name = "dgvStudents";
        this.dgvStudents.ReadOnly = true;
        this.dgvStudents.Size = new System.Drawing.Size(760, 220);
        this.dgvStudents.TabIndex = 0;

        this.lblStudentName.AutoSize = true;
        this.lblStudentName.Location = new System.Drawing.Point(20, 273);
        this.lblStudentName.Name = "lblStudentName";
        this.lblStudentName.Size = new System.Drawing.Size(82, 15);
        this.lblStudentName.Text = "Student Name";

        this.txtStudentName.Location = new System.Drawing.Point(120, 270);
        this.txtStudentName.Name = "txtStudentName";
        this.txtStudentName.Size = new System.Drawing.Size(150, 23);
        this.txtStudentName.TabIndex = 1;

        this.lblLocation.AutoSize = true;
        this.lblLocation.Location = new System.Drawing.Point(20, 303);
        this.lblLocation.Name = "lblLocation";
        this.lblLocation.Size = new System.Drawing.Size(53, 15);
        this.lblLocation.Text = "Location";

        this.txtLocation.Location = new System.Drawing.Point(120, 300);
        this.txtLocation.Name = "txtLocation";
        this.txtLocation.Size = new System.Drawing.Size(150, 23);
        this.txtLocation.TabIndex = 2;

        this.lblAge.AutoSize = true;
        this.lblAge.Location = new System.Drawing.Point(20, 333);
        this.lblAge.Name = "lblAge";
        this.lblAge.Size = new System.Drawing.Size(29, 15);
        this.lblAge.Text = "Age";

        this.txtAge.Location = new System.Drawing.Point(120, 330);
        this.txtAge.Name = "txtAge";
        this.txtAge.Size = new System.Drawing.Size(120, 23);
        this.txtAge.TabIndex = 3;

        this.lblGender.AutoSize = true;
        this.lblGender.Location = new System.Drawing.Point(20, 363);
        this.lblGender.Name = "lblGender";
        this.lblGender.Size = new System.Drawing.Size(45, 15);
        this.lblGender.Text = "Gender";

        this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
        this.cmbGender.Location = new System.Drawing.Point(120, 360);
        this.cmbGender.Name = "cmbGender";
        this.cmbGender.Size = new System.Drawing.Size(120, 23);
        this.cmbGender.TabIndex = 4;

        this.btnAdd.Location = new System.Drawing.Point(330, 270);
        this.btnAdd.Name = "btnAdd";
        this.btnAdd.Size = new System.Drawing.Size(110, 30);
        this.btnAdd.Text = "Add";
        this.btnAdd.UseVisualStyleBackColor = true;
        this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

        this.btnUpdate.Location = new System.Drawing.Point(330, 310);
        this.btnUpdate.Name = "btnUpdate";
        this.btnUpdate.Size = new System.Drawing.Size(110, 30);
        this.btnUpdate.Text = "Update";
        this.btnUpdate.UseVisualStyleBackColor = true;
        this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

        this.btnDelete.Location = new System.Drawing.Point(330, 350);
        this.btnDelete.Name = "btnDelete";
        this.btnDelete.Size = new System.Drawing.Size(110, 30);
        this.btnDelete.Text = "Delete";
        this.btnDelete.UseVisualStyleBackColor = true;
        this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

        this.btnClear.Location = new System.Drawing.Point(330, 390);
        this.btnClear.Name = "btnClear";
        this.btnClear.Size = new System.Drawing.Size(110, 30);
        this.btnClear.Text = "Clear";
        this.btnClear.UseVisualStyleBackColor = true;
        this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

        this.btnShowData.Location = new System.Drawing.Point(330, 430);
        this.btnShowData.Name = "btnShowData";
        this.btnShowData.Size = new System.Drawing.Size(110, 30);
        this.btnShowData.Text = "Show Data";
        this.btnShowData.UseVisualStyleBackColor = true;
        this.btnShowData.Click += new System.EventHandler(this.btnShowData_Click);

        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 500);
        this.Controls.Add(this.dgvStudents);
        this.Controls.Add(this.lblStudentName);
        this.Controls.Add(this.lblLocation);
        this.Controls.Add(this.lblAge);
        this.Controls.Add(this.lblGender);
        this.Controls.Add(this.txtStudentName);
        this.Controls.Add(this.txtLocation);
        this.Controls.Add(this.txtAge);
        this.Controls.Add(this.cmbGender);
        this.Controls.Add(this.btnAdd);
        this.Controls.Add(this.btnUpdate);
        this.Controls.Add(this.btnDelete);
        this.Controls.Add(this.btnClear);
        this.Controls.Add(this.btnShowData);
        this.Name = "Form1";
        this.Text = "Student Management";
        this.Load += new System.EventHandler(this.Form1_Load);
        this.dgvStudents.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudents_CellClick);

        ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
}