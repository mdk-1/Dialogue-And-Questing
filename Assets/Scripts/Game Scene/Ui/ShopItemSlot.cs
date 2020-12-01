using UnityEngine;
using UnityEngine.UI;

//script to handle shop item slots

public class ShopItemSlot : MonoBehaviour
{
    //reference to UI text element on canvas
    public Text textElement;
    //reference to item class
    private Item item;

    //method to display item cost, name and amount to text
    public void SetItem(Item newItem)
    {
        item = newItem;
        int cost = ShopUi.instance.GetShop().GetBuyCostOfItem(item);
        string text = $"[{cost}x1] {item.name} ({item.currentAmount})";
        textElement.text = text;
    }
    //method to return item
    public Item GetItem()
    {
        return item;
    }
    //method to reset item slot
    public void ClearSlot()
    {
        item = null;
        textElement.text = string.Empty;
    }
}
