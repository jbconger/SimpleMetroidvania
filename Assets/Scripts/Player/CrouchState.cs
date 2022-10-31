using UnityEngine;

public class CrouchState : PlayerState
{
	private bool uncrouch;
	private CapsuleCollider2D collider;

    public CrouchState(PlayerController player) : base(player)
	{
		collider = player.GetComponent<CapsuleCollider2D>();
	}

	public override void Enter()
	{
		base.Enter();
		uncrouch = false;
		Crouch();
	}

	public override void Exit()
	{
		base.Exit();
		Uncrouch();
	}

	public override void HandleInput()
	{
		base.HandleInput();

		if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
			player.stateMachine.ChangeState(player.movingState);
	}

	public override void StateUpdate()
	{
		base.StateUpdate();

		if (uncrouch)
			player.stateMachine.ChangeState(player.movingState);
	}

	private void Crouch()
	{
		player.rb2D.velocity = Vector2.zero;
		collider.size = new Vector2(player.playerSize.x, player.crouchHeight);
		collider.offset = new Vector2(player.playerOffset.x, player.crouchOffset);
		player.anim.SetBool("Crouch", true);
	}

	private void Uncrouch()
	{
		collider.size = player.playerSize;
		collider.offset = player.playerOffset;
		player.anim.SetBool("Crouch", false);
	}
}
