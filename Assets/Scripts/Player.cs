using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour, IStateOwner
{
    [Header("Basic character components")]
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private Animator playerAC;

    [Header("Floating collider parameters")]
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private float raycastLength;
    [SerializeField] private float forceFactor;
    [SerializeField] private LayerMask groundLayer;

    [Header("Ground Check parameters")]
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float groundCheckRadius;

    //General properties
    public bool isGrounded { get; private set; }
    public bool isFalling { get; private set; }

    //States properties
    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public AirState AirState { get; private set; }

    //Private fields
    private StateMachine stateMachine;
    private StateDependencies stateDependencies;

    private void Awake()
    {
        stateMachine = new StateMachine();

        stateDependencies = new StateDependencies(this, stateMachine, playerInput, playerData);

        IdleState = new IdleState(stateDependencies);
        MoveState = new MoveState(stateDependencies);
        AirState = new AirState(stateDependencies);
    }
    // Start is called before the first frame update
    private void Start()
    {
        stateMachine.SetInitState(IdleState);

        MessageBroker.Instance.Subscribe(MessageEventName.ON_JUMP, LeaveTheGround);
    }

    // Update is called once per frame
    private void Update()
    {
        stateMachine.Update();
        LookAtDirection();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
        if (isGrounded)
        {
            Float();
        }
    }

    private void LateUpdate()
    {
        stateMachine.LateUpdate();
    }

    private void LookAtDirection()
    {
        Vector3 lookDirection = playerInput.MovementVector;

        lookDirection.y = 0f; // Ignore vertical difference

        // Rotate the character smoothly towards the look direction using DOTween
        if(lookDirection.x != 0 && lookDirection.z != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.DORotateQuaternion(targetRotation, playerData.rotationDuration)
                .OnComplete(() =>
                {
                    
                });
        }
    }

    public Vector3 GetCurrentSpeed()
    {
        return playerRb.velocity;
    }

    public void AddForce(Vector3 force, ForceMode forceMode)
    {
        playerRb.AddForce(force, forceMode);
    }

    public void SetAnimationBool(string animName, bool doPlay)
    {
        playerAC.SetBool(animName, doPlay);
    }

    public void SetAnimationFloat(string animName, float value)
    {
        playerAC.SetFloat(animName, value);
    }

    public void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheckPosition.position, groundCheckRadius, groundLayer);
    }

    public void Float()
    {
        Vector3 capsuleColliderCenterInWorldSpace = capsuleCollider.bounds.center;
        Ray downwardRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

        if (Physics.Raycast(downwardRayFromCapsuleCenter, out RaycastHit hit, raycastLength, groundLayer))
        {
            float distanceToFloatingPoint = capsuleCollider.center.y * transform.localScale.y - hit.distance;

            if (distanceToFloatingPoint == 0)
            {
                return;
            }

            float amountToLife = distanceToFloatingPoint * forceFactor - playerRb.velocity.y;
            Vector3 liftingForce = new Vector3(0f, amountToLife, 0f);

            playerRb.AddForce(liftingForce, ForceMode.VelocityChange);
        }
    }

    private void LeaveTheGround(object eventData)
    {
        isGrounded = false;
    }

    private void OnDrawGizmos()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();

        Vector3 capsuleColliderCenterInWorldSpace = capsuleCollider.bounds.center;
        Ray downwardRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down * raycastLength);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(downwardRayFromCapsuleCenter);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);
    }
}
