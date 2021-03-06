﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class QuitButtonSetter : MonoBehaviour ,ISelectHandler{
    void OnEnable () {
        gameObject.GetComponent<Button>().onClick.AddListener(GameManager.GameManagerInstance.QuitToMainMenu);
	}
    public void OnSelect(BaseEventData baseData){
        if (GetComponent<AudioSource>() != null) {
            GetComponent<AudioSource>().Play();
        }
    }
}
