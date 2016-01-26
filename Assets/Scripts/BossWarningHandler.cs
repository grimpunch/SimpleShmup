using UnityEngine;
using System.Collections;

public class BossWarningHandler : MonoBehaviour
{

	public GameObject bossWarn1;
	public GameObject bossWarn2;
	public string bossMusicTrackName;

	// Use this for initialization
	void Start()
	{
		bossWarn1 = GameObject.Find("BossWarningText1");
		bossWarn2 = GameObject.Find("BossWarningText2");
		bossWarn1.GetComponent<Animator>().enabled = false;
		bossWarn2.GetComponent<Animator>().enabled = false;
		this.enabled = false;
		GameObject.Find("MusicManager").GetComponent<MusicManager>().QueueTrack(bossMusicTrackName);
	}

	// Update is called once per frame
	void Update()
	{
		if (Utils.Paused) {
			bossWarn1.GetComponent<Animator>().enabled = false;
			bossWarn2.GetComponent<Animator>().enabled = false;
		} else {
			bossWarn1.GetComponent<Animator>().enabled = true;
			bossWarn2.GetComponent<Animator>().enabled = true;
		}
	}
}
