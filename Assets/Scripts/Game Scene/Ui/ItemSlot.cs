using System;
using UnityEngine;
using UnityEngine.UI;

//script to visuals for item slots across UI canvas
//item slots refer to slots within inventory & storage

public class ItemSlot : MonoBehaviour
{
    //references to UI
    public Text textElement;
    //reference to item class
    private Item item;

    //method to set item
    //delcare item and cost, display sell cost of item in text
    //check if equipable item and display as green text
    //check if consumable and on cooldown, set as red text
    //if not either of the above set default colour as black
    public void SetItem(Item newItem)
    {
        item = newItem;
        int cost = ShopUi.instance.GetShop().GetSellCostOfItem(item);
        string text = $"[{cost}x1] {item.name} ({item.currentAmount})";

        if (item is Equipable && (item as Equipable).IsEquiped())
        {
            textElement.color = Color.green;
        }
        else if (item is Consumable && (item as Consumable).IsReadyToUse() == false)
        {
            textElement.color = Color.red;
        }
        else
        {
            textElement.color = Color.black;
        }
        textElement.text = text;
    }
    //method to get item
    public Item GetItem()
    {
        return item;
    }
    //method to reset a slot
    public void ClearSlot()
    {
        item = null;
        textElement.text = string.Empty;
    }
}
