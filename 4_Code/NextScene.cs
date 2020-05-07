using System.Collections;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    // reference SceneChanger script
    SceneChanger changer;

    // initialize time needed between switching scenes
    private int timeToWait = 3;

    // Start is called before the first frame update
    void Start()
    {
        // searches for SceneChanger
        GameObject temp = GameObject.Find("SceneChanger");
        if (temp != null)
        {
            // gets SceneChanger
            changer = temp.GetComponent<SceneChanger>();
        }
        else
        {
            Debug.Log("SceneChanger not found");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // fade to next scene if player interacts with door
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.name == "HubTrigger")
            {
                changer.FadeToScene(9);
                StartCoroutine(WaitAndLoad(timeToWait));
            }
            else
            {
                changer.FadeToNextScene();
                StartCoroutine(WaitAndLoad(timeToWait));
            }
        }
    }

    IEnumerator WaitAndLoad(int waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
    }
}