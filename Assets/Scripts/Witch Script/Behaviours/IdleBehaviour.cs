﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    Vector3 newVector;
    GameManager gameManager;
    public float movingSeed;
    [Range(0, 1)] public float rotationSpeed = 0.1f;

    private Vector3 direction;
    public bool finishedRotating;


    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        gameManager = animator.GetComponentInChildren<GameManagerReference>().gameManager;
        newVector = new Vector3(gameManager.getWitchBase().position.x, animator.transform.position.y, gameManager.getWitchBase().position.z);
        direction = newVector - animator.transform.position;
        finishedRotating = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Idle");

        //animator.transform.LookAt(newVector); //Quaternion.Slerp(animator.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

        if (animator.transform.position.x != newVector.x && animator.transform.position.z != newVector.z)
        {

            if (!finishedRotating)
            {
                animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);

                // check if rotation complete 
                if (Vector3.Angle(direction, animator.transform.forward) < .1)
                {
                    // we're now facing the right direction
                    finishedRotating = true;
                }
            }
            else
            {
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, newVector, movingSeed * Time.deltaTime);
            }

        }
        // when reaching base, chagne state to 'Patrol'
        else
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isPatrolling", true);
            animator.SetBool("isChasing", false);
        }
    }
}
