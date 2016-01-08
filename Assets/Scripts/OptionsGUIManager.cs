using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class OptionsGUIManager : MonoBehaviour
{
	
    public Text optionsLivesText;

    public void IncrementLives()
    {
        if(Utils.livesSetting < 99)
            Utils.livesSetting += 1;
    }

    public void DecrementLives()
    {
        if(Utils.livesSetting > 0)
            Utils.livesSetting -= 1;
    }

    public void Update()
    {
        optionsLivesText.text = Utils.livesSetting.ToString("D1") + ""; 
    }

}
