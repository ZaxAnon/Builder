using UnityEngine;
using System.Collections.Generic;

public class Selection : MonoBehaviour {
    public bool enableColorRefresh = false;

    protected Vector3 origin  =   new Vector3(0, 0, 0);
    protected Vector3 pos     =   new Vector3(0, 0, 0);

    public GameObject tilePrefab;
    public Material RedDiffuseMat, RedLineMat, BlueDiffuseMat, BlueLineMat;
    protected Dictionary<Vector2, GameObject> tiles = new Dictionary<Vector2, GameObject>();
    protected int sizeX=3, sizeZ=2, oldX=0, oldZ=0;

    protected Material[] blueMats = new Material[2];
    protected Material[] redMats = new Material[2];
    // Time variables
    protected float TimeSinceLastTileRefresh = 0, TimeSinceLastColorRefresh = 0;

    public static GameObject Create(Object prefab)
    {
        GameObject newObject = Instantiate(prefab) as GameObject;
        //Selection selection = newObject.GetComponent<Selection>();
        return newObject;
    }
	// Use this for initialization
	void Start () {

        blueMats[0] = BlueLineMat;
        blueMats[1] = BlueDiffuseMat;
        redMats[0] = RedLineMat;
        redMats[1] = RedDiffuseMat;


        Vector3 pos = new Vector3(0, 0, 0);
        try
        {
            pos = TileManager.getPos();
        }
        catch (System.Exception)
        {
            return;
        }
        // initialize plane & selection
        origin = pos + new Vector3(0, 0.01f, 0);

        transform.position = origin;
        transform.localScale = new Vector3(1f / 2000f, 1, 1f / 2000f);
        renderer.material.mainTextureScale = new Vector2(1, 1);
	}
    void OnGUI()
    {

            var p = Input.mousePosition;
            GUI.Label(new Rect(p[0] + 10, Screen.height - p[1] - 10, 100, 20), "" + sizeX + " x " + sizeZ);


    }

	void Update () {
        handleSize();

        if (TimeSinceLastColorRefresh > ((sizeX + sizeZ + 100) / 1000))
        {
            if (enableColorRefresh)
            {
                TimeSinceLastColorRefresh = 0;
            }
            colorTiles();
        }
        TimeSinceLastColorRefresh += Time.deltaTime;
	}
    protected void handleSize()
    {
        // This handles the LOCATION of the moving frame.
        if (Input.GetMouseButton(0))
        {
            try
            {
                pos = TileManager.getPos();
            }
            catch (System.Exception)
            {
                return;
            }

            // update plane & selection
            transform.position = origin;
            sizeX = Mathf.CeilToInt(Mathf.Abs(pos[0] - origin[0])) + 1;
            sizeZ = Mathf.CeilToInt(Mathf.Abs(pos[2] - origin[2])) + 1;


            if (pos[0] < origin[0])
            {
                transform.Translate(1 - sizeX, 0, 0);
            }
            if (pos[2] < origin[2])
            {
                transform.Translate(0, 0, 1 - sizeZ);
            }

            renderer.enabled = false;
            if (oldX != sizeX || oldZ != sizeZ)
            {
                makeTiles();
            }


            oldX = sizeX;
            oldZ = sizeZ;



        }
    }
    protected void makeTiles()
        // This function controls the size of the selection grid.
        // It is responsible for the creation of new tiles.
    {
        /*
        if (TimeSinceLastTileRefresh < 0.1f)
        {
            return;
        }
        TimeSinceLastTileRefresh = 0;
         */
        int aX = Mathf.RoundToInt(transform.localPosition[0]);
        int aZ = Mathf.RoundToInt(transform.localPosition[2]);
        cleanOutOfRangeTiles(aX, aZ, sizeX, sizeZ);
        for (int i = 0; i < sizeX; i++)
        {
            for (int k = 0; k < sizeZ; k++)
            {
                Vector2 v = new Vector2(aX + i, aZ + k);
                if (!tiles.ContainsKey(v))
                {
                    GameObject newTile = Instantiate(tilePrefab) as GameObject;
                    newTile.transform.parent = GameObject.Find("SelectionTiles").transform;
                    newTile.transform.localPosition = new Vector3(aX + i, 0.0101f, aZ + k);
                    tiles[v] = newTile;
                }
            }
        }
        colorTiles();
        TimeSinceLastColorRefresh = 0;
    }
    protected void colorTiles()
    {
        //This function controls the color of the tiles used.

        int aX = Mathf.RoundToInt(transform.localPosition[0]);
        int aZ = Mathf.RoundToInt(transform.localPosition[2]);
        for (int i = 0; i < sizeX; i++)
        {
            for (int k = 0; k < sizeZ; k++)
            {
                Vector2 v = new Vector2(aX + i, aZ + k);
                if (!(TileManager.walkable[aX + i, aZ + k]))
                {
                    tiles[v].renderer.materials = redMats;
                }
                else
                {
                    try
                    {
                        tiles[v].renderer.materials = blueMats;
                    }
                    catch (KeyNotFoundException e)
                    {
                        //Debug.Log(e);
                        // I swear this is evil stuff.
                        //Debug.Log(v);
                    }
                }
            }
        }
    }
    protected void cleanOutOfRangeTiles(int aX, int aZ, int sizeX, int sizeZ)
    {
        List<Vector2> toRemove = new List<Vector2>();
        foreach (Vector2 key in tiles.Keys)
        {
            if (    key[0] > aX+sizeX-1   ||
                    key[0] < aX         ||
                    key[1] > aZ+sizeZ-1   ||
                    key[1] < aZ         )
            {
                toRemove.Add(key);
            }
        }
        destroyTiles(toRemove);
    }
    protected void destroyTiles(List<Vector2> keys)
    {
        // Destroy the selected grid tiles.
        foreach (Vector2 key in keys)
        {
            GameObject.Destroy(tiles[key]);
            tiles.Remove(key);
        }
        if (keys.Count > 0)
        {
            Resources.UnloadUnusedAssets();
        }
    }
    public void destroyTiles()
    {
        // Destroy ALL tiles.
        foreach (GameObject tile in tiles.Values)
        {
            GameObject.Destroy(tile);
        }
    }

}
