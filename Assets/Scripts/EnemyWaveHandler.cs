using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyWaveHandler : MonoBehaviour {

	public string me;
	// Use this for initialization
	void Start () {
		me = Random.Range(0,10).ToString();
	}


	// Update is called once per frame
	void Update () {
		Debug.Log("I am still me: " + me);
	}
}
