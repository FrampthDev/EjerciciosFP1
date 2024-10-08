using System.Timers;

namespace Ejercicio1
{
    internal class Program
    {
        static void Main(string[] args){
            Console.WriteLine("Ingrese el numero entero a factorizar: ");

            int n = int.Parse(Console.ReadLine());
            int cont=1, f = 1;


            while (cont<(n+1)){
                
                f = f*cont;
                cont +=1;
                

            }

            Console.WriteLine("El numero factorizado es: "+ f);
    }
    }
}