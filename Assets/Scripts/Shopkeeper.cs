public class Shopkeeper : Trader
{
    public override void SwitchInventoryVisibility()
    {
        base.SwitchInventoryVisibility();

        PlayerInventory pi = GameController.instance.GetPlayerInventory();
        pi?.SetInventoryVisibility(inventoryPanel.activeSelf);

        if (inventoryPanel.activeSelf)
            StartTrading(pi);
        else
            StopTrading();
    }

    public override void SetInventoryVisibility(bool visibility)
    {
        base.SetInventoryVisibility(visibility);

        PlayerInventory pi = GameController.instance.GetPlayerInventory();
        pi?.SetInventoryVisibility(inventoryPanel.activeSelf);

        if (inventoryPanel.activeSelf)
            StartTrading(pi);
        else
            StopTrading();
    }
}