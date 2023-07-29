using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyWizard : MonoBehaviour
{
    [SerializeField]
    int goldAmount = 10;

    public void GiveMoney()
    {
        GameController.instance.GetPlayerInventory().AddGold(goldAmount);
    }
}
