using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonSoundOnSelect : MonoBehaviour , ISelectHandler{
    public void OnSelect(BaseEventData baseData){
        if (GetComponent<AudioSource>() != null) {
            GetComponent<AudioSource>().Play();
        }
    }
}
