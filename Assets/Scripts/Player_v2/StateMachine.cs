public class StateMachine
{
    public State currentState;

    public void Initialize(State startState)
	{
		currentState = startState;
		currentState.Enter();
	}

	public void ChangeState(State newState)
	{
		currentState.Exit();
		currentState = newState;
		currentState.Enter();
	}

	public void RunState()
	{
		currentState.HandleInput();
		currentState.StateUpdate();
	}

	public void RunState(State state)
	{
		state.HandleInput();
		state.StateUpdate();
	}
}
