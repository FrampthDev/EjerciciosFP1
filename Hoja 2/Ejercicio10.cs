namespace Ejercicio10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double capital, interes, plazo;
            double cuota, total, intereses;

            Console.WriteLine("Introduzca el capital prestado: ");
            capital = double.Parse(Console.ReadLine());
            Console.WriteLine("Introduzca el interés anual: ");
            interes = double.Parse(Console.ReadLine()) / 12;
            Console.WriteLine("Introduzca el plazo en años: ");
            plazo = double.Parse(Console.ReadLine()) * 12;

            cuota = (capital * interes) / (100 * (1 - Math.Pow(1 + (interes / 100), -plazo)));
            total = (cuota * plazo);
            intereses = (total - capital);

            Console.WriteLine("cuota: " + cuota);
            Console.WriteLine("total: " + total);
            Console.WriteLine("intereses: " + intereses);
        }
    }
}
