// Javier Zazo Morillo
// Miguel Ángel González López
namespace Práctica_1
{
    class MainClass
    {
        const int DELTA = 400;
        const int FILS = 14, COLS = 22;

        static Random rnd = new Random(); // aleatorios para movimiento del enemigo

        public static void Main(string[] args)
        {
            // Console.SetWindowSize(width, height); // para poner consola de tamaño COLSxFILS

            Console.CursorVisible = false; // ocultamos cursor en pantalla

            int aleatorio,
                jugCol, jugFil,
                eneCol, eneFil,
                balaFil = -1, balaCol = -1,
                bombaCol, bombaFil,
                finPartida; // 0 jugando; 1 gana jugador; 2 gana enemigo; 3 abortar

            bool hayBala = false;

            jugCol = COLS / 4; 
            jugFil = FILS * 3 / 4;
            eneCol = COLS / 2;
            eneFil = FILS / 4;

            //Console.SetWindowSize(COLS, FILS);
            //Console.SetCursorPosition(0, 0);

            // bucle ppal
            while (true) {
                // recogida de INPUT DE USUARIO
                string dir = "";
                if (Console.KeyAvailable)
                { // si se detecta pulsación de tecla
                  // leemos input y transformamos a string
                    dir = (Console.ReadKey(true)).KeyChar.ToString();                    
                    while (Console.KeyAvailable) Console.ReadKey(true); // limpiamos buffer de entrada						
                }

                // lógica de jugador 

                if (dir == "w" && jugFil > FILS / 2)
                {
                    jugFil--;
                }
                if (dir == "a" && jugCol > 0)
                {
                    jugCol--;
                }
                if (dir == "s" && jugFil < FILS)
                {
                    jugFil++;
                }
                if (dir == "d" && jugCol < COLS)
                {
                    jugCol++;
                }

                // lógica de la bala

                if (dir == "l" && !hayBala)
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

                aleatorio = rnd.Next(0, 4); // 0 enemigo arriba; 1 enemigo izquierda; 2 enemigo abajo; 3 enemigo derecha

                if (aleatorio == 0 && eneFil > 0)
                {
                    eneFil--;
                }
                else if (aleatorio == 1 && eneCol > 0)
                {
                    eneCol--;
                }
                else if (aleatorio == 2 && eneFil < FILS / 2 - 1)
                {
                    eneFil++;
                }
                else if (/*aleatorio == 3 &&*/ eneCol < COLS)
                {
                    eneCol++;
                }

                // lógica de la bomba 

                // colisiones

                // RENDERIZADO 

                // Recorre una matriz y dibuja el mapa
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
                        else if (eneCol == c && eneFil == f)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Ç");
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
                Console.Clear();
            }
        }
    }
}
