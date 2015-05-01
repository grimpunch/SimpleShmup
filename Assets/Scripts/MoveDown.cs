using UnityEngine;
using System.Collections;

public class MoveDown : MonoBehaviour {

    private Vector3 velocity;
    private float xinput;
    private float yinput;

    public float movementspeed = 1.0F;


    // Use this for initialization
    void Start() {
        velocity = new Vector3(0.0F, -1.0F * (Time.fixedDeltaTime * movementspeed), 0.0F);
        xinput = 0.0F;
        yinput = -1.0F;
    }


    // Update is called once per frame
    void FixedUpdate() {
        velocity = new Vector3(xinput * (Time.fixedDeltaTime * movementspeed), yinput * (Time.fixedDeltaTime * movementspeed), 0.0F);
        transform.position += velocity;
    }
}