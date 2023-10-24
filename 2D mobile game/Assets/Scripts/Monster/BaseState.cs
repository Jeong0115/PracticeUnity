using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected Monster monster;

    protected BaseState(Monster _monster)
    {
        monster = _monster;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit(); 
}
