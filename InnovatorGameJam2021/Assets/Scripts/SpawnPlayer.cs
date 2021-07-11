using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject explosion;
    public Transform spawnPoint;
    public AudioSource hotHotHotSound;
    public AudioSource expolsionSound;

    public AudioSource fireLoopSound;

    private bool isPlayerDead;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = spawnPoint.transform.position;
        hotHotHotSound.PlayOneShot(hotHotHotSound.clip);
        explosion.SetActive(false);
    }

    private void Update()
    {
       // If player is destroyed, call Respawn method
       if (player == null && !isPlayerDead)
        {
            isPlayerDead = true;
            explosion.SetActive(true);
            fireLoopSound.Stop();
            expolsionSound.PlayOneShot(expolsionSound.clip);
            Respawn();
        }

        if (!isPlayerDead)
        {
            explosion.transform.position = player.transform.position;

            if (!player.gameObject.active)
            {
                fireLoopSound.Stop();
            }
        }
    }

    /// <summary>
    /// Invokes RespawnCouroutine
    /// </summary>
    public void Respawn()
    {
        StartCoroutine("RespawnCoroutine");
    }

    /// <summary>
    /// Gives a 1 second delay before respawning the player
    /// </summary>
    /// <returns></returns>
    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
