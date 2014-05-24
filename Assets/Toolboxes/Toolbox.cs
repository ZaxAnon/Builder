using UnityEngine;
using System.Collections;

public class Toolbox : Singleton<Toolbox> {

    public GameObject gridPrefab;
    public GameObject placementPrefab;
    public GameObject testBuilding;



    public ControlState state;
    protected Toolbox() { } // guarantee this will be always a singleton only - can't use the constructor!
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void recursiveAlphaAdder(Transform parent, float alpha){
        foreach (Transform child in parent)
        {
            if (child.renderer != null)
            {
                child.renderer.material.color += new Color(0, 0, 0, alpha);
            }
            recursiveAlphaAdder(child, alpha);
        }
    }



        public enum ControlState
    {
        Grid,
        Select,
        Place, 
        None
    };
}
