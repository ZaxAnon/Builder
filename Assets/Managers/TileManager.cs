using UnityEngine;
using System.Collections;
using System.IO;

public class TileManager : MonoBehaviour
{
    public Terrain terrain;
    public static bool[,] walkable = new bool[TerrainManager.sizeX, TerrainManager.sizeZ];
    public static Tile[,] tile = new Tile[TerrainManager.sizeX, TerrainManager.sizeZ];
    private TerrainData data;
    // Use this for initialization
    void Start()
    {
        data = terrain.terrainData;
        for (int x = 0; x < data.size[0]; x++)
        {
            for (int z = 0; z < data.size[2]; z++)
            {
                tile[x, z] = new EmptyTile();
            }
        }
        tile[0, 0] = new EmptyTile();
        walkable[0, 0] = false;
        walkable[1, 1] = false;
        //updateStatus();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void updateStatus()
    {
        for (int i=0; i < data.size[0]; i++)
        {
            for (int k=0; k < data.size[2]; k++)
            {
                walkable[i, k] = tile[i, k].isWalkable();
            }
        }
    }
}
