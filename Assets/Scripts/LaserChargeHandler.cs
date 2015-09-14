using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LaserChargeHandler : MonoBehaviour {

    private const int ENEMYLAYER = 9;
    private const int ENEMYSHOTLAYER = 11;
    private const int COLLIDABLELAYER = 13;

    public float laserSpeed = 100f;
    public float timeToFullCharge = 100.0F;
    private float timeCharged = 0.0F;
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

    void ResetLaser() {
        timeCharged = 0.0F;
        Laser.SetActive(false);
        timeEnabled = 0.0F;
        currentLaserLength = 0f;
        currentLaserWidth = 0f;
        if (laserBoxCollider2D) {
            laserBoxCollider2D.offset = new Vector2(0, 0);
            laserBoxCollider2D.size = new Vector2(currentLaserWidth, 0);
        }
        if (laserLineRenderer) laserLineRenderer.SetPosition(2, new Vector3(0, 0));
        playerShoot.enabled = true;
        if (laserChargeSlider) {
            laserChargeSlider.value = 0.0F;
        }
    }

    void OnEnable() {
        ResetLaser();
    }

    void OnDisable() {
        ResetLaser();
    }

    // Update is called once per frame
    void Update() {
        if (Utils.Paused) return;
        if (Laser.active) {
            laserLineRenderer = GameObject.Find("Laser").GetComponent<LineRenderer>();
            laserBoxCollider2D = GameObject.Find("Laser").GetComponent<BoxCollider2D>();
            if (timeEnabled < timeToDischarge) { 
                timeEnabled += Time.deltaTime;
                if (currentLaserLength < GetMaxLaserLength()) { 
                    currentLaserLength = Mathf.Lerp(currentLaserLength, GetMaxLaserLength(), Time.deltaTime * laserSpeed); } 
                else { currentLaserLength = GetMaxLaserLength(); }
                if (currentLaserWidth < maximumLaserWidth) 
                { currentLaserWidth = Mathf.Lerp(currentLaserWidth, maximumLaserWidth, Time.deltaTime * laserSpeed); } 
                else { currentLaserWidth = maximumLaserWidth; }
                laserBoxCollider2D.offset = new Vector2(0, currentLaserLength/2);
                laserBoxCollider2D.size = new Vector2(currentLaserWidth, currentLaserLength);
                laserLineRenderer.SetPosition(2, new Vector3(0, currentLaserLength));
                
                LaserTip.transform.position = new Vector3(transform.position.x, transform.position.y + currentLaserLength, -1F);
                if (currentLaserWidth >= maximumLaserWidth - 0.5f) {
                    laserLineRenderer.SetWidth(Random.Range(0.20f, 0.35f), Random.Range(0.30f, 0.35f));
                } else { laserLineRenderer.SetWidth(currentLaserWidth, currentLaserWidth); }
                laserChargeSlider.value = timeToFullCharge - timeEnabled;
            }
            if (timeEnabled >= timeToDischarge) { 
                ResetLaser();
            }
            return;
        }

        shooting = GetFireButtonDown();
        if (shooting && timeCharged < timeToFullCharge) {
            timeCharged = 0.0F;
            laserChargeSlider.value = 0.0F;
        }

        if (shooting && timeCharged >= timeToFullCharge) {
            timeCharged = 0.0F;
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
            laserChargeSlider.value = timeCharged;
            laserChargeSlider.maxValue = timeToFullCharge;
        }
    }

    private static bool GetFireButtonDown() {
        return Input.GetButton("Fire1");
    }

    private float GetMaxLaserLength() {
        float maximumLaserLength = 4.2f;
        Vector2 Position2D = new Vector2(transform.position.x, transform.position.y);
        LayerMask collidableLayers = 1 << ENEMYLAYER | 1 << COLLIDABLELAYER;
        RaycastHit2D hit = Physics2D.Raycast(Position2D, Vector2.up, 4.2f, collidableLayers);
        if (hit) { maximumLaserLength = Vector2.Distance(Position2D, hit.point); }
        return maximumLaserLength;
    }
}
