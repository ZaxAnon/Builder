using UnityEngine;
using System.Collections;
using System.IO;

public class TileManager : MonoBehaviour
{
    public Terrain terrain;
    private bool[,] walkable = new bool[TerrainManager.sizeX, TerrainManager.sizeZ];
    // Use this for initialization
    void Start()
    {
        TerrainData data = terrain.terrainData;
        for (int x = 0; x < data.size[0]; x++)
        {
            for (int z = 0; z < data.size[2]; z++)
            {
                if (data.GetSteepness(x, z) < 0.1)
                {
                    walkable[x, z] = true;
                }
                else
                {
                    walkable[x, z] = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
