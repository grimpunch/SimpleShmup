using UnityEngine;
using System.Collections;

public class LaserChargePowerUp : MonoBehaviour
{
	private Transform targetPlayer;
	public int chargeAmount;
	public float travelspeedBase = 0.5f;
	float travelspeed;
	public float hangtimeBase = 4f;
	float hangtime;
	// Use this for initialization
	void OnEnable()
	{
		travelspeed = travelspeedBase + Random.Range(0.25f, 0.8f);
		hangtime = hangtimeBase + Random.Range(0.25f, 0.8f);
		transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, Random.Range(0.0f, 360f)));
	}

	void OnTriggerStay2D(Collider2D col2d)
	{
		if (col2d.gameObject.name.StartsWith("ShmupShip")) {
			col2d.GetComponentInChildren<LaserChargeHandler>().AddCharge(chargeAmount);
			gameObject.SetActive(false); // TEMPORARY , NEEDS AUDIO AND SHIZ, TODO REMOVE THIS.
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Utils.Paused) {
			return;
		}
		if (targetPlayer == null) {
			targetPlayer = Utils.AcquireTargetPlayer();
			if (hangtime > 0) {
				hangtime -= Time.deltaTime;
				transform.position -= transform.up * Time.deltaTime * 0.15f;
			}
			return;
		}
		if (!targetPlayer.gameObject.activeSelf) {
			targetPlayer = null;
			return;
		} else {
			if (hangtime < 0) {
				Vector3 vectorToTarget = targetPlayer.position - transform.position;
				float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90);
				Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward.normalized);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 100f);
				transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, Time.deltaTime * travelspeed);
				travelspeed += Time.deltaTime;
			} else {
				hangtime -= Time.deltaTime;
				transform.position -= transform.up * Time.deltaTime * 0.15f;
			}
		}
	}
}
