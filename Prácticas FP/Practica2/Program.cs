// Javier Zazo Morillo
// Miguel Ángel González López
using System;
using System.Runtime.InteropServices;

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
            bool gameOver = false;
            bool pause = false;

            int [] suelo = new int[ANCHO], // límites del tunel
                   techo = new int[ANCHO];

            int naveC = ANCHO / 2, naveF = ALTO / 2,   
                balaC = 0, balaF = 0,  
                enemigoC = 0, enemigoF = 0,
                colC = -1, colF = -1;
                

            // inicializacion
            
    
            IniciaTunel(suelo, techo);
            Render(suelo, techo, enemigoC, enemigoF,naveC,naveF, balaC, balaF, hayBala, ref  colC, ref colF);
           
            while (!gameOver){ // true provisional

                char ch = LeeInput();

                if (ch=='q'){
                    gameOver = true;
                }
                else if (ch == 'p'){
                    pause = true;
                    while(pause == true){
                        ch = LeeInput();
                        if (ch == 'p'){
                            pause = false;
                        }
                    }
                }
                else{

                    
                    LogicaJuego(ch, ref gameOver, ref naveC, ref naveF, ref balaC, ref balaF, ref enemigoC, ref enemigoF, suelo, techo, ref hayEnemigo, ref hayBala, ref colC, ref colF);
                    Render(suelo, techo, enemigoC, enemigoF,naveC,naveF, balaC, balaF, hayBala, ref colC, ref colF);

                    if (DEBUG == true) {
                        Console.SetCursorPosition(0, ALTO);
                        Console.Write($"Techo: [{techo[0]}] Suelo: [{suelo[0]}]"); // 
                        Console.SetCursorPosition(0, ALTO + 1);
                        Console.Write($"Posición enemigo: ({enemigoC},{enemigoF})");
                        Console.SetCursorPosition(0, ALTO + 2);
                        Console.WriteLine($"Posición nave: ({naveC},{naveF})");
                        Console.WriteLine($"Input: {ch}");
                        Console.WriteLine($"GameState: {gameOver}");
                    }

                    Thread.Sleep(120);
                }
                 
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
        static void Render(int[] suelo, int[] techo, int enemigoC, int enemigoF, int naveC, int naveF, int balaC, int balaF, bool hayBala, ref int colC, ref int colF) {
            
            

            RenderTunel(suelo, techo);
            
            if (enemigoC > 0){
                RenderizarEntidad(enemigoC,enemigoF,"<>");
            }

            if (!ColisionNave(naveC, naveF, suelo, techo, enemigoC, enemigoF)){
                RenderizarEntidad(naveC,naveF,"=>");
            }

            if (hayBala && balaC >= 0) {
                RenderizarEntidad(balaC,balaF, "--");
            }
            if (colC >0 && colF > 0){
                RenderizarEntidad(colC, colF,"**");
                colC = -1;
                colF = -1;
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
        static void LogicaJuego(char ch,ref bool gameOver,ref int naveC, ref int naveF, ref int balaC, ref int balaF, ref int enemigoC, ref int enemigoF, int[] suelo, int[] techo, ref bool hayEnemigo, ref bool hayBala, ref int colC, ref int colF){
            
            AvanzaTunel(suelo, techo);
            GeneraAvanzaEnemigo(ref enemigoC, ref enemigoF, ref hayEnemigo, suelo, techo);
            

            if (ColisionNave(naveC, naveF, suelo, techo, enemigoC, enemigoF)) {gameOver = true;}
            AvanzaNave(ref gameOver,ch,ref naveC,ref naveF, enemigoC, enemigoF, suelo, techo);
            if (ColisionNave(naveC, naveF, suelo, techo, enemigoC, enemigoF)) {gameOver = true;}


            ColisionBala(ref hayBala, ref balaC, balaF, ref enemigoC, enemigoF,ref hayEnemigo, suelo, techo, ref colC, ref colF);
            GeneraAvanzaBala(ch, ref hayBala, ref balaC, ref balaF, naveC, naveF, enemigoC, enemigoF, suelo, techo);
            ColisionBala(ref hayBala, ref balaC, balaF, ref enemigoC, enemigoF,ref hayEnemigo, suelo, techo, ref colC, ref colF);
        }

        static void GeneraAvanzaEnemigo(ref int enemigoC, ref int enemigoF, ref bool hayEnemigo, int[] suelo, int []techo){
            
            //int filaAleatoria = rnd.Next(ALTO-(techo[techo.Length]+suelo[suelo.Length])); // ERROR -calcula un número aleatorio entre los espacios posibles de la última fila del túnel
            int filaAleatoria = rnd.Next(techo[techo.Length-1] + 1, suelo[suelo.Length-1]); 
            
            
            int probabilidadDeGeneracion = rnd.Next(3);            


            if (hayEnemigo) enemigoC --;
            
            else if(probabilidadDeGeneracion == 0){ // 1/4 de probabilidades de generar
                enemigoC = ANCHO;
                hayEnemigo = true;
                enemigoF = filaAleatoria;  
            }
            if (enemigoC <=-1){
                hayEnemigo = false;
            }
            
        }

        static void AvanzaNave(ref bool gameOver,char  ch, ref int naveC, ref int naveF, int enemigoC, int enemigoF, int[] suelo, int[] techo){
            
            
            if ((naveF >= techo[naveC] && naveF <= suelo[naveC]) || (naveC != enemigoC && naveF != enemigoF)){
                // Si la nave no está dentro del suelo, ni del techo ni del enemigo:
                
                if (ch == 'l' && naveC > 0){
                    naveC--;
                }
                else if (ch == 'r' && naveC < ANCHO-1){
                    naveC++;
                }
                else if (ch == 'u' && naveF > 0){
                    naveF--;
                }
                else if (ch == 'd' && naveF < ALTO-1){
                    naveF++;
                }
            }           
        }
        static void GeneraAvanzaBala(char ch, ref bool hayBala, ref int balaC, ref int balaF, int naveC, int naveF, int enemigoC, int enemigoF, int [] suelo, int [] techo){
            if (ch == 'x' && !hayBala){
                
                    // Genera una nueva bala por delante de la nave (naveC+1)
                    balaC = naveC +1;
                    balaF = naveF;
                    hayBala = true;
            }
            else if (hayBala && (balaF != techo[balaC] && balaF != techo[balaC] && balaC < ANCHO - 1) && (balaF != enemigoF && balaC != enemigoC)){

                    // la bala avanza una posición
                    balaC += 2;
            } 
            else hayBala = false;              
        }

        static void RenderizarEntidad(int entidadC, int entidadF, string sprite){
           Console.SetCursorPosition(entidadC*2, entidadF);
           Console.Write(sprite);
        }

        static bool ColisionNave(int naveC, int naveF, int [] suelo, int [] techo, int enemigoC, int enemigoF){
            bool colision = false;


            if (naveF >= suelo[naveC] || naveF <= techo[naveC] || (naveF == enemigoF && naveC == enemigoC)){
                colision = true;
                
            }
            
            // Determina si la nave colisiona contra el suelo, el techo o el enemigo.
            return colision;
        }
        static void ColisionBala(ref bool hayBala, ref int balaC, int balaF, ref int enemigoC, int enemigoF,ref bool hayEnemigo ,int[] suelo, int [] techo, ref int colC, ref int colF){
            // si la bala está a la derecha de la pantalla la elimina
            //if (hayBala && )


            if (balaC == enemigoC -1 && balaF == enemigoF){
                colC = balaC + 1;
                colF = balaF;
                enemigoC = 0;
                hayEnemigo = false;              
                balaC = 0;
                hayBala = false;
            }
            else if (balaC == enemigoC && balaF == enemigoF){
                colC = balaC;
                colF = balaF;
                enemigoC = 0;
                hayEnemigo = false;              
                balaC = 0;
                hayBala = false;
            }

            else if (balaC > 0 && balaF >= suelo[balaC - 1]){
                suelo[balaC - 1] = balaF + 1;
                balaC = 0;
                hayBala = false;
            }
            else if (balaF >= suelo[balaC]){
                suelo[balaC] = balaF + 1;
                balaC = 0;
                hayBala = false;
            }
            else if (balaC > 0 && balaF <= techo[balaC - 1]){  
                techo[balaC - 1] = balaF - 1;
                balaC = 0;
                hayBala = false;
            }     
            else if (balaF <= techo[balaC]){  
                techo[balaC] = balaF - 1;
                balaC = 0;
                hayBala = false;
            }     
            
            // si colisiona con el enemigo elimina la bala y el enemigo y devuelve en colC, colF la posición de la colisión
            // si colisiona con el suelo o con el techo elimina la bala y devuelve en colC y colF la posición de la colisión y destruye 3 bloques
            // si no hay colisión simplemente devuelve colC=colF=-1 para indicarlo.
        }
        }
    
}
