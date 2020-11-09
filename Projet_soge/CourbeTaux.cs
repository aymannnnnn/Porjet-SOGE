using System;
using System.IO;
using System.Reflection;


namespace ProjetSoge
{
    public class Swap
    {
        //Frequence des paiment du Swap en une annee
        double Frequency;
        //La duree du contract Swap
        double Years;

        //Forward Rates representent les flux variables dans un swap (floating leg)
        private double[] ForwardRates;
        private double[] SpotRates;
        private bool initiatedForward = false;
        private bool initiatedSpot = false;

        public Swap(int freq  = 4, double years = 2.0)
        {
            if (years * 12 / freq != (int)(years * 12 / freq))
            {
                throw new System.ArgumentException("Nombre d'annee * 12 / frequence doit etre entier");
            }
            Frequency = freq;
            Years = years;
            ForwardRates = new double[(int)(years*freq)];
            SpotRates = new double[(int)(years*freq)];
        }

        public void setForwardRates(double[] rates)
        {
            ForwardRates = rates;
            initiatedForward = true;
        }
        
        public void setSpotRates(double[] rates)
        {
            SpotRates = rates;
            initiatedSpot = true;
        }

        public double[] getForwardRates()
        {
            return ForwardRates;
        }
        
        public double[] getSpotRates()
        {
            return SpotRates;
        }
        
        public void constructYieldCurves()
        {
            int compteur;
            //Variable locale qui servira que pour le calcul dans cette fonction
            double intermediare;

            //Si on donne que les taux forward
            if (!initiatedSpot)
            {
                //Taux forward de 3 mois après 0 mois coincide avec le taux sport de maturite 3 mois
                SpotRates[0] = ForwardRates[0];

                for (compteur = 1; compteur < SpotRates.Length; compteur++)
                {
                    intermediare = (1.0 + SpotRates[compteur - 1] / (Frequency/compteur)) * (1.0 + ForwardRates[compteur] / Frequency);
                    SpotRates[compteur] = (Frequency /(compteur + 1)) * (- 1.0 + intermediare);
                }

            }
            //Si on donne que les taux spot

            if (!initiatedForward)
            {
                //Taux forward de 3 mois après 0 mois coincide avec le taux sport de maturite 3 mois
                ForwardRates[0] = SpotRates[0];

                for (compteur = 1; compteur < SpotRates.Length; compteur++)
                {
                    intermediare = (1 + SpotRates[compteur] / (Frequency / (compteur + 1))) / (1 + SpotRates[compteur - 1] / (Frequency / compteur));
                    ForwardRates[compteur] = Frequency * (intermediare - 1);

                }
            }

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"/Users/macintosh/Desktop/courbes.txt"))
            {
                foreach (double i in SpotRates)
                {
                    file.WriteLine((int)(i*1000000));
                }
                foreach (double i in ForwardRates)
                {
                    file.WriteLine((int)(i*1000000));
                }
            }
        }

        //Pricer un swap revient a calculer le taux fixe (fixed leg)
        public double SwapPricing()
        {
            constructYieldCurves();
            //Variables locales qui serviront que pour le calcul dans cette fonction
            double interm1 = 0 ;
            double interm2 = 0;
            
            for (int i = 0 ; i < SpotRates.Length; i++)
            {
                interm1 += ForwardRates[i]/(1 + SpotRates[i] / (Frequency / (i + 1)));
                interm2 += 1/(1 + SpotRates[i] / (Frequency / (i + 1)));
            }

            return interm1 / interm2;
        }
    }
}