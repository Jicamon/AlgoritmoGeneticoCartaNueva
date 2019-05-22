using System;
using System.Collections.Generic;
using System.Linq;
namespace AlgoritmoEvolutivoCartaNueva{
    class Cromosoma {

        public int[] alelos = new int[4];
        public double aptitud;
        
        public Cromosoma(int[] losAlelos)
        {
            alelos = losAlelos;
            aptitud = calcularAptitud(losAlelos);
        }

        public static double calcularAptitud(int[] losAlelos){
            double Aptitud;
            
            double c1val;
            double c2val;
            double c3val;
            double c4val;        
            
            int v;

            int c1 = losAlelos.ElementAt(0);
            int c2 = losAlelos.ElementAt(1);
            int c3 = losAlelos.ElementAt(2);
            int c4 = losAlelos.ElementAt(3);

            v = Math.Abs((c1+c2+c3+c4) - 10);

            switch(c1){
                case 0:     c1val = 0; break;
                case 1:     c1val = 0.28; break;
                case 2:     c1val = 0.45; break;
                case 3:     c1val = 0.65; break;
                case 4:     c1val = 0.78; break;
                case 5:     c1val = 0.90; break;
                case 6:     c1val = 1.02; break;
                case 7:     c1val = 1.13; break;
                case 8:     c1val = 1.23; break;
                case 9:     c1val = 1.32; break;
                default:    c1val = 1.38; break;
            }

            switch(c2){
                case 0:     c2val = 0; break;
                case 1:     c2val = 0.25; break;
                case 2:     c2val = 0.41; break;
                case 3:     c2val = 0.55; break;
                case 4:     c2val = 0.65; break;
                case 5:     c2val = 0.75; break;
                case 6:     c2val = 0.80; break;
                case 7:     c2val = 0.85; break;
                case 8:     c2val = 0.88; break;
                case 9:     c2val = 0.90; break;
                default:    c2val = 0.90; break;
            }
            
            switch(c3){
                case 0:     c3val = 0; break;
                case 1:     c3val = 0.15; break;
                case 2:     c3val = 0.25; break;
                case 3:     c3val = 0.40; break;
                case 4:     c3val = 0.50; break;
                case 5:     c3val = 0.62; break;
                case 6:     c3val = 0.73; break;
                case 7:     c3val = 0.82; break;
                case 8:     c3val = 0.90; break;
                case 9:     c3val = 0.96; break;
                default:    c3val = 1.00; break;
            }

            switch(c4){
                case 0:     c4val = 0; break;
                case 1:     c4val = 0.20; break;
                case 2:     c4val = 0.33; break;
                case 3:     c4val = 0.42; break;
                case 4:     c4val = 0.48; break;
                case 5:     c4val = 0.53; break;
                case 6:     c4val = 0.56; break;
                case 7:     c4val = 0.58; break;
                case 8:     c4val = 0.60; break;
                case 9:     c4val = 0.60; break;
                default:    c4val = 0.60; break;
            }   

            

            Aptitud = (c1val + c2val + c3val + c4val) / ((500 * v) + 1);

            return Aptitud;
        } 

    }
}