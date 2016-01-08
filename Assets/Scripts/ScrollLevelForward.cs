using UnityEngine;
using System.Collections;

public class ScrollLevelForward : MonoBehaviour
{

    public float scrollSpeed = 0.25F;
    private float movingSpeed;
    public bool stopped;
    public float FadeSpeed = 1.0f;
    
    // Use this for initialization
    void Start()
    {
        movingSpeed = scrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Utils.Paused)
            return;
        if(!stopped && scrollSpeed <= movingSpeed) {
            scrollSpeed += Mathf.Lerp(0f, movingSpeed, FadeSpeed * Time.deltaTime);
        }
        if(!stopped && scrollSpeed >= movingSpeed - 0.1f)
            scrollSpeed = movingSpeed;
        //Slow to a stop
        if(stopped && scrollSpeed >= 0f) { 
            scrollSpeed = FadeSpeed * Time.deltaTime;
            if(scrollSpeed <= 0.1f)
                scrollSpeed = 0f;
        }
        transform.position += transform.up * (scrollSpeed * Time.deltaTime);
    }
}
