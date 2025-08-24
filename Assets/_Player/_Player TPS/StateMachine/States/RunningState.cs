using UnityEngine;

public class RunningState : PlayerBaseState
{
    public RunningState(PlayerStateMachine currentContext, PlayerStateFactory StateFactory)
        : base(currentContext, StateFactory)
    {
    }

    public override void EnterState()
    {
        // Set running animation parameters
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        // Running physics handled in parent state
    }

    public override void ExitState()
    {
        // Cleanup running state
    }

    public override void CheckSwitchStates()
    {
        // Switch based on movement input
        if (_cntx.inputsHandler.moveAmount == 0)
        {
            SwitchState(_factory.Idle());
        }
        else if (!_cntx.isSprinting)
        {
            SwitchState(_factory.Walking());
        }
    }

    public override void InitializeSubState()
    {
        // Running doesn't need substates
    }
}