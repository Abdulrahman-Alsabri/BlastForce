using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedCoins : MonoBehaviour
{
    Score score;

    // Start is called before the first frame update
    void Start()
    {
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
        if (other.gameObject.tag == "Player")
        {
            score.addToCoins();
            Destroy(gameObject);
        }
    }
}
