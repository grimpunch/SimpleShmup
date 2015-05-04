using UnityEngine;
using System.Collections;

public class DelayColliderActivation : MonoBehaviour
{
	public float delay = 0.1f;

	// Use this for initialization
	void OnEnable()
	{
		Invoke ("ActivateCollider", delay);
	}
	
	// Update is called once per frame
	void ActivateCollider()
	{
		GetComponent<Collider>().enabled = true;
	}
}
