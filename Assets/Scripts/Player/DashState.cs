using UnityEngine;

public class DashState : PlayerState
{
	private Vector3 dashDirection;
	private float timeStartedDash;

	public DashState(PlayerController player) : base(player)
	{

	}

	public override void Enter()
	{
		base.Enter();
		base.HandleInput();
		player.anim.SetBool("Dash", true);
		Dash();
	}

	public override void Exit()
	{
		base.Exit();
		player.anim.SetBool("Dash", false);
	}

	public override void HandleInput()
	{
		base.HandleInput();
	}

	public override void StateUpdate()
	{
		base.StateUpdate();
		Dashing();
	}

	private void Dashing()
	{
		player.rb2D.velocity = dashDirection * player.dashSpeed;

		if (Time.time >= timeStartedDash + player.dashLength)
		{
			player.rb2D.velocity = new Vector2(player.rb2D.velocity.x, player.rb2D.velocity.y > 2 ? 2 : player.rb2D.velocity.y);
			player.rb2D.gravityScale = 1;

			player.stateMachine.ChangeState(player.movingState);
		}
	}

	private void Dash()
	{
		dashDirection = new Vector3(player.groundedState.inputs.RawX, player.groundedState.inputs.RawY).normalized;

		if (dashDirection == Vector3.zero)
			dashDirection = player.groundedState.isFacingLeft ? Vector3.left : Vector3.right;

		timeStartedDash = Time.time;
		player.rb2D.gravityScale = 0;

		player.playerAudio.PlayDashSound();
	}
}
