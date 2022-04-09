using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    protected bool stateIsActive;

    protected Animator anim;
    protected GameObject player;
    
    public abstract void EnterState();
    public abstract void ExitState();
}