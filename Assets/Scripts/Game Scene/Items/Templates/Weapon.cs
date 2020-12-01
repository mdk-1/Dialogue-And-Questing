using UnityEngine;

//script to handle weapon class, subclass of equipable items

public abstract class Weapon : Equipable
{
    public float damage;
    public Mesh equipedMesh;
    public Material equipedMaterial;

    //override equip method for weapon
    //add weapon to playerinstance
    public override void Equip()
    {
        base.Equip();
        PlayerWeapons.instance.EquipWeapon(this);
    }
    //method to unequip weapon
    public override void Unequip()
    {
        base.Unequip();
        PlayerWeapons.instance.UnequipWeapon(this);
    }
}
