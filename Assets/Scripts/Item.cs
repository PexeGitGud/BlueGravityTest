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
    }

    public void SetItemForSale(Shopkeeper shopkeeper)
    {
        itemButton.onClick.AddListener(delegate { shopkeeper.BuyItem(this); });
    }

    public void HidePriceTag()
    {
        priceSlot.SetActive(false);
    }
}