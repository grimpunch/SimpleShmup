using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScreenBoundsHandler : MonoBehaviour
{
    private float screenLeftSide;
    private float screenRightSide;
    private float screenTopSide;
    private float screenBottomSide;

    //Public variables
    public bool screenBoundsUpdated = false;


    // Use this for initialization
    void Start()
    {
        getScreenBounds();
    }

    private void getScreenBounds()
    {
        screenRightSide = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0F)).x;
        screenLeftSide = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0F)).x;
        screenTopSide = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y;
        screenBottomSide = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y;
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
    void Update()
    {
        if(Utils.Paused)
            return;
        getScreenBounds();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.0F)), Vector3.one);
        Gizmos.DrawWireCube(Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0.0F)), Vector3.one);
        Gizmos.DrawWireCube(Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0.0F)), Vector3.one);
        Gizmos.DrawWireCube(Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0.0F)), Vector3.one);
    }

}