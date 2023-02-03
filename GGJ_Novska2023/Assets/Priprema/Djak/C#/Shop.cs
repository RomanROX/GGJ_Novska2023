using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject shopUI;
    bool nearShop;

    int[] prices = new int[4];
    int[] upgradeLevels = new int[4];
    public Text[] priceTexts;
    public Text[] levelTexts;

    private void Start()
    {
        prices[0] = 300;
        prices[1] = 350;
        prices[2] = 400;
        prices[3] = 300;

        upgradeLevels[0] = 1;
        upgradeLevels[1] = 1;
        upgradeLevels[2] = 1;
        upgradeLevels[3] = 1;
    }

    private void Update()
    {
        if (nearShop && Input.GetKeyDown(KeyCode.E))
        {
            shopUI.SetActive(!shopUI.activeInHierarchy);
            GameManager.instance.inShop = !GameManager.instance.inShop;
        }
    }

    public void Upgrade(int index)
    {
        if (GameManager.instance.cash >= prices[index])
        {
            prices[index] = Mathf.RoundToInt(prices[index] * 1.5f);
            priceTexts[index].text = prices[index].ToString();
            upgradeLevels[index]++;
            levelTexts[index].text = "LEVEL " + upgradeLevels[index].ToString();
            switch (index)
            {
                case 0:
                    //TAKE CASH AND APPLY ITEM
                    break;
                case 1:
                    //TAKE CASH AND APPLY ITEM
                    break;
                case 2:
                    //TAKE CASH AND APPLY ITEM
                    break;
            }
        }
        else
        {
            Debug.LogWarning("NOT ENOUGH CASH");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) nearShop = true;
        else nearShop = false;
    }
}
