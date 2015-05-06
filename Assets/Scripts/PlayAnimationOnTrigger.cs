using UnityEngine;
using System.Collections;

public class PlayAnimationOnTrigger : MonoBehaviour {
    public string name = "GamePlayArea";
    public Animator animator;
    public float delay = 0.0f;
    
    private Transform newParent;

    public void Start() {
        if (animator == null) {
            animator = gameObject.GetComponent<Animator>();
        }
    }


    void OnTriggerEnter2D(Collider2D col2D) {
        Invoke("AnimateNow", delay);
    }

    void AnimateNow() {
        animator.enabled = true;
        gameObject.GetComponent<PlayAnimationOnTrigger>().enabled = false;
    }
}
