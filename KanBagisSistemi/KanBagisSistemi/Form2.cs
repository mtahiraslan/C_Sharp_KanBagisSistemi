using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace KanBagisSistemi
{
    public partial class Form2 : Form
    {
        MySqlDataAdapter mda;
        MySqlConnectionStringBuilder build=new MySqlConnectionStringBuilder();
        MySqlConnection baglanti;
        char cinsiyet;

        string adi, kan, sehir, siklik, adres, kilo, yas, telefon;

        public Form2()
        {
            InitializeComponent();
            build.Server = "localhost";
            build.UserID = "root";
            build.Database = "kanbagissistemi";
            build.Password = "12seven..";
            baglanti = new MySqlConnection(build.ToString());
            reset();
            
        }
        public void reset()
        {

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox9.Clear();
            richTextBox1.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;

            richTextBox1.BackColor = Color.White;
            textBox1.BackColor = Color.White;
            textBox2.BackColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox9.BackColor = Color.White;


        }
        public void initData()
        {

            adi = textBox9.Text;

            telefon = textBox3.Text;
            kilo = textBox2.Text;
            yas = textBox1.Text;


            cinsiyet = radioButton1.Checked ? 'M' : 'F';
            kan = comboBox1.Text;

            sehir = comboBox3.Text;
            siklik = comboBox2.Text;
            adres = richTextBox1.Text;

        }
        public int check()
        {
            int flag = 0;

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.BackColor = Color.Red;
                flag = 1;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.BackColor = Color.Red;
                flag = 1;
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.BackColor = Color.Red;
                flag = 1;
            }
            if (string.IsNullOrEmpty(textBox9.Text))
            {
                textBox9.BackColor = Color.Red;
                flag = 1;
            }
            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                richTextBox1.BackColor = Color.Red;
                flag = 1;
            }
            if (string.IsNullOrEmpty(comboBox1.Text))
            {

                flag = 1;
            }
            if (string.IsNullOrEmpty(comboBox2.Text))
            {

                flag = 1;
            }
            if (string.IsNullOrEmpty(comboBox3.Text))
            {

                flag = 1;
            }
            return flag;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check() == 0)
            {
                initData();

                baglanti.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO kayitlar(Adi,Tel_No,Yas,Cinsiyet,Kan_Grubu,Kilo,Sehir,Ne_siklikla,Adres) VALUES ('" + adi + "','" + telefon + "','" + yas + "','" + cinsiyet + "','" + kan + "','" + kilo + "','" + sehir + "','" + siklik + "','" + adres + "')", baglanti);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tamam");
                    reset();
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Telefon Numarası Zaten Var.!!");
                        textBox3.Focus();
                    }
                }

                baglanti.Close();
            }
            else MessageBox.Show("Lütfen Tüm Alanları Doldurunuz.!!");


           }

        private void numberonly(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void alpabetonly(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlDataAdapter mda = new MySqlDataAdapter(@"SELECT    *   FROM   Kayitlar  WHERE Kan_Grubu ='" + comboBox4.Text + "' AND Sehir='" + comboBox5.Text + "';", baglanti);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item[5].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item[6].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item[7].ToString();
                dataGridView1.Rows[n].Cells[8].Value = item[8].ToString();
            }
            label14.Text = dataGridView1.RowCount.ToString() + " Sonuçlar Gösteriliyor " + comboBox5.Text + " , " + comboBox4.Text + " Kan Grubu";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox8.Text))
            {
                baglanti.Open();
                MySqlCommand check = new MySqlCommand(@"SELECT COUNT(*)   FROM   Kayitlar  WHERE Tel_No ='" + textBox8.Text + "';", baglanti);
                check.ExecuteNonQuery();


                if (Convert.ToInt32(check.ExecuteScalar()) > 0)
                {
                    label27.Text = textBox8.Text + " İçin Kayıt Bulundu..";
                    label27.Visible = true;
                    label28.Visible = true;
                    button6.Visible = true;
                    button7.Visible = true;

                }

                else MessageBox.Show("Kayıt Bulunamadı.!!");
                baglanti.Close();

            }
            else MessageBox.Show("Boş");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mda = new MySqlDataAdapter("select * from Kayitlar", baglanti);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            dataGridView1.Rows.Clear();
            label14.Text = "";
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item[5].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item[6].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item[7].ToString();
                dataGridView1.Rows[n].Cells[8].Value = item[8].ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                MySqlCommand del = new MySqlCommand(@"DELETE FROM Kayitlar
                WHERE Tel_No ='" + textBox8.Text + "';", baglanti);
                del.ExecuteNonQuery();
                MessageBox.Show("Kayıt Silindi.!!");
                baglanti.Close();

            }
            catch (SystemException ex)
            {
                MessageBox.Show(string.Format("Bir Hata Oluştu: {0}", ex.Message));
            }

            label27.Text = "";
            label28.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox8.Clear();
            label27.Text = "";
            label28.Visible = false;
            button6.Visible = false;
            button7.Visible = false;


            MessageBox.Show("Kaydı Silmemeyi Seçtiniz..");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox7.Text))
            {
                mda = new MySqlDataAdapter(@"SELECT *   FROM   Kayitlar  WHERE Tel_No ='" + textBox7.Text + "';", baglanti);
                DataTable dt = new DataTable();
                mda.Fill(dt);

                if (Convert.ToInt32(dt.Rows.Count) > 0)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        textBox5.Enabled = true;
                        textBox4.Enabled = true;
                        richTextBox2.Enabled = true;
                        textBox10.Enabled = true;
                        radioButton3.Enabled = true;
                        radioButton4.Enabled = true;
                        comboBox6.Enabled = true;
                        comboBox7.Enabled = true;
                        comboBox8.Enabled = true;
                        button4.Enabled = true;

                        textBox5.Text = item[2].ToString(); // Yaş
                        textBox4.Text = item[5].ToString(); // Kilo
                        textBox6.Text = item[1].ToString(); // Numara
                        textBox10.Text = item[0].ToString(); // Ad
                        richTextBox2.Text = item[8].ToString(); // Adres

                        if (item[3].ToString() == "M")  // Cinsiyet
                        {
                            radioButton4.Checked = true;
                        }
                        else radioButton3.Checked = true;

                        comboBox7.SelectedItem = item[4].ToString();    // Grup
                        comboBox6.SelectedItem = item[7].ToString();    // Sıklık
                        comboBox8.SelectedItem = item[6].ToString();    // Şehir
                    }
                }

                else MessageBox.Show("Kayıt Bulunamadı.!!");
                baglanti.Close();

            }
            else MessageBox.Show("Boş");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cinsiyet = radioButton4.Checked ? 'M' : 'F';
            baglanti.Open();
            MySqlCommand update = new MySqlCommand(@"UPDATE Kayitlar SET Ad ='" + textBox10.Text + "', Yas ='" + textBox5.Text + "', Cinsiyet ='" + cinsiyet + "', Kan_Grubu ='"+ comboBox7.Text + "', Kilo ='" + textBox4.Text + "', Sehir ='" + comboBox8.Text + "', Ne_siklikla ='" + comboBox6.Text + "', Adres ='" + richTextBox2.Text + "'WHERE Tel_No ='" + textBox6.Text + "';", baglanti);
            update.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncellendi.!");




            textBox5.Clear();
            textBox4.Clear();
            textBox6.Clear();
            textBox10.Clear();
            richTextBox2.Clear();
            comboBox7.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
            comboBox8.SelectedIndex = -1;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
        }
      }
    

