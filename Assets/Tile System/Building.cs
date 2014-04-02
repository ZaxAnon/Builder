using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour, Tile {
    public Transform model;

	// Use this for initialization
	void Start () {
        Instantiate(model);

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
