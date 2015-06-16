using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHitHandler : MonoBehaviour {

    public int shipHealth = 1;
    private const int ENEMYLAYER = 9;
    private const int ENEMYSHOTLAYER = 11;
    private const int COLLIDABLELAYER = 13;
    public float invulnerabilityTimeOnSpawn = 2.5f;
    private float timeUntilVulnerable = 0.0f;
    private bool vulnerable;
    public SpriteRenderer playerShipRenderer;
    public SpriteRenderer turretLRenderer;
    public SpriteRenderer turretRRenderer;
    public SpriteRenderer turretCRenderer;
    public HSLColor invulnerableColor;

    private LifeHandler lifeHandler;

    // Use this for initialization
    void Start() {
        lifeHandler = GameObject.Find("GameManager").GetComponent<LifeHandler>();
        playerShipRenderer = gameObject.GetComponent<SpriteRenderer>();
        vulnerable = true;
        invulnerableColor = new HSLColor(new Color(1, 0.75f, 0.75f));
        
    }

    void OnEnable() { 
        vulnerable = false;
        shipHealth = 1;
        timeUntilVulnerable = 0f;
    }

    void OnTriggerEnter2D(Collider2D col2d) {
        if (!vulnerable) { return; }
        Debug.Log("Colliding with " + col2d.name);
        if (col2d.gameObject.layer == ENEMYLAYER || col2d.gameObject.layer == ENEMYSHOTLAYER || col2d.gameObject.layer == COLLIDABLELAYER) {
            shipHealth -= 1;
            if (col2d.gameObject.layer == ENEMYSHOTLAYER) {
                col2d.gameObject.SetActive(false);
            }
        }
    }


    // Update is called once per frame
    void Update() {
        if (Utils.Paused) return;
        if (!vulnerable) {
            invulnerableColor.h += 1000 * Time.deltaTime;
            playerShipRenderer.color = invulnerableColor;
            
            timeUntilVulnerable += Time.deltaTime;
            if (timeUntilVulnerable >= invulnerabilityTimeOnSpawn){
                vulnerable = true;
                playerShipRenderer.color = Color.white;
            }
        }
        if (shipHealth <= 0) {
            lifeHandler.SendMessage("Dead");
            gameObject.SendMessage("Gib");
        }
    }
}
