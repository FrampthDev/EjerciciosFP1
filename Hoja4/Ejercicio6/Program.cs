Console.WriteLine("Introduzca el número");
int inputNumber = int.Parse(Console.ReadLine());

int n =0;

while (inputNumber >= 10){
    
    while (inputNumber>0){

        n += inputNumber%10;   
        inputNumber /= 10;
        Console.WriteLine(inputNumber + " " + n);
    }
    if(n >= 10){
        inputNumber=n;
        n=0;
    
    }

}

if (n%3 == 0){
    Console.WriteLine("El número es divisible entre 3");
}
else {
    Console.WriteLine("El número no es divisible entre 3");
}