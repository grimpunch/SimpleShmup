using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShotHandler : MonoBehaviour {

    private List<GameObject> shots;
    private ScreenBoundsHandler screenBounds;
    public float shotSpeed = 1.0F;


    // Use this for initialization
    void Start() {
        screenBounds = GameObject.Find("ScreenBoundsHandler").GetComponent<ScreenBoundsHandler>();
    }

    // Update is called once per frame
    void Update() {
        shots = gameObject.GetComponent<ObjectPoolScript>().pooledObjects;
        foreach (GameObject shot in shots){
        MoveShot(shot);
        }
    }

    void MoveShot(GameObject shot) {
        if (Utils.Paused) return;
        if (shot.transform.position.y < screenBounds.ScreenTop - 0.1F) {
            this.transform.position += transform.up * (shotSpeed * Time.fixedDeltaTime);
        } else {
            shot.SetActive(false);
        }
    }
}
