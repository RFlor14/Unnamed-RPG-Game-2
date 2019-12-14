using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    //makes a reference in ItemsParent, so we can find all the components in the children
    public Transform itemsParent;

    //creates reference to entire UI
    public GameObject inventoryUI;

    //to make sure code runs optimaly, this caches it
    Inventory inventory;

    //creates an array of inventory slots
    InventorySlot[] slots;


    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }


    void Update()
    {
        //allows you to open or close the inventory
       if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

    }

    void UpdateUI()
    {
        //loops through all slots
        for (int i = 0; i < slots.Length; i++)
        {

            //checks if there are more items to add
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else // checks if we dont have anymore items
            {
                // clears that slot
                slots[i].ClearSlot();
            }


        }

    }


}
