// Javier Zazo Morillo
// Miguel Ángel González López
using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Threading;

namespace naves {
    class Program {
        
        const bool DEBUG = true;
        const int ANCHO=24, ALTO = 15;
        static Random rnd = new Random(11);
   
        
        static void Main() {
            //Console.SetWindowSize(ANCHO, ALTO);
            //Console.SetBufferSize(ANCHO, ALTO);
            Console.CursorVisible = false;


            bool hayEnemigo = false; 
            bool hayBala = false;    

            int [] suelo = new int[ANCHO], // límites del tunel
                   techo = new int[ANCHO];

            int naveC = ANCHO / 2, naveF = ALTO / 2,   
                balaC = 0, balaF = 0,  
                enemigoC = 0, enemigoF = 0;

            // inicializacion
            
    
            IniciaTunel(suelo, techo);
            Render(suelo, techo, enemigoC, enemigoF,naveC,naveF);
           
            while (true){ // true provisional
                AvanzaTunel(suelo, techo);
                LogicaJuego(ref naveC, ref naveF, ref balaC, ref balaF, ref enemigoC, ref enemigoF, suelo, techo, ref hayEnemigo);
                Render(suelo, techo, enemigoC, enemigoF,naveC,naveF);

                if (DEBUG == true) {
                    Console.SetCursorPosition(0, ALTO);
                    Console.Write($"Techo: [{techo[0]}] Suelo: [{suelo[0]}]"); // c
                    Console.SetCursorPosition(0, ALTO + 1);
                    Console.Write($"Posición enemigo: ({enemigoC},{enemigoF})");
                    Console.WriteLine($"test: {naveC} | {naveF}");
                }

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
        static void Render(int[] suelo, int[] techo, int enemigoC, int enemigoF, int naveC, int naveF) {
            RenderTunel(suelo, techo);
            
            if (enemigoC >0 ){
                RenderizarEntidad(enemigoC,enemigoF,"<>");
            }

            if (!ColisionNave(naveC, naveF, suelo, techo, enemigoC, enemigoF)){
                RenderizarEntidad(naveC,naveF,"=>");
            }
        }
        static void RenderTunel(int [] suelo, int [] techo){ //renderizado de la pantalla
            Console.Clear();
            for (int i = 0; i < ANCHO; i++) {
                for (int j = 0; j < ALTO; j++) {
                    if (j <= techo[i] || j >= suelo[i]){
                        Console.SetCursorPosition(i*2, j);
                        Console.Write("██");
                    }
                }
            }
        }
        static void IniciaTunel(int [] suelo, int[] techo) {
            techo[ANCHO - 1] = 0;
            suelo[ANCHO - 1] = ALTO - 1;
            for (int i = 0; i < ANCHO - 1; i++) {
                AvanzaTunel(suelo, techo);
            }
        }
        static void LogicaJuego(ref int naveC, ref int naveF, ref int balaC, ref int balaF, ref int enemigoC,ref int enemigoF, int[] suelo, int[] techo, ref bool hayEnemigo){
            GeneraAvanzaEnemigo(ref enemigoC, ref enemigoF, ref hayEnemigo, suelo, techo);
            AvanzaNave(LeeInput(),ref naveC,ref naveF, enemigoC, enemigoF, suelo, techo);
        }

        static void GeneraAvanzaEnemigo(ref int enemigoC, ref int enemigoF, ref bool hayEnemigo, int[] suelo, int []techo){
            
            //int filaAleatoria = rnd.Next(ALTO-(techo[techo.Length]+suelo[suelo.Length])); // ERROR -calcula un número aleatorio entre los espacios posibles de la última fila del túnel
            int filaAleatoria = rnd.Next(techo[techo.Length-1] + 1, suelo[suelo.Length-1]); 
            
            
            int probabilidadDeGeneracion = rnd.Next(3);            


            if (hayEnemigo) enemigoC --;
            
            else if(probabilidadDeGeneracion == 0){ // 1/4 de probabilidades de generar
                enemigoC = ANCHO;
                hayEnemigo = true;
                enemigoF = filaAleatoria;  // ERROR
            }
            if (enemigoC <=-1){
                hayEnemigo = false;
            }
            
        }

        static void AvanzaNave(char ch, ref int naveC, ref int naveF, int enemigoC, int enemigoF, int[] suelo, int[] techo){
            

            if ((naveF != suelo[naveF] && naveF != techo[naveF]) && (naveC != enemigoC && naveF!=enemigoF)){
                // Si la nave no está dentro del suelo, ni del techo ni del enemigo:


                if (ch == 'l' && naveC >0){
                    naveC--;
                }
                if (ch == 'r' && naveC <ANCHO-1){
                    naveC++;
                }
                if (ch == 'u' && naveF >0){
                    naveF--;
                }
                if (ch == 'd' && naveF <ALTO-1){
                    naveF++;
                }
            }

            
        }
        static void GeneraAvanzaBala(bool hayBala, char ch, ref int balaC, ref int balaF, int naveC, int naveF, int enemigoC, int enemigoF, int [] suelo, int [] techo){
            if (ch == 'x' && hayBala){
                
                    // Genera una nueva bala por delante de la nave (naveC+1)
                    balaC = naveC +1;
                    balaF = naveF;
            }
            else if (hayBala && (balaF != techo[balaF] && balaF != techo[balaF]) && (balaF != enemigoF && balaC != enemigoC)){

                    // la bala avanza una posición
                    balaC ++;
            }           
        
        }

        static void RenderizarEntidad(int entidadC, int entidadF, string sprite){
           Console.SetCursorPosition(entidadC*2, entidadF);
           Console.Write(sprite);
        }

        static bool ColisionNave(int naveC, int naveF, int [] suelo, int [] techo, int enemigoC, int enemigoF){
            bool colision = false;

            /*if (naveF == suelo[naveF] || naveF == techo[naveF] || naveF == enemigoF && naveC == enemigoC){
                // ERROR CORREGIR COLISIONES
                colision = true;
                
            }*/
            
            // Determina si la nave colisiona contra el suelo, el techo o el enemigo.
            return colision;
        }
        void ColisionBala(int balaC, int balaF, int enemigoC, int enemigoF, int[] suelo, int [] techo, int colC, int colF){
            // si la bala está a la derecha de la pantalla la elimina
            //if (hayBala && )


            // si colisiona con el enemigo elimina la bala y el enemigo y devuelve en colC, colF la posición de la colisión
            // si colisiona con el suelo o con el techo elimina la bala y devuelve en colC y colF la posición de la colisión y destruye 3 bloques
            //si no hay colisión simplemente devuelve colC=colF=-1 para indicarlo.
        }
        }
    
}
