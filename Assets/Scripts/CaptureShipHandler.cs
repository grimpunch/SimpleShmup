using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CaptureShipHandler : MonoBehaviour
{
  
	public List<GameObject> formationPoints;
	public List<GameObject> dummyPrefabs;
	public LineRenderer tractorBeam;
	public GameObject formationToSendCapturedEnemyTo;
	public GameObject capturedShipDummy;
	private VibrationHandler vibrationHandler;
	public bool capturing = false;
	public int capturedEnemies = 0;

	void Awake()
	{
		foreach (GameObject go in Resources.LoadAll("CapturableEnemyDummies", typeof(GameObject))) {
			dummyPrefabs.Add(go);
		}

	}

	void Start()
	{
		vibrationHandler = GameObject.Find("VibrationManager").GetComponent<VibrationHandler>();
	}

	public void Capture(Vector3 capturedGOPos, string capturedGoName)
	{
		foreach (GameObject formation in formationPoints) {
			if (!formation.GetComponent<PlayerFormationShoot>().hasCapturedEnemy) {
				formationToSendCapturedEnemyTo = formation;
				formationToSendCapturedEnemyTo.GetComponent<ParticleSystem>().Play();
				capturing = true;
				break;
			}
		}
		if (capturing) {
			GameObject dummyGO = new GameObject();
			foreach (GameObject dummy in dummyPrefabs) {
				if (dummy.name.Contains(capturedGoName)) {
					dummyGO = dummy;
					break;
				}
			}
			capturedShipDummy = (GameObject)Instantiate(dummyGO, capturedGOPos, dummyGO.transform.rotation);
		}

	}

	public void ResetFormation()
	{
		foreach (GameObject formation in formationPoints) {
			if (formation.GetComponent<PlayerFormationShoot>().hasCapturedEnemy) {
				formation.GetComponent<ParticleSystem>().Stop();
				formation.GetComponent<PlayerFormationShoot>().hasCapturedEnemy = false;
				Destroy(formation.transform.GetChild(0).gameObject);
				GameObject explosion = GameObject.Find("EnemyDeathParticleObjectPool").GetComponent<ObjectPoolScript>().GetPooledObject();
				explosion.transform.position = capturedShipDummy.transform.position;
				explosion.SetActive(true);
			}
		}

		if (capturedShipDummy != null) {
			Destroy(capturedShipDummy);
			GameObject explosion = GameObject.Find("EnemyDeathParticleObjectPool").GetComponent<ObjectPoolScript>().GetPooledObject();
			explosion.transform.position = capturedShipDummy.transform.position;
			explosion.SetActive(true);
		}
		capturedShipDummy = null;
		formationToSendCapturedEnemyTo = null;
		capturedEnemies = 0;
		capturing = false;
		tractorBeam.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (Utils.Paused || !capturing || capturedShipDummy == null) {
			vibrationHandler.StopAllCoroutines();
			foreach (GameObject formation in formationPoints) {
				formation.GetComponent<ParticleSystem>().Stop();
			}
			return;
		}

		if (Vector3.Distance(capturedShipDummy.transform.position, formationToSendCapturedEnemyTo.transform.position) <= 0.05f) {
			formationToSendCapturedEnemyTo.GetComponent<PlayerFormationShoot>().hasCapturedEnemy = true;
			formationToSendCapturedEnemyTo.GetComponent<ParticleSystem>().Stop();
			capturedShipDummy.transform.position = formationToSendCapturedEnemyTo.transform.position;
			capturedShipDummy.transform.parent = formationToSendCapturedEnemyTo.transform;
			capturedShipDummy = null;
			formationToSendCapturedEnemyTo = null;
			capturing = false;
			capturedEnemies += 1;
			tractorBeam.enabled = false;
			vibrationHandler.StopAllCoroutines();
		} else {
			tractorBeam.enabled = true;
			vibrationHandler.increaseVariablePower(gameObject.transform.parent.GetComponent<PlayerHitHandler>().player);
			tractorBeam.SetPosition(0, formationToSendCapturedEnemyTo.transform.position);
			tractorBeam.SetPosition(1, capturedShipDummy.transform.position);
			formationToSendCapturedEnemyTo.GetComponent<ParticleSystem>().Play();
			capturedShipDummy.transform.position = Vector3.MoveTowards(
				capturedShipDummy.transform.position, 
				formationToSendCapturedEnemyTo.transform.position,
				Time.deltaTime);   
		}
	}
}
