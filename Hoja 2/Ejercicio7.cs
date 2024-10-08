namespace Ejercicio7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int year, sup;
            bool leap;
            
            Console.WriteLine("Introduzca un año: ");
            year = int.Parse(Console.ReadLine());

            sup = year / 4;

            leap = (year == sup * 4);

            sup = year / 100;

            leap = (year != sup * 100);

            sup = year / 400;

            leap = leap || (year == sup * 400);

            Console.WriteLine("El año es bisiesto: " + leap);
    }
}
}
