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
    class database
    {
        SQLiteConnection conn;
        SQLiteCommand cmd;
        SQLiteDataReader dr;

        public static void Create_db()
        {
            string path = "deneme.db";
            string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";

            if (!System.IO.File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
                using (var sqlite = new SQLiteConnection(@"Data Source="+ path))
                {
                    sqlite.Open();
                    string sql = "CREATE TABLE data (id INTEGER, veri TEXT,  tarih TEXT, stat TEXT,  cont TEXT UNIQUE ,  PRIMARY KEY(id AUTOINCREMENT))";
                    SQLiteCommand command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();

                    sql = "CREATE TABLE kalite (id INTEGER, kalite TEXT , PRIMARY KEY(id AUTOINCREMENT))";
                    command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();
                    sqlite.Close();

                }
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "INSERT INTO kalite(kalite) VALUES(@kalite)";

                cmd.Parameters.AddWithValue("@kalite", "480p");


                cmd.ExecuteNonQuery();

                con.Close();

            }

        }

        public static void kalitep(string kalite)
        {

            string path = "deneme.db";
            string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";


            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd = new SQLiteCommand(con);
            string sql = "UPDATE kalite set kalite='"+kalite+"'  where id = 1 ";

            cmd.CommandText = sql;
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Ok.");

        }
        public static void add(string gelenveri)
        {
            try
            {
                string path = "deneme.db";
                string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";


                string veri = gelenveri;
                DateTime bugun = DateTime.Now;

                string tarih = DateTime.Now.Date.ToString("dd.MM.yyyy");
                string stat = "0";
                string cont = veri +" "+ tarih;
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);




                //"server=localhost;username=root;password=;database=follow";
                //string sql2 = "CREATE TABLE passwords (id INTEGER, info TEXT , username TEXT, password TEXT,  PRIMARY KEY(id AUTOINCREMENT))";
                cmd.CommandText = "INSERT INTO data(veri,tarih,stat,cont) VALUES(@veri,@tarih,@stat,@cont)";

                cmd.Parameters.AddWithValue("@veri", veri);
                cmd.Parameters.AddWithValue("@tarih", tarih);
                cmd.Parameters.AddWithValue("@stat", stat);
                cmd.Parameters.AddWithValue("@cont", cont);

                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception e)
            {


            }

        }

        public static int kontrol()
        {
            int don = 0;
            SQLiteConnection conn;
            SQLiteCommand cmd;
            SQLiteDataReader dr;


            string path = "deneme.db";
            string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";
            string stm = "select * FROM data where stat LIKE '0'";
            var con = new SQLiteConnection(cs);
            con.Open();
            cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                don = 1;
                //  dataGridView1.Rows.Insert(0, dr.GetValue(1).ToString());
                // public static String connectionString = "server=localhost;username=root;password=;database=follow


                // class2.Decrypt = textBox2.Text;
                //  textBox3.Text = class2.Decrypt;
            }

            con.Close();

            return don;
        }



    }
}
