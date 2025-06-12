using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AracKiralamaSistemi;

namespace arackiralamantp
{
    public partial class Form2 : Form
    {
        List<Car> arabalar = new List<Car>();
        List<Customer> musteriler = new List<Customer>();
        Customer aktifMusteri;
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Kiralabutton_Click(object sender, EventArgs e)
        {


      

            if (string.IsNullOrWhiteSpace(txtAd.Text) || string.IsNullOrWhiteSpace(txtSoyad.Text) || string.IsNullOrWhiteSpace(txtEhliyet.Text) || string.IsNullOrWhiteSpace(txtTelefon.Text))
            {
                MessageBox.Show("Ad, Soyad ve Ehliyet No alanları boş bırakılamaz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (nudGun.Value <= 0)
            {
                MessageBox.Show("Kiralama süresi 0 olamaz. Lütfen geçerli bir gün sayısı girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (dataGridView1.CurrentRow != null)
            {
                var secilenArac = (Car)dataGridView1.CurrentRow.DataBoundItem;

               
                aktifMusteri = musteriler.FirstOrDefault(m => m.EhliyetNo == txtEhliyet.Text);

                if (aktifMusteri == null)
                {
                    
                    aktifMusteri = new Customer(1, txtAd.Text, txtSoyad.Text, txtTelefon.Text, txtEmail.Text, txtEhliyet.Text);
                    musteriler.Add(aktifMusteri);
                }
                else
                {
                   
                    aktifMusteri.Ad = txtAd.Text;
                    aktifMusteri.Soyad = txtSoyad.Text;
                    aktifMusteri.Telefon = txtTelefon.Text;
                    aktifMusteri.Email = txtEmail.Text;
                }


                bool sonuc = aktifMusteri.Kirala(secilenArac, (int)nudGun.Value);
                
                if (sonuc)
                {
                    if (!musteriler.Any(m => m.EhliyetNo == aktifMusteri.EhliyetNo))
                        musteriler.Add(aktifMusteri);
                    else

                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = musteriler;
                }
                MessageBox.Show(sonuc ? "Kiralama başarılı." : "Araç zaten kiralanmış.");
                txtAd.Text = null;
                txtSoyad.Text = null;
                txtTelefon.Text = null;
                txtEmail.Text = null;
                txtEhliyet.Text = null;
                nudGun.Value = 0;
                dataGridView1.Refresh();
                dataGridView2.Refresh();

                

            }

        }

        private void Iadebutton_Click(object sender, EventArgs e)
        {
            if (aktifMusteri?.AktifAraba != null)
            {
                aktifMusteri.IadeEt(aktifMusteri.AktifAraba);
                MessageBox.Show("Araç iade edildi.");
                dataGridView1.Refresh();
                dataGridView2.Refresh();
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;
            string dosyaYolu = "araclar.csv";

            if (File.Exists(dosyaYolu))
            {
                using (var reader = new StreamReader(dosyaYolu))
                {
                    bool ilkSatir = true;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        
                        if (ilkSatir)
                        {
                            ilkSatir = false;
                            continue;
                        }

                        var values = line.Split(',');

                        int id = int.Parse(values[0]);
                        string marka = values[1];
                        string model = values[2];
                        int yil = int.Parse(values[3]);
                        string plaka = values[4];
                        double ucret = double.Parse(values[5]);

                        arabalar.Add(new Car(id, marka, model, yil, plaka, ucret));
                    }
                }

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = arabalar;
            }
            else
            {
                MessageBox.Show("araclar.csv dosyası bulunamadı.");
            }

        }
    }
}
