using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu, creditsMenu;
    public VideoPlayer videoPlayer;
    public AudioSource music;
    public void Retry() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void LoadGame()
    {
        music.volume = Mathf.Lerp(music.volume, 0, 1f);
        videoPlayer.gameObject.SetActive(true);
        mainMenu.SetActive(false);
        Invoke(nameof(GoToGame), 9f);       
    }
    public void BackToMain() => SceneManager.LoadScene("Main");

    public void OpenCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }
    public void CloseCredits()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }
    public void QuitGame() => Application.Quit();

    void GoToGame() => SceneManager.LoadScene("MainScene");
}
