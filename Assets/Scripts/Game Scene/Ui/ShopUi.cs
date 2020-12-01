using UnityEngine;

//script to handle the shop UI on canvas
public class ShopUi : MonoBehaviour
{
    //reference to shop UI elements on canvas
    public ShopItemInspectPanel itemInspectPanel;
    public GameObject storageWindow;
    public Transform itemsParent;

    //reference to shop item slots as shop inventory
    ShopItemSlot[] inventorySlots;
    //reference to item class
    Item selectedItem;
    private Shop shop = new Shop();
    private bool active;
    public static ShopUi instance;

    ////create instance and declare shop slots
    private void Awake()
    {
        instance = this;
        inventorySlots = itemsParent.GetComponentsInChildren<ShopItemSlot>();
    }
    //initialise SetShop
    private void Start()
    {
        SetShop(new Shop());
        active = storageWindow.activeSelf;
    }
    //if player input keypress O
    //set shop panel active, if pressed again disable
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            storageWindow.SetActive(!storageWindow.activeSelf);
            active = storageWindow.activeSelf;
        }
    }

    //method to delcare the item slots in shop
    public void SelectItem(int index)
    {
        ShopItemSlot slot = inventorySlots[index];
        SelectItem(slot.GetItem());
    }
    //method to delcare the item descrption in shop
    public void SelectItem(Item item)
    {
        selectedItem = item;
        itemInspectPanel.SetItem(selectedItem);
    }
    //method to buy item
    public void BuyItem()
    {
        if (selectedItem == null)
        {
            return;
        }

        shop.BuyItem(selectedItem);
        SelectItem(null);
    }
    //method to return if active
    public bool IsActive()
    {
        return active;
    }
    //method to subscribe shop to OnStorageUpdate event
    //error checking as workaround to getting updateUI function suscribed
    public void SetShop(Shop newStorage)
    {
        try
        {
            shop.OnStorageUpdate -= UpdateUi;
        }
        catch { }

        newStorage.OnStorageUpdate += UpdateUi;
        shop = newStorage;

        UpdateUi();
    }
    //method to reference the shop
    public Shop GetShop()
    {
        return shop;
    }
    //method to update UI of shop
    // for all items in list, call setitem to display or clear the slot to show empty
    private void UpdateUi()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < shop.items.Count)
            {
                inventorySlots[i].SetItem(shop.items[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }
    }
}
