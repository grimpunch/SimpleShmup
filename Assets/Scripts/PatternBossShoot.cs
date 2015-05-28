using UnityEngine;
using System.Collections;

public class PatternBossShoot : MonoBehaviour {

    public GameObject shotPrefab;
    public float shotDelay = 0.2f;
    public bool canShoot = false;
    public enum ShotPattern { Radial , Pinwheel, Aimed, Rank};
    private ShotPattern currentShotPattern = ShotPattern.Rank;
    
    // Going to define the shot patterns used in functions, then invoke them as needed and call 'Shoot' for the specified number of times and in the correct places
    //
    // Pattern Definitions:
    // Radial - Shots will be emitted simultaneously in a circle. repeated for a number of shots, dodged by taking cover in gap between angle of fire
    // Pinwheel - Shots emitted in radial manner with higher angle of cover but the rotation of the shot angle will spin CW or CCW requiring player movement around the boss for safety
    // Aimed - Shots will be in a line burst, fast, towards player directly.
    // Rank - Line of shots perpendicular to the player, expanding outwards slightly at an angle from origin

    void Start() {

    }

    //TODO: All below.
    void RadialPattern() { 
    
    }

    void PinwheelPattern() { 
    
    }

    void AimedPattern() { 
    
    }

    void RankPattern() { 
    
    }


    void FixedUpdate() {
        //This whole function requires changes for pattern implementation
        //Define switches for handling patterns here. use functions for implementation
        if (Utils.Paused) return;
        canShoot = gameObject.GetComponent<Animator>().GetBool("CanFire");
        if (!canShoot) {
            return;
        }
        if (!IsInvoking()) {
            Invoke("Shoot", shotDelay);
        }
        ///////////////////////////////////
    }

    void PlayShotSound(){
    if (GetComponent<AudioSource>() != null) {
            if (!GetComponent<AudioSource>().isPlaying) {
                GetComponent<AudioSource>().Play();
            }
        }
    }

    void Shoot(Vector3 shotPos, Quaternion shotRot) {
        GameObject shotGO = (GameObject)Instantiate(shotPrefab, shotPos, shotRot);
        shotGO.name = gameObject.name + "ShotInstance";
    }
}
