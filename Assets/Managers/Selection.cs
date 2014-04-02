using UnityEngine;
using System.Collections.Generic;

public class Selection : MonoBehaviour {
    private Vector3 origin  =   new Vector3(0, 0, 0);
    private Vector3 pos     =   new Vector3(0, 0, 0);
    public GameObject redSpotPrefab;
    private Dictionary<Vector2, GameObject> redSpots = new Dictionary<Vector2, GameObject>();
    private int sizeX, sizeZ;
    private float TimeSinceLastRedSpotRefresh;
    

    public static GameObject Create(Object prefab)
    {
        GameObject newObject = Instantiate(prefab) as GameObject;
        return newObject;
    }
	// Use this for initialization
	void Start () {




        Vector3 pos = new Vector3(0, 0, 0);
        try
        {
            pos = getPos();
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

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            try
            {
                pos = getPos();
            }
            catch (System.Exception)
            {
                return;
            }
            
            // update plane & selection
            transform.position = origin;
            //pos[0] = Mathf.Clamp(pos[0], 0, TerrainManager.sizeX);
            //pos[2] = Mathf.Clamp(pos[2], 0, TerrainManager.sizeZ);
            sizeX = Mathf.CeilToInt(Mathf.Abs(pos[0] - origin[0])) + 1;
            sizeZ = Mathf.CeilToInt(Mathf.Abs(pos[2] - origin[2])) + 1;


            if (pos[0] < origin[0])
            {
                //sizeX -= 1;
                transform.Translate(1-sizeX, 0, 0);
            }
            if (pos[2] < origin[2])
            {
                //sizeZ -= 1;
                transform.Translate(0, 0, 1-sizeZ);
            }

            transform.localScale = new Vector3(sizeX / 2000f, 1, sizeZ / 2000f);
            renderer.material.mainTextureScale = new Vector2(sizeX, sizeZ);
            renderer.enabled = false;
            makeRedSpots();


        }
	}
    private void makeRedSpots()
    {
        if (TimeSinceLastRedSpotRefresh < 0.1f)
        {

        }
        int aX = Mathf.RoundToInt(transform.localPosition[0]);
        int aZ = Mathf.RoundToInt(transform.localPosition[2]);
        cleanOutOfRangeRedSpots(aX, aZ, sizeX, sizeZ);
        var tiles = TileManager.walkable;
        List<Vector2> toRemove = new List<Vector2>();
        for (int i=0; i < sizeX; i++)
        {
            for (int k=0; k < sizeZ; k++)
            {
                Vector2 v = new Vector2(aX + i, aZ + k);
                if (!(tiles[aX + i,aZ + k]))
                {
                    if (!redSpots.ContainsKey(v)) {
                        GameObject newRedSpot = Instantiate(redSpotPrefab) as GameObject;
                        //newRedSpot.transform.parent = GameObject.Find("Selection(Clone)").transform;
                        newRedSpot.transform.localPosition = new Vector3(aX + i, 0.0101f, aZ + k);
                        //newRedSpot.transform.localPosition = new Vector3( i, 0.0101f, k);
                        redSpots[v] = newRedSpot;
                        //newRedSpot.renderer.material.color
                    }
                }
                else
                {
                    if (redSpots.ContainsKey(v))
                    {
                        toRemove.Add(v);
                    }
                }
            }
        }
        destroyRedSpots(toRemove);
    }
    private void cleanOutOfRangeRedSpots(int aX, int aZ, int sizeX, int sizeZ)
    {
        List<Vector2> toRemove = new List<Vector2>();
        foreach (Vector2 key in redSpots.Keys)
        {
            if (    key[0] > aX+sizeX-1   ||
                    key[0] < aX         ||
                    key[1] > aZ+sizeZ-1   ||
                    key[1] < aZ         )
            {
                toRemove.Add(key);
            }
        }
        destroyRedSpots(toRemove);
    }
    private void destroyRedSpots(List<Vector2> keys)
    {
        foreach (Vector2 key in keys)
        {
            GameObject.Destroy(redSpots[key]);
            redSpots.Remove(key);
        }
    }
    public void destroyRedSpots()
    {
        foreach (GameObject redSpot in redSpots.Values)
        {
            GameObject.Destroy(redSpot);
        }
    }
    private static Vector3 getPos()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = new RaycastHit();
        Physics.Raycast(ray, out hit, 100);
        Vector3 pos = hit.point;
        if (pos == new Vector3(0, 0, 0))
        {
            throw new System.Exception("Raycast miss");
        }
        pos = new Vector3(  Mathf.Floor(pos[0]),
                            Mathf.Floor(pos[1]),
                            Mathf.Floor(pos[2]));

        return pos;

    }
}
