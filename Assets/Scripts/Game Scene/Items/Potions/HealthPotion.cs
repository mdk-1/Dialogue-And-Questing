using System;
using UnityEngine;

//script to handle health potion as a consumable item
//derives from consumable class

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potions/Health Potion", order = 51)]
public class HealthPotion : Consumable
{
    //varaible to hold heal amount per potion
    public int healAmount = 25;

    //method to override descripton of potion
    public override string GetAttributes()
    {
        return description;
    }

    //method to toogle consumable cooldown
    public override bool IsReadyToUse()
    {
        return !Player.instance.disableConsumablesUsage;
    }

    //method to use health potion
    //check if cooldown is off
    //call player instance to return heal amount
    //reset cooldown
    public override void Use()
    {
        if (IsReadyToUse() == false)
        {
            return;
        }

        Player.instance.Heal(healAmount);
        Player.instance.SetConsumablesOnCooldown();
        base.Use();
    }
}
