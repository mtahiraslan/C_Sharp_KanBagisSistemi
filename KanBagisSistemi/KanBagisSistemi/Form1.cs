using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KanBagisSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login();
        }

        private void login()
        {
            if (textBox1.Text == "admin" && textBox2.Text == "1234")
            {
                Form2 nextForm = new Form2();
                this.Hide();
                nextForm.ShowDialog();
                this.Close();

            }
            else
                MessageBox.Show("Hatalı Girdiniz.!!");
        }

        
        }
    }
