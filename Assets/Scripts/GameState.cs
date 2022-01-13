using UnityEngine;

public abstract class GameState
{
    protected GameStateMachine gameStateMachine;
    protected GameManager gameManager;
    
    protected float totalDuration = 1;
    private float currentDuration;
    
    public virtual void Setup(GameStateMachine gameStateMachine, GameManager gameManager)
    {
        this.gameStateMachine = gameStateMachine;
        this.gameManager = gameManager;
    }

    public virtual void OnEnter()
    {
        currentDuration = 0;
    }

    public virtual void OnUpdate()
    {
        currentDuration += Time.deltaTime;
        
        if (currentDuration >= totalDuration)
            OnTimeEnded();
    }

    protected abstract void OnTimeEnded();

    public virtual void OnLeave() { }
}
