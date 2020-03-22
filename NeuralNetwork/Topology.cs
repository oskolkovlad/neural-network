using System.Collections.Generic;

namespace NeuralNetwork
{
    public class Topology
    {
        public Topology(int inputCount, int outputCount, params int[] layers)
        {
            InputCount = inputCount;
            OutputCount = outputCount;
            HiddenLayers = new List<int>();
            HiddenLayers.AddRange(layers);
        }

        public int InputCount { get; }
        public int OutputCount { get; }
        public List<int> HiddenLayers { get; }
    }
}
