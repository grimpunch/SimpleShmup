using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PopulateResolutionSettings : MonoBehaviour {

    private Dropdown resDropdown;
    public List<Resolution> resolutionsList;

    void Start() {
        PopulateResMenu();
    }

    public void PopulateResMenu(){
        resDropdown = this.GetComponent<Dropdown>();
        resolutionsList = new List<Resolution>();
        resDropdown.ClearOptions();
        List <Dropdown.OptionData> droptions = new List<Dropdown.OptionData>();
        Resolution[] resolutions = Screen.resolutions; //all resolution
        foreach (Resolution res in resolutions) { //see everyone resolution in array
            resolutionsList.Add(res); //add resolution in list
            string resStr = res.ToString(); //string format every resolution
            droptions.Add(new Dropdown.OptionData() {text=resStr});
        }
        resDropdown.AddOptions(droptions);


    }

    public void UpdateResolutionChosen(){
        
        foreach (Resolution res in Screen.resolutions){
            if (resDropdown.options[resDropdown.value].text == res.ToString()){
                Utils.resolutionSetting = res;
                Debug.Log("Updating New Game Resolution" + res.ToString());
                break;
            }
        }
        resDropdown.captionText.text = Utils.resolutionSetting.ToString();
        resDropdown.RefreshShownValue();
        GameManager.GameManagerInstance.SetGameResolution();
    }

	// Update is called once per frame
	void Update () {
        if (resDropdown.GetComponentInChildren<Canvas>())
        {
            if (resDropdown.GetComponentInChildren<Canvas>().sortingLayerName != "CanvasUI")
                resDropdown.GetComponentInChildren<Canvas>().sortingLayerName = "CanvasUI";
        }
	}
}
