using System.Collections.Generic;

namespace NeuralNetworkLibrary
{
    public class Topology
    {
        public Topology(int inputCount, int outputCount, double learningRate, params int[] layers)
        {
            InputCount   = inputCount;
            OutputCount  = outputCount;
            LearningRate = learningRate;
            HiddenLayers = new List<int>();
            HiddenLayers.AddRange(layers);
        }

        public int InputCount { get; }
        public int OutputCount { get; }
        public double LearningRate { get; }
        public List<int> HiddenLayers { get; }
    }
}
