using UnityEngine;
using System.Collections;


public class Building : MonoBehaviour, Tile {
    //private static int sizeX = 2, sizeZ = 2;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public bool isWalkable()
    {
        // To specify.
        return false;
    }
    public bool isBuildable()
    {
        return false;
    }

}
