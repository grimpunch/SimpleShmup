using UnityEngine;
using System.Collections;

public class ActivateMonoBehaviourOnTrigger : MonoBehaviour {
    public MonoBehaviour behaviour;
    public float delay = 0.0f;

    void OnTriggerEnter2D(Collider2D col2D) {
        Invoke("ActivateNow", delay);
    }

    void ActivateNow() {
        behaviour.enabled = true;
    }
}
