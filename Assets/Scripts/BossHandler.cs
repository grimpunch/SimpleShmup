using UnityEngine;
using System.Collections;

public class BossHandler : MonoBehaviour {

    public ScrollLevelForward levelScroller;
    private float shipBottomSide;
    private ScreenBoundsHandler screenBounds;
    public float yScrollStopOffset;

    // Use this for initialization
    void Start() {
        GameObject gameplayarea = GameObject.Find("GamePlayArea");
        levelScroller = gameplayarea.GetComponent<ScrollLevelForward>();
        RemoveSpawners(gameplayarea);
        screenBounds = GameObject.Find("ScreenBoundsHandler").GetComponent<ScreenBoundsHandler>();
    }

    void RemoveSpawners(GameObject gameplayarea) { 
        EnemyWaveSpawn[] activeSpawners = gameplayarea.GetComponentsInChildren<EnemyWaveSpawn>();
        foreach (EnemyWaveSpawn AS in activeSpawners) { Destroy(AS.gameObject); }
    }

    // Update is called once per frame
    void Update() {
        
        shipBottomSide = gameObject.transform.position.y - (gameObject.GetComponent<Renderer>().bounds.size.y * 0.5F);
        if (((screenBounds.ScreenTop + screenBounds.ScreenBottom) / 2f) + yScrollStopOffset > shipBottomSide) { /* Ship is in middle of screen, with slight bias above middle */
            StopLevelScroll();
        }
    }

    void StopLevelScroll() {
        levelScroller.stopped = true;
    }

    void OnDisable() {
        if (levelScroller != null) levelScroller.stopped = false;
    }
}
