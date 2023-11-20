using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace image_processing {
    static public class ImageProcessing {
        static public void displayError() {
            MessageBox.Show("Base image does not exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static public Bitmap basicCopy(Bitmap baseImage) {
            int width = baseImage.Width;
            int height = baseImage.Height;
            Bitmap processedImage = new Bitmap(width, height);

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Color pixel = baseImage.GetPixel(x, y);
                    processedImage.SetPixel(x, y, pixel);
                }
            }
            return processedImage;
        }

        static public Bitmap greyScale(Bitmap baseImage) {
            int width = baseImage.Width;
            int height = baseImage.Height;
            Bitmap processedImage = new Bitmap(width, height);

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Color pixel = baseImage.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    processedImage.SetPixel(x, y, Color.FromArgb(grey, grey, grey));
                }
            }
            return processedImage;
        }

        static public Bitmap colorInversion(Bitmap baseImage) {
            int width = baseImage.Width;
            int height = baseImage.Height;
            Bitmap processedImage = new Bitmap(width, height);

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Color pixel = baseImage.GetPixel(x, y);
                    processedImage.SetPixel(x, y, Color.FromArgb(
                        255 - pixel.R, 
                        255 - pixel.G, 
                        255 - pixel.B));
                }
            }
            return processedImage;
        }
    }
}
