// Javier Zazo Morilla
// Miguel Ángel González López

using System.Drawing;

namespace Ejercico_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool triangle, equilateral, isosceles, scalene, rectangle;
            int l1, l2, l3;

            Console.WriteLine("Lado 1: ");
            l1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Lado 2: ");
            l2 = int.Parse(Console.ReadLine());
            Console.WriteLine("Lado 3: ");
            l3 = int.Parse(Console.ReadLine());

            triangle = ((l1 > 0) && (l2 > 0) && (l3 > 0)) && ((l1 < l2 + l3) && (l2 < l1 + l3) && (l3 < l1 + l2));

            equilateral = triangle && (l1 == l2 && l1 == l3);

            isosceles = triangle && ((l1 == l2 && l1 != l3) 
                || (l1 == l3 && l1 != l2) 
                || (l2 == l3 && l2 != l1));

            scalene = triangle && (l1 != l2 && l1 != l3 && l2 != l3);

            rectangle = triangle && ((l1 * l1 == l2 * l2 + l3 * l3)
                || (l2 * l2 == l1 * l1 + l3 * l3)
                || (l3 * l3 == l1 * l1 + l2 * l2));

            Console.WriteLine("Triangulo: " + triangle);
            Console.WriteLine("Equilátero: " + equilateral);
            Console.WriteLine("Isósceles: " + isosceles);
            Console.WriteLine("Escaleno: " + scalene);
            Console.WriteLine("Rectángulo: " + rectangle);

        }
    }
}
