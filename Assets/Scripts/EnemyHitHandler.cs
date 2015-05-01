using UnityEngine;
using System.Collections;

public class EnemyHitHandler : MonoBehaviour {

    public int shipHealth = 5;
    private const int PLAYERSHOTLAYER = 10;

    // Use this for initialization
    void Start() {

    }

    void OnTriggerEnter2D(Collider2D col2d) {
        if (col2d.gameObject.layer == PLAYERSHOTLAYER) {
            shipHealth -= 1;
            Destroy(col2d.gameObject);
        }
    }


    // Update is called once per frame
    void Update() {
        if (shipHealth <= 0) {
            gameObject.SendMessage("Gib");
        }
    }
}
