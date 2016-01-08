using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTailGib : MonoBehaviour
{

    public List<GameObject> bodyparts;
    public GameObject tail;
    
    private GameObject currentbody;

    void Update()
    {
        if(!tail.active) {
            GibAllParts();
        }
    }

    // Update is called once per frame
    void GibAllParts()
    {
        foreach(GameObject part in bodyparts) {
            part.SendMessage("Gib");
        }
        gameObject.SendMessage("Gib");
    }
}
