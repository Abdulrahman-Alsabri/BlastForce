using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    // health of target
    public float health = 10f;

    // reference score script
    Score score;

    // Start is called before the first frame update
    void Start()
    {
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

        // set health depending upon target type
        if (gameObject.tag == "ShootingBlock")
        {
            health = 10f;
        }
        else if (gameObject.tag == "BasicGoon")
        {
            health = 10f;
        }
        else if (gameObject.tag == "FrontSpikeGoon")
        {
            health = 10f;
        }
        else if (gameObject.tag == "FlyingGoon")
        {
            health = 10f;
        }
        else if (gameObject.tag == "ChasingGoon")
        {
            health = 10f;
        }
        else if (gameObject.tag == "LevelOneBoss")
        {
            health = 10f;
        }
    }

    public void takeDamage (float amount)
    {
        // take damage from health until target is dead
        health -= amount;
        if (health <= 0f)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        // send target's tag to calculate appropriate score
        score.addToKills(gameObject.tag);

        // wait 1 second
        yield return new WaitForSeconds(0.4f);

        // destroy target
        Destroy(gameObject);
    }
}
