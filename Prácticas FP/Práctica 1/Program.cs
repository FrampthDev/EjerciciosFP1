// Javier Zazo Morillo
// Miguel Ángel González López
namespace Práctica_1
{
    class MainClass
    {
        const int DELTA = 400;
        const int FILS = 14, COLS = 22;
        //Console.SetWindowSize(FILS,COLS);

        static Random rnd = new Random(); // aleatorios para movimiento del enemigo

        public static void Main(string[] args)
        {
            // Console.SetWindowSize(width, height); // para poner consola de tamaño COLSxFILS

            Console.CursorVisible = false; // ocultamos cursor en pantalla

            int aleatorioCol, aleatorioFil,
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
                aleatorioCol = rnd.Next(-1, 2);
                aleatorioFil = rnd.Next(-1, 2);
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
                else{
                    bombaFil++;
                }
                if (bombaFil>FILS){
                    hayBomba= false;
                }

                // colisiones

                
                if ((balaFil == eneFil && balaCol == eneCol) 
                || (balaFil == eneFil && balaCol == eneCol-1) 
                || (balaFil == eneFil && balaCol == eneCol+1)) // colisiones enemigo-bala
                {
                    finPartida=1;
                }

                if (bombaFil == jugFil && bombaCol == jugCol) // colisiones jugador-bomba
                {
                    finPartida=2;
                }

                if (jugCol == eneCol && jugFil == eneFil)
                {
                    finPartida=2;
                }

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
                            Console.Write("<=>");
                            c= c+2; // corrige los dos huecos siguientes a las coordenadas del enemigo saltandoselos.
                            
                        }
                        else if (bombaCol == c && bombaFil == f){
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("*");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(".");
                        }                           
                    }
                    Console.WriteLine();
                }

                // retardo

                System.Threading.Thread.Sleep(DELTA);
                Console.Clear();
            }
        
        if (finPartida == 1){
            Console.WriteLine("Has ganado");
        }
        if (finPartida == 2){
            Console.WriteLine("Has perdido");
        }
        
        }
    }
}
