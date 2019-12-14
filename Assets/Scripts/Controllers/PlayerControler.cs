using UnityEngine.EventSystems;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerControler : MonoBehaviour
{

    public Interactable focus;

    public LayerMask movementMask;

    Camera cam;
    PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {

        //checks event system if we're currently hovering over UI
        if (EventSystem.current.IsPointerOverGameObject())
            return;


        //Left click movement
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                // Moves player to what we hit
                motor.MoveToPoint(hit.point);


                // Stops focus on objects
                RemoveFocus();

            }
            
        }

        //right click focus
        if (Input.GetMouseButton(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }

            }

        }
    }

    void SetFocus (Interactable newFocus)
    {
        //When setting the focus, we might already have a focus.
        //This de-focuses the previous one
        if (newFocus != focus)
        {
            //if previus focus is null
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        //leave this out, so we can notify our interactable everytime we click on it
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        motor.StopFollowingTarget();
    }

}
