using System.Collections;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // reference to player object, explosion prefab, and PlayerMovement script
    public Transform player;
    public PlayerMovement playerMovement;
    GameObject explosion;

    void Start()
    {
        // Search for Explosion
        explosion = Resources.Load("Explosion") as GameObject;

        // searches for player object
        GameObject temp = GameObject.Find("Player");
        if (temp != null)
        {
            // gets player's transform info
            player = temp.GetComponent<Transform>();
            playerMovement = player.GetComponent<PlayerMovement>();
        }
        else
        {
            Debug.Log("Player not found");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // apply extra "jump" if rocket hits RocketJumpDetector
        if (other.gameObject.name == "RocketJumpDetector")
        {
            playerMovement.moveDirection.y = 30f;
        }

        // apply explosion effect
        GameObject expEffect = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

        // destroy rocket and effect
        Destroy(gameObject);
        Destroy(expEffect, 2);
    }
}
