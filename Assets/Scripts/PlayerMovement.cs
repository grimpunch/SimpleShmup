using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

	public int player = 1;
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
	void Start()
	{
		ship = gameObject;
		screenBounds = GameObject.Find("ScreenBoundsHandler").GetComponent<ScreenBoundsHandler>();
	}

	public float xAnim {
		get { return xinput; }
	}

	private static float xInput(int player)
	{
		if (player == 1) {
			return InputManager.Horizontal_P1;
		} else {
			return InputManager.Horizontal_P2; 
		}
	}

	private static float yInput(int player)
	{
		if (player == 1) {
			return InputManager.Vertical_P1;
		} else {
			return InputManager.Vertical_P2; 
		}
	}

	private static bool focusDown(int player)
	{
		if (player == 1) {
			return InputManager.Fire2_P1;
		} else {
			return InputManager.Fire2_P2;
		}
	}

	// Update is called once per frame
	void Update()
	{
        if (Utils.Paused || GameManager.GameManagerInstance.gameState != Utils.GameState.Gameplay)
			return;
		xinput = xInput(player);
		yinput = yInput(player);
		focusKeyDown = focusDown(player);

		shipLeftSide = ship.transform.position.x - (ship.GetComponent<Renderer>().bounds.size.x * 0.5F);
		shipRightSide = ship.transform.position.x + (ship.GetComponent<Renderer>().bounds.size.x * 0.5F);
		shipTopSide = ship.transform.position.y + (ship.GetComponent<Renderer>().bounds.size.y * 0.5F);
		shipBottomSide = ship.transform.position.y - (ship.GetComponent<Renderer>().bounds.size.y * 0.5F);

		float m_speed = movementspeed;
		if (focusKeyDown)
			m_speed = focusmovementspeed; 

		Vector3 velocity = new Vector3(xinput * (Time.deltaTime * m_speed), yinput * (Time.deltaTime * m_speed), 0.0F);

		velocity = Vector3.ClampMagnitude(velocity, m_speed * Time.deltaTime);

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
