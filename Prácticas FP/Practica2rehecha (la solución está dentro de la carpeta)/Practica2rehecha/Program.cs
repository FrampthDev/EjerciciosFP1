// Javier Zazo Morillo
// Miguel Ángel González López
namespace naves
{
    class Program
    {
        const bool DEBUG = false;
        const int ANCHO = 30, ALTO = 15;
        static Random rnd = new Random(11);

        static void Main()
        {
            Console.CursorVisible = false;

            int[] suelo = new int[ANCHO], // límites del tunel
                  techo = new int[ANCHO];

            int naveC, naveF,
                balaC, balaF,
                enemigoC, enemigoF,
                colC, colF;

            bool crashNave = false;

            IniciaTunel(suelo, techo);

            naveC = ANCHO / 2;
            naveF = (techo[naveC] + suelo[naveC]) / 2;

            balaC = -1;
            balaF = -1;

            enemigoC = -1;
            enemigoF = -1;

            colC = -1;
            colF = -1;

            Render(suelo, techo, enemigoC, enemigoF, naveC, naveF, balaC, balaF, colC, colF, crashNave);

            while (!crashNave)
            {
                char ch = LeeInput();
                if (ch == 'q')
                {
                    crashNave = true;
                }
                else if (ch == 'p')
                {
                    Console.SetCursorPosition(ANCHO * 2, 0);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Pausa");

                    Console.SetCursorPosition(ANCHO * 2, 1);
                    Console.Write("Pulsa enter para continuar");

                    while (Console.ReadKey(true).Key.ToString() != "Enter")
                    {

                    }
                }
                else
                {
                    AvanzaTunel(suelo, techo);
                    GeneraAvanzaEnemigo(ref enemigoC, ref enemigoF, suelo, techo);
                    AvanzaNave(ch, ref naveC, ref naveF, enemigoC, enemigoF, suelo, techo);

                    crashNave = ColisionNave(naveC, naveF, suelo, techo, enemigoC, enemigoF);

                    GeneraAvanzaBala(ch, ref balaC, ref balaF, naveC, naveF, enemigoC, enemigoF, suelo, techo);

                    ColisionBala(ref balaC, balaF, ref enemigoC, enemigoF, suelo, techo, out colC, out colF);

                    Render(suelo, techo, enemigoC, enemigoF, naveC, naveF, balaC, balaF, colC, colF, crashNave);

                    if (DEBUG)
                    {
                        Console.SetCursorPosition(0, ALTO);
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.WriteLine("Techo = " + techo[ANCHO - 1]);
                        Console.WriteLine("Suelo = " + suelo[ANCHO - 1]);
                        Console.WriteLine("EnemigoC = " + enemigoC);
                        Console.WriteLine("EnemigoF = " + enemigoF);
                        Console.WriteLine("NaveC = " + naveC);
                        Console.WriteLine("NaveF = " + naveF);
                        Console.WriteLine("BalaC = " + balaC);
                        Console.WriteLine("BalaF = " + balaF);
                    }
                    Thread.Sleep(120);
                }
            } // while
        } // Main

        static void AvanzaTunel(int[] suelo, int[] techo)
        {
            for (int i = 0; i < ANCHO - 1; i++)
            { // desplazamiento de eltos a la izda
                techo[i] = techo[i + 1];
                suelo[i] = suelo[i + 1];
            }

            int s, t; // ultimas posiciones de suelo y techo
            s = suelo[ANCHO - 1];
            t = techo[ANCHO - 1];

            // generamos nuevos valores para esas ultimas posiciones
            int opt = rnd.Next(5); // 5 alternativas
            if (opt == 0 && s < ALTO - 1) { s++; t++; }   // tunel baja           
            else if (opt == 1 && t > 0) { s--; t--; }   // sube
            else if (opt == 2 && s - t > 7) { s--; t++; } // estrecha
            else if (opt == 3)
            {                    // ensancha
                if (s < ALTO - 1) s++;
                if (t > 0) t--;
            } // con 4 se deja igual, no se hace nada

            // asignamos ultimas posiciones en el array
            suelo[ANCHO - 1] = s;
            techo[ANCHO - 1] = t;
        }
        static char LeeInput()
        {
            char ch = ' ';
            if (Console.KeyAvailable)
            {
                string dir = Console.ReadKey(true).Key.ToString();
                if (dir == "A" || dir == "LeftArrow") ch = 'l';
                else if (dir == "D" || dir == "RightArrow") ch = 'r';
                else if (dir == "W" || dir == "UpArrow") ch = 'u';
                else if (dir == "S" || dir == "DownArrow") ch = 'd';
                else if (dir == "X" || dir == "Spacebar") ch = 'x'; // bomba                   
                else if (dir == "P") ch = 'p'; // pausa					
                else if (dir == "Q" || dir == "Escape") ch = 'q'; // salir
                while (Console.KeyAvailable) Console.ReadKey().Key.ToString();
            }
            return ch;
        }
        static void Render(int[] suelo, int[] techo, int enemigoC, int enemigoF, int naveC, int naveF, int balaC, int balaF, int colC, int colF, bool crashNave)
        {
            RenderTunel(suelo, techo);

            if (enemigoC >= 0)
            {
                Console.SetCursorPosition(enemigoC * 2, enemigoF);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("<>");
            }

            Console.SetCursorPosition(naveC * 2, naveF);

            if (!crashNave)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("=>");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("**");
            }

