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

        public Bitmap originalBitmap = (Bitmap)System.Drawing.Image.FromFile(@"C:\chantier.jpg", true);
        public Bitmap previewBitmap = null;
        public Bitmap resultBitmap = null;
        public double[,] matrix { get; set; }

        public Bitmap openImage(int width)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image file.";
            ofd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            ofd.Filter += "|Bitmap Images(*.bmp)|*.bmp";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(ofd.FileName);
                originalBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
                streamReader.Close();

                return ExtBitmap.CopyToSquareCanvas(originalBitmap,width);
            }

            return null;
        }

        public void SaveImage(System.Drawing.Image resultImg, System.Drawing.Image previewImg)
        {
            if (resultImg == null)
            {
                resultBitmap = (Bitmap)previewImg;
            }
            else
            {
                resultBitmap = (Bitmap)previewImg;
            }

            if (resultBitmap != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Specify a file name and file path";
                sfd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
                sfd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(sfd.FileName).ToUpper();
                    ImageFormat imgFormat = ImageFormat.Png;

                    if (fileExtension == "BMP")
                    {
                        imgFormat = ImageFormat.Bmp;
                    }
                    else if (fileExtension == "JPG")
                    {
                        imgFormat = ImageFormat.Jpeg;
                    }

                    StreamWriter streamWriter = new StreamWriter(sfd.FileName, false);
                    resultBitmap.Save(streamWriter.BaseStream, imgFormat);
                    streamWriter.Flush();
                    streamWriter.Close();

                    resultBitmap = null;
                }
            }
        }

        public void applyXY(ListBox listBoxXFilter, ListBox listBoxYFilter, PictureBox result, PictureBox preview, int value, Label error)
        {
            if (listBoxXFilter.SelectedItem.ToString().Length > 0 && listBoxYFilter.SelectedItem.ToString().Length > 0)
            {
                Filter(listBoxXFilter.SelectedItem.ToString(), listBoxYFilter.SelectedItem.ToString(), preview,  value, result, error);
                ConvertToXYCoord(preview);
            }
            else
            {
                error.Text = "2 filters must be selected";
            }
        }

        public void ConvertToXYCoord(PictureBox result)
        {
            string coord = "";
            int width = result.Image.Width;
            int height = result.Image.Height;
            System.Drawing.Size size = new System.Drawing.Size(width, height);
            Bitmap bitmapIMG = new Bitmap(result.Image, width, height);

            List<ImageEdgeDetection.coord> coorArray = new List<ImageEdgeDetection.coord>();

            int x = 0;
            int y = 0;
            double newX;
            double newY;

            for (x = 0; x < width; x++)
            {
                for (y = 0; y < height; y++)
                {
                    Color pixelColor = Color.FromArgb(bitmapIMG.GetPixel(x, y).ToArgb());
                    if (pixelColor.Name != "ff000000" && pixelColor.Name != "0")
                    {
                        newX = Convert.ToDouble(x);
                        newY = Convert.ToDouble(y);
                        int angle = 110;

                        //Rotate
                        newX = newX * Math.Cos(angle) - newY * Math.Sin(angle);
                        newY = newX * Math.Sin(angle) + newY * Math.Cos(angle);

                        coord = coord + newX.ToString() + "," + newY.ToString() + "|";
                    }
                }
            }
        }

        public void GetMatrix(string filter)
        {
            switch (filter)
            {
                case "Laplacian3x3":
                    matrix = Matrix.Laplacian3x3;
                    break;
                case "Laplacian5x5":
                    matrix = Matrix.Laplacian5x5;
                    break;
                case "LaplacianOfGaussian":
                    matrix = Matrix.LaplacianOfGaussian;
                    break;
                case "Gaussian3x3":
                    matrix = Matrix.Gaussian3x3;
                    break;
                case "Gaussian5x5Type1":
                    matrix = Matrix.Gaussian5x5Type1;
                    break;
                case "Gaussian5x5Type2":
                    matrix = Matrix.Gaussian5x5Type2;
                    break;
                case "Sobel3x3Horizontal":
                    matrix = Matrix.Sobel3x3Horizontal;
                    break;
                case "Sobel3x3Vertical":
                    matrix = Matrix.Sobel3x3Vertical;
                    break;
                case "Prewitt3x3Horizontal":
                    matrix = Matrix.Prewitt3x3Horizontal;
                    break;
                case "Prewitt3x3Vertical":
                    matrix = Matrix.Prewitt3x3Vertical;
                    break;
                case "Kirsch3x3Horizontal":
                    matrix = Matrix.Kirsch3x3Horizontal;
                    break;
                case "Kirsch3x3Vertical":
                    matrix = Matrix.Kirsch3x3Vertical;
                    break;
                default:
                    matrix = Matrix.Laplacian3x3;
                    break;
            }
        }

        public void Filter(string xfilter, string yfilter, PictureBox preview, int value, PictureBox result, Label error)
        {
            GetMatrix(xfilter);
            double[,] xFilterMatrix = /*GetMatrix(xfilter);*/matrix;
            GetMatrix(yfilter);
            double[,] yFilterMatrix = /*GetMatrix(yfilter);*/matrix;

            if (preview.Image.Size.Height > 0)
            {
                Bitmap newbitmap = new Bitmap(preview.Image);
                BitmapData newbitmapData = new BitmapData();
                newbitmapData = newbitmap.LockBits(new Rectangle(0, 0, newbitmap.Width, newbitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);

                byte[] pixelbuff = new byte[newbitmapData.Stride * newbitmapData.Height];
                byte[] resultbuff = new byte[newbitmapData.Stride * newbitmapData.Height];

                Marshal.Copy(newbitmapData.Scan0, pixelbuff, 0, pixelbuff.Length);
                newbitmap.UnlockBits(newbitmapData);


                double blue = 0.0;
                double green = 0.0;
                double red = 0.0;

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

                        try
                        {
                            if (greenTotal < Convert.ToInt32(value))
                            {
                                greenTotal = 0;
                            }
                            else
                            {
                                greenTotal = 255;
                            }
                        }
                        catch (Exception)
                        {

                            throw;
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
                result.Image = resultbitmap;
            }
            else
            {
                error.Text = "You must load an image";
            }

        }

        public Bitmap NightFilter(System.Drawing.Image img, int width)
        {

            if (img != null && width != 0)
            {
               Bitmap imgResized = ExtBitmap.CopyToSquareCanvas((Bitmap)img,width);
                previewBitmap = ImageFilters.ApplyFilter(imgResized, 1, 1, 1, 25);

            }else{
                        previewBitmap = null;
            }
            return previewBitmap;
  
        }

        public Bitmap NormalPicture(Bitmap img, int width)
        {

            if (img != null && width > 0)
            {
                Bitmap imgResized = ExtBitmap.CopyToSquareCanvas((Bitmap)img, width);
                previewBitmap = imgResized;

            }
            else
            {
                previewBitmap = null;

            }
            return previewBitmap;

        }

        public Bitmap PinkFilter(System.Drawing.Image img, int width)
        {
            if (img != null && width != 0)
            {
                Color c = Color.Pink;
                Bitmap imgResized = ExtBitmap.CopyToSquareCanvas((Bitmap)img,width);
                previewBitmap = ImageFilters.ApplyFilterMega(imgResized, 230, 110, c);
              
            }
            else {
                previewBitmap = null;

            }  
            return previewBitmap;
        }
    }
}
