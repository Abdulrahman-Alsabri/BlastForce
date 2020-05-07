using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    // reference score script, with explosion asset
    GameObject explosion;
    Score score;

    // Start is called before the first frame update
    void Start()
    {
        // Search for Explosion
        explosion = Resources.Load("Explosion1") as GameObject;

        // searches for score
        GameObject temp = GameObject.Find("Score");
        if (temp != null)
        {
            // gets score
            score = temp.GetComponent<Score>();
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
            // apply explosion and destroy both block and explosion assets
            GameObject expEffect = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

            // Destroy(gameObject);
            Destroy(expEffect, 2);
            score.playerDamaged("BigBoss");
        }
    }
}
