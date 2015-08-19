using UnityEngine;
using System.Collections;

public class PlayMusicOnLevelLoaded : MonoBehaviour {

    // Use this for initialization
    void Start() {
        if (Application.isPlaying) gameObject.GetComponent<AudioSource>().Play(); 
    }

    // Update is called once per frame
    void Update() {
        if (Application.isPlaying && Utils.Paused) gameObject.GetComponent<AudioSource>().Pause();
        if (Application.isPlaying && !Utils.Paused) gameObject.GetComponent<AudioSource>().UnPause();
    }
}
