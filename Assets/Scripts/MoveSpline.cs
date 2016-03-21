using UnityEngine;
using System.Collections;

public enum MoveSplineMode
{
    Once,
    Loop,
    PingPong
}

public class MoveSpline : MonoBehaviour
{

    public BezierSpline spline;
    public Vector3 formationOffset;
    public bool lookForward;
    public float duration;
    private float progress;
    public bool finishedSpline;
    public MoveSplineMode mode;
	
    private bool goingForward = true;

    public void Reset(){
        progress = 0.0f;
    }


    private void Update()
    {
        if(Utils.Paused)
            return;
        if(!spline)
            return;
        if(goingForward) {
            progress += Time.deltaTime / duration;
            if(progress > 1f) {
                if(mode == MoveSplineMode.Once) {
                    progress = 1f;
                    finishedSpline = true;
                } else if(mode == MoveSplineMode.Loop) {
                    progress -= 1f;
                } else {
                    progress = 2f - progress;
                    goingForward = false;
                }
            }
        } else {
            progress -= Time.deltaTime / duration;
            if(progress < 0f) {
                progress = -progress;
                goingForward = true;
            }
        }
        Vector3 position = spline.GetPoint(progress);
        transform.position = position + formationOffset;
        if(lookForward) {
            transform.LookAt(position + spline.GetDirection(progress));
        }
    }
}