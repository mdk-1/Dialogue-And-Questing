using UnityEngine;
using UnityEngine.UI;

//script to handle to shop inspection panel on UI canvas

public class ShopItemInspectPanel : MonoBehaviour
{
    //reference to UI elements
    public Image icon;
    public Text itemNameElement;
    public Text itemDescriptionElement;

    //reference to Item and StorageUI classes
    private Item selectedItem;
    private StorageUi storageUi;

    //reference storage UI instance
    void Start()
    {
        storageUi = StorageUi.instance;
    }
    //method to set item to picture, name and text
    public void SetItem(Item newItem)
    {
        selectedItem = newItem;

        icon.sprite = selectedItem?.icon;
        itemNameElement.text = selectedItem?.name;
        itemDescriptionElement.text = selectedItem?.description;
    }
    //method to handle buy item button
    //call TransferItem method
    public void BuyItem()
    {
        if (selectedItem == null)
        {
            return;
        }

        storageUi.GetStorage().TransferItem(selectedItem, Player.instance.inventory);
    }
}
