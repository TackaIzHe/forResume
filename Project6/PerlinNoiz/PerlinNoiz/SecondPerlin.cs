using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PerlinNoiz
{
    internal class SecondPerlin
    {

        public class vector2
        {
            public float x;
            public float y;
        }

vector2 randomGradient(int ix, int iy, int aa)
        {
            // No precomputed gradients mean this works for any number of grid coordinates
            const uint w = 8 * sizeof(uint);
            const uint s = w / 2;
            uint a = (uint)ix, b = (uint)iy;



            a *= 3284157443;

            b ^= a << (int)(s) | a >> (int)(w - s);
            b *= (uint)(1911520717 + aa);

            a ^= b << (int)(s) | b >> (int)(w - s);
            a *= 2048419325;
            float random = (float)(a * (3.14159265 / ~(~0u >> 1))); // in [0, 2*Pi]

            // Create the vector from the angle
            vector2 v = new vector2();
            v.x = (float)(Math.Sin(random));
            v.y = (float)(Math.Cos(random));

            return v;
        }

        // Computes the dot product of the distance and gradient vectors.
        float dotGridGradient(int ix, int iy, float x, float y, int aa)
        {
            // Get gradient from integer coordinates
            vector2 gradient = randomGradient(ix, iy, aa);

            // Compute the distance vector
            float dx = x - (float)ix;
            float dy = y - (float)iy;

            // Compute the dot-product
            return (dx * gradient.x + dy * gradient.y);
        }

        float interpolate(float a0, float a1, float w)
        {
            return (a1 - a0) * (3.0f - w * 2.0f) * w * w + a0;
        }


        // Sample Perlin noise at coordinates x, y
        public float perlin(float x, float y, int aa)
        {

            // Determine grid cell corner coordinates
            int x0 = (int)x;
            int y0 = (int)y;
            int x1 = x0 + 1;
            int y1 = y0 + 1;

            // Compute Interpolation weights
            float sx = x - (float)x0;
            float sy = y - (float)y0;

            // Compute and interpolate top two corners
            float n0 = dotGridGradient(x0, y0, x, y, aa);
            float n1 = dotGridGradient(x1, y0, x, y, aa);
            float ix0 = interpolate(n0, n1, sx);

            // Compute and interpolate bottom two corners
            n0 = dotGridGradient(x0, y1, x, y, aa);
            n1 = dotGridGradient(x1, y1, x, y, aa);
            float ix1 = interpolate(n0, n1, sx);

            // Final step: interpolate between the two previously interpolated values, now in y
            float value = interpolate(ix0, ix1, sy);

            return value;
        }

        public Bitmap SecondPerlinCreate(string seed,string mash)
        {

            Random a = new Random();
            int aa1 = a.Next(1, 101);
            if (seed != "")
                try
                {
                    aa1 = Convert.ToInt32(seed);
                }
                catch
                {
                    MessageBox.Show("Ожидалось число");
                }

            const int windowWidth = 1920;
            const int windowHeight = 1080;
            Bitmap bitmap = new Bitmap(windowWidth, windowHeight);

            int mash1 = 250;
            if (mash != "")
                try
                {
                    mash1 = Convert.ToInt32(mash);
                }
                catch
                {
                    MessageBox.Show("Ожидалось число");
                }




             int GRID_SIZE = mash1; // задаёт маштаб


            for (int x = 0; x < windowWidth; x++)
            {
                for (int y = 0; y < windowHeight; y++)
                {
                    int index = (y * windowWidth + x) * 4;


                    float val = 0;
                    float val2 = 0;
                    float val3 = 0;

                    float freq = 1;
                    float amp = 1;


                    for (int i = 0; i < 9; i++)
                    {
                        val += perlin(x * freq / GRID_SIZE, y * freq / GRID_SIZE, aa1) * amp;

                        freq *= 2;
                        amp /= 2;

                    }

                    val2 = val * -1;
                    val3 = val;

                    // Contrast
                    val *= 0.9f;
                    val2 *= 0.8f;
                    val3 *= 0.7f;
                    // Clipping
                    if (val > 1.0f)
                    {
                        val = 1.0f;
                    }
                    else if (val < -1.0f)
                    {
                        val = 0;
                    }

                    int color1 = (int)(((val + 2f) * 0.5f) * 85);
                    int color2 = (int)(((val2 + 2f) * 0.5f) * 85);
                    int color3 = (int)(((val3 + 2f) * 0.5f) * 85) - 16;

                    System.Drawing.Color color;

                    if (color2 < color3)
                        color = System.Drawing.Color.FromArgb(color3 * 2, color1 * 2, 0);
                    else if (color1 > color2)
                        color = System.Drawing.Color.FromArgb(0, color1 * 2, 0);
                    else if (color2 > color1)
                        color = System.Drawing.Color.FromArgb(0, 0, color2 * 2);
                    else
                        color = System.Drawing.Color.FromArgb(0, 0, 0);

                    bitmap.SetPixel(x, y, color);



                }
            }
            return bitmap;
        }


    }
    }
