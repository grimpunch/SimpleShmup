using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public int shipUpgradeLevelIncreaseAmount;
    // Use this for initialization
    void Start() {
        
    }

    void OnTriggerStay2D(Collider2D col2d) {
        if (col2d.gameObject.name == "ShmupShip") {
            GameObject.Find("TurretC").GetComponent<PlayerShoot>().upgradeLevel++;
            Destroy(gameObject); // TEMPORARY , NEEDS AUDIO AND SHIZ, TODO REMOVE THIS.
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
