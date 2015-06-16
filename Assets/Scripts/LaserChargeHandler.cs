using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LaserChargeHandler : MonoBehaviour {

    public float timeToFullCharge = 100.0F;
    private float timeCharged = 0.0F;
    public float rateMultiplier = 1.0F;
    private bool shooting;
    private Slider laserChargeSlider;
    public GameObject Laser;
    public PlayerShoot playerShoot;
    private float timeEnabled = 0.0F;
    public float timeToDischarge = 10.0F;
    
    //Animation of Laser Movement control variables
    private const float maximumLaserLength = 6f;
    private const float maximumLaserWidth = 0.35f;
    private float currentLaserLength;
    private float targetLaserLength;
    private float currentLaserWidth;
    private BoxCollider2D laserBoxCollider2D;
    private LineRenderer laserLineRenderer;

    //Relationship between laser collider offset and laser line renderer segment 2 length.
    //
    // collider offset Y = collider size Y / 2
    // Line renderer size Y (Element 2) = collider size Y == distance to target


    // Use this for initialization
    void Start() {
        laserChargeSlider = GameObject.Find("LaserChargeSlider").GetComponent<Slider>();
        
    }

    // Update is called once per frame
    void Update() {
        if (Utils.Paused) return;
        if (Laser.active) {
            laserLineRenderer = GameObject.Find("Laser").GetComponent<LineRenderer>();
            laserBoxCollider2D = GameObject.Find("Laser").GetComponent<BoxCollider2D>();
            if (timeEnabled < timeToDischarge) { 
                timeEnabled += Time.deltaTime;
                if (currentLaserLength < maximumLaserLength) 
                {currentLaserLength = Mathf.Lerp(currentLaserLength,maximumLaserLength, Time.deltaTime);} 
                else { currentLaserLength = maximumLaserLength; }
                if (currentLaserWidth < maximumLaserWidth) 
                { currentLaserWidth = Mathf.Lerp(currentLaserWidth, maximumLaserWidth, Time.deltaTime); } 
                else { currentLaserWidth = maximumLaserWidth; }
                laserBoxCollider2D.offset = new Vector2(0, currentLaserLength/2);
                laserBoxCollider2D.size = new Vector2(currentLaserWidth, currentLaserLength);
                laserLineRenderer.SetPosition(2, new Vector3(0, currentLaserLength));
                if (currentLaserWidth >= maximumLaserWidth - 0.5f) {
                    laserLineRenderer.SetWidth(Random.Range(0.20f, 0.35f), Random.Range(0.30f, 0.35f));
                } else { laserLineRenderer.SetWidth(currentLaserWidth, currentLaserWidth); }
                laserChargeSlider.value = (timeToDischarge * 10)-(timeEnabled*10);
            }
            if (timeEnabled >= timeToDischarge) { 
                timeEnabled = 0.0F;
                currentLaserLength = 0f;
                currentLaserWidth = 0f;
                laserBoxCollider2D.offset = new Vector2(0, 0);
                laserBoxCollider2D.size = new Vector2(currentLaserWidth, 0);
                laserLineRenderer.SetPosition(2, new Vector3(0, 0));
                playerShoot.enabled = true;
                Laser.active = false; 
            }
            return;
        }

        shooting = Input.GetButton("Fire1");
        if (shooting && timeCharged < timeToFullCharge) {
            timeCharged = 0.0F;
            laserChargeSlider.value = 0.0F;
        }

        if (shooting && timeCharged >= timeToFullCharge) {
            timeCharged = 0.0F;
            Debug.Log("FIRING ZE LASERS!");
            playerShoot.enabled = false;
            Laser.active = true;
        }

        if (!shooting) {
            if (timeCharged < timeToFullCharge) {
                timeCharged += Time.deltaTime * rateMultiplier;
            }
            if (timeCharged >= timeToFullCharge) {
                timeCharged = timeToFullCharge;
            }
            laserChargeSlider.value = timeCharged * (timeToFullCharge / 100.0F);
        }
    }
}
