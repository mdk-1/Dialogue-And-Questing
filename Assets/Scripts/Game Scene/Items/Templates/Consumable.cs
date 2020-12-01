using UnityEngine;

//script to toggle if a consumable item is off cooldown
//derived from item class

public abstract class Consumable : Item
{
    public abstract bool IsReadyToUse();
}
