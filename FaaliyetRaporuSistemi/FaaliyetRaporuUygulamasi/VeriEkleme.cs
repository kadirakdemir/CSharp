using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.AciklamaService;
using FaaliyetRaporu.Service.DurumService;
using FaaliyetRaporu.Service.FaaliyetRaporService;
using FaaliyetRaporu.Service.FaaliyetTuruService;
using FaaliyetRaporu.Service.IslemSonucuService;
using FaaliyetRaporu.Service.KodService;
using FaaliyetRaporu.Service.KonuService;
using FaaliyetRaporu.Service.KullaniciService;
using FaaliyetRaporu.Service.SonucAciklamaService;
using FaaliyetRaporu.Service.TalepService;
using FaaliyetRaporu.Service.YonlendirmeService;
using FaliyetRaporuUygulamasi.Service;
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
    public partial class VeriEkleme : Form
    {

        #region Dependency Injection
        
        private readonly IKodService _kodService;
        private readonly IKonuService _konuService;
        private readonly ITalepService _talepService;
        private readonly IDurumService _durumService;
        private readonly IAciklamaService _aciklamaService;
        private readonly IIslemSonucuService _islemSonucuService;
        private readonly IYonlendirmeService _yonlendirmeService;
        private readonly IFaaliyetTuruService _faaliyetTuruService;
        private readonly ISonucAciklamaService _sonucAciklamaService;


        #endregion

        #region form hareket

        private bool mouseDown;
        private Point lastLocation;
        private static bool panelac = false;

        #endregion

        #region Entity

        Talep _talep;
        Kod _kod;
        Konu _konu;
        Durum _durum;
        IslemSonucu _islemSonucu;
        Yonlendirme _yonlendirme;
        FaaliyetTuru _faaliyetTuru;
        SonucAciklama _sonucAciklama;
        Aciklamalar _aciklama;

        #endregion  
        
        static int _id;

        public bool denetim = true;
        static byte secim;
        public VeriEkleme(ITalepService talepService)
        {
            InitializeComponent();
            _talepService = talepService;
            label1.Text = "Talep";
            label3.Text = "Talep Ekle";
            secim = 1;
            foreach (var item in _talepService.TumKayitlar())
            {
                adComboBox.Items.Add(item.TalepAdi);
            }
        }
        public VeriEkleme(IKodService kodService )
        {           
            InitializeComponent();
            _kodService = kodService;
            secim = 2;
            foreach (var item in _kodService.TumKayitlar())
            {
                adComboBox.Items.Add(item.KodAdi);
            }
            label1.Text = "Kod";
            label3.Text = "Kod Ekle";
        }
        public VeriEkleme(IKonuService konuService)
        {
            InitializeComponent();
            _konuService = konuService;
            secim = 3;
            foreach (var item in _konuService.TumKayitlar())
            {
                adComboBox.Items.Add(item.KonuAdi);
            }
            label1.Text = "Konu";
            label3.Text = "Konu Ekle";
        }
        public VeriEkleme(IDurumService durumService)
        {
            InitializeComponent();
            _durumService = durumService;
            secim = 4;
            foreach (var item in _durumService.TumKayitlar())
            {
                adComboBox.Items.Add(item.Durumu);
            }
            label1.Text = "Durum";
            label3.Text = "Durum Ekle";
        }
        public VeriEkleme(IIslemSonucuService islemSonucuService)
        {
            InitializeComponent();
            _islemSonucuService = islemSonucuService;
            secim = 5;
            foreach (var item in _islemSonucuService.TumKayitlar())
            {
                adComboBox.Items.Add(item.IslemSonuc);
            }
            label1.Text = "İşlem Sonucu";
            label3.Text = "İşlem Sonuç Ekle";
        }
        public VeriEkleme(IYonlendirmeService yonlendirmeService)
        {
            InitializeComponent();
            _yonlendirmeService = yonlendirmeService;
            secim = 6;
            foreach (var item in _yonlendirmeService.TumKayitlar())
            {
                adComboBox.Items.Add(item.YonlendirmeAdi);
            }
            label1.Text = "Yönlendirme";
            label3.Text = "Yönlendirme Ekle";
        }
        public VeriEkleme(IFaaliyetTuruService faaliyetTuruService)
        {
            InitializeComponent();
            _faaliyetTuruService = faaliyetTuruService;
            secim = 7;
            foreach (var item in _faaliyetTuruService.TumKayitlar())
            {
                adComboBox.Items.Add(item.FaaliyetTuruAdi);
            }
            label1.Text = "Faaliyet Türü";
            label3.Text = "Faaliyet Türü Ekle";
        }
        public VeriEkleme(ISonucAciklamaService sonucAciklamaService)
        {
            InitializeComponent();
            _sonucAciklamaService = sonucAciklamaService;
            secim = 8;
            foreach (var item in _sonucAciklamaService.TumKayitlar())
            {
                adComboBox.Items.Add(item.SonucAdi);
            }
            label1.Text = "Sonuç Açıklama";
            label3.Text = "Sonuç Açıklama Ekle";
        }
        public VeriEkleme(IAciklamaService aciklamaService)
        {
            InitializeComponent();
            _aciklamaService = aciklamaService;
            secim = 9;
            foreach (var item in _aciklamaService.TumKayitlar())
            {
                adComboBox.Items.Add(item.Aciklama);
            }
            label1.Text = "Açıklama";
            label3.Text = "Açıklama Ekle";
        }
        private void button4_Click(object sender, EventArgs e)
        {            
            this.Dispose();
            this.Close();
            denetim = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (adComboBox.Text!="")
                {
                    DialogResult secenek = MessageBox.Show("Kaydetmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (secenek == DialogResult.OK)
                    {
                        if (secim==1)
                        {
                            _talep = new Talep();
                            _talep.TalepAdi = adComboBox.Text;
                            _talep.IsActive = true;
                            _talepService.Ekle(_talep);
                        }
                        if (secim==2)
                        {
                            _kod = new Kod();
                            _kod.KodAdi = adComboBox.Text;
                            _kod.IsActive = true;
                            _kodService.Ekle(_kod);
                        }
                        if (secim == 3)
                        {
                            _konu = new Konu();
                            _konu.KonuAdi = adComboBox.Text;
                            _konu.IsActive = true;
                            _konuService.Ekle(_konu);
                        }
                        if (secim == 4)
                        {
                            _durum = new Durum();
                            _durum.Durumu = adComboBox.Text;
                            _durum.IsActive = true;
                            _durumService.Ekle(_durum);
                        }
                        if (secim == 5)
                        {
                            _islemSonucu = new IslemSonucu();
                            _islemSonucu.IslemSonuc = adComboBox.Text;
                            _islemSonucu.IsActive = true;
                            _islemSonucuService.Ekle(_islemSonucu);
                        }
                        if (secim == 6)
                        {
                            _yonlendirme = new Yonlendirme();
                            _yonlendirme.YonlendirmeAdi = adComboBox.Text;
                            _yonlendirme.IsActive = true;
                            _yonlendirmeService.Ekle(_yonlendirme);
                        }
                        if (secim == 7)
                        {
                            _faaliyetTuru = new FaaliyetTuru();
                            _faaliyetTuru.FaaliyetTuruAdi = adComboBox.Text;
                            _faaliyetTuru.IsActive = true;
                            _faaliyetTuruService.Ekle(_faaliyetTuru);
                        }
                        if (secim == 8)
                        {
                            _sonucAciklama = new SonucAciklama();
                            _sonucAciklama.SonucAdi = adComboBox.Text;
                            _sonucAciklama.IsActive = true;
                            _sonucAciklamaService.Ekle(_sonucAciklama);
                        }
                        if (secim == 9)
                        {
                            _aciklama = new Aciklamalar();
                            _aciklama.Aciklama = adComboBox.Text;
                            _aciklama.IsActive = true;
                            _aciklamaService.Ekle(_aciklama);
                        }
                        MessageBox.Show("Kayıt başarıyla gerçekleşti.");
                        tableLayoutPanel2.BackColor = Color.DeepSkyBlue;
                        label2.ForeColor = Color.White;
                        label2.Text = "Kayıt başarıyla gerçekleşti.";
                    }
                    else if (secenek == DialogResult.Cancel)
                    {

                    }
                }
               
            }
            catch (Exception)
            {

                throw;
            }
           
        }
      
        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void tableLayoutPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void tableLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void talepComboBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (adComboBox.Text!="")
                {

                    DialogResult secenek = MessageBox.Show("Kaydı silmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (secenek == DialogResult.OK)
                    {
                        if (secim==1)
                        {
                            _talepService.Sil(_talepService.Bul(_id));
                        }
                        if (secim==2)
                        {
                            _kodService.Sil(_kodService.Bul(_id));
                        }
                        if (secim == 3)
                        {
                            _konuService.Sil(_konuService.Bul(_id));
                        }
                        if (secim == 4)
                        {
                            _durumService.Sil(_durumService.Bul(_id));
                        }
                        if (secim == 5)
                        {
                            _islemSonucuService.Sil(_islemSonucuService.Bul(_id));
                        }
                        if (secim == 6)
                        {
                            _yonlendirmeService.Sil(_yonlendirmeService.Bul(_id));
                        }
                        if (secim == 7)
                        {
                            _faaliyetTuruService.Sil(_faaliyetTuruService.Bul(_id));
                        }
                        if (secim == 8)
                        {
                            _sonucAciklamaService.Sil(_sonucAciklamaService.Bul(_id));
                        }
                        if (secim == 9)
                        {
                            _aciklamaService.Sil(_aciklamaService.Bul(_id));
                        }
                        MessageBox.Show("Kayıt başarıyla silindi.");
                        tableLayoutPanel2.BackColor = Color.Red;
                        label2.ForeColor = Color.White;
                        label2.Text = "Kayıt başarıyla silindi.";
                        adComboBox.Text = "";
                    }
                    else if (secenek == DialogResult.Cancel)
                    {

                    }
                }
                else
                {
                    MessageBox.Show(" Silmek istediğiniz kaydı seçiniz.");
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show(" Silmek istediğiniz kaydı seçiniz.");
            }            
        }

        private void talepComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (secim==1)
            {
                _id = _talepService.Find(x => x.TalepAdi == adComboBox.Text).ID;
            }
            if (secim==2)
            {
                _id = _kodService.Find(x => x.KodAdi == adComboBox.Text).ID;
            }
            if (secim == 3)
            {
                _id = _konuService.Find(x => x.KonuAdi == adComboBox.Text).ID;
            }
            if (secim == 4)
            {
                _id = _durumService.Find(x => x.Durumu == adComboBox.Text).ID;
            }
            if (secim == 5)
            {
                _id = _islemSonucuService.Find(x => x.IslemSonuc == adComboBox.Text).ID;
            }
            if (secim == 6)
            {
                _id = _yonlendirmeService.Find(x => x.YonlendirmeAdi == adComboBox.Text).ID;
            }
            if (secim == 7)
            {
                _id = _faaliyetTuruService.Find(x => x.FaaliyetTuruAdi == adComboBox.Text).ID;
            }
            if (secim == 8)
            {
                _id = _sonucAciklamaService.Find(x => x.SonucAdi == adComboBox.Text).ID;
            }
            if (secim == 98)
            {
                _id = _aciklamaService.Find(x => x.Aciklama == adComboBox.Text).ID;
            }
        }

        private void talepComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            adComboBox.Items.Clear();
            if (secim==1)
            {
                foreach (var item in _talepService.TumKayitlar())
                {
                    adComboBox.Items.Add(item.TalepAdi);
                }
            }
            if (secim==2)
            {
                foreach (var item in _kodService.TumKayitlar())
                {
                    adComboBox.Items.Add(item.KodAdi);
                }
            }
            if (secim == 3)
            {
                foreach (var item in _konuService.TumKayitlar())
                {
                    adComboBox.Items.Add(item.KonuAdi);
                }
            }
            if (secim == 4)
            {
                foreach (var item in _durumService.TumKayitlar())
                {
                    adComboBox.Items.Add(item.Durumu);
                }
            }
            if (secim == 5)
            {
                foreach (var item in _islemSonucuService.TumKayitlar())
                {
                    adComboBox.Items.Add(item.IslemSonuc);
                }
            }
            if (secim == 6)
            {
                foreach (var item in _yonlendirmeService.TumKayitlar())
                {
                    adComboBox.Items.Add(item.YonlendirmeAdi);
                }
            }
            if (secim == 7)
            {
                foreach (var item in _faaliyetTuruService.TumKayitlar())
                {
                    adComboBox.Items.Add(item.FaaliyetTuruAdi);
                }
            }
            if (secim == 8)
            {
                foreach (var item in _sonucAciklamaService.TumKayitlar())
                {
                    adComboBox.Items.Add(item.SonucAdi);
                }
            }
            if (secim == 9)
            {
                foreach (var item in _aciklamaService.TumKayitlar())
                {
                    adComboBox.Items.Add(item.Aciklama);
                }
            }
        }

    }
}
