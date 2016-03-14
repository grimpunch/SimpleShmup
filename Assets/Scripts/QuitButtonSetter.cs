using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class QuitButtonSetter : MonoBehaviour {
    void OnEnable () {
        gameObject.GetComponent<Button>().onClick.AddListener(GameManager.GameManagerInstance.QuitToMainMenu);
	}
}
