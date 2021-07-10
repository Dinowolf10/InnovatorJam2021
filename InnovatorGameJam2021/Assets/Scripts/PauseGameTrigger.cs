using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameTrigger : MonoBehaviour
{
    public GameObject tutorialLevelManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tutorialLevelManager.GetComponent<TutorialLevelManager>().PickTutorialToDisplay(this.gameObject.name);

            Destroy(this.gameObject);
        }
    }
}
