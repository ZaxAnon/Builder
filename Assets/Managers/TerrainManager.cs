using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour {
    private Terrain terrain;
    public Transform grid;
    public Material gridMat;
    public static int sizeX=2000, sizeZ=2000;
    public static TerrainManager instance;
    public int x, z;
	// Use this for initialization
	void Start () {
        instance = this;

        terrain = Terrain.activeTerrain;




        sizeX = x;
        sizeZ = z;
        terrain.terrainData.size = new Vector3(sizeX, (sizeX/2+sizeZ/2), sizeZ);

        
        

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
