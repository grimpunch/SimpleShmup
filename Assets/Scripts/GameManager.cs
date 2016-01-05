using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class Utils {
    public static bool Paused = false;

    internal static Quaternion RotationToTarget(Transform self, Transform target) {
        Vector3 vectorToTarget = target.position - self.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90);
        return Quaternion.AngleAxis(angle, Vector3.forward.normalized);
    }
}


public class GameManager : MonoBehaviour {
	public static GameManager GameManagerInstance;
	public static SceneManager sceneManager;
    
    // Use this for initialization
    void Start() {
		sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
		if (GameManagerInstance != null)
		{
			GameObject.Destroy(gameObject);
		}
		else
		{
			GameObject.DontDestroyOnLoad(gameObject);
			GameManagerInstance = this;
		}
        Utils.Paused = false;
    }

	public void NewGame(){
		sceneManager.StartCoroutine("LoadScene","Level1");
		Debug.Log("New Game Clicked");
	}
	
	// Update is called once per frame
	void Update () {
		if (!sceneManager){
			GameObject.Find("SceneManager").GetComponent<SceneManager>();
		}
        if (Input.GetKeyUp(KeyCode.P)) { 
            Utils.Paused = !Utils.Paused; // Pause or Unpause based on current state.
            //then Update everything else that needs intervention but not the overhead of a monobehavior on every particle system etc.
            ParticleSystemPause(Utils.Paused);
            PauseScreenEffect(Utils.Paused);
            GameObject.Find("PausedIndicator").GetComponent<Text>().enabled = Utils.Paused;
        }
    }

    void PauseScreenEffect(bool pauseState) {
        if (pauseState) Camera.main.GetComponent<Kino.AnalogGlitch>().colorDrift = 0.125f;
        else Camera.main.GetComponent<Kino.AnalogGlitch>().colorDrift = 0f;
    }

    void ParticleSystemPause(bool pauseState) {
        ParticleSystem[] particleSystems = GameObject.FindObjectsOfType<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems){
            if (pauseState) {
                ps.Pause();
            } else { ps.Play(); }
        }
    }
}
