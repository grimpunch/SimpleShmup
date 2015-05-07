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

    // Use this for initialization
    void Start() {
        laserChargeSlider = GameObject.Find("LaserChargeSlider").GetComponent<Slider>();
    }



    // Update is called once per frame
    void Update() {
        if (Laser.active) {
            if (timeEnabled < timeToDischarge) { 
                timeEnabled += Time.deltaTime;
                laserChargeSlider.value = (timeToDischarge * 10)-(timeEnabled*10);
            }
            if (timeEnabled >= timeToDischarge) { 
                timeEnabled = 0.0F;
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
                timeCharged += Time.fixedDeltaTime * rateMultiplier;
            }
            if (timeCharged >= timeToFullCharge) {
                timeCharged = timeToFullCharge;
            }
            laserChargeSlider.value = timeCharged * (timeToFullCharge / 100.0F);
        }
    }
}
