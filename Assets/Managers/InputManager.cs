using UnityEngine;
using System.Collections;

public class InputManager : Singleton<Toolbox>
{
    private GameObject placement;
    // In general, control of the input is split between three entities: ClickManager, which handles left clicks, CameraController, which handles QE\WASD for camera control, and the rest of the keyboard, which is here.
	void Start () {
	
	}
	
	void Update () {
	    if (Input.GetButtonDown("1"))
        {
            if (placement != null)
            {
                
                
                var prefab =  placement.GetComponent<Placement>().prefab;
                placeBuilding(Toolbox.Instance.testBuilding);

                GameObject.Destroy(placement);
                Debug.Log(placement);
                placement = null;
                
            }
            
            
        }
        if (Input.GetButtonDown("2"))
        {
            placement = placementTest();
        }
	}

    private void placeBuilding(Object building)
    {
        var newBuilding = Instantiate(building, TileManager.getPos(), Quaternion.identity) as GameObject;
        TileManager.setMouseTile(newBuilding.GetComponent<Building>());
        newBuilding.transform.parent = GameObject.Find("Buildings").transform;
    }
    private GameObject placementTest()
    {
        GameObject placement = Placement.Create(Toolbox.Instance.placementPrefab, Toolbox.Instance.testBuilding);
        placement.transform.parent = GameObject.Find("Placements").transform;


        return placement;
    }

}
