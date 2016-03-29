using UnityEngine;
using System.Collections;

public class FlashBullets : MonoBehaviour {
    public Material bulletMaterial;
 
    void Start() {
        
    }
    void Update() {
        float _FlashAmount = Mathf.PingPong(Time.time *6f, 0.8F);
        bulletMaterial.SetFloat("_FlashAmount", _FlashAmount);
    }
}