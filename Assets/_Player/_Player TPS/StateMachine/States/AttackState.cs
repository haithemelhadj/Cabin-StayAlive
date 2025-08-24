using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackState : PlayerBaseState
{
    private bool attackInputReceived;
    private float attackTimer;
    private const float ATTACK_WINDOW = 0.5f;

    public AttackState(PlayerStateMachine currentContext, PlayerStateFactory StateFactory)
        : base(currentContext, StateFactory)
    {
    }

    public override void EnterState()
    {
        attackInputReceived = false;
        attackTimer = 0f;
        
        // Handle the attack that caused this state transition
        //HandleAttackInput();
    }

    public override void UpdateState()
    {
        attackTimer += Time.deltaTime;
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        // Apply any physics during attacks if needed
    }

    public override void ExitState()
    {
        // Reset attack flags
        _cntx.inputsHandler.rb_Input = false;
        _cntx.inputsHandler.rt_Input = false;
        _cntx.inputsHandler.comboFlag = false;
    }

    public override void CheckSwitchStates()
    {
        // If no longer interacting with attack animation, return to movement
        if (!_cntx.isInteracting)
        {
            SwitchState(_factory.Movement());
            return;
        }

        // Check for combo inputs during attack window
        if (_cntx.canDoCombo && (_cntx.inputsHandler.rb_Input || _cntx.inputsHandler.rt_Input))
        {
            //HandleAttackInput();
        }
    }

    public override void InitializeSubState()
    {
        // Attack state doesn't need substates for now
    }

    /*
    private void HandleAttackInput()
    {
        if (_cntx.inputsHandler.rb_Input)
        {
            if (_cntx.canDoCombo)
            {
                _cntx.HandleWeaponCombo(_cntx.GetComponent<PlayerInventory>()?.rightweapon);
            }
            else
            {
                _cntx.HandleLightAttack(_cntx.GetComponent<PlayerInventory>()?.rightweapon);
            }
        }
        else if (_cntx.inputsHandler.rt_Input)
        {
            _cntx.HandleHeavyAttack(_cntx.GetComponent<PlayerInventory>()?.rightweapon);
        }
    }
    /**/
}


