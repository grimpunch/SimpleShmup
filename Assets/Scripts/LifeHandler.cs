using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeHandler : MonoBehaviour
{
	public GameObject[] playerShips = null;
	public GameObject playerShip;
	private bool alive_p1 = true;
	private bool alive_p2 = true;
	private bool gameStarted;
	public int startLives;
	private int livesleft;
	private float timeToSpawn;
	public float respawnDelay;
	private Text lifeCounterText;
	private ScoreHandler score;
	private bool preparedForNewLevel = false;

	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}

	void OnLevelWasLoaded(int level)
	{
		preparedForNewLevel = false;
	}

	void StartNewLevel(int level)
	{
		if (level == 0) {
			return;
		}
		lifeCounterText = GameObject.Find("LifeCounterText").GetComponent<Text>();
		score = GameObject.Find("Score").GetComponent<ScoreHandler>();
		SetLifeCounterText(livesleft);
		playerShips = GameObject.FindGameObjectsWithTag("Player");
		if (!Utils.Multiplayer) {
			foreach (GameObject go in playerShips) {
				if (go.name == "ShmupShip_P2") {
					go.SetActive(false);
				}
			}
		}
	}

	// Use this for initialization
	void Start()
	{
		livesleft = Utils.livesSetting;
		gameStarted = true;
	}

	void SetLifeCounterText(int lives)
	{
		lifeCounterText.text = "x " + lives.ToString("D2") + "";
	}

	void Dead(int player)
	{
		Debug.Log("Player Died, respawning depending on lives left");
		if (player == 1) {
			alive_p1 = false;
		} else {
			alive_p2 = false;
		}
		score.ResetMultiplier();
	}

	void Spawn()
	{
		if (Utils.Multiplayer) {
			foreach (GameObject gameObject in playerShips) {
				if (!gameObject.activeSelf) {
					playerShip = gameObject;
				}
			}
		} else {
			foreach (GameObject gameObject in playerShips) {
				if (!gameObject.activeSelf && gameObject.name == "ShmupShip_P1") {
					playerShip = gameObject;
				}
			}
		}
		playerShip.SetActive(true);
		if (playerShip.name == "ShmupShip_P1") {
			alive_p1 = true;
			playerShip.transform.localPosition = new Vector3(0f, -1f, 0f);
		} else {
			alive_p2 = true;
			playerShip.transform.localPosition = new Vector3(0f, -1.3f, 0f);
		}
		livesleft--;
		timeToSpawn = 0;
		playerShip.BroadcastMessage("ResetFormation");
		SetLifeCounterText(livesleft);
	}

	void GameOver()
	{
		Debug.Log("OH NOES YOU DED.");
	}

	// Update is called once per frame
	void Update()
	{
		if (Utils.Paused)
			return;
		if (!preparedForNewLevel) {
			return;
		}
		if (!alive_p1 || !alive_p2) {
			// A Player has been destroyed
			if (livesleft > 0) {
				timeToSpawn += Time.deltaTime;
				if (timeToSpawn > respawnDelay) {
					Spawn();
				}
				return;
			} else {
				if (gameStarted) {
					GameOver();
					gameStarted = false;
				}
			}
		}
	}

	void LateUpdate()
	{
		if (!preparedForNewLevel) {
			StartNewLevel(Application.loadedLevel);
			preparedForNewLevel = true;
		}
	}
}
