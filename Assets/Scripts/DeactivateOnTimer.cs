using UnityEngine;
using System.Collections;

public class DeactivateOnTimer : MonoBehaviour
{

    public float time = 3.0F;

    // Use this for initialization
    void Start()
    {
        Invoke("DeactivateThis", time);
    }

    void OnActivate()
    {
        if(gameObject.GetComponent<ParticleSystem>()) {
            gameObject.GetComponent<ParticleSystem>().Clear();
            gameObject.GetComponent<ParticleSystem>().Play();
        }
    }

    void Update()
    {
        if(this.IsInvoking() && Utils.Paused) {
            CancelInvoke("DeactivateThis");
        }
        if(!Utils.Paused && !this.IsInvoking()) {
            Invoke("DeactivateThis", time);
        }
    }

    void DeactivateThis()
    {
        gameObject.SetActive(false);
    }
}
