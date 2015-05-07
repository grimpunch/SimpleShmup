using UnityEngine;
using System.Collections;

public class EnemyWaveSpawn : MonoBehaviour {

    public GameObject spawnPrefab;
    public int spawnCount;
    private int spawned = 0;
    public float spawnDelay;
    private float timeToSpawn;
    private bool left;
    private Vector3 spawnPos;
    public float spawnDeviation = 0.3f; 
    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (timeToSpawn < 0) {
            timeToSpawn = spawnDelay;
            Invoke("Spawn", spawnDelay);
        } else { timeToSpawn -= Time.fixedDeltaTime; }
        if (spawned >= spawnCount) { Destroy(gameObject); }
    }

    void Spawn() {
        
        if (left) {
            spawnPos = new Vector3(transform.position.x - spawnDeviation, transform.position.y, transform.position.z);
            left = false;
        }
        else if (!left) {
            spawnPos = new Vector3(transform.position.x + spawnDeviation, transform.position.y, transform.position.z);
            left = true;
        }
        GameObject spawnGO = (GameObject)Instantiate(spawnPrefab, spawnPos, transform.rotation);
        spawnGO.name = spawnPrefab.name;
        spawned++;
    }
}
