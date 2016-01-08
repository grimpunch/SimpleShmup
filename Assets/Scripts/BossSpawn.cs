using UnityEngine;
using System.Collections;

public class BossSpawn : MonoBehaviour
{

    public GameObject spawnPrefab;
    
    private bool spawned;
    
    // Use this for initialization
    void Start()
    {
        Spawn();
        if(spawned) {
            Destroy(gameObject);
        }
    }

    void Spawn()
    {
        GameObject spawnGO = (GameObject)Instantiate(spawnPrefab, transform.position, transform.rotation);
        spawnGO.name = spawnPrefab.name;
        spawned = true;
    }
}
