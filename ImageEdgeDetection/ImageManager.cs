using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ImageEdgeDetection
{
    public class ImageManager
    {

        public Bitmap originalBitmap = null;
        public Bitmap previewBitmap = null;
        public Bitmap resultBitmap = null;
        public double[,] matrix { get; set; }
        public String messageError { get; set; }

        public void GetMatrix(string filter)
        {
            switch (filter)
            {
                case "Laplacian3x3":
                    matrix = Matrix.Laplacian3x3;
                    break;
                case "Kirsch3x3Horizontal":
                    matrix = Matrix.Kirsch3x3Horizontal;
                    break;
                default:
                    matrix = Matrix.Kirsch3x3Vertical;
                    break;
                
            }
        }

        public Bitmap Filter(string xfilter, string yfilter, Bitmap preview,int height, int value)
        {
            GetMatrix(xfilter);
            double[,] xFilterMatrix = matrix;
            GetMatrix(yfilter);
            double[,] yFilterMatrix = matrix;

            if (height > 0)
            {
                Bitmap newbitmap = preview;
                BitmapData newbitmapData = new BitmapData();
                newbitmapData = newbitmap.LockBits(new Rectangle(0, 0, newbitmap.Width, newbitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);

                byte[] pixelbuff = new byte[newbitmapData.Stride * newbitmapData.Height];
                byte[] resultbuff = new byte[newbitmapData.Stride * newbitmapData.Height];

                Marshal.Copy(newbitmapData.Scan0, pixelbuff, 0, pixelbuff.Length);
                newbitmap.UnlockBits(newbitmapData);

                double blueX = 0.0;
                double greenX = 0.0;
                double redX = 0.0;

                double blueY = 0.0;
                double greenY = 0.0;
                double redY = 0.0;

                double blueTotal = 0.0;
                double greenTotal = 0.0;
                double redTotal = 0.0;

                int filterOffset = 1;
                int calcOffset = 0;

                int byteOffset = 0;

                for (int offsetY = filterOffset; offsetY <
                    newbitmap.Height - filterOffset; offsetY++)
                {
                    for (int offsetX = filterOffset; offsetX <
                        newbitmap.Width - filterOffset; offsetX++)
                    {
                        blueX = greenX = redX = blueY = greenY = redY = blueTotal = greenTotal = redTotal = 0;

                        byteOffset = offsetY *
                                     newbitmapData.Stride +
                                     offsetX * 4;

                        for (int filterY = -filterOffset;
                            filterY <= filterOffset; filterY++)
                        {
                            for (int filterX = -filterOffset;
                                filterX <= filterOffset; filterX++)
                            {
                                calcOffset = byteOffset +
                                             (filterX * 4) +
                                             (filterY * newbitmapData.Stride);
                                var xCalcul = xFilterMatrix[filterY + filterOffset, filterX + filterOffset];
                                var yCalcul = yFilterMatrix[filterY + filterOffset, filterX + filterOffset];
                                blueX += (double)(pixelbuff[calcOffset]) * xCalcul;
                                greenX += (double)(pixelbuff[calcOffset + 1]) * xCalcul;
                                redX += (double)(pixelbuff[calcOffset + 2]) * xCalcul;
                                blueY += (double)(pixelbuff[calcOffset]) * yCalcul;
                                greenY += (double)(pixelbuff[calcOffset + 1]) * yCalcul;
                                redY += (double)(pixelbuff[calcOffset + 2]) * yCalcul;
                            }
                        }

                        blueTotal = 0;
                        greenTotal = Math.Sqrt((greenX * greenX) + (greenY * greenY));
                        redTotal = 0;

                        
                        if (greenTotal < Convert.ToInt32(value))
                        {
                            greenTotal = 0;
                        }
                        else
                        {
                            greenTotal = 255;
                        }
                       

                        resultbuff[byteOffset] = (byte)(blueTotal);
                        resultbuff[byteOffset + 1] = (byte)(greenTotal);
                        resultbuff[byteOffset + 2] = (byte)(redTotal);
                        resultbuff[byteOffset + 3] = 255;
                    }
                }

                Bitmap resultbitmap = new Bitmap(newbitmap.Width, newbitmap.Height);

                BitmapData resultData = resultbitmap.LockBits(new Rectangle(0, 0,
                                         resultbitmap.Width, resultbitmap.Height),
                                                          ImageLockMode.WriteOnly,
                                                      PixelFormat.Format32bppArgb);

                Marshal.Copy(resultbuff, 0, resultData.Scan0, resultbuff.Length);
                resultbitmap.UnlockBits(resultData);
                return resultbitmap;
            }
            else
            {
                messageError = "You must load an image";
                return null;
            }

        }

        public Bitmap NightFilter(Bitmap bmp, int width)
        {

            if (bmp != null && width != 0)
            {
               Bitmap imgResized = ExtBitmap.CopyToSquareCanvas(bmp, width);
                previewBitmap = ImageFilters.ApplyFilter(imgResized, 1, 1, 1, 25);

            }else{
                        previewBitmap = null;
            }
            return previewBitmap;
  
        }

        public Bitmap NormalPicture(Bitmap bmp, int width)
        {

            if (bmp != null && width > 0)
            {
                Bitmap imgResized = ExtBitmap.CopyToSquareCanvas(bmp, width);
                previewBitmap = imgResized;

            }
            else
            {
                previewBitmap = null;

            }
            return previewBitmap;

        }

        public Bitmap PinkFilter(Bitmap bmp, int width)
        {
            if (bmp != null && width > 0)
            {
                Color c = Color.Pink;
                Bitmap imgResized = ExtBitmap.CopyToSquareCanvas(bmp, width);
                previewBitmap = ImageFilters.ApplyFilterMega(imgResized, 230, 110, c);
              
            }
            else {
                previewBitmap = null;
            }
            
            return previewBitmap;
        }
    }
}
