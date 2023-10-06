using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _2022_2023_csharp_uyg
{
    public partial class Form1 : Form
    {
        // sqlconnection programımızı vt ye bağlar
        SqlConnection conn = new SqlConnection();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vtislemyap("insert Tbl_Ogrenci (OgrNo,Adi,Soyadi,yas)\r\nvalues ('" + txt_Ogrno.Text + "','" + txt_Adi.Text + "','" + txt_Soyadi.Text + "'," + txt_yas.Text + ")","Kayıt");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // .connectionString; vt nin yolunu tanımlamak için kullanılır
            conn.ConnectionString = "server=yourservername; database=_2022_2023_1_donem; uid=userid; pwd=yourpassword;";
            verigetir();
        }

        public void verigetir()
        {
            // vt den verileri getirme işlemini yapamaktadır.
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Ogrenci", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vtislemyap("update tbl_ogrenci set ogrno='"+txt_Ogrno.Text+"' ,adi='" + txt_Adi.Text + "',soyadi='"+txt_Soyadi.Text+"', yas="+txt_yas.Text+" where ID='" + label5.Text + "'","Güncelleme");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            vtislemyap("delete from tbl_ogrenci where ID='" + label5.Text + "'", "Silme");
        }

        public void vtislemyap(string sqlcumlesi,string mesaj)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sqlcumlesi;
            cmd.Connection = conn;

            int sonuc = cmd.ExecuteNonQuery();
            if (sonuc > 0)
            {
                MessageBox.Show(mesaj+" işlemi başarılıdır.", "Bilgi");
                verigetir();
                temizle();
            }
            conn.Close();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            label5.Text = dataGridView1.Rows[index].Cells["ID"].Value.ToString();

            txt_Ogrno.Text= dataGridView1.Rows[index].Cells["OgrNo"].Value.ToString();

            txt_Adi.Text= dataGridView1.Rows[index].Cells["adi"].Value.ToString();

            txt_Soyadi.Text= dataGridView1.Rows[index].Cells["Soyadi"].Value.ToString();

            txt_yas.Text= dataGridView1.Rows[index].Cells["yas"].Value.ToString();

        }

        public void temizle()
        {
            label5.Text = "";
            txt_Ogrno.Text = "";
            txt_Adi.Text = "";
            txt_Soyadi.Text = "";
            txt_yas.Text = "";
        }
    }
}
