using ImageEdgeDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageEdgeDetectionTest
{
    [TestClass]
    public class ImageManagerTest
    {
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
    }
}
