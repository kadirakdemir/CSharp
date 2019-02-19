using FaaliyetRaporu.Data.Repositories;
using FaaliyetRaporu.Data.UnitOfWork;
using FaaliyetRaporu.Service.DurumService;
using FaaliyetRaporu.Service.FaaliyetRaporService;
using FaaliyetRaporu.Service.FaaliyetTuruService;
using FaaliyetRaporu.Service.IslemSonucuService;
using FaaliyetRaporu.Service.KodService;
using FaaliyetRaporu.Service.KonuService;
using FaaliyetRaporu.Service.KullaniciAdresService;
using FaaliyetRaporu.Service.KullaniciService;
using FaaliyetRaporu.Service.RolService;
using FaaliyetRaporu.Service.SonucAciklamaService;
using FaaliyetRaporu.Service.TalepService;
using FaaliyetRaporu.Service.YonlendirmeService;
using FaaliyetRaporu.Service.KullaniciGirisCikisTarihiService;
using FaliyetRaporuUygulamasi;
using FaliyetRaporuUygulamasi.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.util;
using System.Windows.Forms;
using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.AciklamaService;

namespace FaaliyetRaporuWinForm
{
    public partial class Giris : Form
    {
        private bool mouseDown;
        private Point lastLocation;

        #region Dependency Injection

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

        #endregion

        public Giris(IRolService rolService, IKodService kodService, IKonuService konuService, ITalepService talepService, IDurumService durumService,
            IKullaniciService kullaniciService, IAciklamaService aciklamaService, IIslemSonucuService islemSonucuService, IYonlendirmeService yonlendirmeService, 
            IFaaliyetTuruService faaliyetTuruService, IFaaliyetRaporService faaliyetRaporService, ISonucAciklamaService sonucAciklamaService, 
            IKullaniciAdresService kullaniciAdresService, IKullaniciGirisCikisTarihiService kullaniciGirisCikisTarihiService)
        {
            InitializeComponent();

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
        }
        public Giris()
        {

        }
        private void girisbtn_Click(object sender, EventArgs e)
        {
            var oturumac = _kullaniciService.OturumAc(kullanicitext.Text, sifretext.Text);

            if (oturumac != null )
            {
                if (oturumac.OnaylandiMi == true)
                {
                    MasterPage faaliyet = new MasterPage(this, oturumac, _rolService, _kodService, _konuService, _talepService, _durumService, _kullaniciService, _aciklamaService,
                  _islemSonucuService, _yonlendirmeService, _faaliyetTuruService, _faaliyetRaporService, _sonucAciklamaService, _kullaniciAdresService, _kullaniciGirisCikisTarihiService);
                    KullaniciGirisCikisTarihi kgct = new KullaniciGirisCikisTarihi();
                    kgct.GirisTarihi = DateTime.Now;
                    kgct.CikisTarihi = DateTime.Now;
                    kgct.KullaniciID = oturumac.ID;
                    kgct.IsActive = true;
                    oturumac.IsActive = true;
                    _kullaniciService.Guncelle(oturumac);
                    _kullaniciGirisCikisTarihiService.Ekle(kgct);
                    faaliyet.Show();
                    this.Hide();
                }
                else
                {
                    label5.Text = "Hesabınız onaylanmamış!";
                }
            }
            else
            {
                label5.Text = "Hatlı Kullanıcı Adı ve/veya Şifre! Lütfen tekrar deneyin.";
                label5.ForeColor = Color.Red;
            }            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void girisbtn_MouseHover(object sender, EventArgs e)
        {
            girisbtn.BackColor = Color.DeepSkyBlue;
        }

        private void girisbtn_MouseLeave(object sender, EventArgs e)
        {
            girisbtn.BackColor = Color.DodgerBlue;
        }

        private void girisbtn_MouseEnter(object sender, EventArgs e)
        {
            girisbtn.BackColor = Color.DeepSkyBlue;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Settings.Default.Adi = kullanicitext.Text;
                Settings.Default.Sifre = sifretext.Text;
                Settings.Default.Save();
            }
            else
            {
                Settings.Default.Adi = null;
                Settings.Default.Sifre = null;
                Settings.Default.Save();
            }
        }

        private void Giris_Load(object sender, EventArgs e)
        {

            kullanicitext.Text = Settings.Default.Adi;
            sifretext.Text = Settings.Default.Sifre;
            if (kullanicitext.Text != "")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            sifretext.PasswordChar = '*';
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            sifretext.PasswordChar = '\0';
        }
    }
}
