using UnityEngine;

//script to handle equiping items such as weapons
//derived from item class

public abstract class Equipable : Item
{
    private bool equiped;

    //get attributes for item
    public abstract override string GetAttributes();

    //method to toggle equip or unequiped state of item
    public override void Use()
    {
        if (IsEquiped())
        {
            Unequip();
        }
        else
        {
            Equip();
        }
    }
    //method to check if an item is equiped
    public bool IsEquiped()
    {
        return equiped;
    }
    //method to update player inventory instance for equipping
    public virtual void Equip() 
    {
        equiped = true;
        Player.instance.inventory.InvokeOnStorageUpdate();
    }
    //method to update player inventory instance for unequipping
    public virtual void Unequip()
    {
        equiped = false;
        Player.instance.inventory.InvokeOnStorageUpdate();
    }
}
