using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class Utils {
    public static bool Paused = false;
}


public class GameHandler : MonoBehaviour {

    
    // Use this for initialization
    void Start() {
        Utils.Paused = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyUp(KeyCode.P)) { 
            Utils.Paused = !Utils.Paused; // Pause or Unpause based on current state.
            //then Update everything else that needs intervention but not the overhead of a monobehavior on every particle system etc.
            ParticleSystemPause(Utils.Paused);
            GameObject.Find("PausedIndicator").GetComponent<Text>().enabled = Utils.Paused;
        }
    }

    void ParticleSystemPause(bool pauseState) {
        ParticleSystem[] particleSystems = GameObject.FindObjectsOfType<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems){
            if (pauseState) {
                ps.Pause();
            } else { ps.Play(); }
        }
    }
}
