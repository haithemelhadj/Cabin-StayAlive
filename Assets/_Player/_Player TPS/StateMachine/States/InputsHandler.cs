using UnityEngine;

public class InputsHandler : MonoBehaviour
{
    /**/
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    public bool b_Input;
    public bool rb_Input;
    public bool rt_Input;

    public bool rollFlag;
    public bool sprintFlag;
    public bool crouchFlag;
    public bool comboFlag;
    public float rollInputTimer;


    [SerializeField]
    PlayerStateMachine playerStateMachine;
    PlayerControls inputActions;
    //PlayerAttacker playerAttacker;
    PlayerInventory PlayerInventory;
    //PlayerManager playerManager;

    Vector2 movementInput;
    Vector2 cameraInput;

    private void Awake()
    {
        if (playerStateMachine == null) playerStateMachine = GetComponentInParent<PlayerStateMachine>();
        //playerAttacker = GetComponent<PlayerAttacker>();
        PlayerInventory = GetComponent<PlayerInventory>();
        //playerManager = GetComponent<PlayerManager>();
    }


    public void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            inputActions.PlayerActions.Crouch.performed += i => playerStateMachine.HandleCrouch();// = !playerStateMachine.HandleCrouch();

        }
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        HandleMoveInput(delta);
        HandleRollingInput(delta);
        //HandleAttackInput(delta);
    }

    private void HandleMoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    private void HandleRollingInput(float delta)
    {
        b_Input = inputActions.PlayerActions.Roll.inProgress;
        if (b_Input)
        {
            rollInputTimer += delta;
            sprintFlag = true;
        }
        else
        {
            if (rollInputTimer > 0 && rollInputTimer < 0.5f)
            {
                sprintFlag = false;
                rollFlag = true;
            }
            rollInputTimer = 0;
        }
    }
    /*
    public void HandleAttackInput(float delta)
    {
        inputActions.PlayerActions.RB.performed += i => rb_Input = true;
        inputActions.PlayerActions.RT.canceled += i => rt_Input = true;

        if (rb_Input)
        {
            if (playerStateMachine.canDoCombo)
            {
                comboFlag = true;
                playerStateMachine.HandleWeaponCombo(PlayerInventory.rightweapon);
                comboFlag = false;
            }
            else
            {
                if (playerStateMachine.isInteracting)
                    return;
                if (playerStateMachine.canDoCombo)
                    return;
                playerStateMachine.HandleLightAttack(PlayerInventory.rightweapon);
            }
        }

        if (rt_Input)
        {
            playerStateMachine.HandleHeavyAttack(PlayerInventory.rightweapon);
        }

    }
    /**/
    public void HandleCrouchInput()
    {
        Debug.Log("crouch pressed");
    }
    /**/
}
