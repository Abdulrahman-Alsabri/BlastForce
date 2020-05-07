using System.Collections;
using UnityEngine;

public class BackToStart : MonoBehaviour
{
    Score score;
    AudioSource timerMusic;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Collider>().isTrigger = false;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        // searches for Main Villian
        GameObject temp = GameObject.Find("Main Villian(Clone)");
        if (temp != null)
        {
            // gets SceneChanger
            timerMusic = temp.GetComponent<AudioSource>();
        }
        else
        {
            Debug.Log("Main Villian not found");
        }

        if (other.gameObject.tag == "Player")
        {
            // stop music and display results
            timerMusic.Stop();
            score.StopTimerEffects();
            score.Winning();
            StartCoroutine(LoadHub());
        }
    }

    IEnumerator LoadHub()
    {
        yield return new WaitForSeconds(15);
    }
}
