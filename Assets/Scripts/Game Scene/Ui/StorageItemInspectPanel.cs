using UnityEngine;
using UnityEngine.UI;

//script to handle the storage item inspection panel

public class StorageItemInspectPanel : MonoBehaviour
{
    //reference to UI canvas for item
    public Image icon;
    public Text itemNameElement;
    public Text itemDescriptionElement;
    private Item selectedItem;
    private StorageUi storageUi;

    void Start()
    {
        storageUi = StorageUi.instance;
    }

    //method to display item picture, name and text description
    public void SetItem(Item newItem)
    {
        selectedItem = newItem;

        icon.sprite = selectedItem?.icon;
        itemNameElement.text = selectedItem?.name;
        itemDescriptionElement.text = selectedItem?.description;
    }
    //method to transfer item to inventory
    public void TakeItem()
    {
        if (selectedItem == null)
        {
            return;
        }

        storageUi.GetStorage().TransferItem(selectedItem, Player.instance.inventory);
    }
}
