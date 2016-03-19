using UnityEngine;
using System.Collections;

public class PlayMenuAnimationOnClick : MonoBehaviour {

    private Animation animationController;
    void Start(){
        animationController = gameObject.GetComponent<Animation>();
    }
    public void SwitchToMenu(){
        if (animationController.isPlaying)
            return;
        animationController.Play("MenuSpinToMain");   
    }
    public void SwitchToOptions(){
        if (animationController.isPlaying)
            return;
        animationController.Play("MenuSpinToOptions");   
    }
    public void SwitchToControls(){
        if (animationController.isPlaying)
            return;
        animationController.Play("MenuSpinToControls");   
    }
    public void SwitchToMainFromControls(){
        if (animationController.isPlaying)
            return;
        animationController.Play("MenuSpinToMainFromControls");   
    }
}
