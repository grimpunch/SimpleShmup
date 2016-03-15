using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ContinueButtonSetter : MonoBehaviour , ISelectHandler{
    void OnEnable () {
        gameObject.GetComponent<Button>().onClick.AddListener(GameManager.GameManagerInstance.TogglePauseState);
    }
    public void OnSelect(BaseEventData baseData){
        if (GetComponent<AudioSource>() != null) {
            GetComponent<AudioSource>().Play();
        }
    }
}
