using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int width, int height,int seed,float scale,int octaves,float persistance,float lacunarity,Vector2 offset)
    {
        float[,] noise_map=new float[width,height];

        System.Random rand = new System.Random(seed);
        Vector2[] octave_offset = new Vector2[octaves];
        for(int i = 0; i < octaves; i++)
        {
            float offset_x = rand.Next(-100000, 100000)+offset.x;
            float offset_y = rand.Next(-100000, 100000)+offset.y;
            octave_offset[i]=new Vector2(offset_x,offset_y);
        }


        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = width / 2;
        float halfHeight=height / 2;

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for(int i = 0; i < octaves; i++)
                {
                    float sample_x = (x-halfWidth) / scale * frequency+octave_offset[i].x;
                    float sample_y = (y-halfHeight) / scale * frequency+octave_offset[i].y;

                    float perlin_value= Mathf.PerlinNoise(sample_x,sample_y) * 2 -1;
                    noiseHeight += perlin_value * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;

                    
                }

                if(noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }else if(noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                noise_map[x, y] = noiseHeight;

            }
        }

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                noise_map[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noise_map[x,y]);
            }
        }

        return noise_map;
    }

}
