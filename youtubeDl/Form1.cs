using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace youtubeDl
{
    public partial class Form1 : Form
    {
        string q = "";
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            database.Create_db();

            
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            button1.Text = "Start";
            this.BackColor = Color.Green;
            button1.BackColor = Color.Green;

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            //RunWorkerCompleted(
            backgroundWorker1.WorkerReportsProgress = true;

            //notifyIcon1.BalloonTipTitle = "Title";
            //notifyIcon1.BalloonTipText =  "Text";
            //notifyIcon1.Text =  "TEXT";

            ContextMenuStrip s = new ContextMenuStrip();
            ToolStripMenuItem goster = new ToolStripMenuItem();
            goster.Text = "Show All Videos";
            goster.Click += goster_Click;
            s.Items.Add(goster);

            ToolStripMenuItem cizgi = new ToolStripMenuItem();
            cizgi.Text = "-";
           
            s.Items.Add(cizgi);

            ToolStripMenuItem sil = new ToolStripMenuItem();
            sil.Text = "Delete All Videos";
            sil.Click += sil_Click;
            s.Items.Add(sil);

            ToolStripMenuItem quality = new ToolStripMenuItem();
            quality.Text = "Choose Quality";
            quality.Click += quality_Click;
            s.Items.Add(quality);




            this.ContextMenuStrip = s;











        }
        void goster_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
        void quality_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        void sil_Click(object sender, EventArgs e)
        {



            DialogResult dialogResult = MessageBox.Show("Sure", "Delete All ?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    string path = "deneme.db";
                    string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";
                    var con = new SQLiteConnection(cs);
                    SQLiteDataReader dr;
                    con.Open();

                    //string stm = "select * FROM data ORDER BY id ASC  ";
                    //SELECT * FROM (SELECT * FROM graphs WHERE sid=2 ORDER BY id DESC LIMIT 10) g ORDER BY g.id
                    string stm = "delete from data";
                    var cmd = new SQLiteCommand(stm, con);
                    dr = cmd.ExecuteReader();

                    stm = "delete from sqlite_sequence where name='data'";
                    cmd = new SQLiteCommand(stm, con);
                    dr = cmd.ExecuteReader();

                    con.Close();

                    


                    // dataGridView1.Columns[0].Visible = false;



                }
                catch
                { 
                
                }
            }
            else if (dialogResult == DialogResult.No)
            {

                //do something else
            }



        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        public const int WM_DRAWCLIPBOARD = 0x0308;

        private void Form1_Load(object sender, EventArgs e)
        {
            SetClipboardViewer(this.Handle);
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg != WM_DRAWCLIPBOARD)
                return;

            string veri = Clipboard.GetText();
            //Code To handle Clipboard change event
            if (veri.ToLower().Contains("youtube.com/watch"))
            {
                string yeniveri = veri.Replace("https://", "");
                //label1.Text = yeniveri;
                database.add(veri);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            int kont = database.kontrol();

            if (kont == 1)
            { 

            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.

                backgroundWorker1.RunWorkerAsync();
                button1.Text = "STOP";
                button1.BackColor = Color.Red;
                this.BackColor = Color.Red;

                }
            else
            {
                backgroundWorker1.CancelAsync();
                button1.Text = "Start";
                button1.BackColor = Color.Green;
                this.BackColor = Color.Green;
            }

            }


        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                String komut = "yt.exe";
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/C " + komut;

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = true;
                process.Start();




                while (!process.HasExited)
                {
                    q += process.StandardOutput.ReadToEnd();
                 
                }


            }
            catch (Exception ex)
            {

                q += "error";

            }

        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

           
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            int kont = database.kontrol();

            if (kont == 1)
            {

                if (backgroundWorker1.IsBusy != true)
                {
                    // Start the asynchronous operation.

                    backgroundWorker1.RunWorkerAsync();
                    button1.Text = "STOP";
                    button1.BackColor = Color.Red;

                }
                else
                {
                    backgroundWorker1.CancelAsync();
                    button1.Text = "Start";
                    button1.BackColor = Color.Green;
                    this.BackColor = Color.Green;
                }

            }
            else
            {
           
                button1.Text = "Start";
                button1.BackColor = Color.Green;
                this.BackColor = Color.Green;
            }


        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
               // notifyIcon1.ShowBalloonTip(1000);
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }
    }




}
