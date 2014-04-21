using UnityEngine;
using System.Collections;
using System.IO;

public class TileManager : MonoBehaviour
{
    public Terrain terrain;
    public static bool[,] walkable = new bool[TerrainManager.sizeX, TerrainManager.sizeZ];
    public static bool[,] buildable = new bool[TerrainManager.sizeX, TerrainManager.sizeZ];
    private static Tile[,] tile = new Tile[TerrainManager.sizeX, TerrainManager.sizeZ];
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

        // Initial polling of the tiles. Doubtful utility. Probably better than trying to generate tiles by SetMouseTile or SetTile.
        for (int i = 0; i < data.size[0]; i++)
        {
            for (int k = 0; k < data.size[2]; k++)
            {
                walkable[i, k] = tile[i, k].isWalkable();

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        // See below.
        /*
        elapsedSinceRefresh += Time.deltaTime;
        if (elapsedSinceRefresh > 0.5)
        {
            StartCoroutine(updateStatusCor());
            //updateStatus();
            elapsedSinceRefresh = 0;
        }*/
    }
    // All the following crap is to poll ALL the game's tiles periodically for Walkable. While this could prove useful (moving tiles? Tiles that change their properties? Something Something Dark Side?), 
    // it is DEFINITELY too resource-heavy.

    /*
    private float elapsed = 0;
    private float elapsedSinceRefresh = 0;
    private int countdownToRefresh;

    private int calcBlocks = 30;
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
    private IEnumerator updateStatusCor()
    {
        for (int u = 0; u < calcBlocks; u++)
        {
            for (int i = 0; i < data.size[0]; i++)
            {
                for (int k = 0; k < data.size[2]/calcBlocks; k++)
                {
                    walkable[i, k] = tile[i, k].isWalkable();
                }
            }
            Debug.Log("Frame end, yielding");
         
            yield return null;
        }
    }*/
    public static Tile getMouseTile(){
        return tile[getX(), getZ()];
    }
    public static void setMouseTile(Tile newTile)
    {
        setTile(newTile, getX(), getZ());
    }
    public static Tile getTile(int x, int z)
    {
        return tile[x, z];
    }
    public static void setTile(Tile newTile, int x, int z)
    {
        tile[x, z] = newTile;
        walkable[x, z] = newTile.isWalkable();
        buildable[x, z] = newTile.isBuildable();
    }
    public static Vector3 getPos()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = new RaycastHit();
        Physics.Raycast(ray, out hit, 100);
        Vector3 pos = hit.point;
        if (pos == new Vector3(0, 0, 0))
        {
            throw new System.Exception("Raycast miss");
        }
        pos = new Vector3(Mathf.Floor(pos[0]),
                            Mathf.Floor(pos[1]),
                            Mathf.Floor(pos[2]));

        return pos;

    }
    public static int getX(){
        return Mathf.FloorToInt(getPos()[0]);
    }
    public static int getZ()
    {
        return Mathf.FloorToInt(getPos()[2]);
    }
}
