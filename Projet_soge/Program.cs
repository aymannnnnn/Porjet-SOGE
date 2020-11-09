using System;
using ProjetSoge;
namespace Projet_soge


{
    internal class Program
    {
        public static void Main(string[] args)
        {
            double[] forwardRates = new double[8]{0.005,0.01821,0.019694,0.020647,0.037468,0.044047,0.050696,0.067427};
            double[] spotrates = new double[8]{0.005, 0.010420, 0.0105301, 0.0131001, 0.018071, 0.022566, 0.026830, 0.029683};

            Swap swap = new Swap(4,2);
            swap.setForwardRates(forwardRates);
            //swap.setSpotRates(spotrates);
            //swap.constructYieldCurves();
            //Console.WriteLine(string.Join("\n", swap.getSpotRates()));

            Console.Write("Princing du swap => SFR = ");
            Console.WriteLine(swap.SwapPricing());
            //Console.WriteLine(string.Join("\n", swap.getSpotRates()));
            

        }
    }
}