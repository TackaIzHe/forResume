#include <iostream>
#include <math.h>
#include "SFML/Graphics.hpp"


typedef struct {
    float x, y;
} vector2;

vector2 randomGradient(int ix, int iy, int aa) {
    // No precomputed gradients mean this works for any number of grid coordinates
    const unsigned w = 8 * sizeof(unsigned);
    const unsigned s = w / 2;
    unsigned a = ix, b = iy;

    

    a *= 3284157443;

    b ^= a << s | a >> w - s;
    b *= 1911520717+ aa;

    a ^= b << s | b >> w - s;
    a *= 2048419325;
    float random = a * (3.14159265 / ~(~0u >> 1)); // in [0, 2*Pi]

    // Create the vector from the angle
    vector2 v;
    v.x = sin(random);
    v.y = cos(random);

    return v;
}

// Computes the dot product of the distance and gradient vectors.
float dotGridGradient(int ix, int iy, float x, float y, int aa) {
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
    return (a1 - a0) * (3.0 - w * 2.0) * w * w + a0;
}


// Sample Perlin noise at coordinates x, y
float perlin(float x, float y, int aa) {

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

int main()
{

    srand(time(0));
    int aa = rand();
    std::cout << aa;

    const int windowWidth = 1920;
    const int windowHeight = 1080;

    sf::RenderWindow window(sf::VideoMode(windowWidth, windowHeight, 32), "Perlin");

    sf::Uint8* pixels = new sf::Uint8[windowWidth * windowHeight * 4];


    const int GRID_SIZE = 400;


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
                val += perlin(x * freq / GRID_SIZE, y * freq / GRID_SIZE, aa) * amp;
                //val2 += perlin(x * freq / GRID_SIZE, y * freq / GRID_SIZE, aa) * -amp;

                freq *= 2;
                amp /= 2;

            }
            val2 = val * -1;
            val3 = val;

            // Contrast
            val *= 0.9;
            val2 *= 0.8;
            val3 *= 0.7;
            // Clipping
            if (val > 1.0f)
            {
                val = 1.0f;
                val3 = 1.0f;
                val2 = -1.0f;
                
            }
            else if (val < -1.0f)
            {
                val = -1.0f;
                val3 = -1.0f;
                val2 = 1.0f;
              
            }

            // Convert 1 to -1 into 255 to 0
            int color = (int)(((val + 2.0f) * 0.5f) * 255);
            int color2 = (int)(((val2 + 2.0f) * 0.504f) * 255);
            int color3 = (int)(((val3 + 0.5f) * 0.5f) * 180);

            // Set pixel color
            pixels[index] = color3 - 32;
            pixels[index + 1] = color;
            pixels[index + 2] = color2;
            pixels[index + 3] = 250;


            
            
        }
    }

    sf::Texture texture;
    sf::Sprite sprite;


    texture.create(windowWidth, windowHeight);

    texture.update(pixels);
    

    sprite.setTexture(texture);

    while (window.isOpen())
    {
        sf::Event event;
        while (window.pollEvent(event))
        {
            if (event.type == sf::Event::Closed)
                window.close();
        }

        window.clear();
        window.draw(sprite);


        window.display();

    }

    return 0;
}