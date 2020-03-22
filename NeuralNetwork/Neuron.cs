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

            for (var i = 0; i < inputCount; i++)
            {
                Weights.Add(1);
            }
        }

        public NeuronType Type { get; }
        public List<double> Weights { get; }
        public double Output { get; private set; }

        public double FeedForward(List<double> inputs)
        {
            var sum = 0.0;

            for (int i = 0; i < inputs.Count; i++)
            {
                sum += inputs[i] * Weights[i];
            }

            if (Type != NeuronType.Input)
                Output = Sigmoid(sum);
            else
                Output = sum;

            return Output;
        }

        // Сигмоида: https://ru.wikipedia.org/wiki/%D0%A1%D0%B8%D0%B3%D0%BC%D0%BE%D0%B8%D0%B4%D0%B0
        private double Sigmoid(double x) => 1.0 / (1.0 + Math.Exp(-x));

        // TODO: удалить после добавления возможности обучения сети.
        public void SetWeights(params double[] weights)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                Weights[i] = weights[i];
            }
        }


        public override string ToString() => Output.ToString();
    }
}
