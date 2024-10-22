Console.Write("Valor de n: ");
    int n = int.Parse(Console.ReadLine());

    int a,b,c,cont=0;
    for (a = 2;a <= n; a++){
        for (b=2; b<=a+1; b++){
            for (c=2;c<=n;c++)
                if (a*a+b*b==c*c){
                    Console.WriteLine(a + " " + b + " " + c );
                    cont++;
                }

        }
    }
    Console.WriteLine("Total "+ cont);