using UnityEngine;
using System.Collections;

public class PlayerFormationShoot : MonoBehaviour
{

	private bool readyToFire;
	private Vector3 shotPosition;
	private Quaternion shotRotation;
	private ObjectPoolScript playerShotObjectPool;
	int shotLayer;
	public float fireDelay = 0.25f;
	float cooldownTimer = 0;
	public bool hasCapturedEnemy = false;

	// Use this for initialization
	void Start()
	{
		playerShotObjectPool = GameObject.Find("PlayerShotObjectPool").GetComponent<ObjectPoolScript>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Utils.Paused || !hasCapturedEnemy)
			return;
		cooldownTimer -= Time.deltaTime;

		if (FireButtonDown() && cooldownTimer <= 0) {
			// SHOOT!
			cooldownTimer = fireDelay;
			GameObject shotCenter = playerShotObjectPool.GetPooledObject();
			if (shotCenter == null)
				return;
			shotCenter.SetActive(true);
			shotCenter.transform.position = transform.position;
			shotCenter.transform.rotation = transform.rotation;
		}
	}

	private bool FireButtonDown()
	{
		return gameObject.transform.parent.parent.GetComponentInChildren<PlayerShoot>().FireButtonDown();
	}

	private bool FocusFireButtonDown()
	{
		return gameObject.transform.parent.parent.GetComponentInChildren<PlayerShoot>().FocusFireButtonDown();
	}
}
