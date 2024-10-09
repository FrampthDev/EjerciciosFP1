

int number = int.Parse(Console.ReadLine());
int cont=0;

string reversedNumber="";
int reversedNumberOutput=0;

while (number>0){
    string bufferNumber = Convert.ToString(number%10); 
    reversedNumber = reversedNumber.Insert(cont, bufferNumber);
    
    number /=10;
    cont ++;
}

reversedNumberOutput = Convert.ToInt16(reversedNumber);
Console.WriteLine(reversedNumberOutput);




/*string hola = "hola";
int n = 3;

string hola2= hola.Insert(3 , "2");

Console.WriteLine(hola2);*/