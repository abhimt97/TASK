using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TASK
{
    public partial class Form3 : MetroFramework.Forms.MetroForm
    {
        private Form1 _masterForm1;  

    public Form3(Form1 masterForm1 )
    {
        InitializeComponent();
        _masterForm1 = masterForm1;  

    }
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\HP\Documents\d_reminder.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        string time,task;
        public Form3()
        {
            InitializeComponent();
        }
        public void disp_data()
        {
            string numb = "Every Day";
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select task, time from Table2 where date='" + metroDateTime1.Value.ToString("MM-dd-yyyy") + "'OR number ='"+numb+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            metroGrid1.DataSource = dt;
            con.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            disp_data();
        }
        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            time = metroGrid1.SelectedRows[0].Cells["timeDataGridViewTextBoxColumn"].Value.ToString();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            if (time != null||task!= null)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Table2 where time='" + time + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                disp_data();
                 MessageBox.Show(this, "Selected task has been deleted", "Message", MessageBoxButtons.OK, MessageBoxIcon.None);
                time = null;
                Form1 f = new Form1();
                f.disp1_data();
            }
            else
                MessageBox.Show(this, "Select a task to delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _masterForm1.disp1_data();
        }

        private void metroGrid1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            time = metroGrid1.SelectedRows[0].Cells["timeDataGridViewTextBoxColumn"].Value.ToString();
            task = metroGrid1.SelectedRows[0].Cells["taskDataGridViewTextBoxColumn"].Value.ToString();
        }

        private void metroDateTime1_ValueChanged(object sender, EventArgs e)
        {
            disp_data();
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            if (time != null || task != null)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Table2 set task='" + metroTextBox1.Text + "'where task ='"+task+"'";
                cmd.ExecuteNonQuery();
                con.Close();
                disp_data();
                MessageBox.Show(this, "Selected task has been updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.None);
                time = null;      
                _masterForm1.disp1_data();
            }
            else
                MessageBox.Show(this, "Select a task to update", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _masterForm1.disp1_data();
        }

    }
}
