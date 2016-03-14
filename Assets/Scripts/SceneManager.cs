using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{

	public static SceneManager SceneManagerInstance;
	public GameObject loadingUI;
	private AsyncOperation async;
	private float timeToWaitAroundLoads = 2f;
	private float timeWaited;
    public bool isLoading = false;
	// Use this for initialization
	void Start()
	{
		if (SceneManagerInstance != null) {
			GameObject.Destroy(gameObject);
		} else {
			GameObject.DontDestroyOnLoad(gameObject);
			SceneManagerInstance = this;
		}
	}

	public IEnumerator LoadScene(string sceneName)
	{
		if (timeWaited > timeToWaitAroundLoads * 2) {
			timeWaited = 0f;
		}
		async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        isLoading = true;
		async.allowSceneActivation = false;

		yield return async;
	}

	public void ActivateScene()
	{
		async.allowSceneActivation = true;
        isLoading = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (!loadingUI) {
			loadingUI = GameObject.Find("LoadingScreen");
			GameObject.DontDestroyOnLoad(loadingUI);
		}
		if (async != null) {
			timeWaited += Time.deltaTime;
			if (async.progress >= 0.9f && timeWaited > timeToWaitAroundLoads) {
				timeWaited = 0f;
				ActivateScene();
			}
		}
		if (async == null)
			return;
		if (async.progress > 0) {
			if (loadingUI) {
				loadingUI.SetActive(true);
			} else {
				loadingUI = GameObject.Find("LoadingScreen");
			}

		} else {
			if (loadingUI) {
				loadingUI.SetActive(false);
			} else {
				loadingUI = GameObject.Find("LoadingScreen");
			}

		}
	}
}
