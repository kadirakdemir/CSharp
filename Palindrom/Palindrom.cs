using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tersini_Bulma
{
    class Palindrom
    {
        static char[,] dizi;
        static Random rnd = new Random();
		
        public static char GetLetter()
        {           
            int num = rnd.Next(0, 5); // Zero to 25
            char let = (char)('a' + num);
            return let;
        }
		
        static public char[,] CharArrayOlustur(int n)
        {           
            dizi= new  char[n,n];
			
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {                    
                    dizi[i,j]= GetLetter();
                }
            }
            return dizi;
        }

        public static bool tersiniBul(int s)
        {            
            char[,] array = new char[s,s];           
            array = CharArrayOlustur(s);
            int k = s-1;
            int yazılanlar=0;
            int sayac = 0;
			
            for (int i = 0; i < s; i++)
            {
                k = s-1;
                for (int j = 0; j < s; j++)
                {
                    if (array[i,j]==array[i,k])
                    {
                        sayac++;
                        if (sayac==s-1)
                        {
                            for (int d = 0; d < s; d++)
                            {                               
                                Console.Write(array[i,d]);                                                              
                            }
                            yazılanlar++;
                            Console.WriteLine();                          
                        }                       
                    }                   
                    k--;   
                }
                sayac = 0;
            }
			
            for (int w = 0; w < s; w++)
            {
                k = s-1;
                for (int y = 0; y < s; y++)
                {
                    if (array[y, w] == array[k, w])
                    {
                        sayac++;
                        if (sayac == s-1)
                        {
                            for (int d = 0; d < s; d++)
                            {
                                Console.Write(array[d, w]);
                            }
                            yazılanlar++;
                            Console.WriteLine();                          
                        }
                    }                  
                    k--;
                }
                sayac = 0;
            }
			
            if (yazılanlar==0)
            {                
                Console.Write("Bir değer giriniz: ");
                Console.WriteLine(tersiniBul(Convert.ToInt32(Console.ReadLine())));
                return false;
            }
			
            return true;
        }
		
        static void Main(string[] args)
        {
            Console.Write("Bir değer giriniz: ");
            Console.WriteLine(tersiniBul(Convert.ToInt32( Console.ReadLine())));
            Console.ReadKey();
        }
    }
}
