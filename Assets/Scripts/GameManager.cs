﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using XboxCtrlrInput;
using XInputDotNetPure;

public static class Utils
{
	public static bool Paused = false;
	public static int livesSetting = 3;
	public static bool Multiplayer = false;
    public static Resolution resolutionSetting;
    public static bool fullscreenSetting;
    public static ScoreHandler cachedScoreHandler;
    public static ScreenBoundsHandler cachedScreenBoundsHandler;

	internal static Quaternion RotationToTarget(Transform self, Transform target)
	{
		Vector3 vectorToTarget = target.position - self.position;
		float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90);
		return Quaternion.AngleAxis(angle, Vector3.forward.normalized);
	}

    public enum GameState {
        MainMenu,
        Gameplay,
        PauseMenu,
        EndLevelMenu,
        GameOverMenu,
    }

	public static Transform AcquireTargetPlayer()
	{
		Transform target;
		if (!Utils.Multiplayer) {
			if (GameObject.FindWithTag("Player")) {
				GameObject prospectiveTarget = GameObject.Find("ShmupShip_P1");
				if (prospectiveTarget.activeSelf) {
					target = prospectiveTarget.gameObject.transform;
					return target;
				}
			}
		} else {
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length == 0)
				return null;
            GameObject prospectiveTarget = players [UnityEngine.Random.Range(0, players.Length)].gameObject;
            if (prospectiveTarget == null)
                return null;
            if (prospectiveTarget.activeSelf) {
				target = prospectiveTarget.gameObject.transform;
				return target;
			}
		}
		return null;
	}

}

[Serializable]
class SettingsData
{
	public int livesAtStart;
    public bool fullscreenSetting;
}
    

public class GameManager : MonoBehaviour
{
	public static GameManager GameManagerInstance;
	public static LifeHandler lifeManager;
    public bool fpsCounter;
    public FramesPerSecond fpsCount;
    private int current_score;
    public Utils.GameState gameState = Utils.GameState.MainMenu;

    void InitialiseMainMenu(){
        Debug.Log("Initialising main menu");
        GameObject.Find("NewGameButton").GetComponent<Button>().onClick.AddListener(delegate {GameManager.GameManagerInstance.NewGame();});
        GameObject.Find("2PlayerButton").GetComponent<Button>().onClick.AddListener(delegate {GameManager.GameManagerInstance.New2PlayerGame();});
    }

