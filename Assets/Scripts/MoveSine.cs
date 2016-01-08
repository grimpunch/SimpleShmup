using UnityEngine;
using System.Collections;

public class MoveSine : MonoBehaviour
{

    private Vector3 velocity;
    private float xinput;
    private float yinput;
    private ScreenBoundsHandler screenBounds;

    public float movementspeed = 1.0F;
    private GameObject ship;

    private float m_degrees;

    public float m_amplitude = 1.0f;

    public float m_period = 1.0f;


    // Use this for initialization
    void Start()
    {
        ship = gameObject;
        screenBounds = GameObject.Find("ScreenBoundsHandler").GetComponent<ScreenBoundsHandler>();
        xinput = 0.0F;
        yinput = 0.0F;
        velocity = new Vector3(xinput, yinput * (Time.deltaTime * movementspeed), 0.0F);
    }


    // Update is called once per frame
    void Update()
    {
        if(Utils.Paused)
            return;
        // Update degrees
        float degreesPerSecond = 360.0f / m_period;
        m_degrees = Mathf.Repeat(m_degrees + (Time.deltaTime * degreesPerSecond), 360.0f);
        float radians = m_degrees * Mathf.Deg2Rad;

        // Offset by sin wave
        xinput = m_amplitude * Mathf.Sin(radians);
        yinput = 0.0F;
        velocity = new Vector3(xinput * (Time.deltaTime * movementspeed), yinput * (Time.deltaTime * movementspeed), 0.0F);
        ship.transform.position += velocity;
    }

}