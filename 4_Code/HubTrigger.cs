using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubTrigger : MonoBehaviour
{
    // reference scene changer script
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
        if (other.gameObject.tag == "Player")
        {
            // face out and wait
            changer.FadeToScene(10);
            StartCoroutine(WaitAndLoad(timeToWait));
        }
    }

    IEnumerator WaitAndLoad(int waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
    }
}
