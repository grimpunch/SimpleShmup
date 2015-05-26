using UnityEngine;
using System.Collections;

public class PauseAnimation : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (!Utils.Paused) { gameObject.GetComponent<Animator>().StopPlayback(); } else { gameObject.GetComponent<Animator>().StartPlayback(); }
    }
}
