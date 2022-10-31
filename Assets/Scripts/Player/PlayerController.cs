using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Player Fields
	// Player Components
	[HideInInspector] public Rigidbody2D rb2D;
	[HideInInspector] public Animator anim;
	[HideInInspector] public SpriteRenderer sprite;
	[HideInInspector] public PlayerAudio playerAudio;

	// States
	[HideInInspector] public PlayerStateMachine stateMachine;
	[HideInInspector] public GroundedState groundedState;
	[HideInInspector] public MovingState movingState;
	[HideInInspector] public JumpState jumpState;
	[HideInInspector] public DashState dashState;
	[HideInInspector] public CrouchState crouchState;

	// Movement Parameters
	[Header("Checks")]
	[SerializeField] public LayerMask groundMask;
	[SerializeField] public float groundOffset = -1;
	[SerializeField] public float groundRadius = 0.2f;

	[Header("Movement")]
	[SerializeField] public float moveSpeed = 10;
	[SerializeField] public float acceleration = 4;
	[SerializeField] public float moveLerpSpeed = 100;

	[Header("Crouch")]
	[SerializeField] public float crouchHeight = 1;
	[SerializeField] public float crouchOffset = -0.5f;
	[HideInInspector] public Vector2 playerSize;
	[HideInInspector] public Vector2 playerOffset;

	[Header("Jump")]
	[SerializeField] public float jumpForce = 15;
	[SerializeField] public float fallMultiplier = 9;
	[SerializeField] public float jumpVelocityFalloff = 12;

	[Header("Dashing")]
	[SerializeField] public float dashSpeed = 30;
	[SerializeField] public float dashLength = 0.1f;


	#endregion

	void Start()
    {
		// grab components
		rb2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
		playerAudio = GetComponent<PlayerAudio>();

		CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
		playerSize = collider.size;
		playerOffset = collider.offset;

		// create states
		stateMachine = new PlayerStateMachine();

		groundedState = new GroundedState(this);
		movingState = new MovingState(this);
		jumpState = new JumpState(this);
		dashState = new DashState(this);
		crouchState = new CrouchState(this);

		// set start state
		stateMachine.Initialize(movingState);
    }

    void Update()
    {
		groundedState.HandleInput();
		groundedState.StateUpdate();

		stateMachine.currentState.HandleInput();
		stateMachine.currentState.StateUpdate();
    }
}
