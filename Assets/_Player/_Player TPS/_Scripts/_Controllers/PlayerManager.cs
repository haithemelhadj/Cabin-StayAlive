using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    InputHandler inputHandler;
    [SerializeField]
    CameraHandler CameraHandler;
    [SerializeField]
    PlayerLocomotion playerLocomotion;
    [SerializeField]
    Animator anim;

    public bool isInteracting;
    [Header("Player Flags")]
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;
    public bool canDoCombo;


    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        CameraHandler = CameraHandler.singleton;

    }
    private void Update()
    {
        float delta = Time.deltaTime;

        isInteracting = anim.GetBool("isInteracting");
        canDoCombo = anim.GetBool("canDoCombo");


        inputHandler.TickInput(delta);
        playerLocomotion.HandleMovement(delta);
        playerLocomotion.HandleRollingAndSprinting(delta);
        playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
    }


    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        if (CameraHandler != null)
        {
            CameraHandler.FollowTarget(delta);
            CameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
        }
    }

    private void LateUpdate()
    {
        inputHandler.rollFlag = false;
        inputHandler.sprintFlag = false;
        inputHandler.rb_Input = false;
        inputHandler.rt_Input = false;

        if (isInAir)
        {
            playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
        }
    }
}
