using UnityEngine;
using System.Collections;

public class Placement : Selection {
    private Vector3 oldpos;
    new public static GameObject Create(Object prefab)
    {
        GameObject newObject = Instantiate(prefab) as GameObject;
        newObject.renderer.enabled = false;
        //Selection selection = newObject.GetComponent<Selection>();
        return newObject;
    }
	void Update () {

        try
        {
            pos = TileManager.getPos();
        }
        catch (System.Exception)
        {
            return;
        }

        origin = pos;
        if (oldpos != pos)
        {
            transform.position = origin;
            makeTiles();
        }
        oldpos = pos;

	}

    void OnGUI() { }
}
