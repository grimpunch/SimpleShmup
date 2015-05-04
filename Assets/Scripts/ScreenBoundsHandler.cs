using UnityEngine;
using System.Collections;

public class ScreenBoundsHandler : MonoBehaviour {
    private float screenLeftSide;
    private float screenRightSide;
    private float screenTopSide;
    private float screenBottomSide;

    //Public variables
    public bool screenBoundsUpdated = false;


    // Use this for initialization
    void Start() {
        getScreenBounds();
    }

    private void getScreenBounds() {
        screenRightSide = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0F, 0.0F)).x;
        screenLeftSide = Camera.main.ScreenToWorldPoint(new Vector3(0.0F, 0.0F, 0.0F)).x;
        screenTopSide = Camera.main.ScreenToWorldPoint(new Vector3(0.0F, Screen.height, 0.0F)).y;
        screenBottomSide = Camera.main.ScreenToWorldPoint(new Vector3(0.0F, 0.0F, 0.0F)).y;
    }

    public float ScreenLeft {
        get { return screenLeftSide; }
    }

    public float ScreenTop {
        get { return screenTopSide; }
    }

    public float ScreenRight {
        get { return screenRightSide; }
    }

    public float ScreenBottom {
        get { return screenBottomSide; }
    }

    // Update is called once per frame
    void Update() {
            getScreenBounds();
    }
}
