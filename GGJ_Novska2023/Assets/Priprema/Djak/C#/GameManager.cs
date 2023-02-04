using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public int cash;
    [HideInInspector] public float coinMultiplier = 1;
    [HideInInspector] public float damageMultiplier = 1; 
    [HideInInspector] public bool inShop = false;
    public Text coinsText;
    public GameObject gameOverUI;

    private void Start()
    {
        instance = this;
        Time.timeScale = 1f;
        cash = 0;
    }

    private void Update()
    {
        coinsText.text = cash.ToString();
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
