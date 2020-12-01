using System.Linq;
using UnityEngine;

//script to handle inventory UI on canvas
//updates visuals

public class InventoryUi : MonoBehaviour
{
    //reference varaibles for UI elements
    public InventoryItemInspectPanel itemInspectPanel;
    public GameObject inventoryWindow;
    public Transform itemsParent;
    //reference varaibles for storage and itemslot classes
    Storage inventory;
    ItemSlot[] inventorySlots;

    //reference to item enum that can declated as nullable to filter
    //filter to be applied to UI dropdown to remove items from view
    private ItemType? filter = null;
    //reference to item
    private Item selectedItem;
    //reference this as a static instance, should only be single
    public static InventoryUi instance;

    //initialise instance of this
    void Awake()
    {
        instance = this;
    }
    //reference player inventory instance, suscribe to onStorageUpdate for visual updates
    //define the item slots and call updateUI method
    void Start()
    {
        inventory = Player.instance.inventory;
        inventory.OnStorageUpdate += UpdateUi;
        inventorySlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        UpdateUi();
    }

    //if player input of I key is pressed
    //set the inventory panel to active
    //if pressed again, deactivate
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryWindow.SetActive(!inventoryWindow.activeSelf);
        }
    }
    //method to populate item slots as inventory
    //calling get item from item class
    public void SelectItem(int index)
    {
        ItemSlot slot = inventorySlots[index];
        SelectItem(slot.GetItem());
    }
    //method to add inspection panel on a selected item
    //in inventory slot
    public void SelectItem(Item item)
    {
        selectedItem = item;
        itemInspectPanel.SetItem(selectedItem);
    }
    //method to update UI of inventory
    //add list of filtered items that do not have filter applied to array
    // for all items in list, call setitem to display or clear the slot to show empty
    //set player current gold amount
    public void UpdateUi()
    {
        Item[] filteredItems = inventory.items.Where(item => filter == null || item.type == filter).ToArray();

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < filteredItems.Length)
            {
                inventorySlots[i].SetItem(filteredItems[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }

        itemInspectPanel.SetGold(Player.instance.gold);
    }
    //method to handle item filtering
    //if filter is not emply, add item to filter
    //call updateUI method
    public void UpdateFilters(int itemTypeIndex)
    {
        if (itemTypeIndex == 0)
        {
            filter = null;
        }
        else
        {
            filter = (ItemType)itemTypeIndex;
        }

        UpdateUi();
    }
    //Method to handle move button
    //if item is selected and shop panel active, sell item
    //otherwise transfer item to storage
    public void MoveItemToActiveStorage()
    {
        if (selectedItem == null)
        {
            return;
        }
        
        if (ShopUi.instance.IsActive())
        {
            ShopUi.instance.GetShop().SellItem(selectedItem);
        }
        else
        {
            inventory.TransferItem(selectedItem, StorageUi.instance.GetStorage());
        }

        SelectItem(null);
    }
}
