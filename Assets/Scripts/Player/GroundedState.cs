using UnityEngine;

public class GroundedState : PlayerState
{
	public struct Inputs
	{
		public int RawX;
		public int RawY;
		public float X;
		public float Y;
	}

	public Inputs inputs;
	public bool isFacingLeft;

	public bool isGrounded;
    private readonly Collider2D[] ground = new Collider2D[1];

    public GroundedState(PlayerController player) : base(player)
	{
        
	}

	public override void Enter()
	{
		base.Enter();
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void HandleInput()
	{
		base.HandleInput();
		CheckInput();
	}

	public override void StateUpdate()
	{
		base.StateUpdate();
		UpdateSprite();
		Grounding();
		Falling();
	}

	protected void CheckInput()
	{
		inputs.RawX = (int)Input.GetAxisRaw("Horizontal");
		inputs.RawY = (int)Input.GetAxisRaw("Vertical");
		inputs.X = Input.GetAxis("Horizontal");
		inputs.Y = Input.GetAxis("Vertical");

		isFacingLeft = inputs.RawX != 1 && (inputs.RawX == -1 || isFacingLeft);
	}

	protected void UpdateSprite()
	{
		if (isFacingLeft)
			player.sprite.flipX = true;
		else
			player.sprite.flipX = false;
	}

	protected void Grounding()
	{
		bool grounded = Physics2D.OverlapCircleNonAlloc(player.transform.position + new Vector3(0, player.groundOffset), player.groundRadius, ground, player.groundMask) > 0;

		if (!isGrounded && grounded)
		{
			isGrounded = true;
			player.anim.SetBool("Grounded", true);
			player.anim.SetBool("Fall", false);
			player.anim.SetBool("Jump", false);
			player.playerAudio.PlayGroundSound();
		}
		else if (isGrounded && !grounded)
		{
			isGrounded = false;
			player.anim.SetBool("Grounded", false);
		}
	}

	private void Falling()
	{
		if (player.rb2D.velocity.y < player.jumpVelocityFalloff || player.rb2D.velocity.y > 0 && (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.Z)))
		{
			player.rb2D.velocity += player.fallMultiplier * Physics.gravity.y * Vector2.up * Time.deltaTime;
		}
	}
}
