using ImageEdgeDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageEdgeDetectionTest
{
    [TestClass]
    public class ImageManagerTest
    {

        public static Bitmap original = (Bitmap)Image.FromFile(@"C:\chantier.jpg", false);
        public static Bitmap night = (Bitmap)Image.FromFile(@"C:\ChantierNight.jpg", false);
        public static Bitmap pink = (Bitmap)Image.FromFile(@"C:\ChantierPink.jpg", false);


        [TestMethod]
        public void TestGetKirsh3x3Vertical()
        {
            ImageManager IM = new ImageManager();
            IM.GetMatrix("Kirsch3x3Vertical");
            double[,] result =  { {  5, -3, -3, },
                                  {  5,  0, -3, },
                                  {  5, -3, -3, }, };
            for (int i = 0; i < IM.matrix.GetLength(0); i++)
                for (int j = 0; j < IM.matrix.GetLength(1); j++)
                {
                    Assert.AreEqual(result[i, j], IM.matrix[i, j]);
                }  
        }

        [TestMethod]
        public void TestGetLaplacian3x3()
        {
            ImageManager IM = new ImageManager();
            IM.GetMatrix("Laplacian3x3");
            double[,] result =  { { -1, -1, -1,  },
                                  { -1,  8, -1,  },
                                  { -1, -1, -1,  }, };
            for (int i = 0; i < IM.matrix.GetLength(0); i++)
                for (int j = 0; j < IM.matrix.GetLength(1); j++)
                {
                    Assert.AreEqual(result[i, j], IM.matrix[i, j]);
                }
        }

        [TestMethod]
        public void TestGetGaussian3x3()
        {
            ImageManager IM = new ImageManager();
            IM.GetMatrix("Gaussian3x3");
            double[,] result =  { { 1, 2, 1, },
                                  { 2, 4, 2, },
                                  { 1, 2, 1, }, };
            for (int i = 0; i < IM.matrix.GetLength(0); i++)
                for (int j = 0; j < IM.matrix.GetLength(1); j++)
                {
                    Assert.AreEqual(result[i, j], IM.matrix[i, j]);
                }
        }


      

        [TestMethod]
        public void TestNightFilter()
        {
            Bitmap originalBitmap = (Bitmap)System.Drawing.Image.FromFile(@"C:\chantier.jpg", true);
            ImageManager IM = new ImageManager();
            Bitmap result = IM.NightFilter(originalBitmap, 100);

            Bitmap expected = ExtBitmap.CopyToSquareCanvas(night,100);

            //Assert equals width
            Assert.AreEqual(expected.Width, result.Width);

            //Assert equals every pixel
            for (int i = 0; i < result.Width; i++)
            {
                for (int x = 0; x < result.Height; x++)
                {
                    for (int j = 0; j < expected.Width; j++)
                    {
                        for (int y = 0; y < expected.Height; y++)
                        {
                            Assert.AreEqual(expected.GetPixel(j, y), result.GetPixel(i, x));
                        }
                    }
                }
            }

        }

        [TestMethod]
        public void TestPinkFilter()
        {
            ImageManager IM = new ImageManager();
            Bitmap result = IM.PinkFilter(original, 100);

            Bitmap expected = ExtBitmap.CopyToSquareCanvas(pink,100);

            //Assert equals width
            Assert.AreEqual(expected.Width, result.Width);
            Assert.AreEqual(expected.Height, result.Height);

            //Assert equals every pixel
            for (int i = 0; i < result.Width; i++)
            {
                for (int x = 0; x < result.Height; x++)
                {
                    for (int j = 0; j < expected.Width; j++)
                    {
                        for (int y = 0; y < expected.Height; y++)
                        {
                            Assert.AreEqual(expected.GetPixel(j, y), result.GetPixel(i, x));
                        }
                    }
                }
            }
        }



        [TestMethod]
        public void TestNightFilterNoImage()
        {
            ImageManager IM = new ImageManager();
            Bitmap result = IM.NightFilter(null, 100);

            Bitmap expected = null;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestNightFilterNoWidth()
        {

        ImageManager IM = new ImageManager();
           Bitmap result =IM.NightFilter(original, 0);

            Bitmap expected = null;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestPinkFilterNoImage()
        {
            ImageManager IM = new ImageManager();
            Bitmap result = IM.PinkFilter(null, 100);

            Bitmap expected = null;
            //Console.WriteLine(result);

            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void TestPinkFilterNoWidth()
        {
            ImageManager IM = new ImageManager();

            Bitmap result = IM.PinkFilter(original, 0);

            Bitmap expected = null;

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void TestNormalPicture()
        {
            ImageManager IM = new ImageManager();
            Bitmap result = IM.NormalPicture(original, 100);

            Bitmap expected = ExtBitmap.CopyToSquareCanvas(original, 100);

            //Assert equals width
            Assert.AreEqual(expected.Width, result.Width);

            //Assert equals every pixel
            for (int i = 0; i < result.Width; i++)
            {
                for (int x = 0; x < result.Height; x++)
                {
                    for (int j = 0; j < expected.Width; j++)
                    {
                        for (int y = 0; y < expected.Height; y++)
                        {
                            Assert.AreEqual(expected.GetPixel(j, y), result.GetPixel(i, x));
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TestNormalPictureNoWidth()
        {
            ImageManager IM = new ImageManager();

            Bitmap result = IM.NormalPicture(original, 0);

            Bitmap expected = null;

            Assert.AreEqual(expected, result);
        }

    }
}
