using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaliyetRaporuUygulamasi
{
    public class PDFExport
    {
        #region Değişkenler
        public string FileName { get; set; } //pdf oluşturacağımız dosya adı
        public string Text { get; set; } //dosyanın içinde oluşturacağımız pdf adı
        public int PdfRowIndex { get; set; } //pdfrowindex
        public string Path { get; set; }
        #endregion
        
        public DataTable ToDatatable()
        {
            //DataTable Oluştur            
            DataTable dt = new DataTable("");
            dt.Columns.Add(new DataColumn("", typeof(string)));
            dt.Columns.Add(new DataColumn("", typeof(string)));
            dt.Columns.Add(new DataColumn("", typeof(string)));
            dt.Columns.Add(new DataColumn("", typeof(string)));
            dt.Columns.Add(new DataColumn("", typeof(string)));
            dt.Columns.Add(new DataColumn("", typeof(string)));
            dt.Columns.Add(new DataColumn("", typeof(string)));
            dt.Columns.Add(new DataColumn("", typeof(string)));
            dt.Columns.Add(new DataColumn("", typeof(string)));
            dt.Columns.Add(new DataColumn("", typeof(string)));
            dt.Columns.Add(new DataColumn("", typeof(string)));
            dt.Columns.Add(new DataColumn("", typeof(string)));            
            return dt;
        }


        public void ToPdf(DataGridView kayitlar)
        {           
            try
            {
                //float[] genislik = { 6.5f, 2.5f, 6, 7, 5, 5, 5.5f, 4.5f, 5, 5.5f, 6, 6 };  // 5 punto
                float[] genislik = { 5.5f, 3, 5, 13.5f, 5.5f, 5.5f, 4.5f, 5.5f, 4.5f, 4, 4.5f, 4.5f };  //6 punto
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = "C:";
                saveFileDialog.Title = "Excel Kayıt";
                saveFileDialog.FileName = "";
                saveFileDialog.Filter = "PDF|*.pdf";
                if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
                {
                    DataTable dtPDF = ToDatatable();
                    iTextSharp.text.Document document = new iTextSharp.text.Document();
                    PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName.ToString(), FileMode.Create));
                    BaseFont arial = BaseFont.CreateFont("C:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font font = new Font(arial, 6, Font.NORMAL);
                    document.Open();
                    PdfPTable pdfTable = null;
                    pdfTable = new PdfPTable(genislik) { WidthPercentage = 110 };

                    //pdfTable.WidthPercentage = 112;
                    string str = string.Empty;
                    for (int i = 0; i < kayitlar.Columns.Count; i++)
                    {
                        str += kayitlar.Columns[i].HeaderText;

                        if (kayitlar.Columns.Count > i)
                            str += "+";
                    }

                    string str2 = str.TrimEnd(' ', '+').ToString();

                    ///<summary></summary>
                    /// DataGridView kolonlarının sayısı kadar belgenin başlıkları doldurulur.
                    /// Pdf hücreleri oluşturulur.Dökumandaki başlık kısmı için ilk satır oluşturulur ve colspan yapılır.
                    ///
                    ///<summary></summary>
                    ///pdf tablosu hücreleri doldurulur
                    ///

                    DataGridViewRow row;
                    DataGridViewColumn column;
                    for (int i = 0; i < kayitlar.Columns.Count; i++)
                    {
                        column = kayitlar.Columns[i];
                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, font));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 40, 40);

                        pdfTable.AddCell(cell);
                    }

                    for (int i = 0; i < kayitlar.Rows.Count - 1; i++)
                    {
                        row = kayitlar.Rows[i];
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            pdfTable.AddCell(new Phrase(cell.Value.ToString(), font));
                        }
                    }

                    document.Add(pdfTable);

                    MessageBox.Show("Kaydınız Başarıyla Tamamlanmıştır!" + "\n" + "Kayıt Yeri" + " " + saveFileDialog.FileName.ToString(), "Aktarım Sonucu", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    document.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata! Dosya başka program tarafından kullanılıyor olabilir.");               
            }
        }

        /// <summary></summary>
    }
}
