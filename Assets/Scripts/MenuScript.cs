using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusBehaviour : MonoBehaviour
{

    public void goToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void gotoGame()
    {
        SceneManager.LoadScene("KingPin");
    }

    //public void restartGameScene()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}
    public void restartGameScene()
    {
        SceneManager.LoadScene("KingPin");
    }

    public void gotoCharacterSelectMenu()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
}
