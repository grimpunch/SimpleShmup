using UnityEngine;
using System.Collections;

public class SpinObject : MonoBehaviour
{

    public float speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Utils.Paused)
            return;
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
