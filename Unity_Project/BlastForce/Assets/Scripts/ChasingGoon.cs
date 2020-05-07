using System.Collections;
using UnityEngine;

public class ChasingGoon : MonoBehaviour
{
    // declare all needed references
    GameObject explosion;
    Score score;
    Transform player;
    Transform myTransform;
    Animator chasingAnimator;

    // declare and initialize needed variables
    private Vector3 startPos;
    public float range = 5f;
    public float moveSpeed = 1f;
    public float attackSpeed = 10f;
    public float attackRange = 15f;

    // initialize needed variables
    private float distance = 1f;
    private float stop = 1f;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        // searches for player object
        GameObject temp = GameObject.Find("Player");
        if (temp != null)
        {
            // gets player's transform info
            player = temp.GetComponent<Transform>();
        }
        else
        {
            Debug.Log("Player not found");
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

        // Search for Explosion
        explosion = Resources.Load("Explosion1") as GameObject;

        // searches for chasingAnimator
        chasingAnimator = gameObject.GetComponent<Animator>();

        // get my transform
        myTransform = gameObject.transform;

        // get initial transform of goon
        startPos = transform.position;
    }

    void Update()
    {
        // handle goon movement
        Move();

        // check for player existence
        CheckForPlayer();
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
            }
            else if (signFunction < -0.99)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
    }

    private void CheckForPlayer()
    {
        // get distance between player and flying goon
        distance = Vector3.Distance(myTransform.position, player.position);

        if (distance <= attackRange)
        {
            // look at player
            /*
             myTransform.rotation = Quaternion.Slerp(
                myTransform.rotation,
                Quaternion.LookRotation(player.position - myTransform.position), rotationSpeed * Time.deltaTime
                );
            */
            // look at player
            if (player.position.x > myTransform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }

            // wait 2 seconds for charging animation, then move towards player
            if (!isAttacking)
            {
                chasingAnimator.SetBool("isAttacking", true);
                StartCoroutine(Attacking());
            }
            else
            {
                // move towards the player
                if (distance > stop)
                {
                    myTransform.position += myTransform.forward * attackSpeed * Time.deltaTime;
                }
            }
        }
    }

    IEnumerator Attacking()
    {
        if (!isAttacking)
        {
            yield return new WaitForSeconds(2);
            isAttacking = true;
            chasingAnimator.SetBool("isAttacking", false);
            chasingAnimator.SetBool("isPatroling", true);
            attackRange = 40f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // adjust score and give player feedback
            score.playerDamaged("ChasingGoon");
        }

        // apply explosion and destroy both goon and explosion assets
        GameObject expEffect = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        Destroy(expEffect, 2);
    }
}
