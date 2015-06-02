﻿using UnityEngine;
using System.Collections;

public class PatternBossShoot : MonoBehaviour {

    public GameObject aimedShotPrefab;
    public GameObject rankShotPrefab;
    public GameObject radialShotPrefab;
    public GameObject pinWheelShotPrefab;
    private GameObject shotPrefab;
    private Transform playerTransform;
    public float aimedShotDelay = 2.0f;
    public float rankShotDelay = 2.0f;
    public float radialShotDelay = 2.0f;
    public float pinWheelShotDelay = 2.0f;
    private float timeUntilNextShot;
    public bool canShoot = false;
    public enum ShotPattern { Radial , Pinwheel, Aimed, Rank};
    private ShotPattern currentShotPattern = ShotPattern.Aimed;
    public float waitBetweenPatterns = 4f;
    private float timeUntilNextPattern;
    private bool readyForPattern = true;
    public float angleBetweenRadialShots = 5f;
    private Quaternion aimedShotAngle;
    // Settings for Shot Patterns
    public int aimedShotsUntilChange = 6;
    private int aimedShotsLeft;
    public int radialShotsUntilChange = 10;
    private int radialShotsLeft;

    ////////////////////////////

    static T GetRandomEnum<T>() {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    // Going to define the shot patterns used in functions, then invoke them as needed and call 'Shoot' for the specified number of times and in the correct places
    //
    // Pattern Definitions:
    // Radial - Shots will be emitted simultaneously in a circle. repeated for a number of shots, dodged by taking cover in gap between angle of fire
    // Pinwheel - Shots emitted in radial manner with higher angle of cover but the rotation of the shot angle will spin CW or CCW requiring player movement around the boss for safety
    // Aimed - Shots will be in a line burst, fast, towards player directly.
    // Rank - Line of shots perpendicular to the player, expanding outwards slightly at an angle from origin

    void Start() {
        timeUntilNextPattern = waitBetweenPatterns;
        radialShotsLeft = radialShotsUntilChange;
    }

    //TODO: All below.
    void AimedPattern() {
        Debug.Log("Doing Aimed Pattern");
        shotPrefab = aimedShotPrefab;
        if (playerTransform == null) {
            try {
                playerTransform = GameObject.FindWithTag("Player").transform;
            } catch { return; }
        }
        if (aimedShotsLeft == aimedShotsUntilChange) aimedShotAngle = Utils.RotationToTarget(transform, playerTransform);
        if (aimedShotsLeft> 0) {
            if (timeUntilNextShot <= 0) AimedShot();
            else { timeUntilNextShot -= Time.deltaTime; }
            return;
        }
        readyForPattern = true;
        canShoot = false;
        aimedShotsLeft = aimedShotsUntilChange;
    }

    void AimedShot() {
        Shoot(transform.position, aimedShotAngle);
        PlayShotSound();
        timeUntilNextShot = aimedShotDelay;
        aimedShotsLeft--;
    }

    void RankPattern() {
        Debug.Log("Doing Rank Pattern -- Not Implemented");
        shotPrefab = rankShotPrefab;
        readyForPattern = true;
    }

    void RadialPattern() {
        Debug.Log("Doing Radial Pattern");
        shotPrefab = radialShotPrefab;
        if (radialShotsLeft > 0) {
            if (timeUntilNextShot <= 0) RadialShot();
            else { timeUntilNextShot -= Time.deltaTime; }
            return;
        }
        readyForPattern = true;
        canShoot = false;
        radialShotsLeft = radialShotsUntilChange;
    }

    private void RadialShot() {
        int shotsToFire = Mathf.FloorToInt(360.0f / angleBetweenRadialShots);
        float currentShotAngle = angleBetweenRadialShots;
        for (int shotsFired = 0; shotsFired < shotsToFire; shotsFired++) {
            Shoot(transform.position, Quaternion.Euler(0f, 0f, currentShotAngle));
            currentShotAngle += angleBetweenRadialShots;
        }
        PlayShotSound();
        timeUntilNextShot = radialShotDelay;
        radialShotsLeft--;
    }

    void PinwheelPattern() {
        Debug.Log("Doing Pinwheel Pattern -- Not Implemented");
        shotPrefab = pinWheelShotPrefab;
        readyForPattern = true;
    }

    void FixedUpdate() {
        //This whole function requires changes for pattern implementation
        //Define switches for handling patterns here. use functions for implementation
        if (Utils.Paused) return;
        
        if (readyForPattern) {
            if (timeUntilNextPattern <= 0) {
                timeUntilNextPattern = waitBetweenPatterns;
                currentShotPattern = ShotPattern.Aimed; // TODO REMOVE DEBUG GetRandomEnum<ShotPattern>();
                readyForPattern = false;
                canShoot = true;
            } else { timeUntilNextPattern -= Time.deltaTime; }
        } 

        if (!canShoot) {
            return;
        }
 
        switch (currentShotPattern) {
            case ShotPattern.Aimed:
                AimedPattern();
                break;
            case ShotPattern.Rank:
                RankPattern();
                break;
            case ShotPattern.Radial:
                RadialPattern();
                break;
            case ShotPattern.Pinwheel:
                PinwheelPattern();
                break;
        }
        ///////////////////////////////////
    }

    void PlayShotSound(){
    if (GetComponent<AudioSource>() != null) {
                GetComponent<AudioSource>().Play();
            
        }
    }

    void Shoot(Vector3 shotPos, Quaternion shotRot) {
        GameObject shotGO = (GameObject)Instantiate(shotPrefab, shotPos, shotRot);
        shotGO.name = gameObject.name + "ShotInstance";
    }
}