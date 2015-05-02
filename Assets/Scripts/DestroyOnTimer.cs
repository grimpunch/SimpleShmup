﻿using UnityEngine;
using System.Collections;

public class DestroyOnTimer : MonoBehaviour {

    public float time = 3.0F;

    // Use this for initialization
    void Start() {
        Invoke("DestroyThis", time);
    }

    void DestroyThis() {
        Destroy(gameObject);
    }
}