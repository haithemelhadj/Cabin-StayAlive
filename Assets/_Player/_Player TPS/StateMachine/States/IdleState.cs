using UnityEngine;

public class IdleState : PlayerBaseState
{
    public IdleState(PlayerStateMachine currentContext, PlayerStateFactory StateFactory)
        : base(currentContext, StateFactory)
    {
    }

    public override void EnterState()
    {
        // Set idle animation parameters if needed
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        // Idle physics (minimal movement)
    }

    public override void ExitState()
    {
        // Cleanup idle state
    }

    public override void CheckSwitchStates()
    {
        // Switch to movement states based on input
        if (_cntx.inputsHandler.moveAmount > 0)
        {
            if (_cntx.isSprinting)
            {
                SwitchState(_factory.Running());
            }
            else
            {
                SwitchState(_factory.Walking());
            }
        }
    }

    public override void InitializeSubState()
    {
        // Idle doesn't need substates
    }
}