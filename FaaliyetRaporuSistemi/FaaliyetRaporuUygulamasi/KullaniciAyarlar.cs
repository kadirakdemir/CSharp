using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.FaaliyetRaporService;
using FaaliyetRaporu.Service.KullaniciGirisCikisTarihiService;
using FaaliyetRaporu.Service.KullaniciService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaliyetRaporuUygulamasi
{
    public partial class KullaniciAyarlar : Form
    {
        #region DEpendency

        private readonly IFaaliyetRaporService _faaliyetRaporService;
        private readonly IKullaniciService _kullaniciService;
        private readonly IKullaniciGirisCikisTarihiService _kullaniciGirisCikisTarihiService;

        private readonly Kullanici _kullanici;
        #endregion

        GirisCikisTarih _girisCikisTarih;
       
        public KullaniciAyarlar(IFaaliyetRaporService faaliyetRaporService, IKullaniciService kullaniciService, IKullaniciGirisCikisTarihiService kullaniciGirisCikisTarihiService,Kullanici kullanici)
        {
            InitializeComponent();
            _faaliyetRaporService = faaliyetRaporService;
            _kullaniciService = kullaniciService;
            _kullaniciGirisCikisTarihiService = kullaniciGirisCikisTarihiService;
            _kullanici = kullanici;
            label1.Text = _kullanici.Adi + " " + kullanici.Soyadi;
            adTextBox.Text = _kullanici.Adi;
            soyadTextBox.Text = _kullanici.Soyadi;
            emailTextBox.Text = _kullanici.Email;
            telefonTextBox.Text = _kullanici.CepTelefonNo;
            label15.Text = _kullaniciGirisCikisTarihiService.SonGirisTarihi(_kullanici.ID).GirisTarihi.ToString();
            timer1.Enabled = true;
            if (_kullaniciGirisCikisTarihiService.SonGirisTarihi(_kullanici.ID).IsActive==true)
            {
                label14.Text = "Aktif";
            }
            else
            {
                label14.Text = "Pasif";
            }
         
            label18.Text = _faaliyetRaporService.Get(x => x.KullaniciId == _kullanici.ID).Count().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tableLayoutPanel4.Enabled = true;
            tableLayoutPanel5.Enabled = true;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan zaman = DateTime.Now - _kullaniciGirisCikisTarihiService.SonGirisTarihi(_kullanici.ID).GirisTarihi;
            label17.Text =zaman.Days+" gün       "+ zaman.Hours.ToString()+":"+zaman.Minutes.ToString()+":"+zaman.Seconds.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                _kullanici.Adi = adTextBox.Text;
                _kullanici.Soyadi = soyadTextBox.Text;
                _kullanici.Email = emailTextBox.Text;
                _kullanici.CepTelefonNo = telefonTextBox.Text;
                _kullaniciService.Guncelle(_kullanici);
                MessageBox.Show("Başarıyla güncellendi.");
                label19.Text = "Başarıyla güncellendi.";
                label19.ForeColor = Color.White;
                tableLayoutPanel2.BackColor = Color.DeepSkyBlue;

            }
            catch (Exception)
            {
                MessageBox.Show("Güncelleme işlemi yaparken hata oluştu!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (_kullanici.Sifre==eskiSifreTextBox.Text)
                {
                    if (yeniSifreTextBox.Text==sifreTekrarTextBox.Text)
                    {
                        _kullanici.Sifre = yeniSifreTextBox.Text;
                        _kullaniciService.Guncelle(_kullanici);
                    }                    
                }
                else
                {
                    MessageBox.Show("Eski şifre hatalı!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Güncelleme işlemi yaparken hata oluştu!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (_girisCikisTarih==null)
            {
                _girisCikisTarih = new GirisCikisTarih(_kullanici.ID,_kullaniciService, _kullaniciGirisCikisTarihiService);
                _girisCikisTarih.Show();
                _girisCikisTarih.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void button9_MouseDown(object sender, MouseEventArgs e)
        {
            eskiSifreTextBox.PasswordChar = '\0';
        }

        private void button9_MouseUp(object sender, MouseEventArgs e)
        {
            eskiSifreTextBox.PasswordChar = '*';
        }

        private void button8_MouseUp(object sender, MouseEventArgs e)
        {
            yeniSifreTextBox.PasswordChar = '*';
        }

        private void button8_MouseDown(object sender, MouseEventArgs e)
        {
            yeniSifreTextBox.PasswordChar = '\0';
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)
        {
            sifreTekrarTextBox.PasswordChar = '\0';
        }

        private void button7_MouseUp(object sender, MouseEventArgs e)
        {
            sifreTekrarTextBox.PasswordChar = '*';
        }
    }
}
