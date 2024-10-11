namespace Ejercicio11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Ejercicio11
            int n, x, y, z;
            // x = mayor número par no superior a n
            x = (n / 2) * 2;
            // y = primero número par mayor o igual que n
            y = ((n + 1) / 2) * 2;
            // z = primer impar mayor o igual que n
            z = n + ((n + 1) % 2);

        }
    }
}
