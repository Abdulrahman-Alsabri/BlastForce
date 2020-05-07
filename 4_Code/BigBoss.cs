using System.Collections;
using UnityEngine;

public class BigBoss : MonoBehaviour
{
    // declare all needed references
    GameObject explosionEnd;
    GameObject explosion;
    GameObject flyingGoon;
    GameObject mainVillian;
    Score score;

    // declare and initialize needed variables
    private Vector3 startPos;
    public float range = 15f;
    public float moveSpeed = 1.5f;
    public int phase = 1;
    private bool isAttacking = false;
    private bool isPhaseTwo = false;
    private bool isPhaseThree = false;
    private bool isPhaseFour = false;

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

        // Search for Explosion and Flying Goon
        explosionEnd = Resources.Load("Explosion0") as GameObject;
        explosion = Resources.Load("Explosion1") as GameObject;
        flyingGoon = Resources.Load("Flying Goon") as GameObject;
        mainVillian = Resources.Load("Main Villian") as GameObject;

        // get initial transform of goon
        startPos = transform.position;
    }

    void Update()
    {
        // handle goon movement
        Move();
    }

    private void Move()
    {
        if (!isAttacking)
        {
            // alternating function to determine movement
            float signFunction = Mathf.Sin(Time.time * moveSpeed);

            // move goon
            Vector3 v = startPos;
            v.x -= range * signFunction;
            transform.position = v;


            // rotate goon as it reaches end of range
            if (signFunction > 0.99)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                StartCoroutine(Attacking());
            }
            else if (signFunction < -0.99)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                StartCoroutine(Attacking());
            }
        }

        if (phase == 2)
        {
            // increase Boss's height
            Vector3 v = gameObject.transform.position;
            v.y = startPos.y + 3;
            transform.position = v;

            // Spawn a flyingGoon
            if (!isPhaseTwo)
            {
                Instantiate(flyingGoon, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 15f), gameObject.transform.rotation);
                isPhaseTwo = true;

                // Adjust Feedback
                score.BossDamaged(phase);
            }
        }
        else if (phase == 3)
        {
            // increase Boss's height
            Vector3 v = gameObject.transform.position;
            v.y = startPos.y + 6;
            transform.position = v;

            // Spawn a flyingGoon
            if (!isPhaseThree)
            {
                Instantiate(flyingGoon, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 15f), gameObject.transform.rotation);
                isPhaseThree = true;

                // Adjust Feedback
                score.BossDamaged(phase);
            }
        }
        else if (phase == 4)
        {
            // increase Boss's height
            Vector3 v = gameObject.transform.position;
            v.y = startPos.y + 6;
            transform.position = v;

            // Spawn a flyingGoon
            if (!isPhaseFour)
            {
                GameObject finalExplosion = Instantiate(explosionEnd, gameObject.transform.position, gameObject.transform.rotation);
                isPhaseFour = true;
                Destroy(finalExplosion, 2);

                // mainVillian
                GameObject villian = Instantiate(mainVillian, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5f, gameObject.transform.position.z + 10f), Quaternion.Euler(0f, 90f, 0f));

                Destroy(gameObject, 1);

                // Adjust Feedback
                score.BossDamaged(phase);
            }
        }
    }

    IEnumerator Attacking()
    {
        isAttacking = true;
        yield return new WaitForSeconds(4.5f);
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // adjust score and give player feedback
            score.playerDamaged("BigBoss");
        }

        // apply explosion and destroy both goon and explosion assets
        GameObject expEffect = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        Destroy(expEffect, 2);
    }
}
