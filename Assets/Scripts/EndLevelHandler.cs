using UnityEngine;
using System.Collections;

public class EndLevelHandler : MonoBehaviour
{


	// Use this for initialization
	void Start()
	{
		GameObject.Find("MusicManager").GetComponent<MusicManager>().FadeOutTrack();
		gameObject.SetActive(false);
	}

}
