using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour {
    private Terrain terrain;
    public Transform grid;
    public Material gridMat;
    //public Camera mainCamera;
    //public CameraController cameraTarget;
    public static int sizeX=2000, sizeZ=2000;
    public static TerrainManager instance;
    public int x, z;
	// Use this for initialization
	void Start () {
        instance = this;

        terrain = Terrain.activeTerrain;




        sizeX = x;
        sizeZ = z;
        gridMat.mainTextureScale = new Vector2(sizeX, sizeZ);
        terrain.terrainData.size = new Vector3(sizeX, (sizeX/2+sizeZ/2), sizeZ);
        //Instantiate(grid);


        //grid.position = new Vector3(sizeX / 2f, 0.01f, sizeZ / 2f);
        //grid.position = new Vector3(0, 0.001f, 0);
        //grid.localScale = new Vector3(sizeX/2000f, 1, sizeZ/2000f);
        //gridMat.color = new Color(0, 255, 255, 100);
        //grid.renderer.material = gridMat;
        
        

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
