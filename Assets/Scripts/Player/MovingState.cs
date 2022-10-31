using UnityEngine;

public class MovingState : PlayerState
{
	protected bool jump;
	protected bool dash;
	protected bool crouch;

    public MovingState(PlayerController player) : base(player)
	{

	}

	public override void Enter()
	{
		base.Enter();
		jump = false;
		dash = false;
		crouch = false;
	}

	public override void Exit()
	{
		base.Exit();
		player.anim.SetBool("Move", false);
	}

	public override void HandleInput()
	{
		base.HandleInput();
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) && player.groundedState.isGrounded)
			jump = true;
		else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftAlt))
			dash = true;
		else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
			crouch = true;
	}

	public override void StateUpdate()
	{
		base.StateUpdate();

		Moving();

		if (player.rb2D.velocity.y < -0.2)
			player.anim.SetBool("Fall", true);

		if (jump)
			player.stateMachine.ChangeState(player.jumpState);
		else if (dash)
			player.stateMachine.ChangeState(player.dashState);
		else if (crouch)
			player.stateMachine.ChangeState(player.crouchState);
	}

	private void Moving()
	{
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			if (player.rb2D.velocity.x > 0)
				player.groundedState.inputs.X = 0;
			player.groundedState.inputs.X = Mathf.MoveTowards(player.groundedState.inputs.X, -1, player.acceleration * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			if (player.rb2D.velocity.x < 0)
				player.groundedState.inputs.X = 0;
			player.groundedState.inputs.X = Mathf.MoveTowards(player.groundedState.inputs.X, 1, player.acceleration * Time.deltaTime);
		}
		else
		{
			player.groundedState.inputs.X = Mathf.MoveTowards(player.groundedState.inputs.X, 0, player.acceleration * 2 * Time.deltaTime);
		}

		Vector3 velocity = new Vector3(player.groundedState.inputs.X * player.moveSpeed, player.rb2D.velocity.y);
		player.rb2D.velocity = Vector3.MoveTowards(player.rb2D.velocity, velocity, player.moveLerpSpeed * Time.deltaTime);

		// set walking animation
		player.anim.SetBool("Move", player.groundedState.inputs.RawX != 0 && player.groundedState.isGrounded);
	}
}
