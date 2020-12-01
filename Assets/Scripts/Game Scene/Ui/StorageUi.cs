using UnityEngine;

//script to handle the storge panel on UI

public class StorageUi : MonoBehaviour
{
    //reference storage canvas
    public StorageItemInspectPanel itemInspectPanel;
    public GameObject storageWindow;
    public Transform itemsParent;

    //referencing storage slots and items to transfer
    ItemSlot[] inventorySlots;
    Item selectedItem;
    private Storage storage;
    public static StorageUi instance;

    //create instance and declare storage slots
    private void Awake()
    {
        instance = this;
        inventorySlots = itemsParent.GetComponentsInChildren<ItemSlot>();
    }
    //initialise SetStorage
    private void Start()
    {
        SetStorage(new Storage());
    }

    //if player input is detected for P key
    //set storage panel active
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            storageWindow.SetActive(!storageWindow.activeSelf);
        }
    }

    //method to delcare the item slots in storage
    public void SelectItem(int index)
    {
        ItemSlot slot = inventorySlots[index];
        SelectItem(slot.GetItem());
    }
    //method to delcare the item descrption in storage
    public void SelectItem(Item item)
    {
        selectedItem = item;
        itemInspectPanel.SetItem(selectedItem);
    }
    //method to subscribe storage to OnStorageUpdate event
    //error checking as workaround to getting updateUI function suscribed
    public void SetStorage(Storage newStorage)
    {
        try
        {
            storage.OnStorageUpdate -= UpdateUi;
        }
        catch {}

        newStorage.OnStorageUpdate += UpdateUi;
        storage = newStorage;

        UpdateUi();
    }
    //method to return storageUI
    public Storage GetStorage()
    {
        return storage;
    }
    //method to transfer item to player inventory instance
    public void TakeItem()
    {
        if (selectedItem == null)
        {
            return;
        }

        storage.TransferItem(selectedItem, Player.instance.inventory);
        SelectItem(null);
    }
    //method to update visuals across storage panel
    private void UpdateUi()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < storage.items.Count)
            {
                inventorySlots[i].SetItem(storage.items[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }
    }
}
