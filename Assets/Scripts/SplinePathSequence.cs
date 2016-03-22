using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SplinePathSequence : MonoBehaviour
{

	[System.Serializable]
	public class SplinePathSequenceList
	{
		public BezierSpline spline;
		public float duration;
		public bool shoot = true;
	}

	private MoveSpline moveSpline;
	private EnemyShoot enemyShoot;
	public List<SplinePathSequenceList> sequenceList;
	public BezierSpline currentSpline;
	public float currentDuration;
	private int currentSplineIndex;
	public int loopBackTo = 1;
	public bool loop;
	public bool canShoot;
	
	// Use this for initialization
	void Start()
	{
		moveSpline = gameObject.GetComponent<MoveSpline>();
		enemyShoot = gameObject.GetComponent<EnemyShoot>();
		currentSplineIndex = 0;
	}
	
    public void AlterCurrentSplinePathDuration(float duration){
        sequenceList [currentSplineIndex].duration = duration;
    }

	// Update is called once per frame
	void Update()
	{
		if (Utils.Paused) {
			return;
		}
		if (!moveSpline) {
			return;
		}
		if (moveSpline.finishedSpline && moveSpline.mode == MoveSplineMode.Once) {
			moveSpline.finishedSpline = false;
            if (currentSplineIndex >= sequenceList.Count - 1) {
				if (loop) {
					currentSplineIndex = loopBackTo;
					moveSpline.mode = MoveSplineMode.Loop;
				} else {
					if (moveSpline.mode != MoveSplineMode.Once) {
						currentSplineIndex = sequenceList.Count - 1;
						moveSpline.mode = MoveSplineMode.PingPong;
					}
				}
			} else {
				currentSplineIndex += 1;
                moveSpline.Reset();
			}
		}
		currentSpline = sequenceList [currentSplineIndex].spline;
		currentDuration = sequenceList [currentSplineIndex].duration;
		moveSpline.spline = currentSpline;
        moveSpline.duration = currentDuration;
		if (enemyShoot) {
			canShoot = sequenceList [currentSplineIndex].shoot;
			enemyShoot.canShoot = canShoot;
		}
	}
}
