using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {
	public static GameStateManager GameStateManagerInstance;
	
	// Use this for initialization
	void Start () {
		if (GameStateManagerInstance != null)
		{
			GameObject.Destroy(gameObject);
		}
		else
		{
			GameObject.DontDestroyOnLoad(gameObject);
			GameStateManagerInstance = this;
		}
	}

	public void NewGame(){
		SceneManager sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
		sceneManager.StartCoroutine("LoadScene","Level1");
		Debug.Log("New Game Clicked");
	}

	// Update is called once per frame
	void Update () {

	}
}
