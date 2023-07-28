using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageResizer.Helpers
{
    public class ImageHandler
    {
        public static byte[] CropImage(byte[] imageData, int x, int y, int width, int height)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                using (System.Drawing.Image img = System.Drawing.Image.FromStream(ms))
                {
                    using (Bitmap bmp = new Bitmap(width, height))
                    {
                        bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                            System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(x, y, width, height);
                            g.DrawImage(img, new System.Drawing.Rectangle(0, 0, width, height), cropArea, GraphicsUnit.Pixel);
                        }

                        using (MemoryStream ms2 = new MemoryStream())
                        {
                            bmp.Save(ms2, ImageFormat.Jpeg);
                            return ms2.ToArray();
                        }
                    }
                }
            }
        }

        public static byte[] ResizeImage(byte[] imageData, int width, int height)
        {
            // Read the image data from the file into a byte array

            // Create a new Bitmap object from the image data
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                using (Bitmap bmp = new Bitmap(ms))
                {
                    // Create a new Bitmap object with the desired width and height
                    Bitmap resizedBmp = new Bitmap(width, height);

                    // Create a Graphics object to draw the resized image
                    using (Graphics g = Graphics.FromImage(resizedBmp))
                    {
                        // Set the interpolation mode to high quality bilinear for better image quality
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

                        // Draw the resized image
                        g.DrawImage(bmp, 0, 0, width, height);
                    }

                    // Save the resized image to a new byte array in the same format as the original image
                    using (MemoryStream ms2 = new MemoryStream())
                    {
                        resizedBmp.Save(ms2, bmp.RawFormat);
                        return ms2.ToArray();
                    }
                }
            }
        }
    }
}
