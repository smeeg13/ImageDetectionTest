 /*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ImageEdgeDetection.Image.Testes;

namespace ImageEdgeDetection
{
    public partial class MainForm : Form
    {
        private Bitmap originalBitmap = null;
        private Bitmap previewBitmap = null;
        private Bitmap resultBitmap = null;
        
        public MainForm()
        {
            InitializeComponent();
            IfFilters(false);

        }

        private void btnOpenOriginal_Click(object sender, EventArgs e)
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

                previewBitmap = originalBitmap.CopyToSquareCanvas(pictureBoxPreview.Width);
                pictureBoxPreview.Image = previewBitmap;

                IfFilters(false);
            }
        }

        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {
           if(pictureBoxResult.Image == null)
            {
                resultBitmap = (Bitmap)pictureBoxPreview.Image;
            }
            else
            {
                resultBitmap = (Bitmap)pictureBoxResult.Image;
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

        private void listBoxYFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBoxXFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonApplyFilters_Click(object sender, EventArgs e)
        {

            if (listBoxXFilter.SelectedItem.ToString().Length > 0 && listBoxYFilter.SelectedItem.ToString().Length > 0)
            {
                filter(listBoxXFilter.SelectedItem.ToString(), listBoxYFilter.SelectedItem.ToString());
                ConvertToXYCoord(pictureBoxResult);
            }
            else
            {
                labelErrors.Text = "2 filters must be selected";
            }
        }

        public void ConvertToXYCoord(PictureBox pictureBoxelem)
        {
            string coord = "";
            int width = pictureBoxelem.Image.Width;
            int height = pictureBoxelem.Image.Height;
            System.Drawing.Size size = new System.Drawing.Size(width, height);
            Bitmap bitmapIMG = new Bitmap(pictureBoxResult.Image, width, height);

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

        public double [,] getMatrix(string filter)
        {
            switch (filter)
            {
                case "Laplacian3x3":
                    return Matrix.Laplacian3x3;
                case "Laplacian5x5":
                    return Matrix.Laplacian5x5;
                case "LaplacianOfGaussian":
                    return Matrix.LaplacianOfGaussian;
                case "Gaussian3x3":
                    return Matrix.Gaussian3x3;
                case "Gaussian5x5Type1":
                    return Matrix.Gaussian5x5Type1;
                case "Gaussian5x5Type2":
                    return Matrix.Gaussian5x5Type2;
                case "Sobel3x3Horizontal":
                    return Matrix.Sobel3x3Horizontal;
                case "Sobel3x3Vertical":
                    return Matrix.Sobel3x3Vertical;
                case "Prewitt3x3Horizontal":
                    return Matrix.Prewitt3x3Horizontal;
                case "Prewitt3x3Vertical":
                    return Matrix.Prewitt3x3Vertical;
                case "Kirsch3x3Horizontal":
                    return Matrix.Kirsch3x3Horizontal;
                case "Kirsch3x3Vertical":
                    return Matrix.Kirsch3x3Vertical;
                default:
                    return Matrix.Laplacian3x3;
            }
        }

        public void filter(string xfilter, string yfilter)
        {
            double[,] xFilterMatrix = getMatrix(xfilter);
            double[,] yFilterMatrix = getMatrix(yfilter);
            
            if (pictureBoxPreview.Image.Size.Height > 0)
            {
                Bitmap newbitmap = new Bitmap(pictureBoxPreview.Image);
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
                                var xCalcul = xFilterMatrix[filterY + filterOffset,filterX + filterOffset];
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
                            if (greenTotal < Convert.ToInt32(trackBarThreshold.Value))
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
                pictureBoxResult.Image = resultbitmap;
            }
            else
            {
                labelErrors.Text = "You must load an image";
            }
        }

        private void picPreview_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnNightFilter_Click(object sender, EventArgs e)
        {
            if(pictureBoxPreview.Image != null)
            {
                pictureBoxPreview.Image = originalBitmap.CopyToSquareCanvas(pictureBoxPreview.Width); 
                 pictureBoxPreview.Image = ImageFilters.ApplyFilter(new Bitmap(pictureBoxPreview.Image), 1, 1, 1, 25);
                 IfFilters(true);
            }
               
        }

        private void btnPinkFilter_Click(object sender, EventArgs e)
        {
            if (pictureBoxPreview.Image != null)
            {
                Color c = Color.Pink;
                pictureBoxPreview.Image = originalBitmap.CopyToSquareCanvas(pictureBoxPreview.Width);

                pictureBoxPreview.Image = ImageFilters.ApplyFilterMega(new Bitmap(pictureBoxPreview.Image), 230, 110, c);


                IfFilters(true);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            pictureBoxPreview.Image = originalBitmap.CopyToSquareCanvas(pictureBoxPreview.Width);
            IfFilters(false);

        }

        private void trackBarThreshold_Scroll(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void IfFilters(Boolean isFiltered)
        {
            listBoxXFilter.Visible = isFiltered;
            listBoxYFilter.Visible = isFiltered;
            label4.Visible = isFiltered;
            pictureBoxResult.Visible = isFiltered;
            label1.Visible = isFiltered;
            label2.Visible = isFiltered;
            listBoxYFilter.Visible = isFiltered;
            buttonApplyFilters.Visible = isFiltered;
            trackBarThreshold.Visible = isFiltered;
            label6.Visible = isFiltered;

            pictureBoxResult.Image = null;

        }

        private void btnNoFilter_click(object sender, EventArgs e)
        {
            if (pictureBoxPreview.Image != null)
            {
                IfFilters(true);
            }
        }
    }
}
