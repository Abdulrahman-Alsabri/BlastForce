using UnityEngine;

public class PhaseTrigger : MonoBehaviour
{
    BigBoss bigBoss;
    GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        // searches for BigBoss
        GameObject temp = GameObject.Find("Big Boss");
        if (temp != null)
        {
            // gets score
            bigBoss = temp.GetComponent<BigBoss>();
        }
        else
        {
            Debug.Log("BigBoss not found");
        }

        // Search for Explosion
        explosion = Resources.Load("Explosion1") as GameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            // adjust boss's phase number
            bigBoss.phase += 1;

            // apply explosion and destroy both goon and explosion assets
            GameObject expEffect = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

            Destroy(expEffect, 2);
        }
    }
}
