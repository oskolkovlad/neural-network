using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeuralNetwork.Tests
{
    [TestClass()]
    public class PictureConverterTests
    {
        [TestMethod()]
        public void ConvertTest()
        {
            var converter = new PictureConverter();
            var inputs = converter.Convert(@"Q:\Projects\Learn\neural-network\NeuralNetworkTests\images\Parasitized.png");
            converter.Save(@"Q:\Projects\Learn\neural-network\NeuralNetworkTests\images\Parasitized_1.png", inputs);

            inputs = converter.Convert(@"Q:\Projects\Learn\neural-network\NeuralNetworkTests\images\Uninfected.png");
            converter.Save(@"Q:\Projects\Learn\neural-network\NeuralNetworkTests\images\Uninfected_1.png", inputs);
        }
    }
}