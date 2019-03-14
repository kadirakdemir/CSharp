using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dosyaislemler
{
    public class Program
    {
        class Student
        {
            public int ogrenciID { get; set; }
            public string ogrenciAd { get; set; }
            public string ogrenciSoyAd { get; set; }            
            public int ogrenciBolumID { get; set; }     
        }

        class Bolumler
        {
            public int bolumID { get; set; }
            public string bolumAd { get; set; }           
            public int bolumFakulteID { get; set; }         

        }

        class Dersler
        {
            public int dersID { get; set; }
            public string dersAd { get; set; }           
            public int dersBolumID { get; set; }

        }

        class Fakulte
        {
            public int fakulteID { get; set; }
            public string fakulteAd { get; set; }           
            public int fakulteBolumID { get; set; }

        }

        class Akademisyen
        {
            public int akademisyenID { get; set; }
            public string akademisyenAd { get; set; }
            public string akademisyenSoyad { get; set; }
            public int akademisyenBolumID { get; set; }

        }

        static Random rnd = new Random();
        public static char GetLetter()
        {
            int num = rnd.Next(0, 26); // Zero to 25
            char let = (char)('a' + num);
            return let;
        }

        public static void Olustur()
        {
            int sayac1 = 0;
            //string listede Fakulte isimleri oluşturuluyor
            string[] fakulteisim = { "Muhendislik Fakültesi", "Tıp Fakültesi", "Fen Edebiyat Fakültesi", "Truzim Fakültesi", "Isletme Fakültesi", "Hukuk Fakültesi", "Saglık Bilimleri Fakültesi", "Mimarlik Fakültesi", "Ziraat Fafülteis","Ilahiyat Fakültesi" };
            string[] fakulte = new string[10];           

            // fakulte oluşturdaki string ifade fakulte[] dizisine altarılıyor
            for (int i = 0; i < 10; i++)
            {
               fakulte[i] = i.ToString() + "," + fakulteisim[i];
            }

            //string listede Bolum isimleri oluşturuluyor
            string[] bolumisim = { "Bilgisayar Muhendisligi", "Makina Muhendisligi","Tıp", "Edebiyat","Matematik", "Truzim", "Isletme Yonetimi","Isletme", "Hukuk", "Hemsirelik", "Ic Mimarlik","Peyzaj Mimarlık","Mimarlık", "Ziraat Mühendisliği", "Ilahiyat" };
            string[] bolum = new string[15];

            // bolum oluşturdaki string ifade bolum[] dizisine altarılıyor
            for (int i = 0; i < 15; i++)
            {
                if (sayac1 == 10)
                    sayac1 = 0;
               bolum[i] = i.ToString() + "," + bolumisim[i]+","+sayac1;
                sayac1++;
            }

            //string listede Akademisyen isimleri oluşturuluyor
            string[] akademisyenisim = { "ali", "veli", "hasan", "recep", "pınar", "sema", "ayse", "emine", "rıfkı", "serkan", "riza", "canan", "gülen", "kamuran", "aytac","temel","mehmet","basri","handan","hatice" };
            string[] akademisyensoyisim = { "yılmaz", "aksi", "yurt", "boluk", "serap", "akdogan", "agaoglu", "agaoglu", "terzi", "terzioglu", "bilim", "kaya", "ortac", "gurses", "kara","borsan","yugi","turk","galip","dervis" };
            string[] akademisyen = new string[20];           
            sayac1 = 0;
            // akademisyen oluşturdaki string ifade akademisyen[] dizisine altarılıyor
            for (int i = 0; i < 20; i++)
            {
                if (sayac1 == 15)
                    sayac1 = 0;
               akademisyen[i] = i.ToString() + "," + akademisyenisim[i] +","+akademisyensoyisim[i]+ "," + sayac1;               
                sayac1++;
            }

            //string listede Oğrenci isimleri oluşturuluyor
            string[] ogrenciisim = { "ali", "veli", "hasan", "recep", "pınar", "sema", "ayse", "emine", "rıfkı", "serkan", "riza", "canan", "gülen", "kamuran", "aytac", "temel", "mehmet", "basri", "handan", "hatice" };
            string[] ogrencisoyisim = { "yılmaz", "aksi", "yurt", "boluk", "serap", "akdogan", "agaoglu", "agaoglu", "terzi", "terzioglu", "bilim", "kaya", "ortac", "gurses", "kara", "borsan", "yugi", "turk", "galip", "dervis" };
            string[] ogrenci = new string[40];            
            sayac1 = 0;
            // ogrenci oluşturdaki string ifade akademisyen[] dizisine altarılıyor
            for (int i = 0; i < 20; i++)
            {
                if (sayac1 == 15)
                    sayac1 = 0;
              ogrenci[i] = i.ToString() + "," + ogrencisoyisim[i] + "," + ogrenciisim[i] + "," + sayac1;
                sayac1++;
            }

            for (int i = 0; i < 20; i++)
            {
                if (sayac1 == 15)
                    sayac1 = 0;
                ogrenci[i+20] = (20+i) + "," + ogrencisoyisim[19-i] + "," + ogrenciisim[i] + "," + sayac1;
                sayac1++;
            }

            string[] dersisim = { "Algoritma Analizi", "Bilgisayar Mimarisi", "Yapay Zeka", "Gorsel Programlama", "Matematik" };
            string[] ders = new string[135];

            // fakulte oluşturdaki string ifade bolum[] dizisine altarılıyor
            for (int i = 0; i < 100; i++)
            {
                if (sayac1 == 15)
                    sayac1 = 0;
               ders[i] = i.ToString() + "," + GetLetter() + GetLetter() + GetLetter() + "," + sayac1;
                sayac1++;
            }
            sayac1 = 0;
            int sayac2 = 0;
            for (int i = 0; i < 35; i++)
            {
                sayac2++;
                if (sayac2 == 5)
                    sayac2 = 0;
                if (sayac1 == 15)
                    sayac1 = 0;
                ders[i+100] = i+100 + "," + dersisim[sayac2] + "," + sayac1;
                sayac1++;
            }

            // txt dosyalarına oluşturlan string diziler yazılıyor
            System.IO.File.WriteAllLines(@"C:\Users\MK\Desktop\Algoritma Analizi Ödev\Soru1\ogrenciler.txt", ogrenci);
            System.IO.File.WriteAllLines(@"C:\Users\MK\Desktop\Algoritma Analizi Ödev\Soru1\akademisyenler.txt", akademisyen);
            System.IO.File.WriteAllLines(@"C:\Users\MK\Desktop\Algoritma Analizi Ödev\Soru1\fakulteler.txt", fakulte);
            System.IO.File.WriteAllLines(@"C:\Users\MK\Desktop\Algoritma Analizi Ödev\Soru1\bolumler.txt", bolum);
            System.IO.File.WriteAllLines(@"C:\Users\MK\Desktop\Algoritma Analizi Ödev\Soru1\dersler.txt", ders);
        }

        static void Main(string[] args)
        {
            Olustur();

           string[] ogrenciler = System.IO.File.ReadAllLines(@"C:\Users\MK\Desktop\Algoritma Analizi Ödev\Soru1\ogrenciler.txt");
           string[] bolumler = System.IO.File.ReadAllLines(@"C:\Users\MK\Desktop\Algoritma Analizi Ödev\Soru1\bolumler.txt");
           string[] dersler = System.IO.File.ReadAllLines(@"C:\Users\MK\Desktop\Algoritma Analizi Ödev\Soru1\dersler.txt");
           string[] fakulteler = System.IO.File.ReadAllLines(@"C:\Users\MK\Desktop\Algoritma Analizi Ödev\Soru1\fakulteler.txt");
           string[] akademisyenler = System.IO.File.ReadAllLines(@"C:\Users\MK\Desktop\Algoritma Analizi Ödev\Soru1\akademisyenler.txt");

           IEnumerable<Student> ogrenciSorgu =
           from nameLine in ogrenciler
           let splitName = nameLine.Split(',')

           select new Student()
           {
               ogrenciID = Convert.ToInt32(splitName[0]),
               ogrenciAd = splitName[1],
               ogrenciSoyAd = splitName[2],               
               ogrenciBolumID = Convert.ToInt32(splitName[3])
           };


          IEnumerable<Bolumler> bolumSorgu =
          from nameLine in bolumler
          let splitName = nameLine.Split(',')

          select new Bolumler()
          {
              bolumID = Convert.ToInt32(splitName[0]),
              bolumAd = splitName[1],  
              bolumFakulteID= Convert.ToInt32(splitName[2])
          };

         IEnumerable<Dersler> dersSorgu =
         from nameLine in dersler
         let splitName = nameLine.Split(',')

         select new Dersler()
         {
             dersID = Convert.ToInt32(splitName[0]),
             dersAd = splitName[1], 
             dersBolumID = Convert.ToInt32(splitName[2])
         };

          IEnumerable<Fakulte> fakulteSorgu =
          from nameLine in fakulteler
          let splitName = nameLine.Split(',')

          select new Fakulte()
          {
              fakulteID = Convert.ToInt32(splitName[0]),
              fakulteAd = splitName[1],          
            
          };


          IEnumerable<Akademisyen> akademisyenSorgu =
          from nameLine in akademisyenler
          let splitName = nameLine.Split(',')

          select new Akademisyen()
          {
              akademisyenID = Convert.ToInt32(splitName[0]),
              akademisyenAd = splitName[1],
              akademisyenSoyad = splitName[2],
              akademisyenBolumID = Convert.ToInt32(splitName[3])
          };

            List<Student> student = ogrenciSorgu.ToList();
            List<Bolumler> departments = bolumSorgu.ToList();
            List<Dersler> lesson = dersSorgu.ToList();
            List<Fakulte> faculty = fakulteSorgu.ToList();
            List<Akademisyen> academician = akademisyenSorgu.ToList();

            //foreach (var student in ogrenci)
            //{
            //    Console.WriteLine(student.ID + " " + student.FirstName + " " + student.LastName + " " + student.Bolum + " " + student.Danısman);
            //}
            var sorgu = (from OGRENCİ in student                         
                         from BOLUMLER in departments
                         from DERSLER  in lesson
                         from FAKULTE in faculty
                         from AKADEMİSYEN in academician
                         where FAKULTE.fakulteID==BOLUMLER.bolumFakulteID & BOLUMLER.bolumID==OGRENCİ.ogrenciBolumID & BOLUMLER.bolumID==AKADEMİSYEN.akademisyenBolumID&BOLUMLER.bolumID==DERSLER.dersBolumID &DERSLER.dersAd== "Algoritma Analizi"
                         select new
                         {
                             Fakulte=FAKULTE.fakulteAd,
                             Bolum=BOLUMLER.bolumAd,
                             Ders=DERSLER.dersAd,
                             AkademisyenAdı=AKADEMİSYEN.akademisyenAd,
                             AkademiyenSoyadı=AKADEMİSYEN.akademisyenSoyad,
                             OgrenciAd=OGRENCİ.ogrenciAd,
                             OgrenciSoyad=OGRENCİ.ogrenciSoyAd
                         }).Distinct();
           
            foreach (var item in sorgu)
            {
                Console.WriteLine("Fakülte            : "+" "+item.Fakulte+" ");
                Console.WriteLine("Bölüm              : "+" "+item.Bolum+" ");
                Console.WriteLine("Ders               : "+" "+item.Ders + " ");
                Console.WriteLine("Akademisyen Adı    : "+" "+item.AkademisyenAdı + " ");
                Console.WriteLine("Akademisyen Soyadı : "+" "+item.AkademiyenSoyadı + " ");
                Console.WriteLine("Öğrenci Adı        : "+" "+item.OgrenciAd + " ");
                Console.WriteLine("Öğrenci Soyadı     : "+" "+item.OgrenciSoyad + " ");
                Console.WriteLine(" ");

            }
            Console.ReadKey();
        }
    }
}
