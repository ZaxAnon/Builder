using UnityEngine;
using System.Collections;




public class ClickManager : MonoBehaviour {
    public GameObject grid;
    public Material gridMat;
    public ClickAction clickAction;
    private GameObject selection;
    private Transform transforml;
    private Vector3 origin = new Vector3(0, 0, 0);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            doClickAction();
        }
	}
    private void doClickAction()
    {
        switch (clickAction)
        {
            case ClickAction.Grid:
                gridSelection();
                break;
            case ClickAction.Select:
                placeBuilding();
                break;
            case ClickAction.Place:
                break;
            case ClickAction.None:
                break;
            default:
                break;
        }
    }
    private void gridSelection(){
        //Create, update and delete dragging selection frames.
        Vector3 pos = getPos();
             

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(pos);
            // initialize plane & selection
            origin = pos + new Vector3(0, 0.01f, 0);
            selection = (GameObject) Instantiate(grid);
            transforml = selection.transform;

            transforml.position = origin;
            transforml.localScale = new Vector3(1f / 2000f, 1, 1f / 2000f);
            gridMat.mainTextureScale = new Vector2(1, 1);
        }
        if (Input.GetMouseButton(0))
        {
            // update plane & selection
            transforml.position = origin;
            int sizeX = Mathf.CeilToInt(Mathf.Abs(pos[0] - origin[0])) +1;
            int sizeZ = Mathf.CeilToInt(Mathf.Abs(pos[2] - origin[2])) +1;


            if (pos[0] < origin[0])
            {
                sizeX -= 1;
                transforml.Translate(-sizeX, 0, 0);
            }
            if (pos[2] < origin[2])
            {
                sizeZ -= 1;
                transforml.Translate(0, 0, -sizeZ);
            }

            transforml.localScale = new Vector3(sizeX / 2000f, 1, sizeZ / 2000f);
            gridMat.mainTextureScale = new Vector2(sizeX, sizeZ);
            

        }
        if (Input.GetMouseButtonUp(0))
        {
            //delete plane & selection
            GameObject.DestroyImmediate(selection);
        }
    }
    private Vector3 getPos()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = new RaycastHit();
        Physics.Raycast(ray, out hit, 100);
        Vector3 pos = hit.point;
        pos = new Vector3(Mathf.Floor(pos[0]),
                    Mathf.Floor(pos[1]),
                    Mathf.Floor(pos[2]));
        return pos;

    }
    private void placeBuilding()
    {
        if (tryPlaceBuilding()) clickAction = ClickAction.None;

    }
    private bool tryPlaceBuilding()
    {
        return false;
    }
    
    
    public enum ClickAction
    {
        Grid,
        Select,
        Place, 
        None
    };

}
