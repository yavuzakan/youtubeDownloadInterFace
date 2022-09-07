using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace youtubeDl
{
    public partial class Form2 : Form
    {
        public string path = "deneme.db";
        public string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";

        public SQLiteConnection conn;
        public SQLiteCommand cmd;
        public SQLiteDataReader dr;

        public Form2()
        {
            InitializeComponent();
            data_show();

        }
        private void data_show()
        {
            var con = new SQLiteConnection(cs);
            con.Open();
     

            string stm = "select * FROM data";
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                dataGridView1.Rows.Insert(0, dr.GetValue(1).ToString(), dr.GetValue(2).ToString(), dr.GetValue(3).ToString());



            }

            con.Close();

            this.dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ReadOnly = true;


        }
    }
}
