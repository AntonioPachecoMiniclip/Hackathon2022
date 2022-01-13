using UnityEngine;

public abstract class GameState
{
    protected GameStateMachine gameStateMachine;
    protected float totalDuration = 1;
    private float currentDuration;
    
    public virtual void Setup(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
    }

    public virtual void OnEnter()
    {
        currentDuration = 0;
    }

    public virtual void OnUpdate()
    {
        currentDuration += Time.deltaTime;
        
        if (currentDuration >= 10)
            OnTimeEnded();
    }

    protected abstract void OnTimeEnded();

    public virtual void OnLeave() { }
}
