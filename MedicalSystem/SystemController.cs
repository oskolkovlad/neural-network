using NeuralNetworkLibrary;

namespace MedicalSystem
{
    public class SystemController
    {
        public SystemController()
        {
            Topology dataTopology = new Topology(14, 1, 0.1, 7);
            DataNetwork = new NeuralNetwork(dataTopology);

            Topology imageTopology = new Topology(400, 1, 0.1, 200);
            ImageNetwork = new NeuralNetwork(imageTopology);
        }

        public NeuralNetwork DataNetwork { get; }
        public NeuralNetwork ImageNetwork { get; }
    }
}
