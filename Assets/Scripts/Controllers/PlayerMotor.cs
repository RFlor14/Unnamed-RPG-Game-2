using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    //moves the character to clicked area
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    //tracks target
    public void FollowTarget (Interactable newTarget)
    {
        //sets player within the radius but not too close
        agent.stoppingDistance = newTarget.radius * .8f;
        agent.updateRotation = false;

        target = newTarget.interactionTransform;
    }

    //stops following the target
    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;

        target = null;
    }

    void FaceTarget()
    {
        //direction towards the target
        Vector3 direction = (target.position - transform.position).normalized;

        //finds out how we rotate ourselves to look in that direction and avoids changes in y direction
        //changes the direction to rotation
        //look rotation takes a vector with direction and looks in the direction
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        //Quaternion.Slerp smoothens interpolate towards that rotation
        //Slerp allows to spherecly interpolate between two points
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


}
