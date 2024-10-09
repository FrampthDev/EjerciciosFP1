Console.WriteLine("Escriba su número: ");

int n = int.Parse(Console.ReadLine());
int m=n;
bool exit= false;

while (!exit){
    if (m<1){
        Console.WriteLine("El número no tiene ningún 3");
        exit=true;
    }
    if (m%10==3){
        exit=true;
        Console.WriteLine("Hay un 3!");
    }
    m/=10;
    
}