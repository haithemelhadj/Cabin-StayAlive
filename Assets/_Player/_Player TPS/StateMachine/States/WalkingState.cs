using UnityEngine;

public class WalkingState : PlayerBaseState
{
    public WalkingState(PlayerStateMachine currentContext, PlayerStateFactory StateFactory)
        : base(currentContext, StateFactory)
    {
    }

    public override void EnterState()
    {
        // Set walking animation parameters
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        // Walking physics handled in parent state
    }

    public override void ExitState()
    {
        // Cleanup walking state
    }

    public override void CheckSwitchStates()
    {
        // Switch based on movement input
        if (_cntx.inputsHandler.moveAmount == 0)
        {
            SwitchState(_factory.Idle());
        }
        else if (_cntx.isSprinting)
        {
            SwitchState(_factory.Running());
        }
    }

    public override void InitializeSubState()
    {
        // Walking doesn't need substates
    }
}