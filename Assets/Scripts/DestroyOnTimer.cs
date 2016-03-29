using UnityEngine;
using System.Collections;

public class DestroyOnTimer : MonoBehaviour
{

    public float time = 3.0F;

    // Use this for initialization
    void Start()
    {
        time = Random.Range(1f,8f);
        Invoke("DestroyThis", time);
    }

    void Update()
    {
        if(this.IsInvoking() && Utils.Paused) {
            CancelInvoke("DestroyThis");
        }
        if(!Utils.Paused) {
            Invoke("DestroyThis", time);
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
