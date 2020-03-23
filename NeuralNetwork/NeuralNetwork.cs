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


        public Neuron Predict(params double[] inputSignals)
        {
            SendSignalsToInputNeurons(inputSignals);
            PredictAllLayersAfterInput();

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

        private void PredictAllLayersAfterInput()
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


        public double Learn(double[] expected, double[,] inputs, int epoch)
        {
            var signalsNorm = Normalization(inputs);
            var signalsScal = Scaling(inputs);

            var error = 0.0;
            for (int i = 0; i < epoch; i++)
            {
                for (int j = 0; j < expected.Length; j++)
                {
                    var output = expected[j];
                    var input = GetRow(inputs, j);

                    error += BackPropagation(output, input);
                }
            }
            
            var result = error / epoch;
            return result;
        }

        public static double[] GetRow(double[,] matrix, int row)
        {
            var columns = matrix.GetLength(1);
            var array = new double[columns];

            for (int i = 0; i < columns; i++)
            {
                array[i] = matrix[row, i];
            }

            return array;
        }

        private double BackPropagation(double expected, params double[] inputs)
        {
            var actual = Predict(inputs).Output;
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

         
        private double[,] Scaling(double[,] inputs)
        {
            var result = new double[inputs.GetLength(0), inputs.GetLength(1)];

            for (int column = 0; column < inputs.GetLength(1); column++)
            {
                var min = inputs[0, column];
                var max = inputs[0, column];

                for (int row = 1; row < inputs.GetLength(0); row++)
                {
                    var item = inputs[row, column];

                    if (item < min)
                    {
                        min = item;
                    }
                    if (item > max)
                    {
                        max = item;
                    }
                }

                var divider = max - min;
                for (int row = 0; row < inputs.GetLength(0); row++)
                {
                    result[row, column] = (inputs[row, column] - min) / divider;
                }
            }

            return result;
        }

        private double[,] Normalization(double[,] inputs)
        {
            var result = new double[inputs.GetLength(0), inputs.GetLength(1)];

            for (int column = 0; column < inputs.GetLength(1); column++)
            {
                var countRows = inputs.GetLength(0);

                // Среднее значение сигнала нейрона.
                var sum = 0.0;
                for (int row = 0; row < countRows; row++)
                {
                    sum += inputs[row, column];
                }
                var average = sum / countRows;

                // Стандартное квадратичное отклонение нейрона.
                var error = 0.0;
                for (int row = 0; row < countRows; row++)
                {
                    error += Math.Pow(inputs[row, column] - average, 2);
                }
                var standardError = Math.Sqrt(error / countRows);

                for (int row = 0; row < countRows; row++)
                {
                    result[row, column] = (inputs[row, column] - average) / standardError;
                }
            }

            return result;
        }
    }
}
