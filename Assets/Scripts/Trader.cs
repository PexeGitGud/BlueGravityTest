using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Trader : MonoBehaviour 
{
    [Header("Trader Stats")]
    [SerializeField]
    List<Item> items;
    [SerializeField]
    protected int gold = 0;
    public int GetGold() => gold;
    Trader otherTrader;

    [Header("Trader UI")]
    [SerializeField]
    protected GameObject inventoryPanel;
    [SerializeField]
    Transform itemGrid;
    [SerializeField]
    TMP_Text goldText;

    [SerializeField]
    GameObject changeGoldVfx;

    void Start()
    {
        UpdateGold();

        if (items == null)
            items = new List<Item>();

        for (int i = 0; i < items.Count; i++)
        {
            items[i] = Instantiate(items[i], itemGrid);
        }
    }

    protected void UpdateGold() => goldText.text = gold.ToString() + "g";

    public virtual void SwitchInventoryVisibility()
    {
        inventoryPanel?.SetActive(!inventoryPanel.activeSelf);
    }

    public virtual void SetInventoryVisibility(bool visibility)
    {
        inventoryPanel?.SetActive(visibility);
    }

    public void StartTrading(Trader trader)
    {
        otherTrader = trader;
        otherTrader.SetOtherTrader(this);
        ChangeItemsTradability();
    }

    public void SetOtherTrader(Trader trader) 
    { 
        otherTrader = trader;
        ChangeItemsTradability();
    }

    public void StopTrading()
    {
        if (!otherTrader)
            return;

        otherTrader.SetOtherTrader(null);
        otherTrader = null;
    }

    void ChangeItemsTradability()
    {
        foreach (Item i in items)
        {
            i.RemoveItemFromSale();
            
            if (otherTrader)
                i.SetItemForSale(this);
        }
    }

    public bool BuyItem(Item item)
    {
        int itemPrice = item.GetPrice();
        if (itemPrice > gold)
            return false;

        gold -= itemPrice;
        UpdateGold();

        item.RemoveItemFromSale();
        item.transform.SetParent(itemGrid);
        items.Add(item);
        item.SetItemForSale(this);

        //if (changeGoldVfx)
        //        Instantiate(changeGoldVfx, GameController.instance.GetPlayerController().transform);

         return true;
    }

    public void SellItem(Item item)
    {
        if (!otherTrader)
            return;

        if (!otherTrader.BuyItem(item))
        {
            Debug.Log(otherTrader.name+" has Insuficient funds for " + item.name);
            return;
        }

        Debug.Log(otherTrader.name + " Bought Item " + item.name);

        gold += item.GetPrice();
        UpdateGold();

        items.Remove(item);
    }
}