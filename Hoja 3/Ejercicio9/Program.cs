// Javier Zazo Morillo
// Miguel Ángel González López
namespace Ejercicio9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int WaterConsumption;
            double euros;
            const double stretch1 = 0.15, stretch2 = 0.20, stretch3 = 0.35, stretch4 = 0.80;

            Console.WriteLine("Introduzca su consumo de agua en metros cúbicos: (número entero)");
            WaterConsumption = int.Parse(Console.ReadLine());
            Console.WriteLine("Metros cúbicos: " + WaterConsumption);

            if (WaterConsumption < 101) { 
                euros = (WaterConsumption * stretch1);
                Console.WriteLine("Consumo desglosado: " + WaterConsumption + "*" + stretch1);
            }
            else if (WaterConsumption < 501) { 
                euros = ((100 * stretch1) + (WaterConsumption - 100) * stretch2);
                Console.WriteLine("Consumo desglosado: 100*" + stretch1 + " + " + (WaterConsumption - 100) + "*" + stretch2);
            }
            else if (WaterConsumption < 1001) { 
                euros = ((100 * stretch1 + 400 * stretch2)+ (WaterConsumption - 500) * stretch3);
                Console.WriteLine("Consumo desglosado: 100*" + stretch1 + " + 400*" + stretch2 + " + " + (WaterConsumption - 400) + "*" + stretch3);
            }
            else {
                euros = ((100 * stretch1 + 400 * stretch2 + 500 * stretch3) + (WaterConsumption - 1000) * stretch4);
                Console.WriteLine("Consumo desglosado: 100*" + stretch1 + " + 400*" + stretch2 + " + 500*" + stretch3 + " + " + (WaterConsumption - 1000) + "*" + stretch4);
            }
            Console.WriteLine("A pagar: " + Math.Round(euros,2) + " euros");

        }
    }
}
