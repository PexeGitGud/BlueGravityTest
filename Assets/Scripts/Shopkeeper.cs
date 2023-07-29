using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Shopkeeper : MonoBehaviour
{
    [SerializeField]
    List<Item> itemsForSale;

    [SerializeField]
    GameObject shopPanel;
    [SerializeField]
    Transform shopGrid;

    void Start()
    {
        for (int i = 0; i < itemsForSale.Count; i++)
        {
            itemsForSale[i] = Instantiate(itemsForSale[i], shopGrid);
            itemsForSale[i].SetItemForSale(this);
        }
    }

    public void SwitchShopPanelVisibility()
    {
        shopPanel?.SetActive(!shopPanel.activeSelf);
    }

    public void SetShopPanelVisibility(bool visibility)
    {
        shopPanel?.SetActive(visibility);
    }

    public void BuyItem(Item item)
    {
        
        if (!GameController.instance.GetPlayerInventory().BuyItem(item))
        {
            Debug.Log("Insuficient funds for " + item.name);
            return;
        }

        if (!itemsForSale.Remove(item))
            return;//error

        Debug.Log("Bought Item " + item.name);
        Destroy(item.gameObject);
    }
}