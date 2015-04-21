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
        screenLeftSide = 0.0F - screenRightSide;
        shipTopSide = ship.transform.position.y + (ship.renderer.bounds.size.y * 0.5F);
        shipBottomSide = ship.transform.position.y - (ship.renderer.bounds.size.y * 0.5F);
        screenTopSide =  Camera.main.ScreenToWorldPoint(new Vector3(0.0F, Screen.height, 0.0F)).y;
        screenBottomSide = 0.0F - screenTopSide;

        var velocity = new Vector3((xinput * Time.fixedDeltaTime) * movementspeed, (yinput * Time.fixedDeltaTime) * movementspeed, 0.0F);
		
        if ((screenLeftSide < shipLeftSide) && velocity.x < 0.0F) 
        { /* Move Left */
            ship.transform.position += velocity;
        }
        if ((shipRightSide < screenRightSide) && velocity.x > 0.0F) 
        { /* Move Right */
            ship.transform.position += velocity;
		}

        if ((screenTopSide > shipTopSide) && velocity.y > 0.0F) 
        { /* Move Down */
            ship.transform.position += velocity;
        }
        if ((shipBottomSide > screenBottomSide) && velocity.y < 0.0F) 
        { /* Move Up */
            ship.transform.position += velocity;
        }

	}
}
