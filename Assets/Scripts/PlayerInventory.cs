using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInventory : Trader
{
    [Header("Player Equipment")]
    [SerializeField]
    Item equipedOutfit;
    GameObject equipedOutfitVisual;
    [SerializeField]
    Item equipedHair;
    GameObject equipedHairVisual;
    [SerializeField]
    Item equipedHat;
    GameObject equipedHatVisual;

    [Header("Player Equipment UI")]
    [SerializeField]
    Transform outfitEquipmentSlot;
    [SerializeField]
    Transform hairEquipmentSlot;
    [SerializeField]
    Transform hatEquipmentSlot;

    void OnInventory()
    {
        SwitchInventoryVisibility();
    }

    public void EquipItem(Item item)
    {
        if (item.isEquiped())
        {
            UnequipItem(item);
            return;
        }

        switch (item.GetItemType())
        {
            case Item.ItemType.Outfit:
                ChangeEquipment(ref equipedOutfit, item, ref equipedOutfitVisual, outfitEquipmentSlot);
                break;
            case Item.ItemType.Hair:
                ChangeEquipment(ref equipedHair, item, ref equipedHairVisual, hairEquipmentSlot);
                break;
            case Item.ItemType.Hat:
                ChangeEquipment(ref equipedHat, item, ref equipedHatVisual, hatEquipmentSlot);
                break;
            default:
                return;
        }

        items.Remove(item);
    }

    public void UnequipItem(Item item)
    {
        items.Add(item);
        RealignEquipmentTransfrom(item, itemGrid);
        item.Equip(false);

        switch (item.GetItemType())
        {
            case Item.ItemType.Outfit:
                equipedOutfit = null;
                Destroy(equipedOutfitVisual);
                break;
            case Item.ItemType.Hair:
                equipedHair = null;
                Destroy(equipedHairVisual);
                break;
            case Item.ItemType.Hat:
                equipedHat = null;
                Destroy(equipedHatVisual);
                break;
            default:
                return;
        }
    }

    void ChangeEquipment(ref Item oldEquipment, Item newEquipment, ref GameObject visual, Transform parent)
    {
        if (oldEquipment)
        {
            items.Add(oldEquipment);
            RealignEquipmentTransfrom(oldEquipment, itemGrid);
            Destroy(visual);
            oldEquipment.Equip(false);
        }
        oldEquipment = newEquipment;
        RealignEquipmentTransfrom(oldEquipment, parent);
        visual = Instantiate(newEquipment.GetItemVisuals(), transform);
        oldEquipment.Equip(true);
    }

    void RealignEquipmentTransfrom(Item equipment, Transform parent)
    {
        equipment.transform.SetParent(parent);

        RectTransform rect = equipment.GetComponent<RectTransform>();
        if (rect)
        {
            rect.anchorMax = new Vector2(.5f, .5f);
            rect.anchorMin = new Vector2(.5f, .5f);
            rect.anchoredPosition = new Vector2(.5f, .5f);
        }
    }
}