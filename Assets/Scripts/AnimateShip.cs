using UnityEngine;
using System.Collections;

public class AnimateShip : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private float velocity;
    public Sprite[] sprites;

    private const int NORMAL = 0;
    private const int LEFT = 1;
    private const int RIGHT = 2;
    private const int HURT = 3;


    // Use this for initialization
    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Utils.Paused)
            return;
        velocity = this.gameObject.GetComponent<PlayerMovement>().xAnim;
        if(velocity == 0) { 
            //Move Forward
            spriteRenderer.sprite = sprites[0];
        }
        if(velocity < 0.0F) {
            // Move Left
            spriteRenderer.sprite = sprites[1];
        }
        if(velocity > 0) {
            // Move Right
            spriteRenderer.sprite = sprites[2];
        }

    }
}
