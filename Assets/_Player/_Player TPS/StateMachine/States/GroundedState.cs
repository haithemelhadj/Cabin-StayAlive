using UnityEngine;

public class GroundedState : PlayerBaseState
{
    public GroundedState(PlayerStateMachine currentContext, PlayerStateFactory StateFactory)
        : base(currentContext, StateFactory)
    {
    }

    public override void EnterState()
    {
        InitializeSubState();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        // Ground physics handled in parent state
    }

    public override void ExitState()
    {
        // Cleanup when leaving ground
    }

    public override void CheckSwitchStates()
    {
        // Switch to InAir if we're no longer grounded
        if (!_cntx.isGrounded)
        {
            SwitchState(_factory.InAir());
        }
    }

    public override void InitializeSubState()
    {
        // Determine which movement substate to enter
        if (_cntx.inputsHandler.moveAmount == 0)
        {
            SetSubState(_factory.Idle());
        }
        else if (_cntx.isSprinting)
        {
            SetSubState(_factory.Running());
        }
        else
        {
            SetSubState(_factory.Walking());
        }
    }
}