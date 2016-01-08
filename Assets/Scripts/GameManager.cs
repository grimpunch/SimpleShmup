using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class Utils
{
    public static bool Paused = false;
    public static int livesSetting = 3;

    internal static Quaternion RotationToTarget(Transform self, Transform target)
    {
        Vector3 vectorToTarget = target.position - self.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90);
        return Quaternion.AngleAxis(angle, Vector3.forward.normalized);
    }
}

[Serializable]
class SettingsData
{
    public int livesAtStart;
}

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;
    public static SceneManager sceneManager;
    public FramesPerSecond fpsCount;
    // Use this for initialization
    void Start()
    {
        Load();
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();

        if(GameManagerInstance != null) {
            GameObject.Destroy(gameObject);
        } else {
            GameObject.DontDestroyOnLoad(gameObject);
            GameManagerInstance = this;
        }
        if(!fpsCount) {
            fpsCount = gameObject.AddComponent<FramesPerSecond>();
        }
        Utils.Paused = false;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/settings.dat");

        SettingsData data = new SettingsData();
        data.livesAtStart = Utils.livesSetting;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/settings.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/settings.dat", FileMode.Open);
            SettingsData data = (SettingsData)bf.Deserialize(file);
            file.Close();
            Utils.livesSetting = data.livesAtStart;
        }
    }

    public void NewGame()
    {
        sceneManager.StartCoroutine("LoadScene", "Level1");
        Debug.Log("New Game Clicked");
    }
	
    // Update is called once per frame
    void Update()
    {
        if(!sceneManager) {
            GameObject.Find("SceneManager").GetComponent<SceneManager>();
        }
        if(Input.GetKeyUp(KeyCode.P)) { 
            Utils.Paused = !Utils.Paused; // Pause or Unpause based on current state.
            //then Update everything else that needs intervention but not the overhead of a monobehavior on every particle system etc.
            ParticleSystemPause(Utils.Paused);
            PauseScreenEffect(Utils.Paused);
            GameObject.Find("PausedIndicator").GetComponent<Text>().enabled = Utils.Paused;
        }
    }

    void PauseScreenEffect(bool pauseState)
    {
        if(pauseState)
            Camera.main.GetComponent<Kino.AnalogGlitch>().colorDrift = 0.125f;
        else
            Camera.main.GetComponent<Kino.AnalogGlitch>().colorDrift = 0f;
    }

    void ParticleSystemPause(bool pauseState)
    {
        ParticleSystem[] particleSystems = GameObject.FindObjectsOfType<ParticleSystem>();
        foreach(ParticleSystem ps in particleSystems) {
            if(pauseState) {
                ps.Pause();
            } else {
                ps.Play();
            }
        }
    }
}

