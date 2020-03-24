using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeuralNetworkLibrary.Tests
{
    [TestClass()]
    public class PictureConverterTests
    {
        [TestMethod()]
        public void ConvertTest()
        {
            var converter = new PictureConverter();
            var inputs = converter.Convert(@"Q:\Projects\Learn\neural-network\NeuralNetworkLibraryTests\images\Parasitized.png");
            converter.Save(@"Q:\Projects\Learn\neural-network\NeuralNetworkLibraryTests\images\Parasitized_1.png", inputs);

            inputs = converter.Convert(@"Q:\Projects\Learn\neural-network\NeuralNetworkLibraryTests\images\Uninfected.png");
            converter.Save(@"Q:\Projects\Learn\neural-network\NeuralNetworkLibraryTests\images\Uninfected_1.png", inputs);
        }
    }
}