using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Outfit,
        Hair,
        Hat,
    }

    [Header("Stats")]
    [SerializeField]
    Sprite itemSprite;
    [SerializeField]
    int price;
    public int GetPrice() => price;

    [SerializeField]
    ItemType itemType;
    public ItemType GetItemType() => itemType;

    [SerializeField]
    GameObject itemVisuals;
    public GameObject GetItemVisuals() => itemVisuals;
    [SerializeField]
    Animator animator;
    public Animator GetAnimator() => animator;

    Trader owner;
    public Trader GetTrader() => owner;

    bool tradable = false;
    bool equiped = false;
    public bool isEquiped() => equiped;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="equip">Equiping or Unequiping?</param>
    public void Equip(bool equip)
    {
        equiped = equip;
        if (!equip && owner.isTrading()) //It's tradable only if i'm unequiping AND the owner is currently trading.
            SetItemForSale(owner);
    }

    public void SetItemForSale(Trader newTrader)
    {
        itemButton.onClick.RemoveAllListeners();
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

        PlayerInventory pi = owner?.GetComponent<PlayerInventory>();
        if (pi)
            itemButton.onClick.AddListener(delegate { pi.EquipItem(this); });
    }

    public void SetPriceTagVisibility()
    {
        priceSlot.SetActive(tradable);
    }
}