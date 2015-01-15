using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {
    [Range(0, 1)]
    public float offset;
    [Range(0, 1)]
    public float camSpeed;
    public float upY, lowY;
    public float upRotation, lowRotation;
    float upX, upZ, lowX, lowZ;
    public Camera mainCamera;

    private float expFactor = 1;
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
            tryTranslate(0, 0, 5f * camSpeed * 60 * Time.deltaTime);
        }
        if (Input.GetButton("Down"))
        {
            tryTranslate(0, 0, -5f * camSpeed * 60 *Time.deltaTime);
        }
        if (Input.GetButton("Right"))
        {
            tryTranslate(-5f * camSpeed * 60 * Time.deltaTime, 0, 0);
        }
        if (Input.GetButton("Left"))
        {
            tryTranslate(5f * camSpeed * 60 * Time.deltaTime, 0, 0);
        }
        if (Input.GetButton("RotateLeft"))
        {

            if (tryTranslate(-0.3f * 60f * Time.deltaTime, 0, 0))
            {
                transform.Rotate(0, 60f * Time.deltaTime, 0);
            }
        }
        if (Input.GetButton("RotateRight"))
        {

            if (tryTranslate(0.3f * 60f * Time.deltaTime, 0, 0))
            {
                transform.Rotate(0, -60f * Time.deltaTime, 0);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            expFactor = Mathf.Min((float)12.0, expFactor * (float)1.3);
            tryTranslate(0, 60f *  expFactor * camSpeed * Input.GetAxis("Mouse ScrollWheel"), 0);


            float r = 4f * Input.GetAxis("Mouse ScrollWheel");
            mainCamera.transform.Rotate(r, 0, 0);

            if (mainCamera.transform.localRotation.x > upRotation || mainCamera.transform.localRotation.x < lowRotation)
            {
                mainCamera.transform.Rotate(-r, 0, 0);
            }
        }
        else
        {
            expFactor = 1;
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
    private bool tryTranslate(float x, float y, float z) {
        x = x * camSpeed;
        y = y * camSpeed;
        z = z * camSpeed;
        transform.Translate(x,y,z);
        if (!boundaryCheck())
        {

            transform.Translate(-x,-y,-z);
            return true;
        }
        return false;
    }

}
