using UnityEngine;

//script to handle items in worldspace, collider trigger to enable player to pickup
public class ItemOnGround : MonoBehaviour
{
    public Item item;
    private bool playerInTouch;
    public GameObject worldObject;

    // player pushes E to pick up item
    // check item is in range
    // add itme to player inventory instance
    // destory gameobject
    void Update()
    {
        if (playerInTouch && Input.GetKeyDown(KeyCode.E))
        {
            Player.instance.inventory.AddItem(item);
            Destroy(gameObject);
        }
    }
    //control bool toogle to determine if player is in range of item
    private void OnTriggerEnter(Collider other)
    {
        if (Player.instance.CompareTag(other.tag))
        {
            playerInTouch = true;
        }
    }
    //control bool toogle to determine if player is in range of item
    private void OnTriggerExit(Collider other)
    {
        if (Player.instance.CompareTag(other.tag))
        {
            playerInTouch = false;
        }
    }
}
