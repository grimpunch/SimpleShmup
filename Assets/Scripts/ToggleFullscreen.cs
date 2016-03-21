using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleFullscreen : MonoBehaviour {

    void Start(){
        this.gameObject.GetComponent<Toggle>().isOn = Utils.fullscreenSetting;
    }

    public void Toggle(){
        Utils.fullscreenSetting = !Utils.fullscreenSetting; 
    }
	
}
