using UnityEngine;

//script to handle shop, buying and selling, derive from storage class
//calculate buy and sell costs, transfer items to and from inventory instance

public class Shop : Storage
{
    //varaibles to hold buy and sell cost
    public float sellCost = 0.5f;
    public float buyCost = 1f;

    //method to return to value of an item
    //calculate sell cost
    public int GetSellCostOfItem(Item item)
    {
        return (int)(item.cost * sellCost);
    }

    //method to get value of item
    //calculate buy cost
    public int GetBuyCostOfItem(Item item)
    {
        return (int)(item.cost * buyCost);
    }

    //method to buy an item
    //check player has enough gold and transfer item to inventory
    //if item was transfered, update player gold and inventory visuals
    public bool BuyItem(Item item)
    {
        if (Player.instance.gold < GetBuyCostOfItem(item))
        {
            return false;
        }

        int cost = GetBuyCostOfItem(item);
        bool itemTransferred = TransferItem(item, Player.instance.inventory);

        if (itemTransferred)
        {
            Player.instance.ChangeGold(-cost);
            InventoryUi.instance.UpdateUi();
            return true;
        }

        return false;
    }

    //method to sell an item
    //call for sell cost and transfer item
    //if item was transfered, update player gold and inventory visuals
    public bool SellItem(Item item)
    {
        int cost = GetSellCostOfItem(item);

        bool itemTransferred = Player.instance.inventory.TransferItem(item, this);

        if (itemTransferred)
        {
            Player.instance.ChangeGold(cost);
            InventoryUi.instance.UpdateUi();
            return true;
        }

        return false;
    }
}
