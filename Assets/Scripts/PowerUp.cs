using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public int shipUpgradeLevelIncreaseAmount;
    private PlayerShoot playerShootScript;
    // Use this for initialization
    void Start() {
        playerShootScript = GameObject.Find("TurretC").GetComponent<PlayerShoot>();
    }

    void OnTriggerStay2D(Collider2D col2d) {
        if (col2d.gameObject.name == "ShmupShip") {
            playerShootScript.upgradeLevel++;
            Destroy(gameObject); // TEMPORARY , NEEDS AUDIO AND SHIZ, TODO REMOVE THIS.
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
