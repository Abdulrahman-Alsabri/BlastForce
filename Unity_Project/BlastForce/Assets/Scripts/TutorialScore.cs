using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScore : MonoBehaviour
{
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
    }

    private void OnTriggerEnter(Collider other)
    {
        // update score and status message when player finishes a tutorial room
        if (other.gameObject.tag == "Player")
        {
            Scene m_Scene = SceneManager.GetActiveScene();
            string sceneName = m_Scene.name;
            score.FinishedTutorial(sceneName);
        }
    }
}
