using UnityEngine;
using UnityEngine.EventSystems;

public class InAirState : PlayerBaseState
{
    public InAirState(PlayerStateMachine currentContext, PlayerStateFactory StateFactory)
        : base(currentContext, StateFactory)
    {
    }

    public override void EnterState()
    {
        _cntx.isInAir = true;
    }

    public override void UpdateState()
    {
        float delta = Time.deltaTime;

        _cntx.playerRigidbody.AddForce(-Vector3.up * _cntx.fallingSpeed);
        _cntx.playerRigidbody.AddForce(_cntx.moveDirection * _cntx.fallingSpeed / 10f);
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        // Air physics handled in parent state
    }

    public override void ExitState()
    {
        _cntx.isInAir = false;
    }

    public override void CheckSwitchStates()
    {
        // Switch to Grounded when we land
        if (_cntx.isGrounded)
        {
            SwitchState(_factory.Grounded());
        }
    }

    public override void InitializeSubState()
    {
        // InAir state doesn't need substates
    }
}