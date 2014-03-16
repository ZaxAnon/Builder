using UnityEngine;
using System.Collections.Generic;

public class Selection : MonoBehaviour {
    private Vector3 origin = new Vector3(0, 0, 0);
    private Vector3 pos = new Vector3(0, 0, 0);
    private List<RedSpot> redSpots = new List<RedSpot>();
    private int sizeX, sizeZ;
    

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
            int sizeX = Mathf.CeilToInt(Mathf.Abs(pos[0] - origin[0])) + 1;
            int sizeZ = Mathf.CeilToInt(Mathf.Abs(pos[2] - origin[2])) + 1;


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
            makeRedSpots();


        }
	}
    private void makeRedSpots()
    {

    }
    void onDestroy()
    {
        foreach (RedSpot redSpot in redSpots)
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
