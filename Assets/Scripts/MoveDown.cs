using UnityEngine;
using System.Collections;

public class MoveDown : MonoBehaviour {

    private Vector3 velocity;
    private float xinput;
    private float yinput;

    public float movementspeed = 1.0F;


    // Use this for initialization
    void Start() {
        xinput = 0.0F;
        yinput = -1.0F;
    }


    // Update is called once per frame
    void Update() {
        if (Utils.Paused) return;
        this.transform.position += transform.up * (0-movementspeed * Time.deltaTime);
    }
}