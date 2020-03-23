using System.Collections.Generic;

namespace NeuralNetwork
{
    public class Layer
    {
        public Layer(List<Neuron> neurons, NeuronType type = NeuronType.Normal)
        {
            // TODO: проверить нейроны на соответствие типу.

            Neurons = neurons;
            Type = type;
        }

        public List<Neuron> Neurons { get; }
        public NeuronType Type { get; }
        public int NeuronsCount => Neurons?.Count ?? 0;


        public List<double> GetSignals()
        {
            var result = new List<double>();

            foreach (var neuron in Neurons)
            {
                result.Add(neuron.Output);
            }

            return result;
        }


        public override string ToString() => Type.ToString();
    }
}
