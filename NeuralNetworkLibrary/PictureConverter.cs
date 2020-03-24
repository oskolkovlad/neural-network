using System.Collections.Generic;
using System.Drawing;

namespace NeuralNetworkLibrary
{
    public class PictureConverter
    {
        public int Bound => 128;
        public int Height { get; private set; }
        public int Width { get; private set; }

        public double[] Convert(string path)
        {
            var image = new Bitmap(path);
            var resizeImage = new Bitmap(image, new Size(50, 50));
            Height = resizeImage.Height;
            Width  = resizeImage.Width;

            var size = Height * Width;

            var result = new List<double>();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var pixel = image.GetPixel(x, y);
                    var value = Brightness(pixel);
                    result.Add(value);
                }
            }

            return result.ToArray();
        }

        private int Brightness(Color pixel)
        {
            var result = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B;
            return result < Bound ? 0 : 1;
        }

        public void Save(string path, double[] pixels)
        {
            var image = new Bitmap(Width, Height);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var color = pixels[y * Width + x] == 1 ? Color.White : Color.Black;
                    image.SetPixel(x, y, color);
                }
            }

            image.Save(path);
        }
    }
}
