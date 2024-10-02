// DONT TOUCH

namespace Ejemplo1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // PROGRAM HERE IDIOT

            // CircleArea

            /* 
            double Radius, Area;
            Console.Write("Radio circulo: ");
            Radius = double.Parse(Console.ReadLine());
            Area = Radius * Radius * Math.PI;
            Console.Write(Area);
            */


            // VariableTrade

            /*
            int x, y;
            Console.Write("x: ");
            x = int.Parse(Console.ReadLine());
            Console.Write("y: ");
            y = int.Parse(Console.ReadLine());
            Console.WriteLine("Antes: x = " + x + " y = " + y);
            Console.WriteLine("Ahora: x = " + y + " y = " + x);
            */

            // Fraction

            /*
            int x, y, z, r;
            Console.Write("Dividendo: ");
            x = int.Parse(Console.ReadLine());
            Console.Write("Divisor: ");
            y = int.Parse(Console.ReadLine());

            z = x / y;
            r = x % y;

            Console.WriteLine("Resultado: " + z);
            Console.WriteLine("Resto: " + r);
            */

            // Timer

            // No muy eficiente

            /*
            int Time, Auxiliar, Seconds, Minutes, Hours;
            Console.WriteLine("Tiempo: ");
            Time = int.Parse(Console.ReadLine());
            Auxiliar = Time / 60;
            Seconds = Time % 60;
            Hours = Auxiliar / 60;
            Minutes = Auxiliar % 60;
            Console.WriteLine("Horas: "+ Hours);
            Console.WriteLine("Minutos "+ Minutes);
            Console.WriteLine("Segundos: "+ Seconds);
            */


            int Euros, Centimos, IVA;
            Console.WriteLine("Euros: ");
            Euros =int.Parse(Console.ReadLine());
            Console.WriteLine("Centimos: ");
            Centimos = int.Parse(Console.ReadLine());
            Console.WriteLine("IVA: ");
            IVA = int.Parse(Console.ReadLine());
            Centimos += Euros * 100;
            Centimos += Centimos * IVA / 100;
            Console.WriteLine(Centimos / 100 + "," + Centimos + "€");

            // DONT TOUCH FROM HERE
        }
    }
}
