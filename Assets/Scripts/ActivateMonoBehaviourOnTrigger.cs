using UnityEngine;
using System.Collections;

public class ActivateMonoBehaviourOnTrigger : MonoBehaviour {
    public MonoBehaviour[] behaviours;
    private LayerMask ActivatorLayer = 30;
    public float delay = 0.0f;

    void OnTriggerEnter2D(Collider2D col2D) {
        if (col2D.gameObject.layer == ActivatorLayer) {
            Invoke("ActivateNow", delay);
        }
    }

    void ActivateNow() {
        foreach (MonoBehaviour mb in behaviours) {
            mb.enabled = true;
        }
        Destroy(this);
    }
}
