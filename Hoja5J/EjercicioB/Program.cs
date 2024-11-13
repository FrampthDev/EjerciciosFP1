using System.Runtime.ExceptionServices;

namespace EjercicioB
{
    internal class Program
    {
        const int DELTA = 400, WIDTH = 8;
        Random rnd = new();
        static void Main()
        { 
            int TrackPos = 7, CarPos = 5;
            string Dir = "", Car = "#";
            bool EndGame = false;

            Console.WriteLine("Tasa de refresco: " + DELTA);

             while (!EndGame)
            {
                Render(TrackPos, CarPos, Car, EndGame);
                
                System.Threading.Thread.Sleep(DELTA);

                Input(out Dir);

                // Logic(ref TrackPos, ref CarPos);

                PlayerMovement(ref CarPos, Dir);

                Collider(ref EndGame, CarPos, TrackPos, ref Car);

                Render(TrackPos, CarPos, Car, EndGame);

                System.Threading.Thread.Sleep(DELTA);
            }

             Console.WriteLine("You crashed!");
        }
        

        static void Input(out string Dir)
        {
            if (Console.KeyAvailable)
            {
                Dir = (Console.ReadKey(true)).KeyChar.ToString();
            }
            else Dir = "";
        }

        /*static void Logic(ref int TrackPos, ref int CarPos)
        {
            int curva = rnd.Next(-1, 2);

            if (curva == -1)
            {
                TrackPos--;
                CarPos++;
            }
            else if (curva == 1)
            {
                TrackPos++;
                CarPos--;
            }
        }*/

        static void PlayerMovement(ref int CarPos, string Dir)
        {
            if (Dir == "d")
            {
                CarPos++;
            }
            else if (Dir == "a")
            {
                CarPos--;
            }
        }

        static void Collider(ref bool EndGame, int CarPos, int TrackPos,ref string Car)
        {
            if ((CarPos <= 0 ) || (CarPos >= WIDTH + 1)) 
            {
                EndGame = true;
                Car = "*";
            }
        }

        static void Render(int TrackPos, int CarPos, string Car, bool EndGame)
        {
            for (int i = 1; i <= TrackPos; i++) 
            {
                Console.Write(" ");
            }
            Console.Write("|");
            for (int i = 1;i <= CarPos; i++)
            {
                Console.Write(".");
            }
            Console.Write(Car);
            for (int i = 1;i <= WIDTH - CarPos; i++)
            {
                Console.Write(".");
            }
            if (!EndGame)
            {
                Console.Write("|");
            }
            Console.WriteLine();
        }
    }
}
