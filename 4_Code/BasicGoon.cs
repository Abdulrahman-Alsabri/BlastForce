using UnityEngine;

public class BasicGoon : MonoBehaviour
{
    // reference player and score scripts, with explosion asset
    PlayerMovement player;
    GameObject explosion;
    Score score;

    // declare and initialize needed variables
    private Vector3 startPos;
    public float range = 5f;
    public float moveSpeed = 0.6f;

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

        // get initial transform of goon
        startPos = transform.position;
    }

    void Update()
    {
        // handle goon movement
        if (gameObject.layer == 15)
            Move();
    }

    private void Move()
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
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (signFunction < -0.99)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // check if player is dashing
            player = other.gameObject.GetComponent<PlayerMovement>();

            if (player.isDashing)
            {
                // apply explosion and destroy both block and explosion assets
                GameObject expEffect = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
                Destroy(expEffect, 2);
                score.addToKills("BasicGoon");
            }
        }
    }
}
