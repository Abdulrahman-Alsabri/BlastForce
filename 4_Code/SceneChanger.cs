using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    public Animator animator;
    Score score;
    private int sceneToLoad;
    private int timeToWait = 3;

    // Start is called before the first frame update
    void Start()
    {
        // searches for SceneChanger animator
        GameObject temp = GameObject.Find("SceneChanger");
        if (temp != null)
        {
            // gets Animator
            animator = temp.GetComponent<Animator>();
        }
        else
        {
            Debug.Log("Animator not found");
        }

        // searches for Score script
        GameObject temp1 = GameObject.Find("Score");
        if (temp1 != null)
        {
            // gets Animator
            score = temp1.GetComponent<Score>();
        }
        else
        {
            Debug.Log("Score not found");
        }

        Scene m_Scene = SceneManager.GetActiveScene();
        string sceneName = m_Scene.name;
        score.setCoins(sceneName);
    }

    public void FadeToNextScene()
    {
        FadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToScene(int sceneIndex)
    {
        sceneToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
        StartCoroutine(WaitAndLoad(timeToWait));
    }

    IEnumerator WaitAndLoad(int waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        SceneManager.LoadScene(sceneToLoad);
        animator.SetTrigger("FadeIn");
    }
}
