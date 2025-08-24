using System.Collections.Generic;

public enum States
{
    Movement,
    Attack,
    Roll,
    Grounded,
    InAir,
    Idle,
    Walking,
    Running
}

public class PlayerStateFactory
{
    PlayerStateMachine context;
    Dictionary<States, PlayerBaseState> _states = new Dictionary<States, PlayerBaseState>();
    
    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        context = currentContext;
        _states[States.Movement] = new LocomotionState(context, this);
        _states[States.Attack] = new AttackState(context, this);
        _states[States.Roll] = new RollState(context, this);
        _states[States.Grounded] = new GroundedState(context, this);
        _states[States.InAir] = new InAirState(context, this);
        _states[States.Idle] = new IdleState(context, this);
        _states[States.Walking] = new WalkingState(context, this);
        _states[States.Running] = new RunningState(context, this);
    }

    #region Parent States
    public PlayerBaseState Movement()
    {
        return _states[States.Movement];
    }

    public PlayerBaseState Attack()
    {
        return _states[States.Attack];
    }

    public PlayerBaseState Roll()
    {
        return _states[States.Roll];
    }
    #endregion

    #region Sub States
    public PlayerBaseState Grounded()
    {
        return _states[States.Grounded];
    }

    public PlayerBaseState InAir()
    {
        return _states[States.InAir];
    }

    public PlayerBaseState Idle()
    {
        return _states[States.Idle];
    }

    public PlayerBaseState Walking()
    {
        return _states[States.Walking];
    }

    public PlayerBaseState Running()
    {
        return _states[States.Running];
    }
    #endregion
}
