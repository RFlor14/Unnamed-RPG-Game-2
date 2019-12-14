using UnityEngine;
using UnityEngine.UI; //use this everytime you use ui in scripts


//tells each inventory slot which item to store if any
public class InventorySlot : MonoBehaviour
{
    //references image component on icon object
    public Image icon;

    //references remove button
    public Button removeButton;

    Item item;

    //method for adding an item to the slot
    public void AddItem (Item newItem)
    {
        item = newItem;

        //updates the icons
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    //method for clearing out the slot
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    //creates method to remove item when the remove button is pressed
    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }
    
    //method for "equipping" the items
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

}
