using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int cash = 0;
    public bool inShop = false;

    private void Start()
    {
        if(instance == null && instance != this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
