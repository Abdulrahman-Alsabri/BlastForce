using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
  public void GamePlay () {
        // load first scene
        SceneManager.LoadScene("Pre_Tutorial");
  }

  public void Quit () {
        // quit from game
        Application.Quit();
  }
}
