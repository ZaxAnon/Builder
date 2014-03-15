using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {
    public float offset;
    [Range(0, 1)]
    public float camSpeed;
    public float upY, lowY;
    float upX, upZ, lowX, lowZ;
    public Camera mainCamera;
 // Use this for initialization
	void Start () {
        int sizeX = TerrainManager.sizeX;
        int sizeZ = TerrainManager.sizeZ;
        lowX = sizeX * offset;
        lowZ = sizeZ * offset;
        upX = sizeX * (1-offset);
        upZ = sizeZ * (1-offset);

        //upY = 10f;
        //lowY = 0f;
        transform.position = new Vector3(sizeX * offset, 0, sizeX * offset);
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Up"))
        {
            tryTranslate(0, 0, 5f * camSpeed);
        }
        if (Input.GetButton("Down"))
        {
            tryTranslate(0, 0, -5f * camSpeed);
        }
        if (Input.GetButton("Right"))
        {
            tryTranslate(-5f * camSpeed, 0, 0);
        }
        if (Input.GetButton("Left"))
        {
            tryTranslate(5f * camSpeed, 0, 0);
        }
        if (Input.GetButton("RotateLeft"))
        {
            transform.Rotate(0, 1f, 0);
            tryTranslate(-0.3f, 0, 0);
        }
        if (Input.GetButton("RotateRight"))
        {
            transform.Rotate(0, -1f, 0);
            tryTranslate(0.3f, 0, 0);
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            tryTranslate(0, 22f * Input.GetAxis("Mouse ScrollWheel"), 0);


            float r = 12f * Input.GetAxis("Mouse ScrollWheel");
            mainCamera.transform.Rotate(r, 0, 0);

            if (mainCamera.transform.localRotation.x > 0.66 || mainCamera.transform.localRotation.x < 0.25)
            {
                mainCamera.transform.Rotate(-r, 0, 0);
            }
        }
    }
    private bool boundaryCheck() {
        if( transform.position[0] > upX     ||
            transform.position[0] < lowX    ||
            transform.position[1] > upY     ||
            transform.position[1] < lowY    ||
            transform.position[2] > upZ     ||
            transform.position[2] < lowZ    )
        { return false; }
        return true;
    } 
    private void tryTranslate(float x, float y, float z) {
        x = x * camSpeed;
        y = y * camSpeed;
        z = z * camSpeed;
        transform.Translate(x,y,z);
        if (!boundaryCheck())
        {
            transform.Translate(-x,-y,-z);
        }
    }

}
