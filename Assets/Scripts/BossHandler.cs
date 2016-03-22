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
    public SpinObject spinner;
    private EnemyHitHandler bossHitHandler;
    private ParticleSystem bossParticleSmoke;
    private SplinePathSequence bossSplineSeq;
    public float slowedShotSpeed = 0.1f;
    public bool boomed = false;
	// Use this for initialization
	void Start()
	{
        bossHitHandler = GetComponent<EnemyHitHandler>();
        bossParticleSmoke = GetComponent<ParticleSystem>();
        bossSplineSeq = GetComponent<SplinePathSequence>();
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
        if (bossHitHandler){
            if (bossHitHandler.shipHealth < bossHitHandler.startHealth / 4) {
                //Make it look weary now.
                spinner.enabled = true;
                if (!bossParticleSmoke.isPlaying){
                    bossParticleSmoke.Play();
                }
                bossSplineSeq.AlterCurrentSplinePathDuration(4f);
                bossHitHandler.shipHealth--;
            }
            if (bossHitHandler.shipHealth < bossHitHandler.startHealth / 16) {
                //Make it look like it gonna die now.
                bossHitHandler.Flash();
                bossSplineSeq.AlterCurrentSplinePathDuration(30f);
                bossHitHandler.shipHealth -= 30;
                bulletHandler.shotSpeed = slowedShotSpeed;
                if (!boomed){
                    GameObject.Find("BossExplosion").GetComponent<PlayAnimation>().enabled = true;
                    GameObject.Find("BossExplosion").GetComponent<ResetGlitchCam>().enabled = true;
                    ActivateGameObjectsOnTrigger[] booms = GameObject.Find("BossExplosion").GetComponents<ActivateGameObjectsOnTrigger>();
                    Camera.main.GetComponent<Kino.AnalogGlitch>().horizontalShake = 0.5f;
                    foreach(ActivateGameObjectsOnTrigger boom in booms){
                            boom.Boom();
                    }
                        boomed = true;
                    }
                if (boomed && Camera.main.GetComponent<Kino.AnalogGlitch>().horizontalShake > 0.0f){
                    Camera.main.GetComponent<Kino.AnalogGlitch>().horizontalShake -= 0.1f * Time.deltaTime;
                }
            }
        }
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
