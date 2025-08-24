using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerBaseState _currentState;
    PlayerStateFactory _states;

    [Header("State Debugging")]
    public string currentSuperState;
    public string currentSubState;

    public bool lockCursor;

    private void Awake()
    {
        // Lock cursor for FPS experience
        if (lockCursor)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;

        InitializeState();
        GetComponents();
        //camera control
        //singleton = this;
        //myTransform = transform;
        //defaultPosition = cameraTransform.localPosition.z;
        //ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
    }

    private void Start()
    {
        GetPlayerDimentions();
        isGrounded = true;
        //ignoreForGroundCheck = ~(1 << 8 | 1 << 11);
    }

    private void Update()
    {
        HandleCameraRotation();
        PlayerManagerUpdate();
        _currentState.UpdateStates();
        UpdateStateDisplay();
    }


    private void FixedUpdate()
    {
        PlayermanagerFixedUpdate();
        _currentState.FixedUpdateStates();
    }

    private void LateUpdate()
    {
        PlayerManagerLateUpdate();
    }



    private void InitializeState()
    {
        _states = new PlayerStateFactory(this);
        _currentState = _states.Movement();
        _currentState.EnterState();
    }
    private void GetComponents()
    {
        PlayerManagerAwake();
        LocomotionAwake();

    }
    private void LocomotionAwake()
    {
        if (playerRigidbody == null) playerRigidbody = GetComponent<Rigidbody>();
        if (inputsHandler == null) inputsHandler = GetComponent<InputsHandler>();
        //playerManager = GetComponent<PlayerManager>();
        if (cameraObject == null) cameraObject = Camera.main.transform;
        if (myTransform == null) myTransform = transform;
        //if (animationsHandler == null) animationsHandler = GetComponentInChildren<AnimationsHandler>();
        //if (animationsHandler != null) animationsHandler.Initialize();


    }
    private void PlayerManagerAwake()
    {
        //inputHandler = GetComponent<InputHandler>();
        //if (playerLocomotion==null) playerLocomotion = GetComponent<LocomotionState>();
        //if (playerAnimator == null) playerAnimator = GetComponentInChildren<Animator>();
    }

    private void UpdateStateDisplay()
    {
        if (_currentState != null)
        {
            currentSuperState = _currentState.GetType().Name;

            // Use reflection to get the current sub state
            var subStateField = typeof(PlayerBaseState).GetField("_currentSubState",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (subStateField != null)
            {
                var subState = subStateField.GetValue(_currentState) as PlayerBaseState;
                currentSubState = subState?.GetType().Name ?? "None";
            }
        }
    }
    private void PlayerManagerUpdate()
    {
        float delta = Time.deltaTime;

        //isInteracting = playerAnimator.GetBool("isInteracting");
        //canDoCombo = playerAnimator.GetBool("canDoCombo");


        inputsHandler.TickInput(delta);
    }
    private void PlayermanagerFixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

    }
    private void PlayerManagerLateUpdate()
    {
        inputsHandler.rollFlag = false;
        inputsHandler.sprintFlag = false;
        inputsHandler.rb_Input = false;
        inputsHandler.rt_Input = false;

        if (isInAir)
        {
            inAirTimer = inAirTimer + Time.deltaTime;
        }
    }









    /**/
    #region Values & Refrences Managerment
    [Header("-----Components-----")]
    public Transform myTransform;
    public Transform playerCamera;
    public Transform cameraObject;
    public Rigidbody playerRigidbody;
    //public Animator playerAnimator;
    public CapsuleCollider playerCollider;

    [Header("-----Refrences-----")]
    public InputsHandler inputsHandler;
    //public AnimationsHandler animationsHandler;

    [Header("-----Player Flags-----")]
    public bool isInteracting;
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;
    public bool canDoCombo;

    #endregion


    #region Player Dimentions

    [Header("-----Player Dimentions-----")]
    public float playerHeight;
    public float playerWidth;
    public Vector3 playerScale;

    public void GetPlayerDimentions()
    {
        playerScale = myTransform.localScale;
        playerHeight = playerCollider.height * playerScale.y;
        playerWidth = playerCollider.radius * playerScale.x;

    }
    #endregion

    #region Handle Camera Rotation





    #endregion

    #region locomotion


    [Header("-----Ground & Air Detection Stats-----")]
    public LayerMask GroundCheck;
    public float groundDetectionRayStartPoint = 0f;
    public float groundDetectionExtraDistance = 1f;
    public float groundCheckTotalDistance;
    public float inAirTimer;

    [Header("-----Movement Speeds-----")]
    public float currentMovementSpeed = 5;
    public float crouchSpeed = 2.5f;
    public float walkingSpeed = 5;
    public float sprintSpeed = 7;
    [Header("-----Movement Stats-----")]
    public Vector3 moveDirection;
    public float rotationSpeed = 10;
    public float fallingSpeed = 45;
    Vector3 normalVector;
    Vector3 targetPosition;


    #region Movement
    public void HandleMovement(float delta)
    {
        if (inputsHandler.rollFlag)
            return;

        if (isInteracting)
            return;

        moveDirection = myTransform.forward * inputsHandler.vertical;
        moveDirection += myTransform.right * inputsHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;

        float speed = currentMovementSpeed;

        if (inputsHandler.sprintFlag && inputsHandler.moveAmount > 0.5f) //sprint
        {
            speed = sprintSpeed;
            isSprinting = true;
            moveDirection *= speed;
        }
        else
        {
            if (inputsHandler.moveAmount < 0.5f)//walk
            {
                moveDirection *= walkingSpeed;
            }
            else //run
            {

                moveDirection *= speed;
            }
            isSprinting = false;
        }



        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        playerRigidbody.velocity = projectedVelocity;

        //animationsHandler.UpdateAnimatorValues(inputsHandler.moveAmount, 0f, isSprinting);

        //if (animationsHandler.canRotate)
        //{

        //    //HandleRotation(delta);
        //}
    }


    [Header("-----Camera Settings-----")]
    [Header("Camera Values")]
    [Range(50f, 1000f)] public float mouseSensitivity = 100f;

    public float mouseX;
    public float mouseY;

    public bool invertX;
    public bool invertY;

    [Range(0f, 180f)] public float maxLookAngle = 90f;
    [Range(0f, -180f)] public float minLookAngle = -90f;
    private float xRotation = 0f;
    private void HandleCameraRotation()
    {
        // Apply sensitivity and deltaTime
        mouseX = inputsHandler.mouseX * mouseSensitivity * Time.deltaTime;
        mouseY = inputsHandler.mouseY * mouseSensitivity * Time.deltaTime;
        if (invertX)
            mouseX = -mouseX;
        if (invertY)
            mouseY = -mouseY;

        // Vertical rotation (pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookAngle, maxLookAngle);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal rotation (yaw)
        myTransform.Rotate(Vector3.up * mouseX);
    }
    public void HandleRotation(float delta)
    {
        Vector3 targetDir = Vector3.zero;
        float moveOverride = inputsHandler.moveAmount;


        targetDir = cameraObject.forward * inputsHandler.vertical;
        targetDir += cameraObject.right * inputsHandler.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;
        if (targetDir == Vector3.zero)
        {
            targetDir = myTransform.forward;
        }
        float rs = rotationSpeed;
        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

        myTransform.rotation = targetRotation;

    }







    public void HandleRollingAndSprinting(float delta)
    {
        if (isInteracting)
        {
            return;
        }
        if (inputsHandler.rollFlag)
        {
            moveDirection = cameraObject.forward * inputsHandler.vertical;
            moveDirection += cameraObject.right * inputsHandler.horizontal;

            if (inputsHandler.moveAmount > 0)
            {
                //animationsHandler.PlayTargetAnimation("RollForward", true);
                moveDirection.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                myTransform.rotation = rollRotation;
            }
            else
            {
                //animationsHandler.PlayTargetAnimation("RollBackward", true);
            }

        }
    }

    public void HandleFalling(float delta, Vector3 moveDirection)
    {
        isGrounded = false;
        RaycastHit hit;
        Vector3 origin = myTransform.position;
        origin.y += groundDetectionRayStartPoint;

        if (Physics.Raycast(origin, myTransform.forward, out hit, playerWidth+0.1f))// stop if there is a wall in front of the player
        {
            moveDirection = Vector3.zero;
        }
        if (isInAir)
        {
            playerRigidbody.AddForce(-Vector3.up * fallingSpeed);
            playerRigidbody.AddForce(moveDirection * fallingSpeed / 10f);
        }

        Vector3 dir = moveDirection;
        dir.Normalize();
        //origin = origin + dir * groundDirectionRayDistance;

        groundCheckTotalDistance = (playerHeight / 2 + groundDetectionExtraDistance);
        targetPosition = myTransform.position;

        Debug.DrawRay(origin, -Vector3.up * (playerHeight/2 + groundDetectionExtraDistance), Color.red, 0.1f, false);
        if (Physics.SphereCast(origin, 0.2f, -Vector3.up, out hit, (playerHeight / 2 + groundDetectionExtraDistance), GroundCheck))//if grounded 
        {
            normalVector = hit.normal;
            Vector3 tp = hit.point;
            isGrounded = true;
            targetPosition.y = tp.y+(playerHeight/2);

            if (isInAir)
            {
                if (inAirTimer > 0.5f)
                {
                    //Debug.Log("you were is air for" + inAirTimer);
                    //animationsHandler.PlayTargetAnimation("Land", true);
                }
                else
                {
                    //animationsHandler.PlayTargetAnimation("Empty", false);
                }
                inAirTimer = 0;
                isInAir = false;
            }
        }
        else// if not grounded
        {
            if (isGrounded)
            {
                isGrounded = false;
            }
            if (isInAir == false)
            {
                if (isInteracting == false)
                {
                    //animationsHandler.PlayTargetAnimation("Falling", true);
                }
                Vector3 vel = playerRigidbody.velocity;
                vel.Normalize();
                playerRigidbody.velocity = vel * (currentMovementSpeed / 2);
                isInAir = true;
            }
        }
        
        if (isInteracting || inputsHandler.moveAmount > 0)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime / 0.1f);
        }
        else
        {
            myTransform.position = targetPosition;
        }
    }


    #endregion

    [Header("-----Crouch Settings-----")]
    public bool isCrouching;


    public void HandleCrouch()
    {
        Debug.Log("Crouch Toggled");
        if (isCrouching)
        {
            isCrouching = false;
            currentMovementSpeed = walkingSpeed;
            playerCollider.height = playerHeight;
            transform.position = new Vector3(transform.position.x, transform.position.y + playerHeight / 4, transform.position.z);
        }
        else
        {
            isCrouching = true;
            currentMovementSpeed = crouchSpeed;
            playerCollider.height = playerHeight / 2;
            transform.position = new Vector3(transform.position.x, transform.position.y - playerHeight / 4, transform.position.z);

        }
    }

    #endregion



    /*
     * #region attacker
    [Header("-----Attacker-----")]
    public string lastAttack;


    public void HandleWeaponCombo(WeaponItem weapon)
    {
        if (inputsHandler.comboFlag)
        {

            playerAnimator.SetBool("canDoCombo", false);
            if (lastAttack == weapon.OH_Light_Attack_1)
            {
                animationsHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
            }
        }
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        animationsHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
        lastAttack = weapon.OH_Light_Attack_1;
    }


    public void HandleHeavyAttack(WeaponItem weapon)
    {
        animationsHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
        lastAttack = weapon.OH_Heavy_Attack_1;

    }



    #endregion
    /**/


    #region
    #endregion

    #region

    #endregion


}
