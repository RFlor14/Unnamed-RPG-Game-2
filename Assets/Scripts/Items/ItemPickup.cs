using UnityEngine;

//Item pick up inherits from the "Interactable" BASE CLASS
public class ItemPickup : Interactable
{

    public Item item;

    //defines what happens when we decide to interact with an object
    //overrides the Interact method
    public override void Interact()
    {
        base.Interact();


        PickUp();
    }

    void PickUp()
    {
        //Lets us know what item we're picking up
        Debug.Log("Picking up " + item.name);

        //Add to inventory
        bool wasPickedUp = Inventory.instance.Add(item);

        //if item was picked up, remove it from the scene
        if (wasPickedUp)
            Destroy(gameObject);

    }

}
