using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CaptureShipHandler : MonoBehaviour
{
	public int player;
	public float formationChangeSpeed = 1f;
	public List<GameObject> formationPoints;
	public List<GameObject> dummyPrefabs;
	public LineRenderer tractorBeam;
	public GameObject formationToSendCapturedEnemyTo;
	public GameObject capturedShipDummy;
	private VibrationHandler vibrationHandler;
	public bool capturing = false;
	public int capturedEnemies = 0;
	public List<Vector3> normalFormationPoints;
	public List<Vector3> focusFormationPoints;
	private PlayerShoot playerShoot;

	void Awake()
	{
		GetCaptureDummyPrefabs();
		SetFormationPoints();
	}

	void Start()
	{
		vibrationHandler = GameObject.Find("VibrationManager").GetComponent<VibrationHandler>();
		playerShoot = gameObject.transform.parent.FindChild("TurretC").GetComponentInChildren<PlayerShoot>();
	}

	void GetCaptureDummyPrefabs()
	{
		foreach (GameObject go in Resources.LoadAll("CapturableEnemyDummies", typeof(GameObject))) {
			dummyPrefabs.Add(go);
		}
	}

	void SetFormationPoints()
	{
		foreach (GameObject go in formationPoints) {
			normalFormationPoints.Add(go.transform.localPosition);
		}
		foreach (Vector3 v3 in normalFormationPoints) {
			Vector3 fv3 = v3;
			fv3.Scale(new Vector3(0.5f, 0.5f, 0.5f));
			focusFormationPoints.Add(fv3);
		}
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
				explosion.transform.position = formation.transform.position;
				explosion.SetActive(true);
			}
		}

		if (capturedShipDummy != null) {
			Vector3 dummyPos;
			dummyPos = capturedShipDummy.transform.position;
			Destroy(capturedShipDummy);
			GameObject explosion = GameObject.Find("EnemyDeathParticleObjectPool").GetComponent<ObjectPoolScript>().GetPooledObject();
			explosion.transform.position = dummyPos;
			explosion.SetActive(true);
		}
		capturedShipDummy = null;
		formationToSendCapturedEnemyTo = null;
		capturedEnemies = 0;
		capturing = false;
		tractorBeam.enabled = false;
	}

	void PositionFormationPoints()
	{
		if (playerShoot.FocusFireButtonDown()) {
			for (int i = 0; i < formationPoints.Count; i++) {
				if (Vector3.Distance(formationPoints [i].transform.localPosition, focusFormationPoints [i]) > 0.01f) {
					formationPoints [i].transform.localPosition = Vector3.Lerp(formationPoints [i].transform.localPosition, focusFormationPoints [i], Time.deltaTime * formationChangeSpeed);
				} else {
					formationPoints [i].transform.localPosition = focusFormationPoints [i];
				}
			}
		} else {
			for (int i = 0; i < formationPoints.Count; i++) {
				if (Vector3.Distance(formationPoints [i].transform.localPosition, normalFormationPoints [i]) > 0.01f) {
					formationPoints [i].transform.localPosition = Vector3.Lerp(formationPoints [i].transform.localPosition, normalFormationPoints [i], Time.deltaTime * formationChangeSpeed);
				} else {
					formationPoints [i].transform.localPosition = normalFormationPoints [i];
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (!Utils.Paused) {
			PositionFormationPoints();
		}
		if (Utils.Paused || !capturing || capturedShipDummy == null) {
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
