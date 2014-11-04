using UnityEngine;
using System.Collections.Generic;

public class Walker : MonoBehaviour {

    private Tile home;
    private int walkDelay;
    public int goalX, goalZ;
    private Vector3 offset = new Vector3(0.5f, 0.1f, 0.5f);
    private Queue<IntPoint> path = new Queue<IntPoint>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("3")) {
            calculatePath(goalX, goalZ);
        }
        walk();
	}
    private void walk() 
    {
        walkDelay -= 1;
        if (path != null && path.Count > 0 && walkDelay<=0)
        {
            walkDelay = 30;
            var point = path.Dequeue();
            Debug.Log("Moving to: " + point.x + "," + point.z);
            transform.position= (new Vector3(point.x, 0, point.z))+offset;
        }
    }
    private void job() { }
    private void calculatePath(int x, int z)
    {

        var pos = new IntPoint((int)transform.position[0], (int)transform.position[2]);
        var goal = new IntPoint(x, z);
        var listpath = AStar.pathfind(pos, goal);
        path = new Queue<IntPoint>(listpath);
    }

}
