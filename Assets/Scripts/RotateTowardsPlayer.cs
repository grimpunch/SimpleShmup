using UnityEngine;
using System.Collections;

public class RotateTowardsPlayer : MonoBehaviour
{

    public float strength;
    public Transform target;

    // Use this for initialization
    void Start()
    {
        if(target != null && GameObject.Find("ShmupShip")) {
            target = GameObject.Find("ShmupShip").gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Utils.Paused)
            return;
        if(target != null) {
            //TODO , Dot product calc to find vector of 'down' from this transform to player to determine rotation direction
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90);
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * strength);
        } else {
            if(!GameObject.Find("ShmupShip")) {
                return;
            }
            target = GameObject.Find("ShmupShip").gameObject.transform;
        }
    }
}
