using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnEnemies : MonoBehaviour
{
    public GameObject enemy1;

    public GameObject enemy2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(enemy1.gameObject);
            Destroy(enemy2.gameObject);
        }
    }
}
