using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour
{

    private const int PLAYERLASERLAYER = 14;

    void OnTriggerEnter2D(Collider2D col2d)
    {
        if(col2d.gameObject.layer == PLAYERLASERLAYER) {
            gameObject.SetActive(false);
        }
    }
}
