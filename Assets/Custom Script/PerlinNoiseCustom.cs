using UnityEngine;
using System;

public class vector2
{
	public float x;
	public float y;
}

public static class GlobalMembers
{
	public static float interpolate(float a0, float a1, float w)
	{
		/* // You may want clamping by inserting:
		 * if (0.0 > w) return a0;
		 * if (1.0 < w) return a1;
		 */
		return (a1 - a0) * w + a0;
		/* // Use this cubic interpolation [[Smoothstep]] instead, for a smooth appearance:
		 * return (a1 - a0) * (3.0 - w * 2.0) * w * w + a0;
		 *
		 * // Use [[Smootherstep]] for an even smoother result with a second derivative equal to zero on boundaries:
		 * return (a1 - a0) * ((w * (w * 6.0 - 15.0) + 10.0) * w * w * w) + a0;
		 */
	}

	/* Create pseudorandom direction vector
	 */
	public static vector2 randomGradient(int ix, int iy)
	{
		// No precomputed gradients mean this works for any number of grid coordinates
		uint w = 8 * sizeof(uint);
		uint s = w / 2; // rotation width
		uint a = (uint)ix;
		uint b = (uint)iy;
		a *= 3284157443;
		//b ^= a << (int)s | (a >> ((int)w - s));
		b *= 1911520717;
		//a ^= b << (int)s | b >> (int)w - s;
		a *= 2048419325;
		float random = (float)(a * (3.14159265 / ~(~0u >> 1))); // in [0, 2*Pi]
		vector2 v = new vector2();
		v.x = Mathf.Sin(random);
		v.y = Mathf.Cos(random);
		//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
		//ORIGINAL LINE: return v;
		return v;
	}

	// Computes the dot product of the distance and gradient vectors.
	public static float dotGridGradient(int ix, int iy, float x, float y)
	{
		// Get gradient from integer coordinates
		vector2 gradient = randomGradient(ix, iy);

		// Compute the distance vector
		float dx = x - (float)ix;
		float dy = y - (float)iy;

		// Compute the dot-product
		return (dx * gradient.x + dy * gradient.y);
	}

	// Compute Perlin noise at coordinates x, y
	public static float perlin(float x, float y)
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
		float n0;
		float n1;
		float ix0;
		float ix1;
		float value;

		n0 = dotGridGradient(x0, y0, x, y);
		n1 = dotGridGradient(x1, y0, x, y);
		ix0 = interpolate(n0, n1, sx);

		n0 = dotGridGradient(x0, y1, x, y);
		n1 = dotGridGradient(x1, y1, x, y);
		ix1 = interpolate(n0, n1, sx);

		value = interpolate(ix0, ix1, sy);
		return value;
	}
}
