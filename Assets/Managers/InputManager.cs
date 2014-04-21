using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
    // In general, control of the input is split between three entities: ClickManager, which handles left clicks, CameraController, which handles QE\WASD for camera control, and the rest of the keyboard, which is here.
    public GameObject testBuilding;
	void Start () {
	
	}
	
	void Update () {
	    if (Input.GetButtonDown("1"))
        {
            placeBuilding(testBuilding);
        }

	}

    private void placeBuilding(GameObject building)
    {
        var newBuilding = Instantiate(testBuilding, TileManager.getPos(), Quaternion.identity) as GameObject;
        TileManager.setMouseTile(newBuilding.GetComponent<Building>());
        newBuilding.transform.parent = GameObject.Find("Buildings").transform;
    }



}
