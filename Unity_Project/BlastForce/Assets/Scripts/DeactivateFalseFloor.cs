using UnityEngine;

public class DeactivateFalseFloor : MonoBehaviour
{
    // reference FalseFloorTrigger script
    FalseFloorTrigger floorTrigger;

    // Start is called before the first frame update
    void Start()
    {
        // searches for FalseFloorTrigger
        GameObject temp2 = GameObject.Find("FalseFloor");
        if (temp2 != null)
        {
            // gets SceneChanger
            floorTrigger = temp2.GetComponent<FalseFloorTrigger>();
        }
        else
        {
            Debug.Log("FalseFloorTrigger not found");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // deactivate
        floorTrigger.DeactivateFalseFloor();
    }
}
