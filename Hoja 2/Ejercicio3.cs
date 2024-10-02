// Miguel Ángel González López
// Javier Zazo Morillo
namespace Ejemplo1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double notaExamen, notaPractica1, notaPractica2, notaActividad;
            Console.WriteLine("Nota examen: ");
            notaExamen = double.Parse(Console.ReadLine());

            Console.WriteLine("Nota paráctica 1: ");
            notaPractica1 = double.Parse(Console.ReadLine());

            Console.WriteLine("Nota paráctica 2: ");
            notaPractica2 = double.Parse(Console.ReadLine());

            Console.WriteLine("Nota actividad adicional: ");
            notaActividad = double.Parse(Console.ReadLine());

            Console.WriteLine("Nota media: " + Math.Round(notaExamen * 0.7 +  notaPractica1 * 0.1 + notaPractica2 * 0.1 + notaActividad * 0.1, 2));


        }
    }
}
