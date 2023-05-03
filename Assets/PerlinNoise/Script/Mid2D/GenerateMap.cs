using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public enum DrawMode {
        NoiseMap,
        ColorMap
    };
    public DrawMode drawMode;

    [Header("Map Setting")]
    public int width;
    public int height;
    public float scale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public TerrainType[] regions;

    public void Generate()
    {
        float[,] noise_map=Noise.GenerateNoiseMap(width, height,seed, scale, octaves,persistance,lacunarity, offset);


        Color[] color_map = new Color[width * height];
        for(int y=0;y<height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                float currentHeight = noise_map[x,y];
                for(int i= 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        color_map[y * width + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        DisplayMap display = FindObjectOfType<DisplayMap>();
        if(drawMode==DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noise_map));
        }else if(drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(color_map, width, height));
        }
        
    }

    void OnValidate()
    {
        if (width < 1)
        {
            width = 1;
        }
        if(height < 1)
        {
            height = 1;
            height = 1;
        }
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if(octaves < 0)
        {
            octaves = 0;
        }
    }

}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}