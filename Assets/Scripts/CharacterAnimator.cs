using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    const float locomotionAnimationSmoothTime = .1f;

    NavMeshAgent agent;
    protected Animator animator;
    protected CharacterCombat combat;
    

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //gets current speed and divides it by the maximum speed
        float speedPercent = agent.velocity.magnitude / agent.speed;

        //sets speedPercent parameter on the animator and smoothens it out
        NewMethod(speedPercent);

        animator.SetBool("inCombat", combat.inCombat);
    }

    private void NewMethod(float speedPercent)
    {
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
    }
}
