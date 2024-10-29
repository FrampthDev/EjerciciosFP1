// Javier Zazo Morillo
// Miguel Ángel González López
using System.Security.Cryptography.X509Certificates;

namespace Práctica_1
{
    class MainClass
    {
        const int DELTA = 400;
        const int FILS = 14, COLS = 22;
        
        static Random rnd = new Random(); // aleatorios para movimiento del enemigo

        public static void Main(string[] args)
        {
            // Console.SetWindowSize(COLS,FILS); // para poner consola de tamaño COLSxFILS // Rompe el juego
            
            Console.CursorVisible = false; // ocultamos cursor en pantalla

            int minCol=0, maxCol=0,
                minFil=0,maxFil=0,
                aleatorioCol, aleatorioFil,
                jugCol, jugFil,
                eneCol, eneFil,
                balaFil = -1, balaCol = -1,
                bombaCol = -1, bombaFil= -1,
                finPartida=0; // 0 jugando; 1 gana jugador; 2 gana enemigo; 3 abortar

            bool hayBala = false;
            bool hayBomba = false;

            jugCol = COLS / 4; 
            jugFil = FILS * 3 / 4;
            eneCol = COLS / 2;
            eneFil = FILS / 4;

            Console.SetCursorPosition(0,0);

            // bucle ppal
            while (finPartida==0) {
                // recogida de INPUT DE USUARIO
                string dir = "";
                if (Console.KeyAvailable)
                { // si se detecta pulsación de tecla
                  // leemos input y transformamos a string
                    dir = (Console.ReadKey(true)).KeyChar.ToString();                    
                    while (Console.KeyAvailable) Console.ReadKey(true); // limpiamos buffer de entrada						
                }

                // lógica de jugador 

                if (dir == "w" && jugFil > 0)
                {
                    jugFil--;
                }
                else if (dir == "a" && jugCol > 0)
                {
                    jugCol--;
                }
                else if (dir == "s" && jugFil < FILS-1)
                {
                    jugFil++;
                }
                else if (dir == "d" && jugCol < COLS-1)
                {
                    jugCol++;
                }

                // lógica de la bala

                else if (dir == "l" && !hayBala)
                {
                    balaCol = jugCol;
                    balaFil = jugFil;
                    hayBala = true;
                }

                if (hayBala)
                {
                    balaFil--;
                    
                    if (balaFil < 0)
                    {
                        hayBala = false;
                    }
                }

                // lógica del enemigo
                if (eneCol > 0)
                    minCol = -1;
                else minCol = 0;
                
                if (eneCol < COLS-2)
                    maxCol = 1;
                else maxCol = 0;

                if (eneFil > 0)
                    minFil = -1;
                else minFil = 0;
                
                if (eneFil < FILS/2)
                    maxFil = 1;
                else maxFil = 0;
                    
                aleatorioCol = rnd.Next(minCol, maxCol + 1);
                aleatorioFil = rnd.Next(minFil, maxFil + 1);
                //Console.WriteLine(eneCol+ " "+eneFil+" "+aleatorioCol + " " + aleatorioFil); //Debug
                
                eneCol += aleatorioCol;
                eneFil += aleatorioFil;

                // el enemigo puede salirse del mapa.

                // lógica de la bomba 

                if (!hayBomba)
                {
                    bombaCol=eneCol;
                    bombaFil=eneFil;
                    hayBomba=true;
                }

                if (hayBomba)
                {
                    bombaFil--;

                    if (bombaFil > FILS - 1)
                    {
                        hayBomba = false;
                    }
                }

                // colisiones

                if (balaFil == bombaFil && balaCol == bombaCol) // colisiones bala-bomba
                {
                    hayBala = false;
                    hayBomba = false;
                }


                if ((balaFil == eneFil && balaCol == eneCol) 
                || (balaFil == eneFil && balaCol == eneCol+1) 
                || (balaFil == eneFil && balaCol == eneCol+2)) // colisiones enemigo-bala
                {
                    finPartida=1;
                }

                if (bombaFil == jugFil && bombaCol == jugCol) // colisiones jugador-bomba
                {
                    finPartida=2;
                }

                if (jugCol == eneCol && jugFil == eneFil
                || jugCol == eneCol+1 && jugFil == eneFil
                || jugCol == eneCol+2 && jugFil == eneFil)  // colisiones jugador-enemigo
                {
                    finPartida=2;
                }

                // RENDERIZADO 

                // Recorre una matriz y dibuja el mapa

                Console.Clear();

                for (int f = 0; f <= FILS; f++)
                {
                    for (int c = 0; c <= COLS; c++)
                    {
                        if (jugCol == c && jugFil == f) // Renderizado jugador
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("o");
                        }
                        else if (hayBala && balaCol == c && balaFil == f) // Renderizado bala
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("^");
                        }
                        else if (eneCol == c && eneFil == f) // Renderizado enemigo
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("<=>");
                            c = c+2; // corrige los dos huecos siguientes a las coordenadas del enemigo saltandoselos.                    
                        }
                        else if (bombaCol == c && bombaFil == f) // Renderizado bomba
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("*");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" ");
                        }                           
                    }
                    Console.WriteLine();
                }

                // retardo

                System.Threading.Thread.Sleep(DELTA);
            }      

        if (finPartida == 1)
            Console.WriteLine("Has ganado");
        
        if (finPartida == 2)
            Console.WriteLine("Has perdido");    
        }
    }
}
