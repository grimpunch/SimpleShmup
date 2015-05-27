using UnityEngine;
using System.Collections;

public class BossHandler : MonoBehaviour {

    public ScrollLevelForward levelScroller;

    // Use this for initialization
    void Start() {
        GameObject gameplayarea = GameObject.Find("GamePlayArea");
        levelScroller = gameplayarea.GetComponent<ScrollLevelForward>();
        levelScroller.stopped = true;
        RemoveSpawners(gameplayarea);
    }

    void RemoveSpawners(GameObject gameplayarea) { 
        EnemyWaveSpawn[] activeSpawners = gameplayarea.GetComponentsInChildren<EnemyWaveSpawn>();
        foreach (EnemyWaveSpawn AS in activeSpawners) { Destroy(AS.gameObject); }
    }

    // Update is called once per frame
    void Update() {

    }

    void OnDestroy() {
        if (levelScroller != null) levelScroller.stopped = false;
    }
}
