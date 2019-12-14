using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //creates a static variable thats shared by all instances of a class, calling it instance
    //and when starting the game we're setting the instance equals to this particular component 
    //meaning we'll able to acess this particular component with -- Inventory.instance --
    //this also means you MUST ONLY HAVE ONE INVENTORY at all times
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
            

        instance = this;
    }
    #endregion


    //checks if the invetory has changed (when we remove or add an item)
    //  delegate = event that you can subscribe different methods to
    //  when event is triggered all subscribed methods will be called
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    //creates a limited inventory space
    //in this case only 10 spaces (can be chaned within unity)
    public int space = 10; 


    //central for any inventory is a list
    public List<Item> items = new List<Item>();


    //creates method for adding new items
    //making it a bool stops from removing the item in the scene
    public bool Add (Item item)
    {
        //takes into account isDefaultItem variable, this doesnt add the item if its default.
        //if item is not a default item, add it to inventory
        if (!item.isDefaultItem)
        {
            //checks if inventory is full or not
            if (items.Count >= space)
            {
                Debug.Log("Inventory out of space!");
                return false;
            }


            //if inventory is not full, add item
            items.Add(item);

            //triggers the onItemChanged event
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        return true;
    }


    //removes items
    public void Remove (Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }


}
