using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM 
{
    private BaseState curState;

    public FSM(BaseState state)
    {
        curState = state;
    }

    public void ChangeState(BaseState state)
    {
        if(state == curState)
        {
            return;
        }

        if(curState != null)
        {
            curState.OnStateExit();
        }

        curState = state;
        curState.OnStateEnter();
    }

    public void UpdateState()
    {
        if(curState != null)
        {
            curState.OnStateUpdate();
        }
    }
}
