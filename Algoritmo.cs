using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoritmoEvolutivoCartaNueva{
    class Algoritmo{


        public static int[] generadorDeAlelos() {
            int[] cromo = new int[4];
            Random rand = new Random();
            
            for(int i = 0; i < cromo.Length; i++){
                cromo[i] = rand.Next(11);
            }

            return cromo;
        }

        public static Cromosoma generadorDeCromosomas(){
            Cromosoma cromo = new Cromosoma(generadorDeAlelos());
            return cromo;
        }

        public static void imprimirVectorDeInversion(Cromosoma cromo){
            for(int i = 0; i < cromo.alelos.Length; i++){
                if(i == 0){
                    Console.Write("[" + cromo.alelos.ElementAt(i) + ", ");   
                }else{
                    if(!(i == (cromo.alelos.Length-1))){
                        Console.Write(cromo.alelos.ElementAt(i) + ", ");
                    }else{
                        Console.WriteLine(cromo.alelos.ElementAt(i) + "]\tAptitud: " + cromo.aptitud);
                    }
                }
            }
        }
        
        public static void torneo(Cromosoma local, Cromosoma visitante, int peleaNumero){
            Console.WriteLine("-------" + peleaNumero + "-------");
            Console.WriteLine("Local: ");
            imprimirVectorDeInversion(local);
            Console.WriteLine("Visitante: ");
            imprimirVectorDeInversion(visitante);

            Console.WriteLine("Ganador: ");
            if(local.aptitud > visitante.aptitud){
                imprimirVectorDeInversion(local);
            }else{
                imprimirVectorDeInversion(visitante);
            }
            Console.WriteLine();
        }
        public static Cromosoma versus(Cromosoma primero, Cromosoma segundo, int peleaNumero, Boolean datos){
            if(datos){
                torneo(primero,segundo,peleaNumero);
            }
            if(primero.aptitud > segundo.aptitud){
                return primero;
            }else{
                return segundo;
            }
        }

        public static Cromosoma cruzaDeDosPuntos(Cromosoma primero, Cromosoma segundo, int puntoUno, int puntoDos){
            int[] alelosHijo = new int[4];

            for(int i = 0; i < alelosHijo.Length; i++){
                if(i < puntoUno){
                    alelosHijo[i] = primero.alelos.ElementAt(i);
                }else{
                    if(i < puntoDos){
                        alelosHijo[i] = segundo.alelos.ElementAt(i);
                    }else{
                        alelosHijo[i] = primero.alelos.ElementAt(i);
                    }
                }
            }
            
            Cromosoma hijo = new Cromosoma(alelosHijo);
            return hijo;
        }

        public static Cromosoma mutacionDeAlelo(Cromosoma mutante){
            int[] alelosMutados = new int[4];
            Random rand = new Random();
            int aleloAMutar = rand.Next(4);
            //Console.WriteLine("Posicion a Mutar:" + aleloAMutar);
            int noRepetir = mutante.alelos.ElementAt(aleloAMutar);

            for(int i = 0; i < alelosMutados.Length; i++){
                
                if(i == aleloAMutar){
                    do{
                        alelosMutados[i] = rand.Next(11);
                    }while(alelosMutados[i] == noRepetir);
                }else{
                    alelosMutados[i] = mutante.alelos[i];
                }
                
            }

            Cromosoma CromoMutado = new Cromosoma(alelosMutados);
            return CromoMutado;
        }

        public static List<Cromosoma> generarPoblacionInicial(int cantidad){
            List<Cromosoma> poblacionInicial = new List<Cromosoma>();
            for(int i = 1; i <= cantidad; i++){
                poblacionInicial.Add(generadorDeCromosomas());
            }

            return null;
        }
        public static void imprimirTodaLaPoblacion(List<Cromosoma> poblacion, string nombreDeLaPoblacion){
            Console.WriteLine("-------" + nombreDeLaPoblacion + "-------");
            for(int i = 0; i < poblacion.Count; i++){
                Console.Write(i+1 + ".-\t");
                imprimirVectorDeInversion(poblacion.ElementAt(i));
            }
        }

        public static Cromosoma mejorDeUnaGeneracion(List<Cromosoma> Generacion){
            Cromosoma elMejor = null;
            double laMejorApti = 0;

            foreach(Cromosoma cromo in Generacion){
                if(cromo.aptitud > laMejorApti){
                    laMejorApti = cromo.aptitud;
                    elMejor = cromo;
                }
            }
           
            return elMejor;
        }

        public static List<Cromosoma> algoritmoGenetico(
                                                List<Cromosoma> poblacionInicial, 
                                                int iteraciones, 
                                                int probCruza, 
                                                int probMutacion, 
                                                int primerPunto, 
                                                int segundoPunto, 
                                                int datosTorneo,
                                                int detalles){
            Random rand = new Random();
            int flag = 1; 
            int tamañoPoblacion = poblacionInicial.Count;
            Boolean torneo = false;
            List<Cromosoma> locales = new List<Cromosoma>();
            List<Cromosoma> visitantes = new List<Cromosoma>();
            List<Cromosoma> cruza = new List<Cromosoma>();
            List<Cromosoma> mutacion = new List<Cromosoma>();
            List<Cromosoma> nuevaGeneracion = new List<Cromosoma>();
            List<Cromosoma> losMejoresDeCadaIteracion = new List<Cromosoma>();
            if(datosTorneo == 1){
                torneo = true;
            }
            do{
                Console.WriteLine("============================= Iteración: " + flag + " =============================");
                for(int i = 1; i <= tamañoPoblacion; i++){
                    locales.Add(poblacionInicial.ElementAt(rand.Next(tamañoPoblacion)));
                    visitantes.Add(poblacionInicial.ElementAt(rand.Next(tamañoPoblacion)));
                }
                
                
                for(int i = 0; i < tamañoPoblacion; i++){
                    cruza.Add(versus(locales.ElementAt(i), visitantes.ElementAt(i), i+1, torneo));  
                }
                
                for(int i = 0; i < tamañoPoblacion; i+=2){
                    int elR = rand.Next(101);
                    if(i == tamañoPoblacion - 1){
                        mutacion.Add(cruza.ElementAt(i));
                    }else{
                    if(elR < probCruza){
                        mutacion.Add(cruzaDeDosPuntos(cruza.ElementAt(i), cruza.ElementAt(i+1), primerPunto, segundoPunto));
                        mutacion.Add(cruzaDeDosPuntos(cruza.ElementAt(i+1), cruza.ElementAt(i), primerPunto, segundoPunto));
                    }else{
                        mutacion.Add(cruza.ElementAt(i));
                        mutacion.Add(cruza.ElementAt(i+1));
                    }
                    }
                }
                for(int i = 0; i < tamañoPoblacion; i++){
                    if(probMutacion >= rand.Next(100)+1){
                        nuevaGeneracion.Add(mutacionDeAlelo(mutacion.ElementAt(i)));
                    }else{
                        nuevaGeneracion.Add(mutacion.ElementAt(i));
                    }
                }
               
                imprimirTodaLaPoblacion(poblacionInicial, "Inicial de la generacion");
                if(detalles == 1){
                    imprimirTodaLaPoblacion(cruza, "Individuos a Cruzar");
                    imprimirTodaLaPoblacion(mutacion, "Individuos a Mutar");
                }

                imprimirTodaLaPoblacion(nuevaGeneracion, "Nueva Generación");

                locales.Clear();
                visitantes.Clear();
                cruza.Clear();
                mutacion.Clear();
                poblacionInicial.Clear();
                poblacionInicial.AddRange(nuevaGeneracion);
                
                losMejoresDeCadaIteracion.Add(mejorDeUnaGeneracion(nuevaGeneracion));
                
                nuevaGeneracion.Clear();

                flag++;
            }while(iteraciones >= flag);
            imprimirTodaLaPoblacion(poblacionInicial, "POBLACIÓN FINAL");
            imprimirTodaLaPoblacion(losMejoresDeCadaIteracion, "LOS MEJORES DE CADA ITERACION");
            return poblacionInicial;
        }

        public static void algoritmoGeneticoEjecucion(){
            List<Cromosoma> ElRotisimo = new List<Cromosoma>();
            
            Console.WriteLine("Tamaño de la población: ");
            int tamañoPoblacion = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Cantidad de Iteraciones: ");
            int iteraciones = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Probabilidad de Cruza: ");
            int probCruza = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Probabilidad de Mutación(%) (Solo enteros plz): ");
            int probMutacion = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Primer punto de cruza(1-2)");
            int primerPunto = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Segundo punto de cruza(2-3 (mayor al primer punto plz))");
            int segundoPunto = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Datos del Torneo activados(1 = Si, 0 = No)");
            int datosTorneo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("¿Con detalles? (1 = Si , 0 = No)");
            int detalles = Convert.ToInt32(Console.ReadLine());

            List<Cromosoma> poblacionInicial = new List<Cromosoma>();
            for(int j = 1; j <= tamañoPoblacion; j++){
                        poblacionInicial.Add(generadorDeCromosomas());
                    }

            algoritmoGenetico(poblacionInicial, iteraciones, probCruza, probMutacion, primerPunto, segundoPunto, datosTorneo, detalles);
            /*
            if(overkill == 1){
                for(int i = 1; i <= tamañoPoblacion; i++){
                    Console.WriteLine("############################### Ejecución: " + i + " ###############################");
                    for(int j = 1; j <= tamañoPoblacion; j++){
                        poblacionInicial.Add(generadorDeCromosomas());
                    }
                    ElRotisimo.Add(algoritmoGenetico(poblacionInicial, iteraciones, probCruza, probMutacion, primerPunto, segundoPunto, datosTorneo).ElementAt(0));
                    poblacionInicial.Clear();
                }
                Console.WriteLine("############################### El Rotisimo ###############################");             
                imprimirTodaLaPoblacion(ElRotisimo, "EL ROTO");
                algoritmoGenetico(ElRotisimo, iteraciones, probCruza, probMutacion, primerPunto, segundoPunto, datosTorneo);
            }else{
                
                algoritmoGenetico(poblacionInicial, iteraciones, probCruza, probMutacion, primerPunto, segundoPunto, datosTorneo);
            } 
             */     
        }      

    }
}