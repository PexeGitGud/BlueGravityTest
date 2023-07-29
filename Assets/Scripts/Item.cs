using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    Sprite itemSprite;
    [SerializeField]
    int price;
    public int GetPrice() => price;
    [SerializeField]
    bool equipable = false;
    public bool IsEquipable() => equipable;
    Trader owner;
    public Trader GetTrader() => owner;
    bool tradable = false;

    [Header("UI")]
    [SerializeField]
    Image itemImage;
    [SerializeField]
    TMP_Text priceText;
    [SerializeField]
    GameObject priceSlot;
    [SerializeField]
    Button itemButton;

    void Start()
    {
        itemImage.sprite = itemSprite;
        priceText.text = price.ToString()+"g";
        SetPriceTagVisibility();
    }

    public void SetItemForSale(Trader newTrader)
    {
        owner = newTrader;
        itemButton.onClick.AddListener(delegate { owner.SellItem(this); });
        tradable = true;
        SetPriceTagVisibility();
    }

    public void RemoveItemFromSale()
    {
        itemButton.onClick.RemoveAllListeners();
        tradable = false;
        SetPriceTagVisibility();
    }

    public void SetPriceTagVisibility()
    {
        priceSlot.SetActive(tradable);
    }
}