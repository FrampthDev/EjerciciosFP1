Console.WriteLine("Introduzca el número");
int inputNumber = int.Parse(Console.ReadLine());
int nEven =0, 
    nOdd =0,
    result=0;

bool even = inputNumber%2==0, 
    bucle = true; 

while (bucle){

    if (even){
        nEven += inputNumber%10;
    }
    else{
        nOdd += inputNumber%10;
    }

    even = !even;

    inputNumber/=10;

    result = nEven-nOdd;
    if (result>=10){
        
        inputNumber = result;

        result=0;
        nEven=0;
        nOdd=0;
        even = result%2==0;
    }
    else{
        bucle=false;
    }
}

Console.WriteLine(result);