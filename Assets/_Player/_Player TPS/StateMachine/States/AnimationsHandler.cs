using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsHandler : MonoBehaviour
{

    //[SerializeField]
    //PlayerManager playerManager;
    //[SerializeField]
    //InputsHandler inputsHandler;
    [SerializeField]
    PlayerStateMachine playerStateMachine;
    //PlayerLocomotion PlayerLocomotion;
    //[SerializeField]
    //public Animator anim;

    int vertical;
    int horizontal;
    public bool canRotate;


    public void Initialize()
    {
        //playerManager = GetComponentInParent<PlayerManager>();
        //anim = GetComponent<Animator>();
        if(playerStateMachine==null) playerStateMachine = GetComponentInParent<PlayerStateMachine>();
        //if (inputsHandler == null) inputsHandler = GetComponentInParent<InputsHandler>();
        //PlayerLocomotion = GetComponentInParent<PlayerLocomotion>();

        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }
    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
    {
        #region vertical
        float v = 0;

        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            v = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            v = 1f;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            v = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            v = -1f;
        }
        else
        {
            v = 0;
        }

        #endregion

        #region hroizontal
        float h = 0;

        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            h = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            h = 1f;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            h = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            h = -1f;
        }
        else
        {
            h = 0;
        }

        #endregion
        if (isSprinting)
        {
            v = 2;
        }
        //playerStateMachine.playerAnimator.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        //playerStateMachine.playerAnimator.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        //playerStateMachine.playerAnimator.applyRootMotion = isInteracting;
        //playerStateMachine.playerAnimator.SetBool("isInteracting", isInteracting);
        //playerStateMachine.playerAnimator.CrossFade(targetAnim, 0.2f);
    }

    public void CanRotate()
    {
        canRotate = true;
    }

    public void StopRotation()
    {
        canRotate = false;
    }

    public void EnableCombo()
    {
        //playerStateMachine.playerAnimator.SetBool("canDoCombo", true);
    }

    public void DisableCombo()
    {
        //playerStateMachine.playerAnimator.SetBool("canDoCombo", false);
    }

    public void OnAnimatorMove()
    {
        if (playerStateMachine.isInteracting == false)
        {
            return;
        }
        float delta = Time.deltaTime;
        playerStateMachine.playerRigidbody.drag = 0;
        //Vector3 deltaPosition = playerStateMachine.playerAnimator.deltaPosition;
        //deltaPosition.y = 0;
        //Vector3 velocity = deltaPosition / delta;
        //playerStateMachine.playerRigidbody.velocity = velocity;

    }

    public void RestIsInteracting()

    {
        //playerStateMachine.playerAnimator.SetBool("isInteracting", false);
    }
}
