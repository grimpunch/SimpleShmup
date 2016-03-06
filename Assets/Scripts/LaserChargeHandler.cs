using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LaserChargeHandler : MonoBehaviour
{

	private const int ENEMYLAYER = 9;
	private const int ENEMYSHOTLAYER = 11;
	private const int COLLIDABLELAYER = 13;
	public int player = 1;
	public float laserSpeed = 100f;
	public float amountToFullCharge = 100.0F;
	private float amountCharged = 0.0F;
	public float rateMultiplier = 1.0F;
	private bool shooting;
	private Slider laserChargeSlider;
	public GameObject Laser;
	public GameObject LaserTip;
	public PlayerShoot playerShoot;
	private float timeEnabled = 0.0F;
	public float timeToDischarge = 10.0F;
    
	//Animation of Laser Movement control variables
	private const float maximumLaserLength = 4.2f;
	private const float maximumLaserWidth = 0.02f;
	private float currentLaserLength;
	private float targetLaserLength;
	private float currentLaserWidth;
	private BoxCollider2D laserBoxCollider2D;
	private LineRenderer laserLineRenderer;
	private int currentCapturedShips;
	private CaptureShipHandler myCaptureShipHandler;
	public GameObject primingUILabel;
	public GameObject primedUILabel;
	public GameObject activeUILabel;

	//Relationship between laser collider offset and laser line renderer segment 2 length.
	//
	// collider offset Y = collider size Y / 2
	// Line renderer size Y (Element 2) = collider size Y == distance to target


	// Use this for initialization
	void Start()
	{
		if (!Utils.Multiplayer) {
			laserChargeSlider = GameObject.Find("LaserChargeSlider_P1").GetComponent<Slider>();
		} else {
			if (gameObject.name.EndsWith("_P1")) {
				laserChargeSlider = GameObject.Find("LaserChargeSlider_P1").GetComponent<Slider>();
			} else {
				laserChargeSlider = GameObject.Find("LaserChargeSlider_P2").GetComponent<Slider>();
			}
		}
		if (!Utils.Multiplayer) {
			if (GameObject.Find("LaserChargeSlider_P2")) {
				GameObject.Find("LaserChargeSlider_P2").SetActive(false);
			}
			if (gameObject.name.EndsWith("_P2")) {
				gameObject.GetComponent<LaserChargeHandler>().enabled = false;
			}
		}

		myCaptureShipHandler = this.gameObject.transform.parent.GetComponentInChildren<CaptureShipHandler>();
		primingUILabel.SetActive(true);
		primedUILabel.SetActive(false);
		activeUILabel.SetActive(false);
	}



	void ResetLaser()
	{
		amountCharged = 0.0F;
		Laser.SetActive(false);
		timeEnabled = 0.0F;
		currentLaserLength = 0f;
		currentLaserWidth = 0f;
		if (laserBoxCollider2D) {
			laserBoxCollider2D.offset = new Vector2(0, 0);
			laserBoxCollider2D.size = new Vector2(currentLaserWidth, 0);
		}
		if (laserLineRenderer)
			laserLineRenderer.SetPosition(2, new Vector3(0, 0));
		if (laserChargeSlider) {
			laserChargeSlider.value = 0.0F;
		}
		primingUILabel.SetActive(true);
		primedUILabel.SetActive(false);
		activeUILabel.SetActive(false);
	}

	void OnEnable()
	{
		ResetLaser();
	}

	void OnDisable()
	{
		ResetLaser();
	}

	// Update is called once per frame
	void Update()
	{
		if (Utils.Paused)
			return;
		if (!Utils.Multiplayer) {
			if (GameObject.Find("LaserChargeSlider_P2")) {
				GameObject.Find("LaserChargeSlider_P2").SetActive(false);
			}
		}
		if (Laser.activeInHierarchy) {
			primingUILabel.SetActive(false);
			primedUILabel.SetActive(false);
			activeUILabel.SetActive(true);
			laserLineRenderer = GameObject.Find("Laser").GetComponent<LineRenderer>();
			laserBoxCollider2D = GameObject.Find("Laser").GetComponent<BoxCollider2D>();
			if (timeEnabled < timeToDischarge) { 
				timeEnabled += Time.deltaTime;
				currentLaserLength = GetMaxLaserLength();
				if (currentLaserWidth < maximumLaserWidth) {
					currentLaserWidth = Mathf.Lerp(currentLaserWidth, maximumLaserWidth, Time.deltaTime * laserSpeed);
				} else {
					currentLaserWidth = maximumLaserWidth;
				}
				laserBoxCollider2D.offset = new Vector2(0, currentLaserLength / 2);
				laserBoxCollider2D.size = new Vector2(currentLaserWidth, currentLaserLength);
				laserLineRenderer.SetPosition(2, new Vector3(0, currentLaserLength));
                
				LaserTip.transform.position = new Vector3(transform.position.x, transform.position.y + currentLaserLength, -1F);
				laserLineRenderer.SetWidth(currentLaserWidth, currentLaserWidth);
			}
			if (timeEnabled > timeToDischarge) {
				Discharge();
			}
			return;
		}

		shooting = GetFireButtonDown();

		if (shooting && amountCharged >= amountToFullCharge) {
			currentCapturedShips = myCaptureShipHandler.capturedEnemies;
			amountCharged = 0.0F;
			Laser.SetActive(true);
		}

		if (!shooting && amountCharged < amountToFullCharge) {
			if (amountCharged >= amountToFullCharge) {
				amountCharged = amountToFullCharge;
			}
		}

		if (!shooting && amountCharged >= amountToFullCharge) {
			amountCharged = amountToFullCharge;
			primingUILabel.SetActive(false);
			primedUILabel.SetActive(true);
			activeUILabel.SetActive(false);
		}

		laserChargeSlider.value = amountCharged;
		laserChargeSlider.maxValue = amountToFullCharge;
	}

	public void AddCharge(int amount)
	{
		amountCharged += amount * rateMultiplier;
	}

	public void Discharge()
	{
		timeEnabled = timeToDischarge;
		ResetLaser();
		// This below condition is designed to reactivate the player's ability to use the laser if they have not captured an enemy yet.
		if (myCaptureShipHandler.capturedEnemies == currentCapturedShips
		    && myCaptureShipHandler.capturing == false) {
			AddCharge(9999);
		}
	}

	public bool GetFireButtonDown()
	{
		if (player == 1) {
			return InputManager.Fire3_P1;	
		} else {
			return InputManager.Fire3_P2;
		} 
	}

	public bool GetFireButtonUp()
	{
		return !GetFireButtonDown();
	}

	private float GetMaxLaserLength()
	{
		float maximumLaserLength = 4.2f;
		Vector2 Position2D = new Vector2(transform.position.x, transform.position.y);
		LayerMask collidableLayers = 1 << ENEMYLAYER | 1 << COLLIDABLELAYER;
		RaycastHit2D hit = Physics2D.Raycast(Position2D, Vector2.up, 4.2f, collidableLayers);
		if (hit) {
			maximumLaserLength = Vector2.Distance(Position2D, hit.point);
			return maximumLaserLength;
		}
		return maximumLaserLength;
	}
}
