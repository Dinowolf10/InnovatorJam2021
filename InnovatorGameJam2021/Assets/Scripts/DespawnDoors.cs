using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnDoors: MonoBehaviour
{
    public GameObject doors;

    public GameObject bridge;

    private void Start()
    {
        bridge.SetActive(true);
    }

    /// <summary>
    /// Despawns the doors blocking the player's path and allows the
    /// special block to be stood on
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(doors.gameObject);
            bridge.SetActive(false);
        }
    }
}
