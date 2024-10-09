Console.WriteLine("Introdizca su número: ");
int n =int.Parse(Console.ReadLine());
int m = n, sum= 0; // uso m en vez de n para que no sea destructivo.

while (m>1){
    if (m%10 == 3){
        sum++;
    }
    m=m/10;
}
Console.WriteLine(sum);