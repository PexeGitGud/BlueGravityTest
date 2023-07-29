using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    int gold = 10;
    public int GetGold() => gold;

    [SerializeField]
    List<Item> items;

    [Header("UI")]
    [SerializeField]
    Transform inventoryGrid;
    [SerializeField]
    TMP_Text goldText;

    [SerializeField]
    GameObject changeGoldVfx;

    void Start()
    {
        items = new List<Item>();
        UpdateGold();
    }

    void UpdateGold()
    {
        goldText.text = gold.ToString() + "g";
    }

    public bool BuyItem(Item item)
    {
        int itemPrice = item.GetPrice();
        if (itemPrice > gold)
            return false;

        gold -= itemPrice;
        UpdateGold();

        Item newItem = Instantiate(item.gameObject, inventoryGrid).GetComponent<Item>();
        items.Add(newItem);
        newItem.HidePriceTag();

        if (changeGoldVfx)
            Instantiate(changeGoldVfx, GameController.instance.GetPlayerController().transform);

        return true;
    }
}