            if (balaC >= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(balaC * 2, balaF);
                Console.Write("--");
            }

            if (colC >= 0)
            {
                Console.SetCursorPosition(colC * 2, colF);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("**");
            }
        }
        static void RenderTunel(int[] suelo, int[] techo)
        {
            Console.Clear();

            for (int i = 0; i < ANCHO; i++)
            {
                for (int j = 0; j < ALTO; j++)
                {
                    if (j <= techo[i] || j >= suelo[i]) // Se escribe un cuadrado azul para el techo y sus casillas superiores y para el suelo y sus casillas inferiores
                    {
                        Console.SetCursorPosition(i * 2, j);
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
            }
        }
        static void IniciaTunel(int[] suelo, int[] techo)
        {
            techo[ANCHO - 1] = 0;
            suelo[ANCHO - 1] = ALTO - 1; // El túnel empieza siendo lo más ancho posible

            for (int i = 0; i < ANCHO - 1; i++)
            {
                AvanzaTunel(suelo, techo); // Ejecutamos la función de AvanaTunel Ancho - 1 veces para rellenar el resto del túnel
            }
        }
        static void GeneraAvanzaEnemigo(ref int enemigoC, ref int enemigoF, int[] suelo, int[] techo)
        {
            if (enemigoC == -1 && rnd.Next(0, 4) == 0)
            {
                enemigoC = ANCHO - 1;
                enemigoF = rnd.Next(techo[enemigoC], suelo[enemigoC]);
            }
            else if (enemigoC >= 0)
            {
                enemigoC--;
            }
        }
        static void AvanzaNave(char ch, ref int naveC, ref int naveF, int enemigoC, int enemigoF, int[] suelo, int[] techo)
        {
            if ((naveC == enemigoC && naveF == enemigoF) || (naveF == techo[naveC]) || (naveF == suelo[naveC]))
            {

            }
            else if (naveC > 0 && ch == 'l')
            {
                naveC--;
            }
            else if (naveC < ANCHO - 1 && ch == 'r')
            {
                naveC++;
            }
            else if (ch == 'u')
            {
                naveF--;
            }
            else if (ch == 'd')
            {
                naveF++;
            }
        }
        static void GeneraAvanzaBala(char ch, ref int balaC, ref int balaF, int naveC, int naveF, int enemigoC, int enemigoF, int[] suelo, int[] techo)
        {
            if (balaC == -1 && ch == 'x')
            {
                balaC = naveC + 1;
                balaF = naveF;
            }
            else if ((balaC >= 0) && (balaF > techo[balaC]) && (balaF < suelo[balaC]) && (balaC != enemigoC || balaF != enemigoF))
            {
                balaC++;
            }
        }
        static bool ColisionNave(int naveC, int naveF, int[] suelo, int[] techo, int enemigoC, int enemigoF)
        {
            bool crashNave = false;

            if (naveF <= techo[naveC] || naveF >= suelo[naveC] || (naveC == enemigoC && naveF == enemigoF))
            {
                crashNave = true;
            }
            return crashNave;
        }
        static void ColisionBala(ref int balaC, int balaF, ref int enemigoC, int enemigoF, int[] suelo, int[] techo, out int colC, out int colF)
        {
            if (balaC == ANCHO)
            {
                balaC = -1;
            }
            if (balaC >= 0)
            {
                if (balaC == enemigoC && balaF == enemigoF)
                {
                    colC = balaC;
                    colF = balaF;

                    balaC = -1;
                    enemigoC = -1;
                }
                else if (balaF <= techo[balaC])
                {
                    colC = balaC;
                    colF = balaF;

                    balaC = -1;

                    techo[colC] = colF - 1;
                }
                else if (balaF >= suelo[balaC])
                {
                    colC = balaC;
                    colF = balaF;

                    balaC = -1;

                    suelo[colC] = colF + 1;
                }
                else
                {
                    colC = colF = -1;
                }
            }
            else
            {
                colC = colF = -1;
            }
        }
    }
}