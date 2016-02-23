﻿using UnityEngine;
using System.Collections;

public class PauseAnimation : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	void OnDisable()
	{
		Destroy(this.gameObject);
	}


	// Update is called once per frame
	void Update()
	{
		gameObject.GetComponent<Animator>().enabled = !Utils.Paused;
	}
}