    void Start()
	{
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "mainmenu"){
            InitialiseMainMenu();  
            Load();
            Utils.resolutionSetting = Screen.currentResolution;
            SetGameResolution();
        }
		Utils.Paused = false;
        lifeManager = gameObject.GetComponent<LifeHandler>();
		if (GameManagerInstance != null) {
			GameObject.Destroy(gameObject);
		} else {
			GameObject.DontDestroyOnLoad(gameObject);
			GameManagerInstance = this;
            if (!lifeManager){
                gameObject.AddComponent<LifeHandler>();
                lifeManager = gameObject.GetComponent<LifeHandler>();
                SetUpLivesForNewGame();
            }
		}
        if (fpsCounter && !fpsCount) {
			fpsCount = gameObject.AddComponent<FramesPerSecond>();
		}
	}


    public int GetScoreAfterLevelTransition() {
      return current_score;
    }

  
    public void UpdateScoreForEndOfLevel(int score) {
        current_score = score;
    }


	public void Save()
	{
        Debug.Log("Save Settings called");
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/settings.dat");

		SettingsData data = new SettingsData();
		data.livesAtStart = Utils.livesSetting;
        data.fullscreenSetting = Utils.fullscreenSetting;
		bf.Serialize(file, data);
		file.Close();
	}

	public void Load()
	{
		if (File.Exists(Application.persistentDataPath + "/settings.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/settings.dat", FileMode.Open);
			SettingsData data = (SettingsData)bf.Deserialize(file);
			file.Close();
			Utils.livesSetting = data.livesAtStart;
            Utils.fullscreenSetting = data.fullscreenSetting;
		}
	}

    public void SetGameResolution(){
        Resolution currentRes = Utils.resolutionSetting;
        bool full = Utils.fullscreenSetting;
        Debug.Log("Setting New Game Resolution: " + currentRes.ToString());
        if (currentRes.ToString() != Screen.currentResolution.ToString()){
            foreach (Resolution res in Screen.resolutions){
                if (currentRes.ToString() == res.ToString()){
                    Screen.SetResolution(res.width, res.height, full);
                }
            }
        }
    }

    public void QuitToMainMenu(){
        if (!SceneManager.SceneManagerInstance.isLoading){
        SceneManager.SceneManagerInstance.StartCoroutine("LoadScene", "mainmenu");
        Debug.Log("Returning to Main menu");
            gameState = Utils.GameState.MainMenu;
        }
    }

	public void NewGame()
	{
        SceneManager.SceneManagerInstance.StartCoroutine("LoadScene", "Level1");
		Debug.Log("New Game Clicked");
        current_score = 0;
        gameState = Utils.GameState.Gameplay;
        SetUpLivesForNewGame();
    }

	public void New2PlayerGame()
	{
		Utils.Multiplayer = true;
		// TODO more code for setup of controls for multiplayer.
		NewGame();
	}
	
    void SetUpLivesForNewGame() {
        Debug.Log("Life Setup Called");
        lifeManager.livesleft = Utils.livesSetting;
        lifeManager.respawnDelay = 2;
    }

	// Update is called once per frame
	void Update()
	{
        switch(gameState)
        {
        case Utils.GameState.Gameplay:
        {
            if (InputManager.Pause_P1){
                TogglePauseState();
            }
            break;
        }
        case Utils.GameState.PauseMenu:
        {
            if (InputManager.Pause_P1){
                TogglePauseState();
            }
            break;
        }
        case Utils.GameState.EndLevelMenu:
        {
            if (GameObject.Find("EndLevelUICanvas") && GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled == false){
                GameObject.Find("EventSystem").GetComponent<EventSystem>().firstSelectedGameObject = GameObject.Find("EndLevelUICanvas").transform.FindChild("QuitButton").gameObject;
                GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = true;
            }
            break;
        }
        case Utils.GameState.GameOverMenu: 
        {
            if (GameObject.Find("GameOverPanel") && GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled == false){
                GameObject.Find("EventSystem").GetComponent<EventSystem>().firstSelectedGameObject = GameObject.Find("GameOverPanel").transform.GetComponentInChildren<QuitButtonSetter>().gameObject;
                GameObject.Find("GameOverPanel").transform.GetComponentInChildren<QuitButtonSetter>().enabled = true;
                GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = true;
            }
            break;
        }
        default:
            break;
        }

	}

    public void TogglePauseState()
    {
            Utils.Paused = !Utils.Paused;
            // Pause or Unpause based on current state.
            //then Update everything else that needs intervention but not the overhead of a monobehavior on every particle system etc.
            gameState = Utils.Paused ? Utils.GameState.PauseMenu : Utils.GameState.Gameplay;
            ParticleSystemPause(Utils.Paused);
            PauseScreenEffect(Utils.Paused);
            GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = Utils.Paused;
            if (GameObject.Find("PausedIndicator"))
            {
                GameObject.Find("PausedIndicator").GetComponent<Text>().enabled = Utils.Paused;
                GameObject.Find("PausedIndicator").transform.GetChild(0).gameObject.SetActive(Utils.Paused);
            }
    }

	void PauseScreenEffect(bool pauseState)
	{
		if (pauseState){
            Camera.main.GetComponent<Kino.AnalogGlitch>().colorDrift = 0.04f;
            Camera.main.GetComponent<Kino.AnalogGlitch>().scanLineJitter = 0.12f;
        }
        else{
			Camera.main.GetComponent<Kino.AnalogGlitch>().colorDrift = 0f;
            Camera.main.GetComponent<Kino.AnalogGlitch>().scanLineJitter = 0.0f;
        }
	}

	void ParticleSystemPause(bool pauseState)
	{
		ParticleSystem[] particleSystems = GameObject.FindObjectsOfType<ParticleSystem>();
		foreach (ParticleSystem ps in particleSystems) {
            if (ps.gameObject.transform.position.y > Utils.cachedScreenBoundsHandler.ScreenTop
                || ps.isStopped){
                break;
            }
            if (pauseState) {
				ps.Pause();
			} else {
				ps.Play();
			}
		}
	}
}

