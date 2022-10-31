using UnityEngine;

public class PlayerState
{
    protected PlayerController player;

    public PlayerState(PlayerController i_playerController)
	{
        player = i_playerController;
	}

    public virtual void Enter()
	{

	}

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
	{

	}

    public virtual void StateUpdate()
	{

	}


}
