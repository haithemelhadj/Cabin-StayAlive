using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelStatesHandler : PlayerBaseState
{
    public ParallelStatesHandler(PlayerStateMachine currentContext, PlayerStateFactory StateFactory)
        : base(currentContext, StateFactory)
    {
    }
    public override void EnterState()
    {
        //base.EnterState();
    }
    public override void UpdateState()
    {
        //base.UpdateState();
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
        //base.CheckSwitchState();
    }

    public override void InitializeSubState()
    {

    }
}


