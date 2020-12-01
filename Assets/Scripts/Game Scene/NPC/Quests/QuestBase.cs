using System;
using UnityEngine;

// script to create quests as scriptable objects
// holds methods to control and check status of quests

public abstract class QuestBase : ScriptableObject
{
    [Header ("Quest Calibration")]
    public string title;
    [TextArea(3, 240)]
    public string description;
    //reference to quest reward class
    public QuestReward reward;
    //reference to quest status
    public QuestStatus status = QuestStatus.None;
    //level requirement for quests
    public int minLevelToAccept = 0;

    //control bool to check if requirements for quests are complete
    public abstract bool AreRequirementsReached();
    //method to pass through quest progress to string for display
    public abstract string[] GetCompletionProgress();
    //method to update progress of a quest
    public virtual void UpdateProgress()
    {
        if (status != QuestStatus.Accepted && status != QuestStatus.RequirementsReached)
        {
            return;
        }

        if (AreRequirementsReached())
        {
            status = QuestStatus.RequirementsReached;
        }
        else
        {
            status = QuestStatus.Accepted;
        }
    }
    //method to handle quest completion
    //call player instance change gold method and add reward gold
    //for each item to be rewardard, call player instance add item method
    //remove quest from player instance and change status to complete
    public virtual void Complete() 
    {
        Player.instance.ChangeGold(reward.gold);
        
        foreach (Item item in reward.items)
        {
            Player.instance.inventory.AddItem(item);
        }

        Player.instance.quests.Remove(this);

        status = QuestStatus.Completed;
    }
    //method to hand acceptance of quest
    //change status to accepted and add to player instance
    //call update progress for quest
    public virtual void Accept()
    {
        status = QuestStatus.Accepted;
        Player.instance.quests.Add(this);
        UpdateProgress();
    }
    //method to hand cancellation of question
    //changed staus to cancelled and remove from player instance
    public virtual void Cancel()
    {
        status = QuestStatus.Canceled;
        Player.instance.quests.Remove(this);
    }
    //control bool to check is player meets level requirements for quest
    public virtual bool CanBeAccepted()
    {
        return Player.instance.playerStats.level >= minLevelToAccept;
    }
}
//class for quest rewards
//holds gold, experience and list of items
[Serializable]
public class QuestReward
{
    public int gold;
    public int exp;
    public Item[] items;
}
//enum for quest status
//maps quest status to index
public enum QuestStatus
{
    None = 0,
    Accepted = 1,
    RequirementsReached = 2,
    Completed = 3,
    Canceled = 4,
}