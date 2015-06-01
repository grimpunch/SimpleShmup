using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private float xinput;
    private float yinput;

    private float shipLeftSide;
    private float shipRightSide;
    private float shipTopSide;
    private float shipBottomSide;

    private bool focusKeyDown;

    private ScreenBoundsHandler screenBounds;

    public float movementspeed = 1.0F;
    public float focusmovementspeed = 0.6f;
    private GameObject ship;

    // Use this for initialization
    void Start() {
        ship = GameObject.FindGameObjectWithTag("Player");
        screenBounds = GameObject.Find("ScreenBoundsHandler").GetComponent<ScreenBoundsHandler>();
    }

    public float xAnim {
        get { return xinput; }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Utils.Paused) return;
        xinput = Input.GetAxis("Horizontal");
        yinput = Input.GetAxis("Vertical");
        focusKeyDown = Input.GetButton("Fire2");

        shipLeftSide = ship.transform.position.x - (ship.GetComponent<Renderer>().bounds.size.x * 0.5F);
        shipRightSide = ship.transform.position.x + (ship.GetComponent<Renderer>().bounds.size.x * 0.5F);
        shipTopSide = ship.transform.position.y + (ship.GetComponent<Renderer>().bounds.size.y * 0.5F);
        shipBottomSide = ship.transform.position.y - (ship.GetComponent<Renderer>().bounds.size.y * 0.5F);

        float m_speed = movementspeed;
        if (focusKeyDown) { m_speed = focusmovementspeed; Debug.Log("Focus held down"); }

        Vector3 velocity = new Vector3(xinput * (Time.fixedDeltaTime * m_speed), yinput * (Time.fixedDeltaTime * m_speed), 0.0F);

        velocity = Vector3.ClampMagnitude(velocity, m_speed * Time.fixedDeltaTime);

        if (shipLeftSide < screenBounds.ScreenLeft) { /* Ship hugging left side */
            if (velocity.x < 0.0F) {
                velocity = new Vector3(0.0F, velocity.y, 0.0F);
            }
        }
        if (shipRightSide > screenBounds.ScreenRight) { /* Ship hugging right side */
            if (velocity.x > 0.0F) {
                velocity = new Vector3(0.0F, velocity.y, 0.0F);
            }
        }

        if (screenBounds.ScreenTop < shipTopSide) { /* Ship hugging top side */
            if (velocity.y > 0.0F) {
                velocity = new Vector3(velocity.x, 0.0F, 0.0F);
            }
        }
        if (shipBottomSide < screenBounds.ScreenBottom) { /* Ship hugging bottom side */
            if (velocity.y < 0.0F) {
                velocity = new Vector3(velocity.x, 0.0F, 0.0F);
            }
        }

        ship.transform.position += velocity;

    }
}
