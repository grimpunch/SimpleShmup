using UnityEngine;
using System.Collections;

public class DestroyOnAnimationComplete : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
	
	}

	void DestroyThis()
	{
		Destroy(this.gameObject);
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
