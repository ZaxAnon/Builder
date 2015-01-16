using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {
    public Camera mainCamera;
    [Range(0, 2)]
    public float rotationFocus;
    [Range(0f, 0.2f)]
    public float mapCuttingOffset;
    [Range(0, 5)]
    public float camSpeed;
    public float upYBoundary, lowYBoundary;
    public float upRotationBoundary, lowRotationBoundary;
    float upX, upZ, lowX, lowZ;
    [Range(1, 2)]
    public float exponentialScroll;
    [Range((float)0.9, (float)0.97)]
    public float exponentialDecay;
    [Range(2, 20)]
    public float exponentialCap;
    private float expFactor = 1;
    private float oldScroll = 0;
    private float timeSinceDecay;
 // Use this for initialization
	void Start () {
        int sizeX = TerrainManager.sizeX;
        int sizeZ = TerrainManager.sizeZ;
        lowX = sizeX * mapCuttingOffset;
        lowZ = sizeZ * mapCuttingOffset;
        upX = sizeX * (1-mapCuttingOffset);
        upZ = sizeZ * (1-mapCuttingOffset);

        //upY = 10f;
        //lowY = 0f;
        transform.position = new Vector3(5 + sizeX * mapCuttingOffset , 0, 5+ sizeZ * mapCuttingOffset);
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
            transform.Translate(-0.3f * 60f * rotationFocus * mainCamera.transform.position.y * Time.deltaTime, 0, 0);
            transform.Rotate(0, 60f * Time.deltaTime, 0);
            
        }
        if (Input.GetButton("RotateRight"))
        {
            transform.Translate(0.3f * 60f * rotationFocus * mainCamera.transform.position.y * Time.deltaTime, 0, 0);
            transform.Rotate(0, -60f * Time.deltaTime, 0);   
        }


        calcVerticalScroll();



    }
    private void calcVerticalScroll()
    {

        float currentScroll = Input.GetAxis("Mouse ScrollWheel");
        oldScroll = oldScroll * 0.9f + currentScroll * 0.3f;

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {

            expFactor = Mathf.Min(exponentialCap, expFactor * exponentialScroll);
            mouseScroll(currentScroll * expFactor);
            timeSinceDecay = -1 / 30f;

        }
        else
        {
            timeSinceDecay += Time.deltaTime;
            if (timeSinceDecay < (1 / 61f)) return;
            timeSinceDecay = 0;

            expFactor = expFactor * exponentialDecay;
            if (expFactor > 1.0f)
            {
                mouseScroll(oldScroll * expFactor);
            }
            else
            {
                expFactor = 1;
                oldScroll = 0;
            }
        }
    }
    private void mouseScroll(float r) {
        r = r * (float)0.25 * 15f * expFactor * camSpeed;
        tryTranslate(0, r, 0);
        mainCamera.transform.Rotate(r/5f, 0, 0);
        
        if (mainCamera.transform.localRotation.x > upRotationBoundary || mainCamera.transform.localRotation.x < lowRotationBoundary)
        {
            mainCamera.transform.Rotate(-r/5f, 0, 0);
        }
         

    }
    private bool boundaryCheck() {
        if( transform.position[0] > upX     ||
            transform.position[0] < lowX    ||
            transform.position[1] > upYBoundary     ||
            transform.position[1] < lowYBoundary    ||
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
            return false;
        }
        return true;
    }

}
