using UnityEngine;

// THIS IS THE BLUEPRINT FOR ALL SCRIPTABLE OBJECTS //

//tells how we want to create new items
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{
    //overrides old definition and uses this "new" one
    new public string name = "New Item";


    //creates icon for inventory
    public Sprite icon = null;


    //character starts up with default wears, this removes the clutter in the inventory
    public bool isDefaultItem = false;


    //calling this virtual allows you to derive different objects from the class Item.
    //Then define what happens with each item used
    public virtual void Use()
    {
        //uses item
        //something -might- happen


        Debug.Log("Using" + name);
    }


    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }


}
