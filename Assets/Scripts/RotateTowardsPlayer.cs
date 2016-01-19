using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotateTowardsPlayer : MonoBehaviour
{

	public float strength;
	public Transform target;

	// Use this for initialization
	void Start()
	{
		AcquireTarget();
	}

	void AcquireTarget()
	{
		if (!Utils.Multiplayer) {
			if (GameObject.FindWithTag("Player")) {
				GameObject prospectiveTarget = GameObject.Find("ShmupShip_P1");
				if (prospectiveTarget.activeSelf) {
					target = prospectiveTarget.gameObject.transform;
				} else {
					return;
				}
			}
		} else {
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			if (players == null)
				return;
			GameObject prospectiveTarget = players [Random.Range(0, players.Length - 1)].gameObject;
			if (prospectiveTarget.activeSelf) {
				target = prospectiveTarget.gameObject.transform;
			} else {
				return;
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Utils.Paused)
			return;
		if (target != null) {
			//TODO , Dot product calc to find vector of 'down' from this transform to player to determine rotation direction
			Vector3 vectorToTarget = target.position - transform.position;
			float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90);
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward.normalized);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * strength);
		} else {
			AcquireTarget();
			return;
		}
	}
}
