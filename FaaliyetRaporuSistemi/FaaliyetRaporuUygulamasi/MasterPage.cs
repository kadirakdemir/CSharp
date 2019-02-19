using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.AciklamaService;
using FaaliyetRaporu.Service.DurumService;
using FaaliyetRaporu.Service.FaaliyetRaporService;
using FaaliyetRaporu.Service.FaaliyetTuruService;
using FaaliyetRaporu.Service.IslemSonucuService;
using FaaliyetRaporu.Service.KodService;
using FaaliyetRaporu.Service.KonuService;
using FaaliyetRaporu.Service.KullaniciAdresService;
using FaaliyetRaporu.Service.KullaniciGirisCikisTarihiService;
using FaaliyetRaporu.Service.KullaniciService;
using FaaliyetRaporu.Service.RolService;
using FaaliyetRaporu.Service.SonucAciklamaService;
using FaaliyetRaporu.Service.TalepService;
using FaaliyetRaporu.Service.YonlendirmeService;
using FaaliyetRaporuWinForm;
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
    public partial class MasterPage : Form
    {
        #region Dependency
        private readonly IRolService _rolService;
        private readonly IKodService _kodService;
        private readonly IKonuService _konuService;
        private readonly ITalepService _talepService;
        private readonly IDurumService _durumService;
        private readonly IKullaniciService _kullaniciService;
        private readonly IAciklamaService _aciklamaService;
        private readonly IIslemSonucuService _islemSonucuService;
        private readonly IYonlendirmeService _yonlendirmeService;
        private readonly IFaaliyetTuruService _faaliyetTuruService;
        private readonly IFaaliyetRaporService _faaliyetRaporService;
        private readonly ISonucAciklamaService _sonucAciklamaService;
        private readonly IKullaniciAdresService _kullaniciAdresService;
        private readonly IKullaniciGirisCikisTarihiService _kullaniciGirisCikisTarihiService;

        Giris _giris;
        Kullanici _kullanici;

        #endregion

       
        Faaliyet _faaliyet;
        Ayarlar _ayarlar;
        Analiz _analiz;
        Bul _bul;

        private bool mouseDown;
        private Point lastLocation;
        private static bool panelac = false;


        static byte sayac;
        int sayac2;
        static byte gecici2;                     // panel çıkış için
        byte[] oncelik = new byte[5];            // panelden açık formu kapattıktan sonra ondan önceki açık formu getirmek için

        public static string sure;
        public static string saat;
        public static string dakika;
        public static string saniye;

        public MasterPage(Giris giris, Kullanici kullanici, IRolService rolService, IKodService kodService, IKonuService konuService, ITalepService talepService, 
            IDurumService durumService, IKullaniciService kullaniciService, IAciklamaService aciklamaService, IIslemSonucuService islemSonucuService, 
            IYonlendirmeService yonlendirmeService, IFaaliyetTuruService faaliyetTuruService, IFaaliyetRaporService faaliyetRaporService, 
            ISonucAciklamaService sonucAciklamaService, IKullaniciAdresService kullaniciAdresService, IKullaniciGirisCikisTarihiService kullaniciGirisCikisTarihiService)
        {
            InitializeComponent();

            _giris = giris;
            _kullanici = kullanici;

            _rolService = rolService;
            _kodService = kodService;
            _konuService = konuService;
            _talepService = talepService;
            _durumService = durumService;
            _kullaniciService = kullaniciService;
            _aciklamaService = aciklamaService;
            _islemSonucuService = islemSonucuService;
            _yonlendirmeService = yonlendirmeService;
            _faaliyetTuruService = faaliyetTuruService;
            _faaliyetRaporService = faaliyetRaporService;
            _sonucAciklamaService = sonucAciklamaService;
            _kullaniciAdresService = kullaniciAdresService;
            _kullaniciGirisCikisTarihiService = kullaniciGirisCikisTarihiService;

            button11.Text =_kullanici.Adi + "  " + _kullanici.Soyadi;
            timer1.Enabled = true;
            sure = DateTime.Now.ToShortTimeString();
             
        }

        public MasterPage()
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {            
            if (_kullaniciService.Bul(_kullanici.ID)!=null)
            {
                var kullaniciCikisTarihi = _kullaniciGirisCikisTarihiService.SonGirisTarihi(_kullanici.ID);
                kullaniciCikisTarihi.CikisTarihi = DateTime.Now;
                kullaniciCikisTarihi.IsActive = false;
                _kullanici.IsActive = false;
                _kullaniciGirisCikisTarihiService.Guncelle(kullaniciCikisTarihi);
            }
           
            _giris.Dispose();
            _giris.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;  
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;               
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (oncelik[0] == 1 & _faaliyet != null)
                {
                    panel13.Controls.Remove(_faaliyet);
                    _faaliyet.Dispose();
                    _faaliyet = null;
                    sayac--;
                    button2.BackColor = Color.FromArgb(0, 25, 51);
                }
                if (oncelik[0] == 2 & _analiz != null)
                {
                    panel13.Controls.Remove(_analiz);
                    _analiz.Dispose();
                    _analiz = null;
                    sayac--;
                    button7.BackColor = Color.FromArgb(0, 25, 51);
                }
                if (oncelik[0] == 3 & _bul != null)
                {
                    panel13.Controls.Remove(_bul);
                    _bul.Dispose();
                    _bul = null;
                    sayac--;
                    button8.BackColor = Color.FromArgb(0, 25, 51);
                }
                if (oncelik[0] == 6 & _ayarlar != null)
                {
                    panel13.Controls.Remove(_ayarlar);
                    _ayarlar.Dispose();
                    _ayarlar = null;
                    sayac--;
                    button3.BackColor = Color.FromArgb(0, 25, 51);
                }
                if (gecici2 != 0)
                {
                    gecici2--;
                    oncelik[0] = oncelik[1];
                    oncelik[1] = oncelik[2];
                    oncelik[2] = oncelik[3];
                    oncelik[3] = oncelik[4];
                    oncelik[4] = 0;
                }
                if(gecici2==0)
                {
                    oncelik[0] = 0;
                    label7.Text = "";
                    panel10.Visible = false;
                }
                if (oncelik[0] == 1)
                {
                    panel13.Controls.Add(_faaliyet);
                    label7.Text = _faaliyet.Text;
                    button2.BackColor = Color.FromArgb(223, 5, 5);
                }
                if (oncelik[0] == 2)
                {
                    panel13.Controls.Add(_analiz);
                    label7.Text = _analiz.Text;
                    button7.BackColor = Color.FromArgb(223, 5, 5);
                }
                if (oncelik[0] == 3)
                {
                    panel13.Controls.Add(_bul);
                    label7.Text = _bul.Text;
                    button8.BackColor = Color.FromArgb(223, 5, 5);
                }
                if (oncelik[0] == 6)
                {
                    panel13.Controls.Add(_ayarlar);
                    label7.Text = _ayarlar.Text;
                    button3.BackColor = Color.FromArgb(223, 5, 5);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Kapatma Hatası oldu.");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (panelac == false)
            {
                //panel1.Visible = false;
                pictureBox1.Visible = false;
                panel1.Width = 54;
                panelac = true;
            }
            else
            {
                pictureBox1.Visible = true;
                panel1.Width = 230;
                panelac = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (faaliyet == null || faaliyet.Disposing) //Form3 kapalı ve gizli değil ise
            //{
            //    faaliyet = new Faaliyet();
            //    faaliyet.MdiParent = this;
            //    faaliyet.Show();
            //}
            //else
            //{
            //    faaliyet.Activate(); //Form2 bir yerlerde açık ise aktif et
            //}
            if (_analiz != null)
            {
                panel13.Controls.Remove(_analiz);               
            }
            if (_bul != null)
            {
                panel13.Controls.Remove(_bul);
            }
            if (_ayarlar != null)
            {
                panel13.Controls.Remove(_ayarlar);
            }

            if (_faaliyet != null)
            {
                panel10.Visible = true;               
                panel13.Controls.Add(_faaliyet);
                button2.BackColor = Color.FromArgb(223, 5, 5);
                button3.BackColor = Color.FromArgb(0, 25, 51);
                button7.BackColor = Color.FromArgb(0, 25, 51);
                button8.BackColor = Color.FromArgb(0, 25, 51);
                button9.BackColor = Color.FromArgb(0, 25, 51);
                sayac2 = sayac - 1;
                while (sayac2 != 0)
                {
                    oncelik[sayac2] = oncelik[sayac2 - 1];
                    sayac2--;
                }
                oncelik[0] = 1;
                button1.Visible = true;
            }

            if (_faaliyet == null || _faaliyet.Disposing)
            {

                _faaliyet = new Faaliyet(_kullanici, _rolService, _kodService, _konuService, _talepService, _durumService, _kullaniciService, _aciklamaService,
                   _islemSonucuService, _yonlendirmeService, _faaliyetTuruService, _faaliyetRaporService, _sonucAciklamaService, _kullaniciAdresService);

                _faaliyet.TopLevel = false;
                panel13.Controls.Add(_faaliyet);
                panel10.Visible = true;
                _faaliyet.Show();
                _faaliyet.Dock = DockStyle.Fill;
                _faaliyet.BringToFront();
                label7.Text = _faaliyet.Text;
                button2.BackColor = Color.FromArgb(223, 5, 5);
                button3.BackColor = Color.FromArgb(0, 25, 51);
                button7.BackColor = Color.FromArgb(0, 25, 51);
                button8.BackColor = Color.FromArgb(0, 25, 51);
                button9.BackColor = Color.FromArgb(0, 25, 51);

                sayac++;
                sayac2 = sayac;
                while (sayac2 != -1)
                {
                    oncelik[sayac2 + 1] = oncelik[sayac2];
                    sayac2--;
                }
                oncelik[0] = 1;

                gecici2++;
                button1.Visible = true;
            }
            else
            {
                _faaliyet.Activate();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (_faaliyet != null)
            {
                panel13.Controls.Remove(_faaliyet);
            }
            if (_bul != null)
            {
                panel13.Controls.Remove(_bul);
            }
            if (_ayarlar != null)
            {
                panel13.Controls.Remove(_ayarlar);
            }

            if (_analiz != null)
            {
                panel10.Visible = true;
                panel13.Controls.Add(_analiz);
                button7.BackColor = Color.FromArgb(223, 5, 5);
                button2.BackColor = Color.FromArgb(0, 25, 51);
                button3.BackColor = Color.FromArgb(0, 25, 51);
                button8.BackColor = Color.FromArgb(0, 25, 51);
                button9.BackColor = Color.FromArgb(0, 25, 51);
                sayac2 = sayac - 1;
                while (sayac2 != 0)
                {
                    oncelik[sayac2] = oncelik[sayac2 - 1];
                    sayac2--;
                }
                oncelik[0] = 2;
                button1.Visible = true;
            }
            else if (_analiz == null || _analiz.Disposing)
            {
                _analiz = new Analiz();
                _analiz.TopLevel = false;
                panel13.Controls.Add(_analiz);
                panel10.Visible = true;
                _analiz.Show();
                _analiz.Dock = DockStyle.Fill;
                _analiz.BringToFront();
                label7.Text = "Analiz";
                button7.BackColor = Color.FromArgb(223, 5, 5);
                button2.BackColor = Color.FromArgb(0, 25, 51);
                button3.BackColor = Color.FromArgb(0, 25, 51);
                button8.BackColor = Color.FromArgb(0, 25, 51);
                button9.BackColor = Color.FromArgb(0, 25, 51);

                sayac++;
                sayac2 = sayac;
                while (sayac2 != -1)
                {
                    oncelik[sayac2 + 1] = oncelik[sayac2];
                    sayac2--;
                }
                oncelik[0] = 2;
                gecici2++;
                button1.Visible = true;
            }
            else
            {
                _analiz.Activate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_faaliyet != null)
            {
                panel13.Controls.Remove(_faaliyet);
            }
            if (_analiz != null)
            {
                panel13.Controls.Remove(_analiz);
            }
            if (_bul != null)
            {
                panel13.Controls.Remove(_bul);
            }
            if (_ayarlar != null)
            {
                panel10.Visible = true;
                panel13.Controls.Add(_ayarlar);
                button3.BackColor = Color.FromArgb(223, 5, 5);
                button2.BackColor = Color.FromArgb(0, 25, 51);
                button7.BackColor = Color.FromArgb(0, 25, 51);
                button8.BackColor = Color.FromArgb(0, 25, 51);
                button9.BackColor = Color.FromArgb(0, 25, 51);

                sayac2 = sayac - 1;
                while (sayac2 != 0)
                {
                    oncelik[sayac2] = oncelik[sayac2 - 1];
                    sayac2--;
                }
                oncelik[0] = 6;
                button1.Visible = true;
            }

            if (_ayarlar == null || _ayarlar.Disposing)
            {
                _ayarlar = new Ayarlar(_rolService, _faaliyetRaporService, _kullaniciService,_kullaniciGirisCikisTarihiService, _kullanici);
                _ayarlar.TopLevel = false;
                panel13.Controls.Add(_ayarlar);
                panel10.Visible = true;
                _ayarlar.Show();
                _ayarlar.Dock = DockStyle.Fill;
                _ayarlar.BringToFront();
                label7.Text = _ayarlar.Text;
                button3.BackColor = Color.FromArgb(223, 5, 5);
                button2.BackColor = Color.FromArgb(0, 25, 51);
                button7.BackColor = Color.FromArgb(0, 25, 51);
                button8.BackColor = Color.FromArgb(0, 25, 51);
                button9.BackColor = Color.FromArgb(0, 25, 51);

                sayac++;
                sayac2 = sayac;
                while (sayac2 != -1)
                {
                    oncelik[sayac2 + 1] = oncelik[sayac2];
                    sayac2--;
                }
                oncelik[0] = 6;

                gecici2++;
                button1.Visible = true;
            }
            else
            {
                _ayarlar.Activate();
            }
        }

        private void panel3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                if (_faaliyet != null)
                {
                    _faaliyet.WindowState = FormWindowState.Maximized;
                }
                else if (_analiz != null)
                {
                    _analiz.WindowState = FormWindowState.Maximized;
                }
                else if (_bul != null)
                {
                    _bul.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;
                if (_faaliyet != null)
                {
                    _faaliyet.WindowState = FormWindowState.Normal;
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                else if (_analiz != null)
                {
                    _analiz.WindowState = FormWindowState.Normal;
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                else if (_bul != null)
                {
                    _bul.WindowState = FormWindowState.Normal;
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
            }
        }

        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void button11_MouseEnter(object sender, EventArgs e)
        {
            button11.BackColor = Color.FromArgb(0, 25, 51);
        }

        private void button11_MouseLeave(object sender, EventArgs e)
        {
            button11.BackColor = Color.FromArgb(0, 25, 51);
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToShortTimeString();
            
        }
    }
}
