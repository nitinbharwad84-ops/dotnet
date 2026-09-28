using System;
using System.Windows.Forms;

namespace MyWinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly TextBox txtA;
        private readonly TextBox txtB;
        private readonly Button btnCalculate;
        private readonly Label lblResult;

        public Form1()
        {
            this.Text = "Calculator App";
            this.Width = 400;
            this.Height = 300;

            txtA = new TextBox();
            txtA.Left = 50;
            txtA.Top = 30;
            txtA.Width = 100;

            txtB = new TextBox();
            txtB.Left = 50;
            txtB.Top = 70;
            txtB.Width = 100;

            btnCalculate = new Button();
            btnCalculate.Text = "Calculate";
            btnCalculate.Left = 50;
            btnCalculate.Top = 110;
            btnCalculate.Click += BtnCalculate_Click;

            lblResult = new Label();
            lblResult.Left = 50;
            lblResult.Top = 150;
            lblResult.Width = 260;
            lblResult.Height = 120;
            lblResult.BorderStyle = BorderStyle.FixedSingle;
            lblResult.Text = "Result will appear here";
            lblResult.AutoEllipsis = true;
            lblResult.AutoSize = false;
            lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.Controls.Add(txtA);
            this.Controls.Add(txtB);
            this.Controls.Add(btnCalculate);
            this.Controls.Add(lblResult);
        }

        private void BtnCalculate_Click(object? sender, EventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(txtA.Text);
                int b = Convert.ToInt32(txtB.Text);

                int addition = a + b;
                int subtraction = a - b;
                int multiplication = a * b;
                double division = b == 0 ? 0 : (double)a / b;

                lblResult.Text = "Addition: " + addition + Environment.NewLine +
                                "Subtraction: " + subtraction + Environment.NewLine +
                                "Multiplication: " + multiplication + Environment.NewLine +
                                "Division: " + division;
            }
            catch
            {
                MessageBox.Show("Enter valid numbers");
            }
        }
    }
}