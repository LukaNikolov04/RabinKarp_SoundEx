using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19294C_
{
    public class Program
    {
        public static readonly Stopwatch watch = new Stopwatch();
        public static readonly char[] par = { ' ', ',', '.'};
        static void Rabin_Karp_algoritam(String T,String P, int d = 256, int q = 113)
        {
            watch.Restart();
            int n = T.Length;
            int m = P.Length;


            if (n == 0 || m == 0 || m > n || d == 0 || q == 0)
            {
                Console.WriteLine("Neodgovarajuci parametri ! ");
                return;
            }



            int i;
            int j;
            int p = 0;
            int t = 0;
            int h = 1;
          

            for(i = 0;i<m;i++)
            {
                p = (p * d + P[i]) % q;
                t = (t * d + T[i]) % q;
                if(i != m-1)
                    h = (h * d) % q;
            }

            i = 0;

            while(i <= n-m)
            {

                if(p == t)
                {
                    j = 0;
                    while(j < m && P[j] == T[i+j])
                    {
                        j++;
                    }

                    if (j == m)                  
                        Console.WriteLine("Podstring pronadjen na indeksu: " + i + '\n');
                    
                }

                if(i < n-m)
                {
                    t = (d * (t - T[i] * h) + T[i + m]) % q;

                    if (t < 0)
                        t += q;
                }
                i++;
            }
            watch.Stop();
            Console.WriteLine("Vreme izvrsenja Rabin Karp algoritma za " + m + " karaktera : " + watch.Elapsed + " s \n");
        }

        static String SoundEx_Code(String P)
        {
            if (P.Length == 0)
            {
                //Console.WriteLine("Prazan string ! ");
                String s = "";
                return s;
            }

            String copyP = P.ToLower();
            String mappiranje = "a123e12hi22455o12623u1w2y2";
            int codep;
            int i;
            char prvoslovo = P.ElementAt(0);
            char[] p = copyP.ToCharArray();

            for(i = 0; i < copyP.Length; i++) 
            {                
                codep = p[i] - 97;
                if(codep >= 0 && codep <= 25) // ukoliko jeste slovo 
                {
                    p[i] = mappiranje.ElementAt(codep);                    
                }
                else
                {
                    // ukoliko nije slovo (neki drugi ASCII znak), ukloni ga
                    copyP = new string(p);
                    copyP = copyP.Remove(i, 1);
                    p = copyP.ToCharArray();
                    if (i == 0)
                    {// ukoliko prvi znak nije slovo
                        if (copyP.Length != 0)
                            prvoslovo = copyP[0];
                        else
                            prvoslovo = '0';
                    }
                    i--;                   
                }
            }

            copyP = new string(p);

            // ukoliko su dva susedna slova kodirana istom cifrom, ostaje samo prva cifra
            for(i = 0; i < copyP.Length - 1; i++)
            {
                if (copyP[i] == copyP[i + 1])
                    copyP = copyP.Remove(i + 1, 1);
            }

            // ukoliko su dva slova sa istim kodom razdvojena sa 'h' ili 'w', ostaje samo prva cifra
            for(i = 1; i < copyP.Length - 2; i++)
            {
                if (copyP[i] == copyP[i+2] && (copyP[i+1] == 'h' || copyP[i+1] == 'w'))
                {
                    copyP = copyP.Remove(i + 2, 1);
                    i++;
                }
            }

            // brisanje slova (samoglasnici + 'h' + 'w')
            for(i = 1; i < copyP.Length; i++)
            {
                if (copyP[i] >= 97 && copyP[i] <= 122)
                {
                    copyP = copyP.Remove(i, 1);
                    i--;
                }
            }

            if(copyP.Length > 4)
            {
                copyP = copyP.Substring(0, 4);
            }
            else if(copyP.Length < 4)
            {
                while (copyP.Length != 4)
                    copyP = String.Concat(copyP, "0");
            }

            p = copyP.ToCharArray();
            p[0] = prvoslovo;

            String codeP = new string(p);

           Console.WriteLine("Za rec " + P + " kod je : " + codeP + '\n');

            return codeP;
        }

        static void SoundEx_algoritam(String T,String P)
        {
            watch.Restart();
            if(T.Length == 0 || P.Length == 0)
            {
                Console.WriteLine("Nevalidni parametri ! ");
                return;
            }

            String codeP = SoundEx_Code(P);

            string[] words = T.Split(par);

            foreach (string word in words)
            {
                String codeword = SoundEx_Code(word);
                if(codeword == codeP)
                {
                    if(word != P)
                    {
                        Console.WriteLine("Rec " + P + " zvuci kao rec " + word + '\n');
                    }
                }
            }
            watch.Stop();
            Console.WriteLine("Vreme izvrsenja SoundEx algoritma : " + watch.Elapsed + " s \n");
        }

        static void Main(string[] args)
        {
            //primeri 
            String ascii5podstring = "Kitty";
            String ascii10podstring = "impression";
            String ascii20podstring = " looking out in dark";
            String ascii50podstring = "he stood listening to her father, and glancing at ";
            String hex5podstring = "17420";
            String hex10podstring = "e672062726";
            String hex20podstring = "2061732068652073746f";
            String hex50podstring = "696e6720746f20686572206661746865722c20616e6420676c";
            String sound5podstring = "borne";
            String sound10podstring = "beneficence";
            String sound20podstring = "reconstructionism";


            List<String> ASCIItxt = new List<string>();
            List<String> Hextxt = new List<string>();
            List<String> Soundtxt = new List<string>();

            StreamReader ascii1 = null, ascii2 = null, ascii3 = null, ascii4 = null;
            StreamReader hex1 = null, hex2 = null, hex3 = null, hex4 = null;
            StreamReader sound1 = null, sound2 = null, sound3 = null, sound4 = null;

            try
            {
                ascii1 = new StreamReader("ASCII100.txt");
                ascii2 = new StreamReader("ASCII1000.txt");
                ascii3 = new StreamReader("ASCII10000.txt");
                ascii4 = new StreamReader("ASCII100000.txt");

                hex1 = new StreamReader("HEX100.txt");
                hex2 = new StreamReader("HEX1000.txt");
                hex3 = new StreamReader("HEX10000.txt");
                hex4 = new StreamReader("HEX100000.txt");

                sound1 = new StreamReader("SOUND100.txt");
                sound2 = new StreamReader("SOUND1000.txt");
                sound3 = new StreamReader("SOUND10000.txt");
                sound4 = new StreamReader("SOUND100000.txt");

                ASCIItxt.Add(ascii1.ReadToEnd());
                ASCIItxt.Add(ascii2.ReadToEnd());
                ASCIItxt.Add(ascii3.ReadToEnd());
                ASCIItxt.Add(ascii4.ReadToEnd());

                Hextxt.Add(hex1.ReadToEnd());
                Hextxt.Add(hex2.ReadToEnd());
                Hextxt.Add(hex3.ReadToEnd());
                Hextxt.Add(hex4.ReadToEnd());

                Soundtxt.Add(sound1.ReadToEnd());
                Soundtxt.Add(sound2.ReadToEnd());
                Soundtxt.Add(sound3.ReadToEnd());
                Soundtxt.Add(sound4.ReadToEnd());
            }
            finally
            {
                ascii1.Close();
                ascii2.Close();
                ascii3.Close();
                ascii4.Close();

                hex1.Close();
                hex2.Close();
                hex3.Close();
                hex4.Close();

                sound1.Close();
                sound2.Close();
                sound3.Close();
                sound4.Close();
            }

            int br = 100;

            SoundEx_Code("Ohajo");

            //for (int i = 0; i < 4; i++)
            //{
            //    Console.WriteLine("      TXT      \n");
            //    Console.WriteLine("Performanse za tekst duzine " + br + ": \n");

            //    Rabin_Karp_algoritam(ASCIItxt[i], ascii5podstring);
            //    Rabin_Karp_algoritam(ASCIItxt[i], ascii10podstring);
            //    Rabin_Karp_algoritam(ASCIItxt[i], ascii20podstring);
            //    Rabin_Karp_algoritam(ASCIItxt[i], ascii50podstring);

            //    SoundEx_algoritam(Soundtxt[i], sound5podstring);
            //    SoundEx_algoritam(Soundtxt[i], sound10podstring);
            //    SoundEx_algoritam(Soundtxt[i], sound20podstring);

            //    Console.WriteLine("      HEX      \n");
            //    Console.WriteLine("Performanse za hexa tekst duzine " + br + ": \n");

            //    Rabin_Karp_algoritam(Hextxt[i], hex5podstring, 16);
            //    Rabin_Karp_algoritam(Hextxt[i], hex10podstring, 16);
            //    Rabin_Karp_algoritam(Hextxt[i], hex20podstring, 16);
            //    Rabin_Karp_algoritam(Hextxt[i], hex50podstring, 16);

            //    br *= 10;
            //    Console.ReadLine();
            //}

            Console.WriteLine("KRAJ \n");
            Console.ReadLine();

        }
    }
}
