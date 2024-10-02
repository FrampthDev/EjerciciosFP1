// Miguel Ángel González López
// Javier Zazo Morillo
namespace Ejemplo1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int age1, age2, age3, media;

            Console.WriteLine("Introduzca la edad del primer alumno: ");
            age1= int.Parse(Console.ReadLine());

            Console.WriteLine("Introduzca la edad del segundo alumno: ");
            age2 = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduzca la edad del tercer alumno: ");
            age3 = int.Parse(Console.ReadLine());

            Console.WriteLine("La media de las edades es: " + (age1 + age2 + age3)/3);

        }
    }
}
