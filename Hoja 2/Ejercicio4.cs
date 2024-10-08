namespace Ejercicio4
{
    internal class Program
    {
        static void Main()
        {
            int d1, d2, d3, d4, n;
            bool one3, two3, twocon3, cap;

            Console.WriteLine("Introduzca un número de 4 dígitos: ");
            n = int.Parse(Console.ReadLine());

            d4 = n / 1000;
            d3 = n % 1000 / 100;
            d2 = n % 100 / 10;
            d1 = n % 10;

            one3 = ((d4 == 3) || (d3 == 3) || (d2 == 3) || (d1 == 3));

            two3 = (((d4 == 3) && (d3 == 3)) || ((d4 == 3) && (d2 == 3)) || ((d4 == 3) && (d1 == 3))
                || ((d3 == 3) && (d2 == 3)) || ((d3 == 3) && (d1 == 3))
                || ((d2 == 3) && (d1 == 3)));

            twocon3 = (((d4 == 3) && (d3 == 3) && (d2 != 3)) 
                || ((d4 != 3) && (d3 == 3) && (d2 == 3) && (d1 != 3)) 
                || ((d3 != 3) && (d2 == 3) && (d1 == 3)));

            cap = ((d4*1000+d3*100+d2*10+d1) == (d1+d2*10+d3*100+d4*1000));

            Console.WriteLine("Algún 3: " + one3);
            Console.WriteLine("Dos 3 o más : " + two3);
            Console.WriteLine("Dos 3 seguidos: " + twocon3);
            Console.WriteLine("Capicúa: " + cap); 

        }
    }
}
