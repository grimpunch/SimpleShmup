using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{

	public Dictionary<string,AudioSource> audioSources;
	private AudioSource currentAudioSource;
	private AudioSource nextAudioSource;
	private float currentAudioSourceVolume;
	private float nextAudioSourceVolume;
	private bool crossfading;
	public string initialTrack;
	public float pausedVolume = 0.3f;
	public float musicVolume = 0.75f;

	// Use this for initialization
	void Awake()
	{
		audioSources = new Dictionary<string,AudioSource>();
		foreach (AudioSource a in (AudioSource[]) gameObject.GetComponentsInChildren<AudioSource>()) {
			audioSources.Add(a.gameObject.name, a);
			a.volume = 0f;
			a.Play();
		}

	}

	void Start()
	{
		QueueTrack(initialTrack);
	}

	public void QueueTrack(string track)
	{
		if (audioSources.Count > 0) {
			audioSources.TryGetValue(track, out nextAudioSource);
		}
		if (currentAudioSource != null) {
			StartCoroutine(CrossFade(currentAudioSource, nextAudioSource));
		} else {
			StartCoroutine(CrossFade(null, nextAudioSource));
		}
	}

	public void FadeOutTrack()
	{
		crossfading = true;

		StartCoroutine(FadeOut());
	}

	IEnumerator FadeOut()
	{
		while (currentAudioSourceVolume > 0f) {
			currentAudioSourceVolume -= Time.deltaTime * 0.25f;
			currentAudioSource.volume = currentAudioSourceVolume;
			yield return 0;
		}
		currentAudioSource.volume = currentAudioSourceVolume;
		yield break;
	}

	IEnumerator CrossFade(AudioSource currentSource, AudioSource nextSource)
	{
		if (currentAudioSource == null) {
			if (currentAudioSource == null && !crossfading) {
				nextAudioSourceVolume = 0f;
				nextSource.volume = nextAudioSourceVolume;
				nextSource.Play();			
			}
			crossfading = true;
			while (nextSource.volume < musicVolume) {
				nextAudioSourceVolume += Time.deltaTime * 0.25f;
				nextSource.volume = nextAudioSourceVolume;
				yield return 0;
			}
			nextSource.volume = musicVolume;
			currentAudioSource = nextSource;
			nextAudioSource = null;
			crossfading = false;
			currentAudioSourceVolume = musicVolume;
			nextAudioSourceVolume = 0f;
			yield break;
		}
		if (currentAudioSource != null) {
			if (!crossfading) {
				Debug.Log("Crossfading from:" + currentSource.name + " to " + nextSource.name);
				nextAudioSourceVolume = 0f;
				nextSource.volume = nextAudioSourceVolume;
				currentAudioSourceVolume = currentSource.volume;
				nextSource.Play();
				crossfading = true;
			}

			while (nextSource.volume < musicVolume) {
				Debug.Log("Crossfading progressing:" + currentSource.volume + " and " + nextSource.volume);
				nextAudioSourceVolume += Time.deltaTime * 0.25f;
				nextSource.volume = nextAudioSourceVolume;
				currentAudioSourceVolume -= Time.deltaTime;
				currentAudioSource.volume = currentAudioSourceVolume;
				yield return 0;
			}
			currentAudioSource.volume = 0f;
			currentAudioSource = nextSource;
			Debug.Log("Crossfade complete, current audio source is :" + currentAudioSource.name);
			nextAudioSource = null;
			nextAudioSourceVolume = 0f;
			currentAudioSource.volume = musicVolume;
			currentAudioSourceVolume = musicVolume;
			crossfading = false;
			yield break;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (!crossfading && Utils.Paused) {
			if (currentAudioSource) {
				currentAudioSource.volume = pausedVolume;
			}
		} else {
			if (!crossfading && currentAudioSource != null)
				currentAudioSource.volume = musicVolume;
		}
	}
}
