using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NeuralNetwork.Tests
{
    [TestClass()]
    public class NeuralNetworkTests
    {
        [TestMethod()]
        public void FeedForwardTest()
        {
            // Arrange
            var topology = new Topology(4, 1, 0.1, 2);
            var neuralNetwork = new NeuralNetwork(topology);

            double[] outputs = { 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1 };
            double[,] inputs =
            {
                // Результат - Пациент болен  - 1
                //             Пациент здоров - 0

                // Неправильная температура T
                // Хороший возраст A
                // Курит S
                // Правильно питается F
                //T  A  S  F
                { 0, 0, 0, 0 },
                { 0, 0, 0, 1 },
                { 0, 0, 1, 0 },
                { 0, 0, 1, 1 },
                { 0, 1, 0, 0 },
                { 0, 1, 0, 1 },
                { 0, 1, 1, 0 },
                { 0, 1, 1, 1 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 1 },
                { 1, 0, 1, 0 },
                { 1, 0, 1, 1 },
                { 1, 1, 0, 0 },
                { 1, 1, 0, 1 },
                { 1, 1, 1, 0 },
                { 1, 1, 1, 1 }
            };


            // Act
            var errorOutput = neuralNetwork.Learn(outputs, inputs, 100000);

            var results = new List<double>();
            for (int i = 0; i < outputs.Length; i++)
            {
                var row = NeuralNetwork.GetRow(inputs, i);
                var result = neuralNetwork.FeedForward(row).Output;
                results.Add(result);
            }


            // Assert
            for (int i = 0; i < results.Count; i++)
            {
                var expected = Math.Round(outputs[i], 3);
                var actual   = Math.Round(results[i], 3);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void DataSetTest()
        {
            // Arrange
            var outputs = new List<double>();
            var inputs = new List<double[]>();


            using(var sread = new StreamReader("heart.csv"))
            {
                var header = sread.ReadLine();
                var line = "";
                while(!sread.EndOfStream)
                {
                    line = sread.ReadLine();
                    var values = line.Split(',').Select(v => Convert.ToDouble(v.Replace('.', ','))).ToList();
                    var input = values.Take(values.Count - 1).ToArray();
                    var output = values.Last();

                    inputs.Add(input);
                    outputs.Add(output);
                }
            }

            var topology = new Topology(outputs.Count, 1, 0.1, outputs.Count / 2);
            var neuralNetwork = new NeuralNetwork(topology);


            // Act
            var inputSignals = new double[inputs.Count, inputs[0].Length];
            for (int i = 0; i < inputSignals.GetLength(0); i++)
            {
                for (int j = 0; j < inputSignals.GetLength(1); j++)
                {
                    inputSignals[i, j] = inputs[i][j];
                }
            }

            var errorOutput = neuralNetwork.Learn(outputs.ToArray(), inputSignals, 1000);

            var results = new List<double>();
            for (int i = 0; i < outputs.Count; i++)
            {
                var row = inputs[i];
                var result = neuralNetwork.FeedForward(row).Output;
                results.Add(result);
            }


            // Assert
            for (int i = 0; i < results.Count; i++)
            {
                var expected = Math.Round(outputs[i], 3);
                var actual = Math.Round(results[i], 3);
                Assert.AreEqual(expected, actual);
            }
        }
    }
}