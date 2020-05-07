using UnityEngine;

public class DestructibleBlock : MonoBehaviour
{
    // reference PlayerMovement scriptl, and explosion prefab
    PlayerMovement player;
    GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        // Search for Explosion
        explosion = Resources.Load("Explosion1") as GameObject;
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
            }
        }
    }
}
