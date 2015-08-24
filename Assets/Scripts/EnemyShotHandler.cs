using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShotHandler : MonoBehaviour {

    private List<GameObject> shots;
    private ScreenBoundsHandler screenBounds;
    public float shotSpeed = 0.5F;

    // Use this for initialization
    void Start() {
        screenBounds = GameObject.Find("ScreenBoundsHandler").GetComponent<ScreenBoundsHandler>();
    }

    // Update is called once per frame
    void Update() {
        shots = gameObject.GetComponent<ObjectPoolScript>().pooledObjects;
        if (Utils.Paused) return;
        foreach (GameObject shot in shots) {
            if (shot.activeSelf) MoveShot(shot);
        }
    }

    internal void RemoveAllShots() {
        shots = gameObject.GetComponent<ObjectPoolScript>().pooledObjects;
        foreach (GameObject shot in shots) {
            if (shot.activeSelf) shot.SetActive(false);
        }
    }

    void MoveShot(GameObject shot) {
        if (shot.transform.position.x > screenBounds.ScreenRight + 0.2F || shot.transform.position.x < screenBounds.ScreenLeft - 0.2F
            || shot.transform.position.y < screenBounds.ScreenBottom - 0.1F || shot.transform.position.y > screenBounds.ScreenTop + 0.1F) {
            shot.SetActive(false);
        }
        if (shot.transform.position.y > screenBounds.ScreenBottom - 0.1F || shot.transform.position.y < screenBounds.ScreenTop + 0.1F) {
            shot.transform.position += shot.transform.up * (shotSpeed * Time.deltaTime);
        } else {
            shot.SetActive(false);
        }
    }
}