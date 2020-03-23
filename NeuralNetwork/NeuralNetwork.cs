using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        public NeuralNetwork(Topology topology)
        {
            Topology = topology;
            Layers = new List<Layer>();

            CreateInputLayer();
            CreateHiddenLayers();
            CreateOutputLayer();
        }

        public Topology Topology { get; }
        public List<Layer> Layers { get; }


        private void CreateInputLayer()
        {
            var inputNeurons = new List<Neuron>();

            for (int i = 0; i < Topology.InputCount; i++)
            {
                var neuron = new Neuron(1, NeuronType.Input);
                inputNeurons.Add(neuron);
            }

            var inputLayer = new Layer(inputNeurons, NeuronType.Input);
            Layers.Add(inputLayer);
        }

        private void CreateHiddenLayers()
        {
            for (int i = 0; i < Topology.HiddenLayers.Count; i++)
            {
                var hiddenNeurons = new List<Neuron>();
                var lastLayer = Layers.Last();

                for (int j = 0; j < Topology.HiddenLayers[i]; j++)
                {
                    var neuron = new Neuron(lastLayer.NeuronsCount);
                    hiddenNeurons.Add(neuron);
                }

                var hiddenLayer = new Layer(hiddenNeurons);
                Layers.Add(hiddenLayer);
            }
        }

        private void CreateOutputLayer()
        {
            var outputNeurons = new List<Neuron>();
            var lastLayer = Layers.Last();

            for (int i = 0; i < Topology.OutputCount; i++)
            {
                var neuron = new Neuron(lastLayer.NeuronsCount, NeuronType.Output);
                outputNeurons.Add(neuron);
            }

            var outputLayer = new Layer(outputNeurons, NeuronType.Output);
            Layers.Add(outputLayer);
        }


        public Neuron FeedForward(params double[] inputSignals)
        {
            SendSignalsToInputNeurons(inputSignals);
            FeedForwardAllLayersAfterInput();

            if (Topology.OutputCount == 1)
            {
                return Layers.Last().Neurons[0];
            }
            else
            {
                return Layers.Last().Neurons.OrderByDescending(n => n.Output).First();
            }
        }

        private void SendSignalsToInputNeurons(params double[] inputSignals)
        {
            for (int i = 0; i < inputSignals.Length; i++)
            {
                var signal = new List<double>() { inputSignals[i] };
                var neuron = Layers[0].Neurons[i];

                neuron.FeedForward(signal);
            }
        }

        private void FeedForwardAllLayersAfterInput()
        {
            for (int i = 1; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                var previousLayerSignals = Layers[i - 1].GetSignals();

                foreach (var neuron in layer.Neurons)
                {
                    neuron.FeedForward(previousLayerSignals);
                }
            }
        }

        public double Learn(List<Tuple<double, double[]>> dataset, int epoch)
        {
            var error = 0.0;

            for (int i = 0; i < epoch; i++)
            {
                foreach (var data in dataset)
                {
                    error += BackPropagation(data.Item1, data.Item2);
                }
            }

            var result = error / epoch;
            return result;
        }


        private double BackPropagation(double expected, params double[] inputs)
        {
            var actual = FeedForward(inputs).Output;
            var errorOutput = actual - expected;

            foreach (var neuron in Layers.Last().Neurons)
            {
                neuron.Learn(errorOutput, Topology.LearningRate);
            }

            for (int i = Layers.Count - 2; i >= 0; i--)
            {
                var layer         = Layers[i];
                var previousLayer = Layers[i + 1];

                for (int j = 0; j < layer.NeuronsCount; j++)
                {
                    var neuron = layer.Neurons[j];

                    for (int k = 0; k < previousLayer.NeuronsCount; k++)
                    {
                        var previousNeuron = previousLayer.Neurons[k];
                        var weight = previousNeuron.Weights[j];
                        var delta = previousNeuron.Delta;

                        var error = weight * delta;
                        neuron.Learn(error, Topology.LearningRate);
                    }
                }
            }

            return errorOutput * errorOutput;
        }
    }
}
