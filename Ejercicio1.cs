// Miguel Ángel González López
// Javier Zazo Morillo
namespace Ejemplo1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int hours, minutes, seconds;

            Console.WriteLine("Introduce las horas: ");
            hours = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduce los minutos: ");
            minutes = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduce los segundos: ");
            seconds = int.Parse(Console.ReadLine());

            // Pasar todo a segundos y sumar
            seconds += hours * 3600 + minutes * 60;

            // Imprimir resultado
            Console.WriteLine("Tiempo en segundos: " + seconds);
        }
    }
}
