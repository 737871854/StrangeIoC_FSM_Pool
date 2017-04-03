/*
 * Copyright (c) 
 * 
 * 文件名称：   FSMSystem.cs
 * 
 * 简    介:    状态机管理类，有限状态机系统类 （好处是状态与状态之间是独立的）
 * 
 * 创建标识：   Mike 2017/4/3 17:28:26
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;

public class FSMSystem
{
    /// <summary>
    /// 保存当前状态机下面有哪些状态
    /// </summary>
    private Dictionary<StateID, FSMState> states;

    /// <summary>
    /// 状态机处于什么状态
    /// </summary>
    private FSMState currentState;
    public FSMState CurrentState
    {
        get
        {
            return currentState;
        }
    }

    public FSMSystem()
    {
        states = new Dictionary<StateID, FSMState>();
    }

    /// <summary>
    /// 往状态机里面添加状态
    /// </summary>
    /// <param name="state"></param>
    public void AddState(FSMState state)
    {
        if (state == null)
        {
            Debug.Log("The state you want to add is null");
            return;
        }

        if (states.ContainsKey(state.ID))
        {
            Debug.LogError("The state " + state.ID + " you want to add has already been added.");
            return;
        }

        state.fsm = this;
        states.Add(state.ID, state);
    }

    /// <summary>
    /// 从状态机里面移除状态
    /// </summary>
    /// <param name="state"></param>
    public void RemoveState(FSMState state)
    {
        if (state == null)
        {
            Debug.Log("The state you want to remvoe is null");
            return;
        }

        if (!states.ContainsKey(state.ID))
        {
            Debug.LogError("The state " + state.ID + " you want to remove is not exit in map.");
            return;
        }

        states.Remove(state.ID);
    }

    /// <summary>
    /// 执行状态切换
    /// </summary>
    /// <param name="trans"></param>
    public void PerformTransition(Transition trans)
    {
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("NullTransition is not allowed for a realy transition.");
            return;
        }

        StateID id = currentState.GetOutputState(trans);
        if (id == StateID.NullStateID)
        {
            Debug.Log("Transition is not to be happend!没有符合条件的转换");
            return;
        }

        FSMState state;
        states.TryGetValue(id, out state);
        currentState.DoBeforeLeaving();
        currentState = state;
        currentState.DoBeforeEntering();
    }

    /// <summary>
    /// 启动状态机
    /// </summary>
    public void Start(StateID id)
    {
        FSMState state;
        bool isGet = states.TryGetValue(id, out state);
        if (isGet)
        {
            state.DoBeforeEntering();
            currentState = state;
        }
        else
        {
            Debug.LogError("The state " + id + " is not exit in the fsm.");
        }
    }
}
