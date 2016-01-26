using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class NamingProcess: MonoBehaviour
{
	public string naming;

	void Awake()
	{
		name = naming;
	}
}
