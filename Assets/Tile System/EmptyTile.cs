﻿using UnityEngine;
using System.Collections;

public class EmptyTile : Tile {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public bool isWalkable()
    {
        return true;
    }
    public bool isBuildable()
    {
        return true;
    }
}
