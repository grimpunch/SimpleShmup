using UnityEngine;
using System.Collections;

public class EnemyHitHandler : MonoBehaviour {

    public int shipHealth = 5;
    private const int PlayerShotLayer = 10;
    // Use this for initialization
    void Start() {

    }

    // OnCollide, check for collision with bullet

    void OnTriggerEnter2D(Collider2D col2d) {
        
        if (col2d.gameObject.layer == PlayerShotLayer) {
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
