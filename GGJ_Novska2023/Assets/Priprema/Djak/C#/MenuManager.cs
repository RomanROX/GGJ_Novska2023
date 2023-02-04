using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu, creditsMenu;
    public void Retry() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void LoadGame() => SceneManager.LoadScene("ScenaDjak");
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
}
