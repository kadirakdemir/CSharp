using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.FaaliyetRaporService;
using FaaliyetRaporu.Service.KullaniciGirisCikisTarihiService;
using FaaliyetRaporu.Service.KullaniciService;
using FaaliyetRaporu.Service.RolService;
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
    public partial class Ayarlar : Form
    {
        #region Dependency

        private readonly IRolService _rolService;
        private readonly IFaaliyetRaporService _faaliyetRaporService;
        private readonly IKullaniciService _kullaniciService;
        private readonly IKullaniciGirisCikisTarihiService _kullaniciGirisCikisTarihiService;

        private readonly Kullanici _kullanici;
        #endregion

        KullaniciAyarlar _ka;
        GenelAyarlar _ga;
        KullaniciEkle _ke;

        public Ayarlar(IRolService rolService, IFaaliyetRaporService faaliyetRaporService, IKullaniciService kullaniciService, IKullaniciGirisCikisTarihiService kullaniciGirisCikisTarihiService, Kullanici kullanici)
        {
            InitializeComponent();
            _rolService = rolService;
            _faaliyetRaporService = faaliyetRaporService;
            _kullaniciService = kullaniciService;
            _kullaniciGirisCikisTarihiService = kullaniciGirisCikisTarihiService;
            _kullanici = kullanici;
            _ga = new GenelAyarlar();
            _ga.TopLevel = false;
            if (_ga != null)
            {
                panel5.Controls.Add(_ga);
                _ga.Show();
                _ga.Dock = DockStyle.Fill;
                _ga.BringToFront();                
            }
            if (_kullanici.RolID==3)
            {
                button4.Visible = false;
            }
            //panel6.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            _ka = new KullaniciAyarlar(_faaliyetRaporService, _kullaniciService, _kullaniciGirisCikisTarihiService, _kullanici);
            _ka.TopLevel = false;
            if (_ka != null)
            {
                panel5.Controls.Add(_ka);
                _ka.Show();
                _ka.Dock = DockStyle.Fill;
                _ka.BringToFront();
                button2.BackColor = Color.FromArgb(0, 25, 51);
                button2.ForeColor = Color.White;
                button1.BackColor = Color.WhiteSmoke;
                button1.ForeColor = Color.FromArgb(0, 25, 51);
                button4.BackColor = Color.WhiteSmoke;
                button4.ForeColor = Color.FromArgb(0, 25, 51);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel5.Controls.Clear();
            _ga = new GenelAyarlar();
            _ga.TopLevel = false;
            if (_ga != null)
            {
                panel5.Controls.Add(_ga);
                _ga.Show();
                _ga.Dock = DockStyle.Fill;
                _ga.BringToFront();
                button1.BackColor = Color.FromArgb(0, 25, 51);
                button1.ForeColor = Color.White;
                button2.BackColor = Color.WhiteSmoke;
                button2.ForeColor = Color.FromArgb(0, 25, 51);
                button4.BackColor = Color.WhiteSmoke;
                button4.ForeColor = Color.FromArgb(0, 25, 51);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _ke = new KullaniciEkle(_rolService, _kullanici, _kullaniciService,_faaliyetRaporService);
            _ke.TopLevel = false;
            if (_ke != null)
            {
                panel5.Controls.Add(_ke);
                _ke.Show();
                _ke.Dock = DockStyle.Fill;
                _ke.BringToFront();
                button4.BackColor = Color.FromArgb(0, 25, 51);
                button4.ForeColor = Color.White;
                button1.BackColor = Color.WhiteSmoke;
                button1.ForeColor = Color.FromArgb(0, 25, 51);
                button2.BackColor = Color.WhiteSmoke;
                button2.ForeColor = Color.FromArgb(0, 25, 51);
            }
        }
    }
}
