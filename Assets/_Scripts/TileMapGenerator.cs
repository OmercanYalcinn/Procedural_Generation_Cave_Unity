
using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    [Header("Tile Prefabs")]//---------------------------------------
    [SerializeField] private GameObject grassTilePrefab;
    [SerializeField] private GameObject waterTilePrefab;
    [SerializeField] private GameObject mountainTilePrefab;

    // currentSettings is the OBJECT that is from MapSettings Class
    private MapSettings currentSettings;
    private System.Random pseudoRandom;

    /*Method: Update Settings: by equalizing the current settings as new settings, they are not same anymore*/
    public void UpdateSettings(MapSettings newSettings){
        currentSettings = newSettings;
    }

    /*Method: Generate Map: For the generating/creating map according to the new settings that is decided on UI*/
    // *NOTE: for future me Omercan, devide several of those placements and parts that has ability to be method themselves to make code cleaner and more understandable
    public void GenerateMap(){
        
        // Null Check on Current Settings

        if (currentSettings == null) return;
        
        // Object to hold all tiles, like parent of all
        GameObject mapParent = new GameObject("TileMap");

        if (currentSettings.useRandomSeed)
        {
            currentSettings.seed = UnityEngine.Random.Range(0, int.MaxValue);
        }
        pseudoRandom = new System.Random(currentSettings.seed);

        for (int x = 0; x < currentSettings.width; x++)
        {
            for (int y = 0; y < currentSettings.height; y++)
            {
                Vector3 tilePosition = new Vector3(
                    x * currentSettings.tileSize,
                    y * currentSettings.tileSize,
                    0
                );

                float noiseValue = Mathf.PerlinNoise(
                    (x + currentSettings.seed) / currentSettings.noiseScale,
                    (y + currentSettings.seed) / currentSettings.noiseScale
                );

                float adjustedThreshold = currentSettings.fillPercent / 100f;
                noiseValue = (noiseValue + adjustedThreshold) / 2f;

                /*Those three prefab decision is happining under those if else logic system to decide which */
                GameObject tilePrefab;
                if(noiseValue < currentSettings.waterThreshold)
                    tilePrefab = waterTilePrefab;
                else if (noiseValue > currentSettings.mountainThreshold)
                    tilePrefab = mountainTilePrefab;
                else
                    tilePrefab = grassTilePrefab;

                if (pseudoRandom.NextDouble() < adjustedThreshold)
                {
                    tilePrefab = grassTilePrefab;
                }

                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                tile.transform.SetParent(mapParent.transform);
            }
        }
    
    }

    public void RegenerateMap(){
        GameObject existingMap = GameObject.Find("TileMap");
        if (existingMap != null)
            Destroy(existingMap);
        
        GenerateMap();
    }

}
