using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//script to handle items as quest rewards, derives from questbase
//remove required quest item for quest completion

[CreateAssetMenu(fileName = "GetItemQuest", menuName = "Quests/GetItemQuest", order = 51)]
public class GetItemQuest : QuestBase
{
    //reference to item to find as item reward
    public ItemToFind[] itemsToFind;
    //control bool to remove item on quest completion
    public bool removeItemsOnComplete;

    //overide method for getcompletion progress
    //check players inventory for required item by item id and caluclate amount
    //string for display is item name and amount against amount required
    //add progress of item requirement to array
    public override string[] GetCompletionProgress()
    {
        List<string> progress = new List<string>();

        foreach (ItemToFind itemToFind in itemsToFind)
        {
            int currentAmount = Player.instance.inventory.items.Where(item => item.id == itemToFind.item.id).Sum(item => item.currentAmount);
            string itemProgress = $"{itemToFind.item.name}: {currentAmount}/{itemToFind.requiredAmount}";
            progress.Add(itemProgress);
        }

        return progress.ToArray();
    }
    //overtide method to check is requirements are met
    //checking player inventory instance for items by id and amount
    //return current amount greater or equal to the required amount of items
    public override bool AreRequirementsReached()
    {
        return itemsToFind.All(itemToFind => 
        {int currentAmount = Player.instance.inventory.items.Where(item => item.id == itemToFind.item.id).Sum(item => item.currentAmount);
        return currentAmount >= itemToFind.requiredAmount;});
    }
    //method to override complete
    //for each item required to find for quest
    //remove from player inventory instance by calling remove item
    //passing through item to find and amount
    //call onstorage update to update visuals against update progress
    //complete quest
    public override void Complete()
    {
        if (removeItemsOnComplete)
        {
            foreach (ItemToFind itemToFind in itemsToFind)
            {
                Player.instance.inventory.RemoveItem(itemToFind.item, itemToFind.requiredAmount);
            }
        }

        Player.instance.inventory.OnStorageUpdate -= UpdateProgress;

        base.Complete();
    }
    //method to overide accept quest
    //update player inventory instance visuals with onstorage update
    //call accept quest
    public override void Accept()
    {
        Player.instance.inventory.OnStorageUpdate += UpdateProgress;
        base.Accept();
    }
    //method to overide cancel quest
    //update player inventory instance visuals with onstorage update
    //call cancel quest
    public override void Cancel()
    {
        Player.instance.inventory.OnStorageUpdate -= UpdateProgress;
        base.Cancel();
    }
}

//class to define item to reward
[Serializable]
public class ItemToFind
{
    public Item item;
    public int requiredAmount;
}
