using UnityEngine;
using System.Collections;

public class ActivateGameObjectsOnTrigger : MonoBehaviour
{
    public GameObject[] gameObjects;
    private LayerMask ActivatorLayer = 30;
    public float delay = 0.0f;

    public void Boom()
    {
        Invoke("ActivateNow", delay);
    }

    void ActivateNow()
    {
        foreach(GameObject gb in gameObjects) {
            gb.SetActive(true);
        }
        Destroy(this);
    }
}
