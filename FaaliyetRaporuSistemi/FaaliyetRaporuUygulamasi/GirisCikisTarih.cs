using FaaliyetRaporu.Core.Domain.Entites;
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
    public partial class GirisCikisTarih : Form
    {
        #region

        private readonly IKullaniciService _kullaniciService;
        private readonly IKullaniciGirisCikisTarihiService _kullaniciGirisCikisTarihiService;

        #endregion

        #region form hareket

        private bool mouseDown;
        private Point lastLocation;
        private static bool panelac = false;

        #endregion
        
        int i = 0;
        public GirisCikisTarih(int id,IKullaniciService kullaniciService, IKullaniciGirisCikisTarihiService kullaniciGirisCikisTarihiService)
        {
            InitializeComponent();
            _kullaniciService = kullaniciService;
            _kullaniciGirisCikisTarihiService = kullaniciGirisCikisTarihiService;
            dataGridView1.Rows.Clear();
            foreach (var item in _kullaniciGirisCikisTarihiService.Get(x=>x.KullaniciID==id))
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = item.GirisTarihi;
                dataGridView1.Rows[i].Cells[1].Value = item.CikisTarihi;
                dataGridView1.Rows[i].Cells[2].Value = _kullaniciService.Bul(id).Adi+" "+ _kullaniciService.Bul(id).Soyadi;

                if (item.IsActive==true)
                {
                    dataGridView1.Rows[i].Cells[3].Value = "Aktif";
                }
                else
                {
                    dataGridView1.Rows[i].Cells[3].Value = "Pasif";
                }
                i++;
            }
            i = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void tableLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void tableLayoutPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void tableLayoutPanel1_MouseDoubleClick(object sender, MouseEventArgs e)
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
    }
}
