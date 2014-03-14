using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
    public Transform model;

	// Use this for initialization
	void Start () {
        Instantiate(model);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
