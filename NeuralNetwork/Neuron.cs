using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
    public class Neuron
    {
        public Neuron(int inputCount, NeuronType type = NeuronType.Normal)
        {
            Type = type;
            Weights = new List<double>();
            Inputs  = new List<double>();

            InitWeightsRandomValues(inputCount);
        }

        private void InitWeightsRandomValues(int inputCount)
        {
            var rnd = new Random();
            for (var i = 0; i < inputCount; i++)
            {
                if (Type == NeuronType.Input)
                {
                    Weights.Add(1);
                }
                else
                {
                    Weights.Add(rnd.NextDouble());
                }
                
                Inputs.Add(0);
            }
        }

        public NeuronType Type { get; }
        public List<double> Weights { get; }
        public List<double> Inputs { get; }
        public double Output { get; private set; }
        public double Delta { get; private set; }


        public double FeedForward(List<double> inputs)
        {
            var sum = 0.0;
            for (int i = 0; i < inputs.Count; i++)
            {
                Inputs[i] = inputs[i];
                sum += inputs[i] * Weights[i];
            }

            if (Type != NeuronType.Input)
                Output = Sigmoid(sum);
            else
                Output = sum;

            return Output;
        }


        // Сигмоида: https://ru.wikipedia.org/wiki/%D0%A1%D0%B8%D0%B3%D0%BC%D0%BE%D0%B8%D0%B4%D0%B0
        private double Sigmoid(double x)   => 1.0 / (1.0 + Math.Exp(-x));
        private double SigmoidDx(double x) => Sigmoid(x) / (1.0 - Sigmoid(x));


        public void Learn(double error, double learningRate)
        {
            if (Type == NeuronType.Input)
            {
                return;
            }

            Delta = error * SigmoidDx(Output);

            for (int i = 0; i < Weights.Count; i++)
            {
                var weight = Weights[i];
                var input  = Inputs[i];

                var newWeight = weight - input * Delta * learningRate;
                Weights[i] = newWeight;
            }
        }


        public override string ToString() => Output.ToString();
    }
}
