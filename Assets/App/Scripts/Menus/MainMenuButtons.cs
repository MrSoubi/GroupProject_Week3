using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] string levelToLoad;

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    public void PlayButton()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}