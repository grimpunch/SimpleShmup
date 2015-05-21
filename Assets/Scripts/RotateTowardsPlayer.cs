using UnityEngine;
using System.Collections;

public class RotateTowardsPlayer : MonoBehaviour {

    public float strength;
    public Transform target;

    // Use this for initialization
    void Start() {
        target = GameObject.Find("ShmupShip").gameObject.transform;
    }

    // Update is called once per frame
    void Update() {
        if (target != null) {
            //TODO , Dot product calc to find vector of 'down' from this transform to player to determine rotation direction
            if (strength >= 0) { strength -= Time.deltaTime; }
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * strength);
        } else {
            if (!GameObject.Find("ShmupShip")) { return; }
            target = GameObject.Find("ShmupShip").gameObject.transform; }
    }
}
