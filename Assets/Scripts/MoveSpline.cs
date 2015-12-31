using UnityEngine;
using System.Collections;

public class MoveSpline : MonoBehaviour {

	public BezierSpline spline;
	public bool lookForward;
	public float duration;
	public bool loop = true;
	private float progress;
	
	private void Update () {
		if (Utils.Paused) return;
		progress += Time.deltaTime / duration;
		if (progress > 1f) {
			if (loop){
				progress = 0f;
			}
			else{
				progress = 1f;
			}
		}
		Vector3 position = spline.GetPoint(progress);
		transform.position =  position;
		if (lookForward){
			transform.LookAt(position + spline.GetDirection(progress));
		}
	}
}