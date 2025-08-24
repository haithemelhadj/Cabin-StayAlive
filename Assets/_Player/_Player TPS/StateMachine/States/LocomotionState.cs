using UnityEngine;

public class LocomotionState : PlayerBaseState
{
    public LocomotionState(PlayerStateMachine currentContext, PlayerStateFactory StateFactory)
        : base(currentContext, StateFactory)
    {
    }
    public override void EnterState()
    {
        InitializeSubState();
    }
    public override void UpdateState()
    {
        float delta = Time.deltaTime;
        //base.UpdateState();

        _cntx.HandleMovement(delta);
        _cntx.HandleRollingAndSprinting(delta);
        _cntx.HandleFalling(delta, _cntx.moveDirection);
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {
        //base.FixedUpdateState();
    }
    public override void ExitState()
    {
        //base.ExitState();
    }
    public override void CheckSwitchStates()
    {
        // Check for attack input
        if (_cntx.inputsHandler.rb_Input || _cntx.inputsHandler.rt_Input)
        {
            SwitchState(_factory.Attack());
            return;
        }

        // Check for roll input
        if (_cntx.inputsHandler.rollFlag)
        {
            SwitchState(_factory.Roll());
            return;
        }
    }

    public override void InitializeSubState()
    {
        if (_cntx.isGrounded)
        {
            SetSubState(_factory.Grounded());
        }
        else
        {
            SetSubState(_factory.InAir());
        }
    }
    /**/




    /**/
}

