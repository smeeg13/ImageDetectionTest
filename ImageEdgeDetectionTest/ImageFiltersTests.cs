using ImageEdgeDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace ImageEdgeDetectionTest
{
    [TestClass]
    public class ImageFiltersTest
    {

        [TestMethod]
        public void TestApplyFilterNoImage()
        {
            Bitmap result = ImageFilters.ApplyFilter(null, 1, 1, 1, 25);

            Bitmap expected = null;

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void TestApplyFilterMegaNoImage()
        {
            Bitmap result = ImageFilters.ApplyFilterMega(null, 230, 110, Color.Pink);

            Bitmap expected = null;

            Assert.AreEqual(expected, result);
        }
    }
}
