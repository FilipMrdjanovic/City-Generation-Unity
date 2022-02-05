using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    Vector2 RandomUnitVector()
    {
        float random = UnityEngine.Random.Range(0f, 260f);
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }

    float interpolate(float a0, float a1, float w)
    {
         return (float)((a1 - a0) * ((w * (w * 6.0 - 15.0) + 10.0) * w * w * w) + a0);
    }

    float dotGridGradient(int ix, int iy, float x, float y)
    {
        // Get gradient from integer coordinates
        Vector2 gradient = RandomUnitVector();

        // Compute the distance vector
        float dx = x - (float)ix;
        float dy = y - (float)iy;

        // Compute the dot-product
        return (dx * gradient.x + dy * gradient.y);
    }

    public float perlin(float x, float y)
    {
        // Determine grid cell coordinates
        int x0 = (int)x;
        int x1 = x0 + 1;
        int y0 = (int)y;
        int y1 = y0 + 1;

        // Determine interpolation weights
        // Could also use higher order polynomial/s-curve here
        float sx = x - (float)x0;
        float sy = y - (float)y0;

        // Interpolate between grid point gradients
        float n0, n1, ix0, ix1, value;

        n0 = dotGridGradient(x0, y0, x, y);
        n1 = dotGridGradient(x1, y0, x, y);
        ix0 = interpolate(n0, n1, sx);

        n0 = dotGridGradient(x0, y1, x, y);
        n1 = dotGridGradient(x1, y1, x, y);
        ix1 = interpolate(n0, n1, sx);

        value = interpolate(ix0, ix1, sy);
        return value;
    }

    public void Print()
    {
        for (int i = 0; i < 20; i++)
        {
            Console.Write("[");
            for (int j = 0; j < 20; j++)
            {
                Console.Write(perlin(UnityEngine.Random.value, UnityEngine.Random.value) + ", ");
            }
            Console.Write("]\n");
        }
    }

    public float customPerlin(float x, float z)
    {
        return (x + z) % UnityEngine.Random.Range(0.1f, 1);
    }

}