using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HammingNeuralNetwork
{
    public class Neuron
    {
        public double[] Weights;
        public double Y;
        public double S;
    }

    public class NeuralNetwork
    {
        public int n;
        public int m;
        public double T;
        public double E;
        public double Emax;
        public Neuron[] FirstLayer;
        public Neuron[] SecondLayer;
    }

}
