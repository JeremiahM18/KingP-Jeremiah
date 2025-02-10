using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void gotoGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
}
