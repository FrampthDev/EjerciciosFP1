// Miguel Ángel González López
// Javier Zazo Morillo
namespace Ejemplo1
{
    internal class Program
    {
        static void Main(string[] args)
        {   
            int cent;

            int b500, b200, b100, b50, b20, b10, b5, m2, m1, c50, c20, c10, c5, c2;

            Console.WriteLine("Introduzca el importe a retirar (euros,céntimos): ");


            cent = (int)Math.Round(100 * double.Parse(Console.ReadLine()));


            // Billetes de 500
            Console.WriteLine("Tu cambio: ");
            b500 = cent / 50000;
            cent = cent % 50000;
            
            // Billetes de 200
            b200 = cent / 20000;
            cent = cent % 20000;
            
            // Billetes de 100
            b100 = cent / 10000;
            cent = cent % 10000;
            
            // Billetes de 50
            b50 = cent / 5000;
            cent = cent % 5000;
            
            // Billetes de 20
            b20 = cent / 2000;
            cent = cent % 2000;
            
            // Billetes de 10
            b10 = cent / 1000;
            cent = cent % 1000;
            
            // Billetes de 5
            b5 = cent / 500;
            cent = cent % 500;
            
            // Monedas de 2
            m2 = cent / 200;
            cent = cent % 200;
            
            // Monedas de 1
            m1 = cent / 100;
            cent = cent % 100;
            
            // Monedas de 50c
            c50 = cent / 50;
            cent = cent % 50;           

            //Monedas de 20c
            c20 = cent / 20;
            cent = cent % 20;
            
            //Monedas de 10c
            c10 = cent / 10;
            cent = cent % 10;
            
            //Monedas de 5c
            c5 = cent / 5;
            cent = cent % 5;
            
            //Monedas de 2c
            c2 = cent / 2;
            cent = cent % 2;            


            // Impresión en pantalla
            Console.WriteLine("Billetes de 500: " + b500);
            Console.WriteLine("Billetes de 200: " + b200);
            Console.WriteLine("Billetes de 100: " + b100);
            Console.WriteLine("Billetes de 50: " + b50);
            Console.WriteLine("Billetes de 20: " + b20);
            Console.WriteLine("Billetes de 10: " + b10);
            Console.WriteLine("Billetes de 5: " + b5);
            Console.WriteLine("Monedas de 2: " + m2);
            Console.WriteLine("Monedas de 1: " + m1);
            Console.WriteLine("Monedas de 50c: " + c50);
            Console.WriteLine("Monedas de 20c: " + c20);
            Console.WriteLine("Monedas de 10c: " + c10);
            Console.WriteLine("Monedas de 5c: " + c5);
            Console.WriteLine("Monedas de 2c: " + c2);
            Console.WriteLine("Monedas de 1c: " + cent);
        }
    }
}
