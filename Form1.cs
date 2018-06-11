using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Media;
using System.Threading;



namespace TASK
{   
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        string curdt,curtim;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\HP\Documents\d_reminder.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = DateTime.Today.ToString("dd");
            label2.Text = DateTime.Today.ToString("dddd");
            disp1_data();
            
        }
        public void disp1_data()
        {
            label5.Text = null;
            string numb = "Every Day";

            con.Open();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "select task from Table2 where date='" + DateTime.Today.ToString("MM-dd-yyyy") + "'OR number ='"+numb+"'";
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        label5.Text += dr.GetString(0) + "\n\n";
                    }
                }
                con.Close();
            }
        }
        
        private void rectangleShape1_Click(object sender, EventArgs e)
        {

        }

        private void rectangleShape1_Click_1(object sender, EventArgs e)
        {

        }

        private void rectangleShape1_Click_2(object sender, EventArgs e)
        {

        }

        private void rectangleShape1_Click_3(object sender, EventArgs e)
        {

        }

        private void metroDateTime1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
       {

        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            Form2 se = new Form2(this);
            se.Show();   
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            Form3 se = new Form3(this);
            se.Show();
        }
        public void tim1()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select task from Table2 where date='" + curdt + "' AND time='" + curtim + "'";

            var taskn = cmd.ExecuteReader();
            if (taskn.HasRows)
            {
                taskn.Read();
                var taskvl = taskn.GetString(0);
                con.Close();
                SoundPlayer player = new SoundPlayer();
                player.SoundLocation = @"G:\SONGS\MALAYALAM\alarm2.wav";
                player.PlayLooping();
                if (this.WindowState == FormWindowState.Minimized)
                {
                    notifyIcon1.ShowBalloonTip(7000, "Reminder", "'" + taskvl + "' ", ToolTipIcon.None);
                    System.Threading.Thread.Sleep(5000);
                    player.Stop();
                }
                else if (this.WindowState == FormWindowState.Normal )
                {
                    notifyIcon1.ShowBalloonTip(7000, "Reminder", "'" + taskvl + "' ", ToolTipIcon.Info);
                    System.Threading.Thread.Sleep(5000);
                    player.Stop();
                    var v = MetroFramework.MetroMessageBox.Show(this, taskvl, "Reminder", MessageBoxButtons.OK, MessageBoxIcon.None);
                    System.Threading.Thread.Sleep(500);
                    curtim = DateTime.Now.ToString("HH:mm:ss");
                    if (v == DialogResult.OK)
                        player.Stop();
                }
            }
            con.Close();

        }
        public void tim2()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            string num = "Every Day";
            cmd.CommandText = "select task from Table2 where number= '" + num + "'AND date != '" + curdt + "' AND time='" + curtim + "'";
            var taskn = cmd.ExecuteReader();
            if (taskn.HasRows)
            {
                taskn.Read();
                var taskvl = taskn.GetString(0);
                con.Close();
                SoundPlayer player = new SoundPlayer();
                player.SoundLocation = @"G:\SONGS\MALAYALAM\alarm2.wav";
                player.PlayLooping();
                if (this.WindowState == FormWindowState.Normal)
                {
                    notifyIcon1.ShowBalloonTip(7000, "Reminder", "'" + taskvl + "' ", ToolTipIcon.None);
                    System.Threading.Thread.Sleep(5000);
                    player.Stop();
                    var v = MetroFramework.MetroMessageBox.Show(this, taskvl, "Reminder", MessageBoxButtons.OK, MessageBoxIcon.None);
                    System.Threading.Thread.Sleep(500);
                    curtim = DateTime.Now.ToString("HH:mm:ss");
                    if (v == DialogResult.OK)
                        player.Stop();
                }
                else
                {
                   
                    notifyIcon1.ShowBalloonTip(7000, "Reminder", "'" + taskvl + "' ", ToolTipIcon.None);
                    System.Threading.Thread.Sleep(5000);
                    player.Stop();
                   
                }
            }
            con.Close();

        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString("HH:mm:ss");
            curtim = DateTime.Now.ToString("HH:mm:ss");
            curdt = DateTime.Now.ToString("MM-dd-yyyy");
            tim1();
            tim2();
           

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           
        }
    }
}
