namespace Ejercicio_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num, d4, d3, d2, d1;

            Console.WriteLine("Introduzca un número de 4 cifras en binario: ");

            num = int.Parse(Console.ReadLine());

            d4 = num / 1000;
            num -= d4 *1000;
            d3 = num / 100;
            num -= d3 * 100;
            d2 = num / 10;
            num -= d2 * 10;
            d1 = num;

            num = (d4 * 8) + (d3 * 4) + (d2 * 2 )+ d1;

            Console.WriteLine("El número en decimal es igual a: " + num);
         
        }
    }
}
