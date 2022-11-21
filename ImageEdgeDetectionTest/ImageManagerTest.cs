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
        public void TestGetMatrix()
        {
            ImageManager IM = new ImageManager();
            double [,] matrix = IM.GetMatrix("Kirsch3x3Vertical");
            double[,] result = { {  5, -3, -3, },
                                  {  5,  0, -3, },
                                  {  5, -3, -3, }, };
            Assert.AreEqual(matrix, result);
        }
    }
}
