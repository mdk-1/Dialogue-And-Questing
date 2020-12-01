using UnityEngine;

//script to handle items as scriptable objects
//map item type in enum
public enum ItemType
{
    Food = 1,
    Weapon = 2,
    Apparel = 3,
    Crafting = 4,
    Ingredients = 5,
    Potions = 6,
    Scrolls = 7,
    Quest = 8,
    Money = 9,
}

[System.Serializable]
public abstract class Item : ScriptableObject
{
    [Header ("Item Calibration")]
    public int id;
    public new string name;
    public string description;
    public ItemType type;
    public int maxStack = 1;
    public int currentAmount = 1;
    public int cost = 0;
    //references to inventory display picture
    //and worldspace object 
    public Sprite icon;
    public GameObject onGroundPrefab;

    //method to remove item from player inventory instance
    public virtual void Use() 
    {
        Player.instance.inventory.RemoveItem(this);
    }

    //method to drop item from inventory into worldspace
    //store objectsinmap object in heirarcy to local
    //instatiate ongroundprefab assigned to item
    //remove item from stack
    public void Drop()
    {
        GameObject container = GameObject.FindGameObjectWithTag("ObjectsInMapContainer");
        GameObject itemOnGroundObject = Instantiate(onGroundPrefab, Player.instance.transform.position, Quaternion.identity, container.transform);
        ItemOnGround itemOnGround = itemOnGroundObject.GetComponent<ItemOnGround>();

        if (itemOnGround != null)
        {
            itemOnGround.item = this;
        }

        Player.instance.inventory.RemoveItemStack(this);
    }

    //get attributes for item description
    public abstract string GetAttributes();

    //get cost for item value
    public virtual int GetTotalCost()
    {
        return cost * currentAmount;
    }
    //return stackable value
    public bool Stackable()
    {
        return maxStack > 1;
    }
}
