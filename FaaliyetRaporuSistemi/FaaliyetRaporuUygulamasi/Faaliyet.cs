using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.AciklamaService;
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace FaliyetRaporuUygulamasi
{
    public partial class Faaliyet : Form
    {
        //  private bool disposed = false;

        #region Dependency_Injection

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

        Kullanici _kullanici;
        #endregion

        #region  Yazıcı değişkenler

        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height
        #endregion

        #region

        VeriEkleme _veriEkleme;
       

        #endregion  

        int i = 0;
        DataTable dt = new DataTable();

        List<int> _id = new List<int>();

        public Faaliyet(Kullanici kullanici, IRolService rolService, IKodService kodService, IKonuService konuService, ITalepService talepService, IDurumService durumService,
            IKullaniciService kullaniciService, IAciklamaService aciklamaService, IIslemSonucuService islemSonucuService, IYonlendirmeService yonlendirmeService, 
            IFaaliyetTuruService faaliyetTuruService, IFaaliyetRaporService faaliyetRaporService, ISonucAciklamaService sonucAciklamaService, IKullaniciAdresService kullaniciAdresService)
        {
            InitializeComponent();
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

        }

        #region  Butonlar

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();
            int bulunanKayit = _faaliyetRaporService.kayitGetir(kodComboBox.Text).Count();
            tableLayoutPanel5.BackColor = Color.WhiteSmoke;
            label5.Text = "Bulunan Kayıt :" + " " + bulunanKayit.ToString();
            label5.TextAlign = ContentAlignment.MiddleLeft;
            _id.Clear();

            try
            {
                if (kodComboBox.Text != "")
                {
                    foreach (var item in _faaliyetRaporService.kayitGetir(kodComboBox.Text))
                    {
                        _id.Add(item.ID);
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = item.Talep;
                        dataGridView1.Rows[i].Cells[1].Value = item.Kod;
                        dataGridView1.Rows[i].Cells[2].Value = item.FaaliyetTuru;
                        dataGridView1.Rows[i].Cells[3].Value = _aciklamaService.Bul(item.AciklamaID).Aciklama;
                        dataGridView1.Rows[i].Cells[4].Value = item.Konu;
                        dataGridView1.Rows[i].Cells[5].Value = item.Yonlendirme;
                        dataGridView1.Rows[i].Cells[6].Value = _kullaniciService.Bul(item.KullaniciId).Adi + " " + _kullaniciService.Bul(item.KullaniciId).Soyadi;
                        dataGridView1.Rows[i].Cells[7].Value = _durumService.Bul(item.DurumID).Durumu;
                        dataGridView1.Rows[i].Cells[8].Value = _islemSonucuService.Bul(item.IslemSonucuID).IslemSonuc;
                        dataGridView1.Rows[i].Cells[9].Value = item.SonucAciklama;
                        dataGridView1.Rows[i].Cells[10].Value = item.IslemBaslangisTarihi;
                        dataGridView1.Rows[i].Cells[11].Value = item.IslemBitisTarihi;
                        i++;
                    }

                    label13.Text = _id[i - 1].ToString();

                    talepComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[0].Value.ToString();
                    kodComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[1].Value.ToString();
                    faaliyetturuComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[2].Value.ToString();
                    aciklamaComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[3].Value.ToString();
                    konuComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[4].Value.ToString();
                    yonlendirmeComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[5].Value.ToString();
                    islemyapanComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[6].Value.ToString();
                    durumComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[7].Value.ToString();
                    islemsonucuComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[8].Value.ToString();
                    sonucaciklamaComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[9].Value.ToString();
                    baslangicdateTimePicker1.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[10].Value.ToString();
                    bitisdateTimePicker2.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[11].Value.ToString();

                    i = 0;
                }
                else
                {
                    label5.Text = "Bulunan Kayıt :" + " " + _faaliyetRaporService.TumKayitlar().Count().ToString();
                    label5.TextAlign = ContentAlignment.MiddleLeft;
                    foreach (var item in _faaliyetRaporService.TumKayitlar())
                    {
                        _id.Add(item.ID);
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = item.Talep;
                        dataGridView1.Rows[i].Cells[1].Value = item.Kod;
                        dataGridView1.Rows[i].Cells[2].Value = item.FaaliyetTuru;
                        dataGridView1.Rows[i].Cells[3].Value = _aciklamaService.Bul(item.AciklamaID).Aciklama;
                        dataGridView1.Rows[i].Cells[4].Value = item.Konu;
                        dataGridView1.Rows[i].Cells[5].Value = item.Yonlendirme;
                        dataGridView1.Rows[i].Cells[6].Value = _kullaniciService.Bul(item.KullaniciId).Adi + " " + _kullaniciService.Bul(item.KullaniciId).Soyadi;
                        dataGridView1.Rows[i].Cells[7].Value = _durumService.Bul(item.DurumID).Durumu;
                        dataGridView1.Rows[i].Cells[8].Value = _islemSonucuService.Bul(item.IslemSonucuID).IslemSonuc;
                        dataGridView1.Rows[i].Cells[9].Value = item.SonucAciklama;
                        dataGridView1.Rows[i].Cells[10].Value = item.IslemBaslangisTarihi;
                        dataGridView1.Rows[i].Cells[11].Value = item.IslemBitisTarihi;
                        i++;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bu kod numarasına ait kayıt Yok.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            i = 0;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int secim = e.RowIndex;
            if (secim >= 0)
            {
                label13.Text = _id[secim].ToString();
                talepComboBox.Text = dataGridView1.Rows[secim].Cells[0].Value.ToString();
                kodComboBox.Text = dataGridView1.Rows[secim].Cells[1].Value.ToString();
                faaliyetturuComboBox.Text = dataGridView1.Rows[secim].Cells[2].Value.ToString();
                aciklamaComboBox.Text = dataGridView1.Rows[secim].Cells[3].Value.ToString();
                konuComboBox.Text = dataGridView1.Rows[secim].Cells[4].Value.ToString();
                yonlendirmeComboBox.Text = dataGridView1.Rows[secim].Cells[5].Value.ToString();
                islemyapanComboBox.Text = dataGridView1.Rows[secim].Cells[6].Value.ToString();
                durumComboBox.Text = dataGridView1.Rows[secim].Cells[7].Value.ToString();
                islemsonucuComboBox.Text = dataGridView1.Rows[secim].Cells[8].Value.ToString();
                sonucaciklamaComboBox.Text = dataGridView1.Rows[secim].Cells[9].Value.ToString();
                baslangicdateTimePicker1.Text = dataGridView1.Rows[secim].Cells[10].Value.ToString();
                bitisdateTimePicker2.Text = dataGridView1.Rows[secim].Cells[11].Value.ToString();
            }

        }

        private void kodComboBox_MouseClick(object sender, MouseEventArgs e)
        {

            kodComboBox.Items.Clear();
            foreach (var item in _kodService.TumKayitlar())
            {
                kodComboBox.Items.Add(item.KodAdi);
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            talepComboBox.Text = "";
            kodComboBox.Text = "";
            faaliyetturuComboBox.Text = "";
            aciklamaComboBox.Text = "";
            konuComboBox.Text = "";
            yonlendirmeComboBox.Text = "";
            islemyapanComboBox.Text = "";
            durumComboBox.Text = "";
            islemsonucuComboBox.Text = "";
            sonucaciklamaComboBox.Text = "";
            baslangicdateTimePicker1.Value = DateTime.Now;
            bitisdateTimePicker2.Value = DateTime.Now;
            label13.Text = "-";
            dataGridView1.Rows.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secenek = MessageBox.Show("Kaydı veritabanına kaydetmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (secenek == DialogResult.OK)
                {
                    FaaliyetRapor faaliyetRapor = new FaaliyetRapor();

                    faaliyetRapor.Talep = talepComboBox.Text;
                    faaliyetRapor.Kod = kodComboBox.Text;
                    faaliyetRapor.FaaliyetTuru = faaliyetturuComboBox.Text;
                    faaliyetRapor.AciklamaID = _aciklamaService.Find(x=>x.Aciklama== aciklamaComboBox.Text).ID;
                    faaliyetRapor.Konu = konuComboBox.Text;
                    faaliyetRapor.Yonlendirme = yonlendirmeComboBox.Text;
                    faaliyetRapor.KullaniciId = _kullanici.ID;
                    faaliyetRapor.DurumID= _durumService.Find(x => x.Durumu == durumComboBox.Text).ID;
                    faaliyetRapor.IslemSonucuID = _islemSonucuService.Find(x=>x.IslemSonuc==islemsonucuComboBox.Text).ID;                  
                    faaliyetRapor.SonucAciklama = sonucaciklamaComboBox.Text;
                    faaliyetRapor.IslemBaslangisTarihi = baslangicdateTimePicker1.Value;
                    faaliyetRapor.IslemBitisTarihi = bitisdateTimePicker2.Value;

                    _faaliyetRaporService.Ekle(faaliyetRapor);
                    MessageBox.Show("Kayıt başarıyla gerçekleşti.");
                    label6.Text = "Kayıt başarıyla gerçekleşti.";
                    tableLayoutPanel5.BackColor = Color.DeepSkyBlue;
                    label6.ForeColor = Color.White;
                    label5.ForeColor = Color.White;
                }
                else if (secenek == DialogResult.Cancel)
                {

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata! Kayıt başarıyla başarısız.");
                label6.Text = "Hata! Kayıt ekleme başarısız.";
                tableLayoutPanel5.BackColor = Color.Orange;
                label6.ForeColor = Color.White;
                label5.ForeColor = Color.White;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Kaydı güncellemek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (secenek == DialogResult.OK)
            {
                FaaliyetRapor fr = _faaliyetRaporService.Bul(Convert.ToInt32(label13.Text));
                try
                {
                    fr.Talep = talepComboBox.Text;
                    fr.Kod = kodComboBox.Text;
                    fr.FaaliyetTuru = faaliyetturuComboBox.Text;
                    fr.AciklamaID = _aciklamaService.Find(x => x.Aciklama == aciklamaComboBox.Text).ID;
                    fr.Konu = konuComboBox.Text;
                    fr.Yonlendirme = yonlendirmeComboBox.Text;
                    fr.KullaniciId = _kullanici.ID;
                    fr.DurumID = _durumService.Find(x=>x.Durumu==durumComboBox.Text).ID;
                    fr.IslemSonucuID = _islemSonucuService.Find(x => x.IslemSonuc == islemsonucuComboBox.Text).ID;
                    fr.SonucAciklama = sonucaciklamaComboBox.Text;
                    fr.IslemBaslangisTarihi = baslangicdateTimePicker1.Value;
                    fr.IslemBitisTarihi = bitisdateTimePicker2.Value;

                    _faaliyetRaporService.Guncelle(fr);
                    MessageBox.Show("Kayıt başarıyla güncellendi.");
                    label6.Text = "Kayıt başarıyla güncellendi.";
                    tableLayoutPanel5.BackColor = Color.Orange;
                    label6.ForeColor = Color.White;
                    label6.TextAlign = ContentAlignment.MiddleLeft;
                    label5.ForeColor = Color.White;
                    label5.TextAlign = ContentAlignment.MiddleLeft;

                }
                catch (Exception)
                {
                    MessageBox.Show("Hata! Kayıt güncelleme başarısız.");
                    label6.Text = "Hata! Kayıt güncelleme başarısız.";
                    tableLayoutPanel5.BackColor = Color.Orange;
                    label6.ForeColor = Color.White;
                    label6.TextAlign = ContentAlignment.MiddleLeft;
                    label5.ForeColor = Color.White;
                    label5.TextAlign = ContentAlignment.MiddleLeft;

                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Kaydı silmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (secenek == DialogResult.OK)
            {
                FaaliyetRapor fr = _faaliyetRaporService.Bul(Convert.ToInt32(label13.Text));
                _faaliyetRaporService.Sil(fr);
                MessageBox.Show("Kayıt başarıyla silindi.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 1)
            {
                PDFExport PdfExport = new PDFExport();
                PdfExport.FileName = this.Name;
                PdfExport.PdfRowIndex = 4;
                PdfExport.ToPdf(dataGridView1); // Hangi datagridview içinde ki verileri PDF olarak export edeceğiz
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 1)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = "C:";
                saveFileDialog.Title = "Excel Kayıt";
                saveFileDialog.FileName = "";
                saveFileDialog.Filter = "Excel Dosyası(2007)|*.xlsx|Excel Dosyası(2010)|*.xlsx|Excel Dosyası(2013)|*.xlsx|Excel Dosyası(2016)|*.xlsx";

                if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
                {
                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                    excel.Visible = true;
                    Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                    Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                    int StartCol = 1;
                    int StartRow = 1;
                    int j = 0, i = 0;


                    //başlıkları yazdırıyoruz...
                    for (j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                        myRange.Cells.Interior.Color = Color.Yellow;
                        myRange.Value2 = dataGridView1.Columns[j].HeaderText;
                    }

                    StartRow++;

                    //datagridview içeriğini yazdırıyoruz...
                    for (i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            try
                            {
                                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                                myRange.Value2 = dataGridView1[j, i].Value.ToString() == null ? "" : dataGridView1[j, i].Value.ToString();
                                myRange.Columns.AutoFit();
                            }
                            catch
                            {
                                ;
                            }
                        }
                    }
                    excel.ActiveWorkbook.SaveCopyAs(saveFileDialog.FileName.ToString());
                    excel.ActiveWorkbook.Saved = true;
                }
            }
        }
        #endregion

        #region Yazıcı İşlemler

        private void button6_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;
            printDialog.UseEXDialog = true;

            //Get the document
            if (DialogResult.OK == printDialog.ShowDialog())
            {
                printDocument1.DocumentName = "Test Page Print";
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left - 90;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top - 100;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                       (double)iTotalWidth * (double)iTotalWidth *
                                       ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headres
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= dataGridView1.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dataGridView1.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("Customer Summary", new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Customer Summary", new System.Drawing.Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new System.Drawing.Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Customer Summary", new System.Drawing.Font(new System.Drawing.Font(dataGridView1.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new System.Drawing.Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new System.Drawing.Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Value != null)
                            {
                                e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font,
                                            new SolidBrush(Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new System.Drawing.Rectangle((int)arrColumnLefts[iCount],
                                    iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));
                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }

                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_BeginPrint_1(object sender, PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dataGridView1.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void kodComboBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int bulunanKayit = _faaliyetRaporService.kayitGetir(kodComboBox.Text).Count();
            if (bulunanKayit > 0)
            {
                dataGridView1.Rows.Clear();

                label5.Text = "Bulunan Kayıt :" + " " + bulunanKayit.ToString();
                _id.Clear();
                foreach (var item in _faaliyetRaporService.kayitGetir(kodComboBox.Text))
                {
                    _id.Add(item.ID);
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = item.Talep;
                    dataGridView1.Rows[i].Cells[1].Value = item.Kod;
                    dataGridView1.Rows[i].Cells[2].Value = item.FaaliyetTuru;
                    dataGridView1.Rows[i].Cells[3].Value = _aciklamaService.Bul(item.AciklamaID).Aciklama;
                    dataGridView1.Rows[i].Cells[4].Value = item.Konu;
                    dataGridView1.Rows[i].Cells[5].Value = item.Yonlendirme;
                    dataGridView1.Rows[i].Cells[6].Value = _kullaniciService.Bul(item.KullaniciId).Adi + " " + _kullaniciService.Bul(item.KullaniciId).Soyadi;
                    dataGridView1.Rows[i].Cells[7].Value = _durumService.Bul(item.DurumID).Durumu;
                    dataGridView1.Rows[i].Cells[8].Value = _islemSonucuService.Bul(item.IslemSonucuID).IslemSonuc;
                    dataGridView1.Rows[i].Cells[9].Value = item.SonucAciklama;
                    dataGridView1.Rows[i].Cells[10].Value = item.IslemBaslangisTarihi;
                    dataGridView1.Rows[i].Cells[11].Value = item.IslemBitisTarihi;
                    i++;
                }

                label13.Text = _id[i - 1].ToString();

                talepComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[0].Value.ToString();
                kodComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[1].Value.ToString();
                faaliyetturuComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[2].Value.ToString();
                aciklamaComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[3].Value.ToString();
                konuComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[4].Value.ToString();
                yonlendirmeComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[5].Value.ToString();
                islemyapanComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[6].Value.ToString();
                durumComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[7].Value.ToString();
                islemsonucuComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[8].Value.ToString();
                sonucaciklamaComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[9].Value.ToString();
                baslangicdateTimePicker1.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[10].Value.ToString();
                bitisdateTimePicker2.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[11].Value.ToString();
                i = 0;
            }
            else
            {
                label5.Text = "Bulunan Kayıt :" + " " + bulunanKayit.ToString();
            }
        }

        private void kodComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int bulunanKayit = _faaliyetRaporService.kayitGetir(kodComboBox.Text).Count();
                if (bulunanKayit > 0)
                {
                    dataGridView1.Rows.Clear();

                    label5.Text = "Bulunan Kayıt :" + " " + bulunanKayit.ToString();
                    _id.Clear();
                    foreach (var item in _faaliyetRaporService.kayitGetir(kodComboBox.Text))
                    {
                        _id.Add(item.ID);
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = item.Talep;
                        dataGridView1.Rows[i].Cells[1].Value = item.Kod;
                        dataGridView1.Rows[i].Cells[2].Value = item.FaaliyetTuru;
                        dataGridView1.Rows[i].Cells[3].Value = _aciklamaService.Bul(item.AciklamaID).Aciklama; ;
                        dataGridView1.Rows[i].Cells[4].Value = item.Konu;
                        dataGridView1.Rows[i].Cells[5].Value = item.Yonlendirme;
                        dataGridView1.Rows[i].Cells[6].Value = _kullaniciService.Bul(item.KullaniciId).Adi + " " + _kullaniciService.Bul(item.KullaniciId).Soyadi;
                        dataGridView1.Rows[i].Cells[7].Value = _durumService.Bul(item.DurumID).Durumu;
                        dataGridView1.Rows[i].Cells[8].Value = _islemSonucuService.Bul(item.IslemSonucuID).IslemSonuc;
                        dataGridView1.Rows[i].Cells[9].Value = item.SonucAciklama;
                        dataGridView1.Rows[i].Cells[10].Value = item.IslemBaslangisTarihi;
                        dataGridView1.Rows[i].Cells[11].Value = item.IslemBitisTarihi;
                        i++;
                    }

                    label13.Text = _id[i - 1].ToString();

                    talepComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[0].Value.ToString();
                    kodComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[1].Value.ToString();
                    faaliyetturuComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[2].Value.ToString();
                    aciklamaComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[3].Value.ToString();
                    konuComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[4].Value.ToString();
                    yonlendirmeComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[5].Value.ToString();
                    islemyapanComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[6].Value.ToString();
                    durumComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[7].Value.ToString();
                    islemsonucuComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[8].Value.ToString();
                    sonucaciklamaComboBox.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[9].Value.ToString();
                    baslangicdateTimePicker1.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[10].Value.ToString();
                    bitisdateTimePicker2.Text = dataGridView1.Rows[bulunanKayit - 1].Cells[11].Value.ToString();
                    i = 0;
                }
                else
                {
                    label5.Text = "Bulunan Kayıt :" + " " + bulunanKayit.ToString();
                }
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            Yazdiriliyor f = new Yazdiriliyor();
            f.dgw = dataGridView1;
            f.ShowDialog();
        }

        private void talepComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            talepComboBox.Items.Clear();
            foreach (var item in _talepService.TumKayitlar())
            {
                talepComboBox.Items.Add(item.TalepAdi);
            }
        }

        private void faaliyetturuComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            faaliyetturuComboBox.Items.Clear();
            foreach (var item in _faaliyetTuruService.TumKayitlar())
            {
                faaliyetturuComboBox.Items.Add(item.FaaliyetTuruAdi);
            }
        }

        private void konuComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            konuComboBox.Items.Clear();
            foreach (var item in _konuService.TumKayitlar())
            {
                konuComboBox.Items.Add(item.KonuAdi);
            }
        }

        private void yonlendirmeComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            yonlendirmeComboBox.Items.Clear();
            foreach (var item in _yonlendirmeService.TumKayitlar())
            {
                yonlendirmeComboBox.Items.Add(item.YonlendirmeAdi);
            }
        }

        private void durumComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            durumComboBox.Items.Clear();
            foreach (var item in _durumService.TumKayitlar())
            {
                durumComboBox.Items.Add(item.Durumu);
            }
        }

        private void islemsonucuComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            islemsonucuComboBox.Items.Clear();
            foreach (var item in _islemSonucuService.TumKayitlar())
            {
                islemsonucuComboBox.Items.Add(item.IslemSonuc);
            }
        }

        private void sonucaciklamaComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            sonucaciklamaComboBox.Items.Clear();
            foreach (var item in _sonucAciklamaService.TumKayitlar())
            {
                sonucaciklamaComboBox.Items.Add(item.SonucAdi);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {            
            if (_veriEkleme==null|| _veriEkleme.Disposing|| _veriEkleme.denetim==false)
            {
                _veriEkleme = new VeriEkleme(_talepService);
                _veriEkleme.Show();
                _veriEkleme.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (_veriEkleme == null || _veriEkleme.Disposing || _veriEkleme.denetim == false)
            {
                _veriEkleme = new VeriEkleme(_kodService);
                _veriEkleme.Show();
                _veriEkleme.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (_veriEkleme == null || _veriEkleme.Disposing || _veriEkleme.denetim == false)
            {
                _veriEkleme = new VeriEkleme(_faaliyetTuruService);
                _veriEkleme.Show();
                _veriEkleme.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (_veriEkleme == null || _veriEkleme.Disposing || _veriEkleme.denetim == false)
            {
                _veriEkleme = new VeriEkleme(_konuService);
                _veriEkleme.Show();
                _veriEkleme.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (_veriEkleme == null || _veriEkleme.Disposing || _veriEkleme.denetim == false)
            {
                _veriEkleme = new VeriEkleme(_aciklamaService);
                _veriEkleme.Show();
                _veriEkleme.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (_veriEkleme == null || _veriEkleme.Disposing || _veriEkleme.denetim == false)
            {
                _veriEkleme = new VeriEkleme(_yonlendirmeService);
                _veriEkleme.Show();
                _veriEkleme.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (_veriEkleme == null || _veriEkleme.Disposing || _veriEkleme.denetim == false)
            {
                _veriEkleme = new VeriEkleme(_durumService);
                _veriEkleme.Show();
                _veriEkleme.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (_veriEkleme == null || _veriEkleme.Disposing || _veriEkleme.denetim == false)
            {
                _veriEkleme = new VeriEkleme(_islemSonucuService);
                _veriEkleme.Show();
                _veriEkleme.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (_veriEkleme == null || _veriEkleme.Disposing || _veriEkleme.denetim == false)
            {
                _veriEkleme = new VeriEkleme(_sonucAciklamaService);
                _veriEkleme.Show();
                _veriEkleme.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void aciklamaComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            aciklamaComboBox.Items.Clear();
            foreach (var item in _aciklamaService.TumKayitlar())
            {
                aciklamaComboBox.Items.Add(item.Aciklama);
            }
        }
    }
}
