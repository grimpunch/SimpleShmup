using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeHandler : MonoBehaviour {

    public GameObject playerShip;
    private bool alive = true;
    private bool gameStarted;
    public int startLives;
    private int livesleft;
    private float timeToSpawn;
    public float respawnDelay;
    private Text lifeCounterText;
	private ScoreHandler score;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    void OnLevelWasLoaded(int level) {
        lifeCounterText = GameObject.Find("LifeCounterText").GetComponent<Text>();
    }

    // Use this for initialization
    void Start() {
        lifeCounterText = GameObject.Find("LifeCounterText").GetComponent<Text>();
        livesleft = startLives;
        SetLifeCounterText(livesleft);
        gameStarted = true;
		score = GameObject.Find("Score").GetComponent<ScoreHandler>();
    }

    void SetLifeCounterText(int lives) {
        lifeCounterText.text = "x " + lives.ToString("D2")+"";
    }

    void Dead() {
        Debug.Log("Player Died, respawning depending on lives left");
        alive = false;
		score.ResetMultiplier();
    }

    void Spawn() {
        playerShip.SetActive(true);
        playerShip.transform.localPosition = new Vector3(0f, -1f, 0f);
        alive = true;;
        livesleft--;
        timeToSpawn = 0;
        SetLifeCounterText(livesleft);
    }

    void GameOver() {
        Debug.Log("OH NOES YOU DED.");
    }

    // Update is called once per frame
    void Update() {
        if (Utils.Paused) return;
        if (!alive) { 
            // Player has been destroyed
            if (livesleft > 0) {
                timeToSpawn += Time.deltaTime;
                if (timeToSpawn > respawnDelay){
                    Spawn();
                }
                return;
            } else {
                if (gameStarted) { GameOver(); gameStarted = false; }
            }
        }
    }
}
