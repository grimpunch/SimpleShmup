using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeHandler : MonoBehaviour {

    public GameObject playerPrefab;
    private bool spawnInvoked = false;
    private bool alive = true;
    public int startLives;
    private int livesleft;
    private float timeToSpawn;
    public float respawnDelay;
    private Text lifeCounterText;

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
    }

    void SetLifeCounterText(int lives) {
        lifeCounterText.text = "x " + lives.ToString("D2")+"";
    }

    void Dead() {
        Debug.Log("Player Died, respawning depending on lives left");
        alive = false;
    }

    void Spawn() {
        GameObject spawnGO = (GameObject)Instantiate(playerPrefab, transform.position, transform.rotation);
        spawnGO.name = playerPrefab.name;
        spawnGO.transform.parent = transform.parent;
        alive = true;
        livesleft--;
        SetLifeCounterText(livesleft);
    }

    void GameOver() {
        Debug.Log("OH NOES YOU DED.");
    }

    // Update is called once per frame
    void Update() {
        if (!alive) { 
            // Player has been destroyed
            if (livesleft > 0) {
                if (!spawnInvoked){
                    Invoke("Spawn", respawnDelay);
                    spawnInvoked = true;
                }
                return;
            } else { GameOver(); spawnInvoked = false; }
        }
    }
}
