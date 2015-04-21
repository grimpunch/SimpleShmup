﻿using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    private float screenLeftSide;
    private float screenRightSide;
    private float screenTopSide;
    private float screenBottomSide;
    private Vector3 velocity;
    private float xinput;
    private float yinput;

    public enum EnemyShipType { 
        A, /* Straight Down */ 
        B, /* Sine */
        C, /*  */
        D, /*  */
        E  /*  */
    }

    public EnemyShipType shipType = EnemyShipType.A;

    public float movementspeed = 1.0F;
    private GameObject ship;

    // Use this for initialization
    void Start() {
        ship = gameObject;
        velocity = new Vector3(0.0F, -1.0F * (Time.fixedDeltaTime * movementspeed), 0.0F);
    }
    
    float m_degrees;
   
    [SerializeField]
    float m_speed = 1.0f;
   
    [SerializeField]
    float m_amplitude = 1.0f;
   
    [SerializeField]
    float m_period = 1.0f;


    // Update is called once per frame
    void Update() {
        screenRightSide = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0F, 0.0F)).x;
        screenLeftSide = Camera.main.ScreenToWorldPoint(new Vector3(0.0F, 0.0F, 0.0F)).x;
        screenTopSide = Camera.main.ScreenToWorldPoint(new Vector3(0.0F, Screen.height, 0.0F)).y;
        screenBottomSide = Camera.main.ScreenToWorldPoint(new Vector3(0.0F, 0.0F, 0.0F)).y;

        switch (shipType) {
            case EnemyShipType.A: {
                xinput = 0.0F;
                yinput = -1.0F;
                break; 
            }
            case EnemyShipType.B: { 
                float deltaTime = Time.deltaTime;
                
                // Update degrees
                float degreesPerSecond = 360.0f / m_period;
                m_degrees = Mathf.Repeat(m_degrees + (Time.fixedDeltaTime * degreesPerSecond), 360.0f);
                float radians = m_degrees * Mathf.Deg2Rad;

                // Offset by sin wave
                xinput = m_amplitude * Mathf.Sin(radians);
                yinput = -1.0F;
                break; 
            }
            case EnemyShipType.C: {
                
                break; 
            }
            case EnemyShipType.D: {
            
                break; 
            }
            case EnemyShipType.E: {
           
                break; 
            }
        }


        velocity = new Vector3(xinput * (Time.fixedDeltaTime * movementspeed), yinput * (Time.fixedDeltaTime * movementspeed), 0.0F);

        if (ship.transform.position.y + 5 < screenBottomSide) { /* Ship has gone off bottom of screen */
            Debug.Log("Removing Enemy as it's offscreen");
            Destroy(gameObject);
        }

        ship.transform.position += velocity;

    }
}