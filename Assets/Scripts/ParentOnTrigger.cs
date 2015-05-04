using UnityEngine;
using System.Collections;

public class ParentOnTrigger : MonoBehaviour {
    public string name = "GamePlayArea";
    public Transform target;
    public float delay = 0.0f;
    public bool setLocalPosition = false;
    public Vector3 localPosition = Vector3.zero;

    private Transform newParent;

    public void Start() {
        newParent = GameObject.Find(name).transform;
        if (target == null) {
            target = transform;
        }
    }

    
    void OnTriggerEnter2D(Collider2D col2D) {
            Invoke("ParentNow", delay);
    }

    void ParentNow() {
        target.parent = newParent;

        if (setLocalPosition) {
            target.localPosition = localPosition;
        }
        gameObject.GetComponent<ParentOnTrigger>().enabled = false;
    }
}
