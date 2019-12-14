using UnityEngine;

public class Interactable: MonoBehaviour
{
    public float radius = 3f;

    //controls where you stand and look on certain items (ex. only infront of chests)
    public Transform interactionTransform;
    

    //checks if player focusing on the object is close enough to interact with "it"
    bool isFocus = false;
    Transform player;


    //Makes interaction happen only once
    bool hasInteracted = false;


    //actual method for interacting
    //Sets up INHERITANCE methods within this BASE CLASS
    //virtual allows you to trigger this method, but within other classes you can overwrite it
    //this allows you to put in your own functionality for each type of interactable
    public virtual void Interact()
    {
        //This method is meant to be overwritten
        // Debug.Log("Interacting with " + transform.name);
    }

    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }


    public void OnFocused (Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }


    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
    // end line


    void OnDrawGizmosSelected()
    {
        //this makes sure we dont get any errors in the inspector when on NEW items
        if (interactionTransform == null)
            interactionTransform = transform;


        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }


}
