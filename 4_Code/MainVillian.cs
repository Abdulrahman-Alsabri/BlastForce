using System.Collections;
using UnityEngine;

public class MainVillian : MonoBehaviour
{
    Score score;
    Collider hubEnterance;
    AudioSource levelMusic;
    AudioSource timerMusic;

    // Start is called before the first frame update
    void Start()
    {
        // searches for score
        GameObject temp = GameObject.Find("HubEnterance");
        if (temp != null)
        {
            // gets score
            hubEnterance = temp.GetComponent<Collider>();
            levelMusic = temp.GetComponent<AudioSource>();

            hubEnterance.enabled = true;
            hubEnterance.isTrigger = true;
        }
        else
        {
            Debug.Log("HubEnterance not found");
        }

        // searches for score
        GameObject temp2 = GameObject.Find("Score");
        if (temp2 != null)
        {
            // gets score
            score = temp2.GetComponent<Score>();
        }
        else
        {
            Debug.Log("Score not found");
        }

        timerMusic = gameObject.GetComponent<AudioSource>();

        StartCoroutine(WaitForVictory());
    }

    IEnumerator WaitForVictory()
    {
        levelMusic.Stop();
        yield return new WaitForSeconds(5);
        score.MainVillianTrigger();
        yield return new WaitForSeconds(5);
        score.EndTimerMessage();
        yield return new WaitForSeconds(5);
        score.TriggerTimer();
        timerMusic.Play();
    }
}
