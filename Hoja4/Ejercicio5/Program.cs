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

reversedNumberOutput = Convert.ToInt32(reversedNumber);
Console.WriteLine(reversedNumberOutput);

if (number%reversedNumberOutput==1){
    Console.WriteLine("Es capicua");
}
else {
    Console.WriteLine("No es capicua");
}