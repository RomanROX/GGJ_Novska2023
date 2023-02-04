using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject shopUI;
    bool nearShop;

    int[] prices = new int[3];
    int[] upgradeLevels = new int[3];
    public Text[] priceTexts;
    public Text[] levelTexts;
    public Text coins;
    public Text noCoinsIndicator;

    private void Start()
    {
        prices[0] = 100;
        prices[1] = 100;
        prices[2] = 100;

        upgradeLevels[0] = 1;
        upgradeLevels[1] = 1;
        upgradeLevels[2] = 1;
    }

    private void Update()
    {
        if (nearShop && Input.GetKeyDown(KeyCode.E))
        {
            if(shopUI.activeInHierarchy)
            {
                shopUI.SetActive(false);
                GameManager.instance.inShop = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                
            }
            else
            {
                shopUI.SetActive(true);
                GameManager.instance.inShop = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                coins.text = GameManager.instance.cash.ToString();
            }
        }
    }

    public void Upgrade(int index)
    {
        if (GameManager.instance.cash >= prices[index])
        {
            switch (index)
            {
                case 0:
                    GameManager.instance.cash -= prices[index];
                    GameManager.instance.coinMultiplier *= 1.2f;
                    break;
                case 1:
                    GameManager.instance.cash -= prices[index];
                    GameManager.instance.damageMultiplier *= 1.2f;
                    break;
                case 2:
                    GameManager.instance.cash -= prices[index];
                    StartCoroutine(FindObjectOfType<MainPlant>().Regenerate());
                    break;
            }
            prices[index] = Mathf.RoundToInt(prices[index] * 1.5f);
            priceTexts[index].text = prices[index].ToString() + "$";
            upgradeLevels[index]++;
            levelTexts[index].text = "Lvl " + upgradeLevels[index].ToString();
        }
        else
        {
            noCoinsIndicator.gameObject.SetActive(true);
            Invoke(nameof(ResetNekaj), .5f);
        }
    }

    private void ResetNekaj()
    {
        noCoinsIndicator.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) nearShop = true;
        else nearShop = false;
    }
}