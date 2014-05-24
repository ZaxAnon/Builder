using UnityEngine;
using System.Collections;

public class Placement : Selection {
    public Object prefab;
    private Vector3 oldpos;
    new public static GameObject Create(Object placementPrefab, Object buildingPrefab)
    {
        GameObject newObject = Instantiate(placementPrefab) as GameObject;
        newObject.renderer.enabled = false;
        Placement placement = newObject.GetComponent<Placement>();
        placement.prefab = placementPrefab;

        GameObject newBuilding = Instantiate(buildingPrefab) as GameObject;

        newBuilding.transform.parent = placement.transform;

        Toolbox.recursiveAlphaAdder(newBuilding.transform, -0.40f);
        newBuilding.transform.localPosition = new Vector3(0, 0, 0);

        Building building = newBuilding.GetComponent<Building>();

        placement.sizeX = building.sizeX;
        placement.sizeZ = building.sizeZ;
        Debug.Log(""+building.sizeX+"\t"+building.sizeZ);
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
