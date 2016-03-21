using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class OptionsGUIManager : MonoBehaviour
{
	
    public Text optionsLivesText;
    public Dropdown resolutionDropdown;
    public Resolution currentScreenResolution;


    public void IncrementLives()
    {
        if(Utils.livesSetting < 99)
            Utils.livesSetting += 1;
            GameManager.GameManagerInstance.Save();
    }

    public void DecrementLives()
    {
        if(Utils.livesSetting > 0)
            Utils.livesSetting -= 1;
            GameManager.GameManagerInstance.Save();
    }

    public void Update()
    {
        optionsLivesText.text = Utils.livesSetting.ToString("D1") + ""; 
        resolutionDropdown.transform.FindChild("Label").gameObject.GetComponent<Text>().text = Screen.currentResolution.ToString();
    }

}
