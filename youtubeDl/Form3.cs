using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace youtubeDl
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            comboBox1.Items.Add("240p");
            comboBox1.Items.Add("360p");
            comboBox1.Items.Add("480p");


            comboBox1.SelectedIndex = 2;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            String kalite = comboBox1.SelectedItem.ToString();
            
            database.kalitep(kalite);
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
