using UnityEngine;
using UnityEngine.UI;

//script to define consumable slots
public class ConsumablesSlot : MonoBehaviour
{
    //references to stackable item UI
    public Image icon;
    public Text stackCounter;
    Item item;

    //method to set item
    //define item icon and enable
    //check if item is stackable, pass amount through as string to text UI
    //set stackcounter active
    public void SetItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;

        if (item.Stackable())
        {
            stackCounter.text = item.currentAmount.ToString();
            stackCounter.gameObject.SetActive(true);
        }
    }

    //method to clear slot of stackable item
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        stackCounter.gameObject.SetActive(false);
    }
    //method to use item
    //calling use item from item class
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
