using UnityEngine;
using System.Collections.Generic;

public class Walker : MonoBehaviour {

    private Tile home;
    private Queue<Vector2> path = new Queue<Vector2>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        walk();
	}
    private void walk() { }
    private void job() { }
    private void navigateTo(int x, int z)
    {

    }

}
