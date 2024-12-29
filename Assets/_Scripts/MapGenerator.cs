using System;
using System.Numerics;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    // This is for the check box to open and close randomise map generation
    public bool useRandomSeed;
    
    //The seed name can be given by the creator of the map and after 
    //write string input as an constant same map generation
    public string seed;

    [Range(0,100)]
    [SerializeField] private int randomFillPercent;

    // creation of 2d array for the 2d map
    int[,] map;

    public void GenerateMap(){
        // after the creation of the map according to width and height of 2d map that we would like to create
        map = new int[width, height];
        RandomFillMap();
    }

    void RandomFillMap(){
        if(useRandomSeed){
            seed = Time.time.ToString();
        }
        // the usage of hashvalue is becauase it is a numeric value used to uniquely 
        //identify an object in a hash-based collection like directory or hash set.
        // There is also PRNG usgae
        System.Random psuedoRandom = new System.Random(seed.GetHashCode());
        // psuedo random number is generated requires an initial Seed.

        // Randomize Map Creation is working on below nested for loops
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++){
                map[x,y] = (psuedoRandom.Next(0,100) < randomFillPercent)? 1: 0;
            }
        }
    }
    // The color setting is done by the Gizmos, think like a bits 1 and 0s
    void OnDrawGizmos(){
        if (map != null){
            for (int x = 0; x < width; x++){
                for (int y = 0; y < height; y++){
                    Gizmos.color = (map[x,y] == 1) ? Color.black : Color.white;
                    UnityEngine.Vector3 position = new UnityEngine.Vector3 (-width/2 + x + .5f, 0, -height/2 + y + .5f);
                    Gizmos.DrawCube(position, UnityEngine.Vector3.one);
                }
            }
        }
    }

    // Methods to update parameters from UI
    public void SetWidth(int newWidth) => width = newWidth;
    public void SetHeight(int newHeight) => height = newHeight;
    public void SetRandomFillPercent(int fillPercent) => randomFillPercent = fillPercent;
    public void SetSeed(string newSeed) => seed = newSeed;
    public void SetUseRandomSeed(bool useRandom) => useRandomSeed = useRandom;
}
