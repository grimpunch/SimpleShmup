using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossHandler : MonoBehaviour
{

	public ScrollLevelForward levelScroller;
	private float shipBottomSide;
	private ScreenBoundsHandler screenBounds;
	public float yScrollStopOffset;
	private EnemyShotHandler bulletHandler;
	private EnemyShotHandler bulletHarassHandler;
	// Use this for initialization
	void Start()
	{
		GameObject gameplayarea = GameObject.Find("GamePlayArea");
		levelScroller = gameplayarea.GetComponent<ScrollLevelForward>();
		bulletHandler = GameObject.Find("EnemyShotObjectPool").GetComponent<EnemyShotHandler>();
		bulletHarassHandler = GameObject.Find("EnemyHarassShotObjectPool").GetComponent<EnemyShotHandler>();
		RemoveSpawners(gameplayarea);
		screenBounds = GameObject.Find("ScreenBoundsHandler").GetComponent<ScreenBoundsHandler>();
	}

	void RemoveSpawners(GameObject gameplayarea)
	{ 
		EnemyWaveSpawn[] activeSpawners = gameplayarea.GetComponentsInChildren<EnemyWaveSpawn>();
		foreach (EnemyWaveSpawn AS in activeSpawners) {
			Destroy(AS.gameObject);
		}
	}

	// Update is called once per frame
	void Update()
	{
		StopLevelScroll();
	}

	void StopLevelScroll()
	{
		levelScroller.stopped = true;
	}

	void DestroySpawnedBullets()
	{
		bulletHandler.RemoveAllShots();
		bulletHarassHandler.RemoveAllShots();
	}

	void OnDisable()
	{
		if (Application.isPlaying)
			DestroySpawnedBullets();
		if (levelScroller != null)
			levelScroller.stopped = false;
	}
}
