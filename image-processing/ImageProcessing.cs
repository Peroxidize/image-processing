using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

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

        static public Bitmap histogram(Bitmap baseImage) {
            int width = baseImage.Width;
            int height = baseImage.Height;
            Bitmap greyImage = greyScale(baseImage);

            Color pixel;
            int[] histogramData = new int[256];
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    pixel = greyImage.GetPixel(x, y);
                    histogramData[pixel.R]++;
                }
            }

            // set the bg to white
            Bitmap processedImage = new Bitmap(256, height);
            for (int x = 0; x < 256; x++) {
                for (int y = 0; y < height; y++) {
                    processedImage.SetPixel(x, y, Color.White);
                }
            }
            // plot the data
            for (int x = 0; x < 256; x++) {
                for (int y = 0; y < Math.Min(histogramData[x] / 5, height); y++) {
                    processedImage.SetPixel(x, height - 1 - y, Color.Black);
                }
            }
            return processedImage;
        }

        static public Bitmap sepia(Bitmap baseImage) {
            int width = baseImage.Width;
            int height = baseImage.Height;
            Bitmap processedImage = new Bitmap(width, height);

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Color pixel = baseImage.GetPixel(x, y);
                    int R = pixel.R;
                    int G = pixel.G;
                    int B = pixel.B;
                    int _R = Math.Min(255, (int)(0.393 * R + 0.769 * G + 0.189 * B));
                    int _G = Math.Min(255, (int)(0.349 * R + 0.686 * G + 0.168 * B));
                    int _B = Math.Min(255, (int)(0.272 * R + 0.534 * G + 0.131 * B));
                    processedImage.SetPixel(x, y, Color.FromArgb(_R, _G, _B));
                }
            }
            return processedImage;
        }

        static public Bitmap subtract(Bitmap baseImage, Bitmap background) {
            int baseWidth = baseImage.Width;
            int baseHeight = baseImage.Height;
            Bitmap subtractedImage = new Bitmap(baseWidth, baseHeight);

            Color green = Color.FromArgb(0, 255, 0);
            int greygreen = (green.R + green.G + green.B) / 3;
            int threshold = 5;

            for (int x = 0; x < baseWidth; x++) {
                for (int y = 0; y < baseHeight; y++) {
                    Color basePixel = baseImage.GetPixel(x, y);
                    Color bgPixel = background.GetPixel(x, y);
                    int grey = (basePixel.R + basePixel.G + basePixel.B) / 3;
                    int value = Math.Abs(grey - greygreen);

                    if (value > threshold) {
                        subtractedImage.SetPixel(x, y, basePixel);
                    } else {
                        subtractedImage.SetPixel(x, y, bgPixel);
                    }
                }
            }

            return subtractedImage;
        }

        //**************************************************************
        //                    Pointer based processing
        //**************************************************************

        public static bool pointer_GreyScale(Bitmap b) {
            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;

                byte red, green, blue;

                for (int y = 0; y < b.Height; ++y) {
                    for (int x = 0; x < b.Width; ++x) {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        p[0] = p[1] = p[2] = (byte)(.299 * red + .587 * green + .114 * blue);

                        p += 3;  ///very good....
					}
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        public static bool pointer_ColorInversion(Bitmap b) {
            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;

                byte red, green, blue;

                for (int y = 0; y < b.Height; ++y) {
                    for (int x = 0; x < b.Width; ++x) {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        p[0] = (byte)(255 - blue);
                        p[1] = (byte)(255 - green);
                        p[2] = (byte)(255 - red);

                        p += 3;  ///very good....
					}
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        public static int[] pointer_Histogram(Bitmap b) {
            int[] result = new int[256];

            BitmapData data = b.LockBits(new Rectangle(Point.Empty, b.Size), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            IntPtr Ptr = data.Scan0;

            unsafe {
                byte* p = (byte*)(void*)Ptr;
                for (int i = 0; i < b.Width; i++) {
                    for (int j = 0; j < b.Height; j++) {
                        // Exclude all transparent pixels
                        if (p[3] == 0) {
                            p += 4;
                            continue;
                        }
                        int g = (int)(.299 * p[2] + .587 * p[1] + .114 * p[0]);
                        if (g > 255) {
                            g = 255;
                        }
                        result[g]++;
                        p[0] = p[1] = p[2] = 255;
                        p += 4;
                    }
                }
            }

            b.UnlockBits(data);

            return result;
        }

        public static bool pointer_Sepia(Bitmap b) {
            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;

                byte red, green, blue;

                for (int y = 0; y < b.Height; ++y) {
                    for (int x = 0; x < b.Width; ++x) {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        //int _R = Math.Min(255, (int)(0.393 * R + 0.769 * G + 0.189 * B));
                        //int _G = Math.Min(255, (int)(0.349 * R + 0.686 * G + 0.168 * B));
                        //int _B = Math.Min(255, (int)(0.272 * R + 0.534 * G + 0.131 * B));

                        p[2] = (byte) Math.Min(255, (int)(0.393 * red + 0.769 * green + 0.189 * blue));
                        p[1] = (byte) Math.Min(255, (int)(0.349 * red + 0.686 * green + 0.168 * blue));
                        p[0] = (byte) Math.Min(255, (int)(0.272 * red + 0.534 * green + 0.131 * blue));

                        p += 3;  ///very good....
					}
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        public static bool pointer_Subtraction(Bitmap b, Bitmap baseImg) {
            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData baseData = baseImg.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr Scan1 = baseData.Scan0;

            unsafe {
                byte* p = (byte*)(void*)Scan0;
                byte* based = (byte*)(void*)Scan1;

                int nOffset = stride - b.Width * 3;

                byte red, green, blue;
                byte based_red, based_green, based_blue;

                Color _green = Color.FromArgb(0, 255, 0);
                byte greygreen = (byte) ((_green.R + _green.G + _green.B) / 3);
                int threshold = 5;

                for (int y = 0; y < b.Height; ++y) {
                    for (int x = 0; x < b.Width; ++x) {
                        based_blue = based[0];
                        based_green = based[1];
                        based_red = based[2];

                        byte grey = (byte) ((based_blue + based_green + based_red) / 3);
                        byte value = (byte) Math.Abs(grey - greygreen);

                        if (value > threshold) {
                            p[0] = based_blue;
                            p[1] = based_green;
                            p[2] = based_red;
                        }

                        p += 3;  ///very good....
                        based += 3;
					}
                    p += nOffset;
                    based += nOffset;
                }
            }

            b.UnlockBits(bmData);
            baseImg.UnlockBits(baseData);

            return true;
        }

        //**************************************************************
        //                 End of pointer based processing
        //**************************************************************
    }
}
