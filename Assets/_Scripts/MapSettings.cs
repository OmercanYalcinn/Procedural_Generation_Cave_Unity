using UnityEngine;
using System;


[Serializable] 
public class MapSettings
{
    [Header("Map Dimensions")]
    public int width = 20;
    public int height = 20;
    public float tileSize = 1f;
    
    [Header("Generation Parameters")]
    public int seed;
    [Range(0, 100)]
    public float fillPercent = 50f;
    public bool useRandomSeed = false;
    
    [Header("Terrain Parameters")]
    public float noiseScale = 20f;
    public float waterThreshold = 0.3f;
    public float mountainThreshold = 0.7f;
}
