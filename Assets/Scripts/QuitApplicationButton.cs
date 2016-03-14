using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuitApplicationButton : MonoBehaviour {

    void Start () {
        Text buttonText = gameObject.GetComponentInChildren<Text>();
        switch(Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                buttonText.text = "Quit to Windows";
                break;
            case RuntimePlatform.WindowsPlayer:
                buttonText.text = "Quit to Windows";
                break;
            case RuntimePlatform.OSXPlayer:
                buttonText.text = "Quit to OSX";
                break;
            case RuntimePlatform.LinuxPlayer:
                buttonText.text = "Quit to Linux";
                break;
            default:
                buttonText.text = "Quit";
                break;
        }
	}

    public void QuitGame() {
        Application.Quit();
    }
	
}
