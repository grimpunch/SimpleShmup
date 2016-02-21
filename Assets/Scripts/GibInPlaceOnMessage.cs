using UnityEngine;
using System.Collections;

public class GibInPlaceOnMessage : MonoBehaviour
{
	public GameObject inplaceReplacement;

	public void Gib()
	{
		if (!gameObject)
			return;
		inplaceReplacement.SetActive(true);
		gameObject.SetActive(false);
	}
}
