using System;
using System.Collections;
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
    public partial class Yazdiriliyor : Form
    {
        public Yazdiriliyor()
        {
            InitializeComponent();
        }
        public int SatirSayisi = 0;
        public DataGridView dgw;
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
        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
                    foreach (DataGridViewColumn GridCol in dgw.Columns)
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
                while (iRow <= dgw.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dgw.Rows[iRow];
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
                            e.Graphics.DrawString("Customer Summary", new System.Drawing.Font(dgw.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Customer Summary", new System.Drawing.Font(dgw.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new System.Drawing.Font(dgw.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new System.Drawing.Font(dgw.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Customer Summary", new System.Drawing.Font(new System.Drawing.Font(dgw.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dgw.Columns)
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

            try
            {
                //ÇİZİM BAŞLANGICI
                Font myFont = new Font("Calibri", 7); //font oluşturduk
                SolidBrush sbrush = new SolidBrush(Color.Black);//fırça oluşturduk
                Pen myPen = new Pen(Color.Black); //kalem oluşturduk

                e.Graphics.DrawString("Düzenlenme Tarihi: " + DateTime.Now.ToLongDateString() + "   " + DateTime.Now.ToLongTimeString(), myFont, sbrush, 50, 25);
                e.Graphics.DrawLine(myPen, 50, 45, 770, 45); // Çizgi çizdik... 1. Kalem, 2. X, 3. Y Koordinatı, 4. Uzunluk, 5. BitişX 

                myFont = new Font("Calibri", 15, FontStyle.Bold);//Fatura başlığı yazacağımız için fontu kalın yaptık ve puntoyu büyütüp 15 yaptık.
                e.Graphics.DrawString("Ürün Listesi", myFont, sbrush, 350, 65);
                e.Graphics.DrawLine(myPen, 50, 95, 770, 95); //çizgi çizdik.

                myFont = new Font("TALEP", 10, FontStyle.Bold); //Detay başlığını yazacağımız için fontu kalın yapıp puntoyu 10 yaptık.
                e.Graphics.DrawString("KOD", myFont, sbrush, 120, 110); //Detay başlığı
                e.Graphics.DrawString("FAALİYET TÜRÜ", myFont, sbrush, 240, 110); //Detay başlığı
                e.Graphics.DrawString("AÇIKLAMA", myFont, sbrush, 340, 110); // Detay başlığı
                e.Graphics.DrawString("KONU", myFont, sbrush, 400, 110); //Detay başlığı
                e.Graphics.DrawString("YÖNLENDİRME", myFont, sbrush, 500, 110); //Detay başlığı
                e.Graphics.DrawString("İŞLEM YAPAN", myFont, sbrush, 600, 110); //Detay başlığı
                e.Graphics.DrawString("DURUM", myFont, sbrush, 700, 110); //Detay başlığı
                e.Graphics.DrawString("İŞLEM SONUCU", myFont, sbrush, 750, 110); // Detay başlığı
                e.Graphics.DrawString("SONUÇ AÇIKLAMA", myFont, sbrush, 800, 110); //Detay başlığı
                e.Graphics.DrawString("BAŞLANGIÇ TARİHİ", myFont, sbrush, 900, 110); //Detay başlığı
                e.Graphics.DrawString("BİTİŞ TARİHİ", myFont, sbrush, 1000, 110); //Detay başlığı
                e.Graphics.DrawLine(myPen, 50, 125, 770, 125); //Çizgi çizdik.

                int y = 150; //y koordinatının yerini belirledik.(Verilerin yazılmaya başlanacağı yer)

                myFont = new Font("Calibri", 10); //fontu 10 yaptık.

                int i = 0;//satır sayısı için değişken tanımladık.
                while (i <= dgw.Rows.Count)//döngüyü son satırda sonlandıracağız.
                {
                    e.Graphics.DrawString(dgw[0, i].Value.ToString(), myFont, sbrush, 10, y);//1.sütun
                    e.Graphics.DrawString(dgw[1, i].Value.ToString(), myFont, sbrush, 120, y);//2.sütun
                    e.Graphics.DrawString(dgw[2, i].Value.ToString(), myFont, sbrush, 240, y);//3.sütun
                    e.Graphics.DrawString(dgw[3, i].Value.ToString(), myFont, sbrush, 340, y);//4.sütun
                    e.Graphics.DrawString(dgw[4, i].Value.ToString(), myFont, sbrush, 400, y);//5.sütun
                    e.Graphics.DrawString(dgw[5, i].Value.ToString(), myFont, sbrush, 500, y);//1.sütun
                    e.Graphics.DrawString(dgw[6, i].Value.ToString(), myFont, sbrush, 600, y);//2.sütun
                    e.Graphics.DrawString(dgw[7, i].Value.ToString(), myFont, sbrush, 700, y);//3.sütun
                    e.Graphics.DrawString(dgw[8, i].Value.ToString(), myFont, sbrush, 750, y);//4.sütun
                    e.Graphics.DrawString(dgw[9, i].Value.ToString(), myFont, sbrush, 800, y);//5.sütun
                    e.Graphics.DrawString(dgw[10, i].Value.ToString(), myFont, sbrush, 900, y);//1.sütun
                    e.Graphics.DrawString(dgw[11, i].Value.ToString(), myFont, sbrush, 1000, y);//2.sütun
                   
                    //e.Graphics.DrawRectangle(Pens.Black, new System.Drawing.Rectangle(50,
                    //              y, 10, 20));
                    //e.Graphics.DrawRectangle(Pens.Black, new System.Drawing.Rectangle(120,
                    //              y, 150, 20));
                    //e.Graphics.DrawRectangle(Pens.Black, new System.Drawing.Rectangle(350,
                    //              y, 100, 20));
                    //e.Graphics.DrawRectangle(Pens.Black, new System.Drawing.Rectangle(600,
                    //              y, 100, 20));
                    //e.Graphics.DrawRectangle(Pens.Black, new System.Drawing.Rectangle(700,
                    //              y, 100, 20));
                    y += 20; //y koordinatını arttırdık.
                    i += 1;  //satır sayısını arttırdık

                    //yeni sayfaya geçme kontrolü
                    if (y > 1000)
                    {
                        e.Graphics.DrawString("(Devamı -->)", myFont, sbrush, 700, y + 50);
                        y = 50;
                        break; //burada yazdırma sınırına ulaştığımız için while döngüsünden çıkıyoruz
                        //çıktığımızda while baştan başlıyor i değişkeni değer almaya devam ediyor
                        //yazdırma yeni sayfada başlamış oluyor
                    }
                }
                //çoklu sayfa kontrolü
                if (i < SatirSayisi)
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                    i = 0;
                }
                StringFormat myStringFormat = new StringFormat();
                myStringFormat.Alignment = StringAlignment.Far;
            }
            catch
            {
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
                foreach (DataGridViewColumn dgvGridCol in dgw.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
