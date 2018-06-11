using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Win32;

namespace TASK
{
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {
        private Form1 _masterForm;  

    public Form2(Form1 masterForm )
    {
        InitializeComponent();
        _masterForm = masterForm;  

    }
         SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\HP\Documents\d_reminder.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            metroComboBox1.SelectedItem = "Once";
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.SetValue("reminder2", Application.ExecutablePath.ToString());
            if (metroTextBox1.Text.Length == 0)
            {
                MessageBox.Show(this, "Enter a task description", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " insert into Table2 values('" + metroTextBox1.Text + "','" + dateTimePicker1.Text + "','" + metroDateTime1.Text + "','"+metroComboBox1.Text+"')";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(this, "Successfully added your task", "Message", MessageBoxButtons.OK, MessageBoxIcon.None);
                _masterForm.disp1_data();
                this.Close();
            }
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
