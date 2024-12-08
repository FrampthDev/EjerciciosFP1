// Javier Zazo Morillo
// Miguel Ángel González López
using System;
using System.Globalization;
using System.Threading;

namespace naves {
    class Program {
        
        const bool DEBUG = true;
        const int ANCHO=24, ALTO = 15;
        static Random rnd = new Random(11);

        bool hayEnemigo = false;        
        static void Main() {
            //Console.SetWindowSize(ANCHO, ALTO);
            //Console.SetBufferSize(ANCHO, ALTO);

            int [] suelo = new int[ANCHO], // límites del tunel
                   techo = new int[ANCHO];

            int naveC, naveF,   
                balaC, balaF,  
                enemigoC, enemigoF;

            // inicializacion
            naveC = ANCHO/2;

            Renderizado(suelo, techo);
           
            while (true){ // true provisional
                AvanzaTunel(suelo, techo);
                //LogicaJuego(ref int naveC, ref int NaveF, ref int balaC, ref int balaF, ref int enemigoC,ref int enemigoF);
                Renderizado(suelo, techo);
                Thread.Sleep(120); 
            } // while
        } // Main




        static void AvanzaTunel(int [] suelo, int [] techo){
            for (int i=0; i<ANCHO-1; i++){ // desplazamiento de eltos a la izda
                techo[i] = techo[i+1];
                suelo[i] = suelo[i+1];
            }

            int s,t; // ultimas posiciones de suelo y techo
            s = suelo[ANCHO-1];
            t = techo[ANCHO-1]; 
            
            // generamos nuevos valores para esas ultimas posiciones
            int opt = rnd.Next(5); // 5 alternativas
            if (opt==0 && s<ALTO-1) {s++; t++;}   // tunel baja           
            else if (opt==1 && t>0) {s--; t--;}   // sube
            else if (opt==2 && s-t>7) {s--; t++;} // estrecha
            else if (opt==3) {                    // ensancha
                if (s<ALTO-1) s++;
                if (t>0) t--;
            } // con 4 se deja igual, no se hace nada

            // asignamos ultimas posiciones en el array
            suelo[ANCHO-1] = s;
            techo[ANCHO-1] = t;
        }   


        static char LeeInput(){
            char ch=' ';
			if (Console.KeyAvailable) {	
				string dir = Console.ReadKey(true).Key.ToString();			
				if (dir == "A" || dir=="LeftArrow") ch = 'l';
				else if (dir == "D" || dir=="RightArrow") ch='r';
				else if (dir == "W" || dir=="UpArrow") ch='u';
				else if (dir == "S" || dir=="DownArrow") ch='d';                    
				else if (dir == "X" || dir=="Spacebar") ch='x'; // bomba                   
                else if (dir == "P") ch = 'p'; // pausa					
				else if (dir == "Q" || dir=="Escape") ch='q'; // salir
                while (Console.KeyAvailable) Console.ReadKey ().Key.ToString ();	
			}
            return ch;
        }
        static void Renderizado(int [] suelo, int [] techo){ //renderizado de la pantalla
            Console.Clear();
            for (int i = 0; i < ANCHO; i++) {
                for (int j = 0; j < techo[i]; j++) {
                    Console.SetCursorPosition(i, j);
                    Console.Write("█");
                 }
                for (int j = 0; j < suelo[i]; j++) {
                    Console.SetCursorPosition(i, ALTO - j);
                    Console.Write("█");
                 }
            }
        }

        static void LogicaJuego(ref int naveC, ref int naveF, ref int balaC, ref int balaF, ref int enemigoC,ref int enemigoF){

        
        }

        static void GeneraAvanzaEnemigo(ref int enemigoC, ref int enemigoF, ref bool hayEnemigo, int[] suelo, int []techo){
            
            int filaAleatoria = rnd.Next(ALTO-(techo[techo.Length]+suelo[suelo.Length])); // calcula un número aleatorio entre los espacios posibles de la última fila del túnel
            int probabilidadDeGeneracion = rnd.Next(3);            


            if (hayEnemigo) enemigoC --;
            else {
                if (probabilidadDeGeneracion == 0){ // 1/4 de probabilidades de generar
                enemigoC = ANCHO;
                enemigoF = filaAleatoria + techo[techo.Length]; 
                }
            }
        }

        static void AvanzaNave(char ch, ref int naveC, ref int naveF, int enemigoC, int enemigoF, int[] suelo, int[] techo){
            

            if ((naveF != suelo[naveF] && naveF != techo[naveF]) && (naveC != enemigoC && naveF!=enemigoF)){
                // Si la nave no está dentro del suelo, ni del techo ni del enemigo:

                switch (LeeInput()){
                    case 'l':
                        naveC--;
                        break;
                    case 'r':
                        naveC++;
                        break;
                    case 'u':
                        naveF--;
                        break;
                    case 'd':
                        naveF++;
                        break;
            }


            }
        }
}
}
