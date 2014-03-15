using UnityEngine;
using System.Collections;




public class ClickManager : MonoBehaviour {
    public GameObject gridPrefab;
    public ClickAction clickAction;


    private GameObject selection;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        doClickAction();

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
        
             

        if (Input.GetMouseButtonDown(0))
        {
            selection = Selection.Create(gridPrefab);
        }
       
        if (Input.GetMouseButtonUp(0))
        {
            //delete plane & selection
            GameObject.Destroy(selection);
        }
    }
    private void drawGrid(float x1, float x2, float z1, float z2){
        // Draws a grid between (x1, y1) and (x2, y2)
        /*
        GameObject grid = (GameObject) Instantiate(gridPrefab);
        if (z1 > z2)
        {
            float tmp = z2;
            z2 = z1;
            z1 = tmp;
        }
        if (x1 > x2)
        {
            float tmp = x2;
            x2 = x1;
            x1 = tmp;
        }


        x1 = Mathf.Floor(x1);
        x2 = Mathf.Ceil(x2);
        z1 = Mathf.Floor(z1);
        z2 = Mathf.Ceil(z2);
        int sizeX = Mathf.RoundToInt(Mathf.Abs(x1 - x2));
        int sizeZ = Mathf.RoundToInt(Mathf.Abs(z1 - z2));
        Vector3 og = new Vector3(x1, 0, z1);
        grid.transform.localPosition = og;
        Material gridMaterial = grid.renderer.material;
         * */


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
