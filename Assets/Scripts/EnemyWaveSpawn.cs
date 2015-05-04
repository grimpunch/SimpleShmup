using UnityEngine;
using System.Collections;

public class EnemyWaveSpawn : MonoBehaviour {

    public GameObject spawnPrefab;
    public int spawnCount;
    private int spawned = 0;
    public float spawnDelay;
    private float timeToSpawn;

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
        GameObject spawnGO = (GameObject)Instantiate(spawnPrefab, transform.position, transform.rotation);
        spawnGO.name = spawnPrefab.name;
        spawned++;
    }
}
