using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusBehaviour : MonoBehaviour
{
    AudioSource musicSource;

    public void goToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void gotoGame()
    {
        StartCoroutine(WaitForSoundAndTransition("KingPin"));

       // musicSource = GetComponentInChildren<AudioSource>();
       // musicSource.Play();

        //SceneManager.LoadScene("KingPin");
    }

    private IEnumerator WaitForSoundAndTransition(string sceneName)
    {
        AudioSource audio = GetComponentInChildren<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
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
