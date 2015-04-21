using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private float xinput;
	private float yinput;

    private float shipLeftSide;
    private float shipRightSide;

    private float screenLeftSide;
    private float screenRightSide;

    private float shipTopSide;
    private float shipBottomSide;

    private float screenTopSide;
    private float screenBottomSide;

    
    public float movementspeed = 1.0F;
	private GameObject ship;

	// Use this for initialization
	void Start () {
		ship = GameObject.FindGameObjectWithTag("Player");
        
	}
	

	// Update is called once per frame
	void Update () {
        xinput = Input.GetAxis("Horizontal");
        yinput = Input.GetAxis("Vertical");
        shipLeftSide = ship.transform.position.x - (ship.renderer.bounds.size.x * 0.5F);
        shipRightSide = ship.transform.position.x + (ship.renderer.bounds.size.x * 0.5F);
        screenRightSide = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0F, 0.0F)).x;
        screenLeftSide = Camera.main.ScreenToWorldPoint(new Vector3(0.0F, 0.0F, 0.0F)).x;
        shipTopSide = ship.transform.position.y + (ship.renderer.bounds.size.y * 0.5F);
        shipBottomSide = ship.transform.position.y - (ship.renderer.bounds.size.y * 0.5F);
        screenTopSide =  Camera.main.ScreenToWorldPoint(new Vector3(0.0F, Screen.height, 0.0F)).y;
        screenBottomSide = Camera.main.ScreenToWorldPoint(new Vector3(0.0F, 0.0F, 0.0F)).y;
 
        Vector3 velocity = new Vector3(xinput * (Time.fixedDeltaTime * movementspeed), yinput * (Time.fixedDeltaTime * movementspeed), 0.0F);

        if (shipLeftSide < screenLeftSide) { /* Ship hugging left side */
            if (velocity.x < 0.0F){
                velocity = new Vector3(0.0F, velocity.y, 0.0F);
            }
        }
        if (shipRightSide > screenRightSide) { /* Ship hugging right side */
            if (velocity.x > 0.0F) {
                velocity = new Vector3(0.0F, velocity.y, 0.0F);
            }
        }

        if (screenTopSide < shipTopSide) { /* Ship hugging top side */
            if (velocity.y > 0.0F) {
                velocity = new Vector3(velocity.x, 0.0F, 0.0F);
            }
        }
        if (shipBottomSide < screenBottomSide) { /* Ship hugging bottom side */
            if (velocity.y < 0.0F) {
                velocity = new Vector3(velocity.x, 0.0F, 0.0F);
            }
        }
        
        ship.transform.position += velocity;
        
	}
}
