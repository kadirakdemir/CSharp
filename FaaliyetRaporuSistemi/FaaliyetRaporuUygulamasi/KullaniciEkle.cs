using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.FaaliyetRaporService;
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
    public partial class KullaniciEkle : Form
    {
        #region Dependency Injection

        private readonly IRolService _rolService;
        private readonly IKullaniciService _kullaniciService;
        private readonly IFaaliyetRaporService _faaliyetRaporService;

        #endregion

        Kullanici _kullanici;

        List<int> _id = new List<int>();
        static int secimId;

        int i = 0;
        public KullaniciEkle(IRolService rolService, Kullanici kullanici, IKullaniciService kullaniciService, IFaaliyetRaporService faaliyetRaporService)
        {
            InitializeComponent();
            _rolService = rolService;
            _kullanici = kullanici;
            _kullaniciService = kullaniciService;
            _faaliyetRaporService = faaliyetRaporService;
            foreach (var item in _rolService.TumKayitlar())
            {
                rolComboBox.Items.Add(item.RolAdi);
            }
            if (_kullanici.RolID != 1)
            {
                rolComboBox.Items.RemoveAt(0);
            }
            try
            {
                foreach (var item in _kullaniciService.TumKayitlar())
                {
                    _id.Add(item.ID);
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = item.Adi;
                    dataGridView1.Rows[i].Cells[1].Value = item.Soyadi;
                    dataGridView1.Rows[i].Cells[2].Value = item.Email;
                    dataGridView1.Rows[i].Cells[3].Value = item.CepTelefonNo;
                    dataGridView1.Rows[i].Cells[4].Value = item.Sifre;
                    dataGridView1.Rows[i].Cells[5].Value = _rolService.Find(x => x.ID == item.RolID).RolAdi;
                    dataGridView1.Rows[i].Cells[6].Value = item.GizliSoru;
                    dataGridView1.Rows[i].Cells[7].Value = item.GizliSoruCevap;
                    if (item.OnaylandiMi == true)
                    {
                        dataGridView1.Rows[i].Cells[8].Value = "Onaylandı";
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[8].Value = "Onaylanmadı";
                        dataGridView1.Rows[i].Cells[8].Style.BackColor = Color.Red;
                        dataGridView1.Rows[i].Cells[8].Style.ForeColor = Color.White;
                    }
                    if (item.KilitliMi == true)
                    {
                        dataGridView1.Rows[i].Cells[9].Value = "Açık";
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[9].Value = "Kilitli";
                        dataGridView1.Rows[i].Cells[9].Style.BackColor = Color.Red;
                        dataGridView1.Rows[i].Cells[9].Style.ForeColor = Color.White;
                    }
                    if (item.IsActive == true)
                    {
                        dataGridView1.Rows[i].Cells[10].Value = "Aktif";
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[10].Value = "Pasif";
                        dataGridView1.Rows[i].Cells[10].Style.BackColor = Color.Red;
                        dataGridView1.Rows[i].Cells[10].Style.ForeColor = Color.White;
                    }

                    dataGridView1.Rows[i].Cells[11].Value = item.OlusturmaTarihi;
                    dataGridView1.Rows[i].Cells[12].Value = item.SifreDegistirmeTarihi;
                    i++;
                }
                i = 0;

                if (_kullanici.RolID == 1)
                {
                    onayCheckBox.Enabled = true;
                    kilitleCheckBox.Enabled = true;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                secimId = _id[index];
                adTextBox.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                soyadTextBox.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                emailTextBox.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
                telefonTextBox.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
                sifreTextBox.Text = dataGridView1.Rows[index].Cells[4].Value.ToString();
                rolComboBox.Text = dataGridView1.Rows[index].Cells[5].Value.ToString();
                gizliSoruTextBox.Text = dataGridView1.Rows[index].Cells[6].Value.ToString();
                gizliSoruCevapTextBox.Text = dataGridView1.Rows[index].Cells[7].Value.ToString();
                if (dataGridView1.Rows[index].Cells[8].Value.Equals("Onaylandı"))
                {
                    onayCheckBox.Checked = true;
                }
                else
                {
                    onayCheckBox.Checked = false;
                }

                if (dataGridView1.Rows[index].Cells[9].Value.Equals("Açık"))
                {
                    kilitleCheckBox.Checked = false;
                }
                else
                {
                    kilitleCheckBox.Checked = true;
                }

                if (dataGridView1.Rows[index].Cells[10].Value.Equals("Aktif"))
                {
                    label12.Text = "Aktif";
                }
                else
                {
                    label12.Text = "Pasif";
                }


            }
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void kaydetButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secenek = MessageBox.Show("Kaydı veritabanına kaydetmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (secenek == DialogResult.OK)
                {
                    Kullanici kullanici = new Kullanici();
                    if (sifreTextBox.Text == sifreTekrarTextBox.Text & sifreTekrarTextBox.Text != "")
                    {
                        kullanici.Adi = adTextBox.Text;
                        kullanici.Soyadi = soyadTextBox.Text;
                        kullanici.Email = emailTextBox.Text;
                        kullanici.CepTelefonNo = telefonTextBox.Text;
                        kullanici.RolID = _rolService.Find(x => x.RolAdi == rolComboBox.Text).ID;
                        kullanici.Sifre = sifreTextBox.Text;
                        kullanici.GizliSoru = gizliSoruTextBox.Text;
                        kullanici.GizliSoruCevap = gizliSoruCevapTextBox.Text;
                        if (_kullanici.RolID == 1)
                        {
                            kullanici.OnaylandiMi = onayCheckBox.Checked;
                            kullanici.KilitliMi = kilitleCheckBox.Checked;
                        }
                        kullanici.OlusturmaTarihi = DateTime.Now;
                        kullanici.SifreDegistirmeTarihi = DateTime.Now;
                        kullanici.IsActive = false;
                        _kullaniciService.Ekle(kullanici);

                        MessageBox.Show("Kayıt başarıyla gerçekleşti.");
                        label13.ForeColor = Color.White;
                        label13.Text = "Kayıt başarıyla gerçekleşti.";
                        tableLayoutPanel2.BackColor = Color.DeepSkyBlue;
                    }
                    else
                    {
                        MessageBox.Show("Şifre hata! Tekrar giriniz.");
                        errorProvider1.SetError(sifreTekrarTextBox, "Şifreyi doğru giriniz!");
                    }
                }
                else if (secenek == DialogResult.Cancel)
                {

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu tekrar deneyiniz.");
            }

        }

        private void silButton_Click(object sender, EventArgs e)
        {
            try
            {
                Kullanici kullanici;
                FaaliyetRapor fa = new FaaliyetRapor();
                DialogResult secenek = MessageBox.Show("Kaydı silmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (secenek == DialogResult.OK)
                {
                    if (_kullanici.RolID == 1)
                    {
                        if (_kullaniciService.Bul(secimId).RolID != 1)
                        {
                            for (int k = 0; k < _faaliyetRaporService.Get(x => x.KullaniciId == _kullanici.ID).Count(); k++)
                            {
                                fa = _faaliyetRaporService.Find(x => x.KullaniciId == _kullanici.ID);
                                fa.KullaniciId = _kullanici.ID;
                                _faaliyetRaporService.Guncelle(fa);
                            }
                            _kullaniciService.Sil(_kullaniciService.Bul(secimId));
                            MessageBox.Show("Kayıt başarıyla silindi.");
                            label13.Text = "Kayıt başarıyla silindi.";
                            label13.ForeColor = Color.White;
                            tableLayoutPanel2.BackColor = Color.Red;
                        }

                        if (_kullaniciService.Bul(secimId).RolID == 1 & _kullaniciService.Get(x => x.RolID == 1).Count() > 1)
                        {
                            kullanici = _kullaniciService.Get(x => x.RolID == 1 & x.ID != _kullanici.ID).FirstOrDefault();

                            for (int k = 0; k < _faaliyetRaporService.Get(x => x.KullaniciId == _kullanici.ID).Count(); k++)
                            {
                                fa = _faaliyetRaporService.Find(x => x.KullaniciId == _kullanici.ID);
                                fa.KullaniciId = kullanici.ID;
                                _faaliyetRaporService.Guncelle(fa);
                            }
                            _kullaniciService.Sil(_kullaniciService.Bul(secimId));
                            MessageBox.Show("Kayıt başarıyla silindi.");
                            label13.Text = "Kayıt başarıyla silindi.";
                            label13.ForeColor = Color.White;
                            tableLayoutPanel2.BackColor = Color.Red;
                        }
                        else
                        {
                            MessageBox.Show("Başka admin kullanıcı olmadığından silme işlemi başarısız!");
                        }
                    }
                    else if (_kullanici.RolID == 2 & _kullaniciService.Bul(secimId).RolID != 1)
                    {
                        if (_kullanici.RolID == 2 & _kullanici.ID == _kullaniciService.Bul(secimId).ID)
                        {
                            MessageBox.Show("Kendinizi silemezsiniz!");
                        }
                        else
                        {
                            for (int k = 0; k < _faaliyetRaporService.Get(x => x.KullaniciId == _kullanici.ID).Count(); k++)
                            {
                                fa = _faaliyetRaporService.Find(x => x.KullaniciId == _kullanici.ID);
                                fa.KullaniciId = _kullanici.ID;
                                _faaliyetRaporService.Guncelle(fa);
                            }
                            _kullaniciService.Sil(_kullaniciService.Bul(secimId));
                            MessageBox.Show("Kayıt başarıyla silindi.");
                            label13.Text = "Kayıt başarıyla silindi.";
                            label13.ForeColor = Color.White;
                            tableLayoutPanel2.BackColor = Color.Red;
                        }
                    }
                    else if (_kullanici.RolID == 2 & _kullaniciService.Bul(secimId).RolID == 1)
                    {
                        MessageBox.Show("Admin kullanıcı silemezsiniz!");
                    }
                }
                else if (secenek == DialogResult.Cancel)
                {

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Silme işlemi başarısız!");
            }
        }

        private void guncelleButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secenek = MessageBox.Show("Kaydı güncellemek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (secenek == DialogResult.OK)
                {
                    if (sifreTextBox.Text == sifreTekrarTextBox.Text & sifreTekrarTextBox.Text != "")
                    {
                        Kullanici kullanici = _kullaniciService.Bul(secimId);
                        kullanici.Adi = adTextBox.Text;
                        kullanici.Soyadi = soyadTextBox.Text;
                        kullanici.Email = emailTextBox.Text;
                        kullanici.CepTelefonNo = telefonTextBox.Text;
                        kullanici.RolID = _rolService.Find(x => x.RolAdi == rolComboBox.Text).ID;
                        kullanici.Sifre = sifreTextBox.Text;
                        kullanici.GizliSoru = gizliSoruTextBox.Text;
                        kullanici.GizliSoruCevap = gizliSoruCevapTextBox.Text;
                        if (_kullanici.RolID == 1)
                        {
                            kullanici.OnaylandiMi = onayCheckBox.Checked;
                            kullanici.KilitliMi = kilitleCheckBox.Checked;
                        }
                        kullanici.OlusturmaTarihi = DateTime.Now;
                        kullanici.SifreDegistirmeTarihi = DateTime.Now;
                        _kullaniciService.Guncelle(kullanici);

                        MessageBox.Show("Kayıt başarıyla güncellendi.");
                        label13.Text = "Kayıt başarıyla güncellendi.";
                        label13.ForeColor = Color.White;
                        tableLayoutPanel2.BackColor = Color.Orange;
                    }
                    else
                    {
                        MessageBox.Show("Şifreyi doğru giriniz!");
                        errorProvider1.SetError(sifreTekrarTextBox, "Şifreyi doğru giriniz!");
                    }
                }
                else if (secenek == DialogResult.Cancel)
                {

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu tekrar deneyiniz.");
            }
        }
    }
}
