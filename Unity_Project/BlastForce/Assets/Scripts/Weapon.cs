using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    // declare firePoint and projectile references
    public Transform firePoint;
    public GameObject projectile;

    // needed variables for shooting
    public float damage = 10f;
    public float range = 100f;
    public float rocketSpeed = 3000f;

    // Start is called before the first frame update
    void Start()
    {
        // Search for Projectile "Rocket"
        projectile = Resources.Load("Rocket1") as GameObject;

        // searches for firePoint object
        GameObject temp2 = GameObject.Find("FirePoint");
        if (temp2 != null)
        {
            // gets firePoint's transform info
            firePoint = temp2.GetComponent<Transform>();
        }
        else
        {
            Debug.Log("FirePoint not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        // declare Screen and World vector3
        Vector3 VScreen = new Vector3();
        Vector3 VWorld;

        // initialize screen position
        VScreen.x = Mathf.Abs(Input.mousePosition.x - Screen.width);
        VScreen.y = Mathf.Abs(Input.mousePosition.y - Screen.height);
        VScreen.z = Camera.main.transform.position.z;

        // do not include z-axis in shooting
        VWorld = Camera.main.ScreenToWorldPoint(VScreen);
        VWorld.z = 0f;

        // keep shooting rays in direction of mouse (excluding z-axis)
        RaycastHit hit;
        Ray ray = new Ray(firePoint.transform.position, (VWorld - firePoint.transform.position));

        
        // These two lines are just for debugging purposes (They show ray direction)
        Debug.DrawRay(firePoint.transform.position, ray.direction * 25f, Color.red);
        if (Physics.Raycast(firePoint.transform.position, (VWorld - firePoint.transform.position), out hit, range))
        {
            Debug.DrawLine(hit.point, hit.point + Vector3.left * 25f, Color.green);
        }
        

        // shoot if mouse left button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            // initialize a clone of projectile to be fired
            GameObject rocket = Instantiate(projectile, firePoint.transform.position, Quaternion.LookRotation(ray.direction));
            rocket.GetComponent<Rigidbody>().AddForce(ray.direction * rocketSpeed);
            rocket.transform.Rotate(90, 0, 0);

            if (Physics.Raycast(firePoint.transform.position, (VWorld - firePoint.transform.position), out hit, range))
            {
                // Debug.Log(hit.transform.name);

                // apply damage to target
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.takeDamage(damage);
                }

            }

            // wait one second and destroy rocket
            yield return new WaitForSeconds(1);
            Destroy(rocket);
        }

    }
}
