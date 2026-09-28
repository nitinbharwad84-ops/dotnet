namespace MyWinFormsApp;

partial class Form1 : Form
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(350, 250);
        this.Text = "Addition App";
    }
}