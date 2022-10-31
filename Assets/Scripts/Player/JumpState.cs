using UnityEngine;

public class JumpState : MovingState
{
	public JumpState (PlayerController player) : base(player)
	{

	}

	public override void Enter()
	{
		base.Enter();
		dash = false;
		Jump();
	}

	public override void Exit()
	{
		base.Exit();
		player.anim.SetBool("Jump", false);
	}

	public override void HandleInput()
	{
		base.HandleInput();
	}

	public override void StateUpdate()
	{
		base.StateUpdate();

		if (player.groundedState.isGrounded)
			player.stateMachine.ChangeState(player.movingState);
		else if (dash)
			player.stateMachine.ChangeState(player.dashState);
	}

	private void Jump()
	{
		player.rb2D.velocity = new Vector2(player.rb2D.velocity.x, player.jumpForce);
		player.anim.SetBool("Jump", true);
		player.playerAudio.PlayJumpSound();
	}
}
