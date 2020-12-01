using System.Linq;
using UnityEngine;

//script to handle equipable weapons
public class PlayerWeapons : MonoBehaviour
{
    //reference to renderers
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    //reference to weapon script and instance
    private Weapon weapon;
    public static PlayerWeapons instance;

    //create instance of this
    private void Awake()
    {
        instance = this;
    }
    //initialize default weapon and call for equip
    private void Start()
    {
        Weapon initialWeapon = (Weapon)Player.instance.inventory.items.First(x => x.type == ItemType.Weapon);
        initialWeapon.Equip();
    }

    //method to equip weapon
    //define weapon to equip and update filter/mesh varaibles
    public void EquipWeapon(Weapon weaponToEquip)
    {
        weapon = weaponToEquip;
        meshFilter.mesh = weapon.equipedMesh;
        meshRenderer.materials = new[] { weapon.equipedMaterial };
    }
    //method to equip weapon
    //reset filter/mesh varaibles
    public void UnequipWeapon(Weapon weaponToUnequip)
    {
        if (weaponToUnequip?.id != weapon?.id)
        {
            return;
        }

        weapon = null;
        meshFilter.mesh = null;
        meshRenderer.materials = new Material[0];
    }
}