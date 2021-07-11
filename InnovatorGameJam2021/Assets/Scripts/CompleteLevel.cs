using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    public AudioSource splashSound;

    [SerializeField]
    private string nextSceneToLoad;

    /// <summary>
    /// Loads the next level when the Player enters the pool
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && nextSceneToLoad != null)
        {
            collision.gameObject.SetActive(false);
            splashSound.PlayOneShot(splashSound.clip);
            StartCoroutine("NextLevelDelay");
        }
    }

    IEnumerator NextLevelDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextSceneToLoad);
    }
}
