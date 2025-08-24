using UnityEngine;

public class RollState : PlayerBaseState
{
    private float rollTimer;
    private const float ROLL_DURATION = 0.6f; // Typical roll animation duration

    public RollState(PlayerStateMachine currentContext, PlayerStateFactory StateFactory)
        : base(currentContext, StateFactory)
    {
    }

    public override void EnterState()
    {
        rollTimer = 0f;
        
        // The roll handling is already done in the locomotion state
        // We just need to monitor when the roll is complete
    }

    public override void UpdateState()
    {
        rollTimer += Time.deltaTime;
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        // Roll physics are handled in PlayerStateMachine.HandleRollingAndSprinting
    }

    public override void ExitState()
    {
        // Reset roll flag
        _cntx.inputsHandler.rollFlag = false;
    }

    public override void CheckSwitchStates()
    {
        // Check if roll animation is complete or if we're no longer interacting
        if (!_cntx.isInteracting || rollTimer >= ROLL_DURATION)
        {
            SwitchState(_factory.Movement());
            return;
        }

        // Emergency exit if something goes wrong
        if (rollTimer > ROLL_DURATION * 2f)
        {
            SwitchState(_factory.Movement());
            return;
        }
    }

    public override void InitializeSubState()
    {
        // Roll state doesn't need substates
    }
}


