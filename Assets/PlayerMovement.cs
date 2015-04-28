using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private float xinput;
    private float yinput;

    private float shipLeftSide;
    private float shipRightSide;
    private float shipTopSide;
    private float shipBottomSide;

    private ScreenBoundsHandler screenBounds;

    public float movementspeed = 1.0F;
    private GameObject ship;

    // Use this for initialization
    void Start() {
        ship = GameObject.FindGameObjectWithTag("Player");
        screenBounds = GameObject.Find("ScreenBoundsHandler").GetComponent<ScreenBoundsHandler>();
    }


    // Update is called once per frame
    void Update() {
        xinput = Input.GetAxis("Horizontal");
        yinput = Input.GetAxis("Vertical");
        shipLeftSide = ship.transform.position.x - (ship.renderer.bounds.size.x * 0.5F);
        shipRightSide = ship.transform.position.x + (ship.renderer.bounds.size.x * 0.5F);
        shipTopSide = ship.transform.position.y + (ship.renderer.bounds.size.y * 0.5F);
        shipBottomSide = ship.transform.position.y - (ship.renderer.bounds.size.y * 0.5F);

        Vector3 velocity = new Vector3(xinput * (Time.fixedDeltaTime * movementspeed), yinput * (Time.fixedDeltaTime * movementspeed), 0.0F);

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
