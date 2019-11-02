﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public float captureSpeed = 2.5f;
    public float waitTime = 3;
    private Transform targetPlayer;
    private CaptureWait waitScript;

    WitchLogic witchLogic;
    GameManager gameManager;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        waitScript = animator.GetComponent<CaptureWait>();

        witchLogic = animator.GetComponent<WitchLogic>();
        targetPlayer = witchLogic.getTargetPlayer();
        targetPlayer.GetComponent<PlayerLogic>().gotCaught(); // make player disabled
        witchLogic.playSound(witchLogic.laughSound);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (animator.transform.position.x != targetPlayer.position.x && animator.transform.position.z != targetPlayer.position.z)
        {
            centerOnPlayer(targetPlayer, animator);

            // pause for a little while before moving back to Idle state
            Debug.Log("before CoRouting");            
        }
        else
        {
            waitScript.DoCoroutine(waitTime);
            targetPlayer.GetComponent<PlayerLogic>().playSound(targetPlayer.GetComponent<PlayerLogic>().caughtSound);
            targetPlayer.GetComponent<PlayerController>().getAnimator().SetBool("isCaught", true);
            targetPlayer.GetComponent<PlayerController>().getAnimator().SetBool("isIdle", false);
            targetPlayer.GetComponent<PlayerController>().getAnimator().SetBool("isWalking", false);
            targetPlayer.GetComponent<PlayerController>().getAnimator().SetBool("isRunning", false);

        }

        // TODO: Decrament player fruitcount slowly 
        targetPlayer.GetComponent<PlayerLogic>().loseFruits();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        targetPlayer.GetComponent<PlayerController>().getAnimator().SetBool("isCaught", false);
        targetPlayer.GetComponent<PlayerController>().getAnimator().SetBool("isIdle", true);
        targetPlayer.GetComponent<PlayerLogic>().spawn();

        witchLogic.stopChasing();
        //targetPlayer.GetComponent<PlayerLogic>().enableControls(); moved to PlayerRespawnBehaviour.cs
    }

    private void centerOnPlayer(Transform t, Animator animator)
    {
        /*
         * Moves this gameObject towards t's position
         */

        Vector3 targetPosition = new Vector3(t.position.x, animator.transform.position.y, t.position.z);
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, targetPosition, captureSpeed * Time.deltaTime);

        // Note animator.transform pivot should be the same as the spotlight pivot

    }

}
