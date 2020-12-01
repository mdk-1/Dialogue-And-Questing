using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//script to handle storage/chest addition and subtraction 
//add to item stacks, transfer to and from inventory

public class Storage
{
    //list of stored items at 10 slots
    public List<Item> items = new List<Item>();
    public int slotsCount = 10;

    //subscribe onStorageUpdate to update storage function for visuals across UpdateUI
    public delegate void UpdateStorage();
    public event UpdateStorage OnStorageUpdate;

    //method to add item to storage
    //check if stackable, or the same item type
    //call add to slot if not stackable or not stackable item exists
    //call add to existing slot if stackable item exists
    public virtual bool AddItem(Item item)
    {
        if (item.Stackable() == false)
        {
            return AddItemToNewSlot(item);
        }

        Item[] sameItems = items.Where(x => x.id == item.id).ToArray();

        if (sameItems.Length == 0)
        {
            return AddItemToNewSlot(item);
        }

        bool res = AddItemToExistingSlot(sameItems, item);
        if (res)
        {
            return true;
        }

        return AddItemToNewSlot(item);
    }

    //method to add item to existing slot
    //stackable items will be an item array, check current amount
    //for each stackable item to transfer increase amount
    //call OnStorage update to apply visuals 
    private bool AddItemToExistingSlot(IEnumerable<Item> sameItems, Item item)
    {
        Item[] stackableItems = sameItems.Where(x => x.currentAmount < x.maxStack).OrderByDescending(x => x.currentAmount).ToArray();

        if (stackableItems.Length == 0)
        {
            return false;
        }

        foreach (Item itemToAddStack in stackableItems)
        {
            int sum = itemToAddStack.currentAmount + item.currentAmount;

            itemToAddStack.currentAmount = Mathf.Clamp(sum, 0, itemToAddStack.maxStack);

            item.currentAmount = sum - itemToAddStack.maxStack;
            if (item.currentAmount <= 0)
            {
                break;
            }
        }

        if (item.currentAmount > 0)
        {
            return false;
        }

        OnStorageUpdate();
        return true;
    }
    //method to add item to new slot 
    //check storage has room
    //add item to storage
    //call storage update to apply visuals
    private bool AddItemToNewSlot(Item item)
    {
        if (items.Count >= slotsCount)
        {
            OnStorageUpdate();
            return false;
        }

        items.Add(item);

        OnStorageUpdate();
        return true;
    }
    //method to add item back to inventory that isn't stacked
    //check if item is equipable, remove as unequiped
    //remove item from storage
    //call storage update to apply visuals
    public virtual void RemoveItem(Item item)
    {
        item.currentAmount--;
        if (item.currentAmount <= 0)
        {
            items.Remove(item);
        }

        if (item is Equipable)
        {
            Equipable itemAsEquipable = item as Equipable;
            itemAsEquipable.Unequip();
        }

        OnStorageUpdate();
    }
    //method to add item back to inventory that is stacked
    //check if same items exist via item ID and order by amount to store in array
    //if same items exist in same item array, deduct the amount to be removed
    //or if no same items exist remove entire stack
    //call onstorage update for visuals
    public virtual void RemoveItem(Item item, int amountToRemove)
    {
        Item[] sameItems = items.Where(x => x.id == item.id).OrderBy(x => x.currentAmount).ToArray();

        foreach (Item sameItem in sameItems)
        {
            sameItem.currentAmount -= amountToRemove;

            if (sameItem.currentAmount > 0)
            {
                break;
            }

            if (sameItem.currentAmount == 0)
            {
                RemoveItemStack(sameItem);
                break;
            }

            amountToRemove = Mathf.Abs(sameItem.currentAmount);
            RemoveItemStack(sameItem);
        }

        OnStorageUpdate();
    }
    //method to remove an item stack if equipable
    //remove item stack as equipable and unequiped
    //call onstorage update for visuals
    public virtual void RemoveItemStack(Item item)
    {
        items.Remove(item);

        if (item is Equipable)
        {
            Equipable itemAsEquipable = item as Equipable;
            itemAsEquipable.Unequip();
        }

        OnStorageUpdate();
    }
    //method to handle item trasnfer from storage
    //declare item to be instantiated and set amount
    //if item is equipable, transfer item unequiped
    //call for remove item of item
    //call for onstorage update for visuals
    public virtual bool TransferItem(Item item, Storage anotherStorage)
    {
        Item itemToTransfer = Object.Instantiate(item);
        itemToTransfer.currentAmount = 1;

        bool itemTransferred = anotherStorage.AddItem(itemToTransfer);

        if (itemTransferred)
        {
            if (itemToTransfer is Equipable)
            {
                Equipable itemAsEquipable = itemToTransfer as Equipable;
                itemAsEquipable.Unequip();
            }

            RemoveItem(item);

            OnStorageUpdate();
            anotherStorage.OnStorageUpdate();
        }

        return itemTransferred;
    }
    //method to invoke the storage update visuals
    public void InvokeOnStorageUpdate()
    {
        OnStorageUpdate?.Invoke();
    }
}

