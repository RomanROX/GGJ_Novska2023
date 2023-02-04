using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    bool isPaused = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !GameManager.instance.inShop)
        {
            if(isPaused)
            {
                isPaused = false;
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                isPaused = true;
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void Resume()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
