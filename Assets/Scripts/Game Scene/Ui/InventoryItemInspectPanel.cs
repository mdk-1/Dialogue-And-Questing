using UnityEngine;
using UnityEngine.UI;

//script to handle the inventory inspection panel

public class InventoryItemInspectPanel : MonoBehaviour
{
    //reference to UI elements on canvas
    public Image icon;
    public Text itemNameElement;
    public Text itemDescriptionElement;
    public Text goldElement;
    //reference to item class
    private Item selectedItem;

    //method to set player gold amount to text
    public void SetGold(int gold)
    {
        goldElement.text = $"GOLD: {gold}";
    }

    //method to set item to picture, name and text
    public void SetItem(Item newItem)
    {
        selectedItem = newItem;

        icon.sprite = selectedItem?.icon;
        itemNameElement.text = selectedItem?.name;
        itemDescriptionElement.text = selectedItem?.description;
    }

    //method to handle to use button
    //if an item is selected, call use item
    //if no items remain, pass through empty to setitem
    public void UseItem()
    {
        if (selectedItem != null)
        {
            selectedItem.Use();

            if (selectedItem.currentAmount <= 0)
            {
                SetItem(null);
            }
        }
    }

    //method to handle the discard button
    //if an item is selected
    //call drop method and pass through nothing to setitem
    public void DropItem()
    {
        if (selectedItem != null)
        {
            selectedItem.Drop();
            SetItem(null);
        }
    }
}
