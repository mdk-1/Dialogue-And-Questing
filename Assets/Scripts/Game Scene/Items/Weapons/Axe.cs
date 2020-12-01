using UnityEngine;

//script to handle axe as an item, subclass of weapon

[CreateAssetMenu(fileName = "Axe", menuName = "Items/Weapon/Axe", order = 51)]
public class Axe : Weapon
{
    //overide getattributes to return axe description
    public override string GetAttributes()
    {
        return description;
    }
}
