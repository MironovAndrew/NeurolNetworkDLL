using System;
using System.Data;
using System.Drawing;
using System.Net;

namespace NeurolNetworkConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            Topology topology = new Topology(learningRate: 0.01d, inputNeuronsCount: 900, outputNeuronsCount: 10, 140);

            NeurolNetwork neurolNetwork = new NeurolNetwork(topology);

            List<(int[] expected, int[] inputs)> dataset = new List<(int[] expected, int[] inputs)>();

            string directoryPath = @"S:\Desktop\Dataset\Numbers\";

            for (int i = 0; i < 10; i++)
            {
                foreach (var imagePath in Directory.GetFiles(directoryPath + i))
                {
                    int[] nums = new int[10];
                    nums.Select(x => x == 0);
                    nums[i] = 1;


                    int[] trainData = GetPixels(imagePath, 30, 30);

                    dataset.Add((expected: nums, inputs: trainData));
                }
            }
















            var files = new string[]
            {
                //@"S:\Desktop\Dataset\Test\0.png",
                //@"S:\Desktop\Dataset\Test\1.png",
                //@"S:\Desktop\Dataset\Test\2.png",
                //@"S:\Desktop\Dataset\Test\3.png",
                //@"S:\Desktop\Dataset\Test\4.png",
                 @"S:\Desktop\Dataset\Numbers\0\1.png",
                 @"S:\Desktop\Dataset\Numbers\1\1.png",
                 @"S:\Desktop\Dataset\Numbers\2\1.png",
                 @"S:\Desktop\Dataset\Numbers\3\1.png",
                 @"S:\Desktop\Dataset\Numbers\4\1.png",
                 @"S:\Desktop\Dataset\Numbers\5\1.png",
                 @"S:\Desktop\Dataset\Numbers\6\1.png",
                 @"S:\Desktop\Dataset\Numbers\7\1.png",
                 @"S:\Desktop\Dataset\Numbers\8\1.png",
                 @"S:\Desktop\Dataset\Numbers\9\1.png"
            };






            //  int[] predictionDataForZero = GetPixels(files[0], 30, 30);
            //  int[] predictionDataForOne = GetPixels(files[1], 30, 30);
            //  int[] predictionDataForThree = GetPixels(files[3], 30, 30);
            //  int[] predictionDataForEight = GetPixels(files[8], 30, 30);
            //  int[] predictionDataForNine = GetPixels(files[9], 30, 30);
            //
            //  double[] resultForZero = { 0, 1 };
            //  double[] resultForOne = { 0, 1 };
            //  double[] resultForThree = { 0, 1 };
            //  double[] resultForEight = { 0, 1 };
            //  double[] resultForNine = { 0, 1 };
            //
            //  int count = 0;
            //
            //  bool condition = //(resultForZero.ToList().IndexOf(resultForZero.Max()) == 0)
            //       (resultForOne.ToList().IndexOf(resultForOne.Max()) == 1)
            //      && (resultForThree.ToList().IndexOf(resultForThree.Max()) == 3)
            //      && (resultForEight.ToList().IndexOf(resultForEight.Max()) == 8)
            //      && (resultForNine.ToList().IndexOf(resultForNine.Max()) == 9);
            //
            //  while (!condition)
            //  {
            //      neurolNetwork.Train(dataset, 2);
            //
            //      resultForZero = neurolNetwork.Predict(predictionDataForZero);
            //      resultForOne = neurolNetwork.Predict(predictionDataForOne);
            //      resultForThree = neurolNetwork.Predict(predictionDataForThree);
            //      resultForEight = neurolNetwork.Predict(predictionDataForEight);
            //      resultForNine = neurolNetwork.Predict(predictionDataForNine);
            //
            //      Console.WriteLine("Эпоха №" + count);
            //      Console.WriteLine("Ноль: " + String.Join("    ", resultForOne.Select(x => Math.Round(x, 4))));
            //      Console.WriteLine("Три: " + String.Join("    ", resultForThree.Select(x => Math.Round(x, 4))));
            //      Console.WriteLine("Восемь: " + String.Join("    ", resultForEight.Select(x => Math.Round(x, 4))));
            //      Console.WriteLine("Девять: " + String.Join("    ", resultForNine.Select(x => Math.Round(x, 4))));
            //      Console.WriteLine("\n\n");
            //
            //      count = count + 2;
            //  }
            //
            //  Console.Clear();
            //  Console.WriteLine("Эпоха №" + count + 2);



            neurolNetwork.Train(dataset, 100);



            Serialization serialization = new Serialization();
            serialization.Write(neurolNetwork);












            foreach (var file in files)
            {
                int[] predictionData = GetPixels(file, 30, 30);

                double[] neuronsOutputs = neurolNetwork.Predict(predictionData).Select(x=>Math.Round(x,5)).ToArray();


                Console.WriteLine(file);

                Console.WriteLine($"[{String.Join("   ", neuronsOutputs)}]");
                Console.WriteLine(neuronsOutputs.ToList().IndexOf(neuronsOutputs.Max()));


                Console.WriteLine("\n\n\n");
            }









        }





        static int[] GetPixels(string filePath, int width, int height)
        {
            Image image = Image.FromFile(filePath);

            Bitmap picture = new Bitmap(image);

            int[] pixels = new int[width * height];

            int array_index = 0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = picture.GetPixel(x, y);

                    pixels[array_index++] = ((pixelColor.R + pixelColor.G + pixelColor.B) / 3 >= 200 ? 0 : 1);
                }
            }

            return pixels;
        }


    }
}

