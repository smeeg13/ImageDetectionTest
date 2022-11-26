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

        public Bitmap original = new Bitmap(Properties.Resources.nofilter);
        public Bitmap night = new Bitmap(Properties.Resources.night);
        public Bitmap pink = new Bitmap(Properties.Resources.pink);
        public Bitmap laplacianlaplacian = new Bitmap(Properties.Resources.laplacianlaplacian);
        public Bitmap laplaciankirschH = new Bitmap(Properties.Resources.laplaciankirschH);
        public Bitmap laplaciankirschV = new Bitmap(Properties.Resources.laplacianKircshV);
        public Bitmap kirschHlaplacian = new Bitmap(Properties.Resources.kirschHlaplacian);
        public Bitmap kirschHkirschH = new Bitmap(Properties.Resources.kirschHkirschH);
        public Bitmap kirschHkirschV = new Bitmap(Properties.Resources.kirschHkirschV);
        public Bitmap kirschVLaplacian = new Bitmap(Properties.Resources.kirschVlaplacian);
        public Bitmap kirschVkirschH = new Bitmap(Properties.Resources.kirschVkirschH);
        public Bitmap kirschVkirschV = new Bitmap(Properties.Resources.kirschVkirschV);

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
        public void TestGetKirsch3x3Horizontal()
        {
            ImageManager IM = new ImageManager();
            IM.GetMatrix("Kirsch3x3Horizontal");
            double[,] result = { {  5,  5,  5, },
                                 { -3,  0, -3, },
                                 { -3, -3, -3, }, };
            for (int i = 0; i < IM.matrix.GetLength(0); i++)
                for (int j = 0; j < IM.matrix.GetLength(1); j++)
                {
                    Assert.AreEqual(result[i, j], IM.matrix[i, j]);
                }
        }


      

        [TestMethod]
        public void TestNightFilter()
        {
            ImageManager IM = new ImageManager();
            Bitmap result = IM.NightFilter(original, original.Width);
            Bitmap expected = ExtBitmap.CopyToSquareCanvas(night, original.Width);
            //Assert equals width
            Assert.AreEqual(expected.Width, result.Width);
            Assert.AreEqual(expected.Height, result.Height);
            //Assert equals every pixel
            for (int i = 0; i < result.Width-1; i++)
                for (int j = 0; j < result.Height-1; j++) 
                    Assert.AreEqual(expected.GetPixel(i, j), result.GetPixel(i, j));
                
        }

        [TestMethod]
        public void TestPinkFilter()
        {
            ImageManager IM = new ImageManager();
            Bitmap result = IM.PinkFilter(original, original.Width);

            Bitmap expected = ExtBitmap.CopyToSquareCanvas(pink,original.Width);

            //Assert equals width
            Assert.AreEqual(expected.Width, result.Width);
            Assert.AreEqual(expected.Height, result.Height);

            //Assert equals every pixel
            for (int i = 0; i < result.Width - 1; i++)
                for (int j = 0; j < result.Height - 1; j++)
                    Assert.AreEqual(expected.GetPixel(i, j), result.GetPixel(i, j));
                
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
            Bitmap result = IM.NormalPicture(original, original.Width);

            Bitmap expected = ExtBitmap.CopyToSquareCanvas(original, original.Width);

            //Assert equals width
            Assert.AreEqual(expected.Width, result.Width);

            //Assert equals every pixel
            for (int i = 0; i < result.Width - 1; i++)
                for (int j = 0; j < result.Height - 1; j++)
                    Assert.AreEqual(expected.GetPixel(i, j), result.GetPixel(i, j));
        }

        [TestMethod]
        public void TestNormalPictureNoWidth()
        {
            ImageManager IM = new ImageManager();

            Bitmap result = IM.NormalPicture(original, 0);

            Bitmap expected = null;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestNormalPictureNoPicture()
        {
            ImageManager IM = new ImageManager();

            Bitmap result = IM.NormalPicture(null, 100);

            Bitmap expected = null;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestFilterLaplacianLaplacian()
        {
            ImageManager IM = new ImageManager();
            Bitmap expected = ExtBitmap.CopyToSquareCanvas(laplacianlaplacian, original.Width);
            Bitmap result = IM.Filter("Laplacian3x3", "Laplacian3x3", original, original.Height, 100);
            for (int i = 0; i < result.Width - 1; i++)
                for (int j = 0; j < result.Height - 1; j++)
                    Assert.AreEqual(expected.GetPixel(i, j), result.GetPixel(i, j));

        }

        [TestMethod]
        public void TestFilterImageExisting()
        {
            ImageManager IM = new ImageManager();
            Bitmap expected = null;
            Bitmap result = IM.Filter("Kirsch3x3Horizontal", "Laplacian3x3", original, 0, 100);
            Assert.AreEqual(expected, result);
            Assert.AreEqual("You must load an image", IM.messageError);
        }

        [TestMethod]
        public void TestFilterLaplacianKirschH()
        {
            ImageManager IM = new ImageManager();
            Bitmap expected = ExtBitmap.CopyToSquareCanvas(laplaciankirschH, original.Width);
            Bitmap result = IM.Filter("Laplacian3x3", "Kirsch3x3Horizontal", original, original.Height, 100);
            for (int i = 0; i < result.Width - 1; i++)
                for (int j = 0; j < result.Height - 1; j++)
                    Assert.AreEqual(expected.GetPixel(i, j), result.GetPixel(i, j));

        }

        [TestMethod]
        public void TestFilterLaplacianKirschV()
        {
            ImageManager IM = new ImageManager();
            Bitmap expected = ExtBitmap.CopyToSquareCanvas(laplaciankirschV, original.Width);
            Bitmap result = IM.Filter("Laplacian3x3", "Kirsch3x3Vertical", original, original.Height, 100);
            for (int i = 0; i < result.Width - 1; i++)
                for (int j = 0; j < result.Height - 1; j++)
                    Assert.AreEqual(expected.GetPixel(i, j), result.GetPixel(i, j));

        }

        [TestMethod]
        public void TestFilterKirschHLaplacian()
        {
            ImageManager IM = new ImageManager();
            Bitmap expected = ExtBitmap.CopyToSquareCanvas(kirschHlaplacian, original.Width);
            Bitmap result = IM.Filter("Kirsch3x3Horizontal", "Laplacian3x3", original, original.Height, 100);
            for (int i = 0; i < result.Width - 1; i++)
                for (int j = 0; j < result.Height - 1; j++)
                    Assert.AreEqual(expected.GetPixel(i, j), result.GetPixel(i, j));

        }

        [TestMethod]
        public void TestFilterKirschHKirschH()
        {
            ImageManager IM = new ImageManager();
            Bitmap expected = ExtBitmap.CopyToSquareCanvas(kirschHkirschH, original.Width);
            Bitmap result = IM.Filter("Kirsch3x3Horizontal", "Kirsch3x3Horizontal", original, original.Height, 100);
            for (int i = 0; i < result.Width - 1; i++)
                for (int j = 0; j < result.Height - 1; j++)
                    Assert.AreEqual(expected.GetPixel(i, j), result.GetPixel(i, j));

        }

        [TestMethod]
        public void TestFilterKirschHKirschV()
        {
            ImageManager IM = new ImageManager();
            Bitmap expected = ExtBitmap.CopyToSquareCanvas(kirschHkirschV, original.Width);
            Bitmap result = IM.Filter("Kirsch3x3Horizontal", "Kirsch3x3Vertical", original, original.Height, 100);
            for (int i = 0; i < result.Width - 1; i++)
                for (int j = 0; j < result.Height - 1; j++)
                    Assert.AreEqual(expected.GetPixel(i, j), result.GetPixel(i, j));

        }

        [TestMethod]
        public void TestFilterKirschVLaplacian()
        {
            ImageManager IM = new ImageManager();
            Bitmap expected = ExtBitmap.CopyToSquareCanvas(kirschVLaplacian, original.Width);
            Bitmap result = IM.Filter("Kirsch3x3Vertical", "Laplacian3x3", original, original.Height, 100);
            for (int i = 0; i < result.Width - 1; i++)
                for (int j = 0; j < result.Height - 1; j++)
                    Assert.AreEqual(expected.GetPixel(i, j), result.GetPixel(i, j));

        }

        [TestMethod]
        public void TestFilterKischVKirschH()
        {
            ImageManager IM = new ImageManager();
            Bitmap expected = ExtBitmap.CopyToSquareCanvas(kirschVkirschH, original.Width);
            Bitmap result = IM.Filter("Kirsch3x3Vertical", "Kirsch3x3Horizontal", original, original.Height, 100);
            for (int i = 0; i < result.Width - 1; i++)
                for (int j = 0; j < result.Height - 1; j++)
                    Assert.AreEqual(expected.GetPixel(i, j), result.GetPixel(i, j));

        }

        [TestMethod]
        public void TestFilterKirschVKirschV()
        {
            ImageManager IM = new ImageManager();
            Bitmap expected = ExtBitmap.CopyToSquareCanvas(kirschVkirschV, original.Width);
            Bitmap result = IM.Filter("Kirsch3x3Vertical", "Kirsch3x3Vertical", original, original.Height, 100);
            for (int i = 0; i < result.Width - 1; i++)
                for (int j = 0; j < result.Height - 1; j++)
                    Assert.AreEqual(expected.GetPixel(i, j), result.GetPixel(i, j));

        }




    }
}
