/*
 * Copyright (c) 
 * 
 * 文件名称：   FSMState.cs
 * 
 * 简    介:    状态共性
 * 
 * 创建标识：   Mike 2017/4/3 17:02:53
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 状态转换条件
/// </summary>
public enum Transition
{
    NullTransition = 0,
    /// <summary>
    /// 看到主角
    /// </summary>
    SawPlayer,
    /// <summary>
    /// 看不到主角
    /// </summary>
    LostPlayer,
}

/// <summary>
/// 状态ID，每一个状态的唯一标识，一个状态只有一个stateid, 而且和其他的状态不可以重复
/// </summary>
public enum StateID
{
    NullStateID = 0,
    /// <summary>
    /// 巡逻状态
    /// </summary>
    Patrol,
    /// <summary>
    /// 追逐主角状体
    /// </summary>
    Chase,
}

public abstract class FSMState
{
    protected StateID stateID;
    public StateID ID
    {
        get { return stateID; }
    }

    // 存储在什么条件下，转换到什么状态
    private Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();

    public FSMSystem fsm;

    /// <summary>
    /// 添加转换状态
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="id"></param>
    public void AddTransition(Transition trans, StateID id)
    {
        if (trans == Transition.NullTransition || id == StateID.NullStateID)
        {
            Debug.LogError("Transition or StateID is null");
            return;
        }

        if (map.ContainsKey(trans))
        {
            Debug.LogError("State " + stateID + " is already has transition " + trans);
            return;
        }

        map.Add(trans, id);
    }

    /// <summary>
    /// 删除转换状态
    /// </summary>
    /// <param name="trans"></param>
    public void RemoveTransition(Transition trans)
    {
        if (!map.ContainsKey(trans))
        {
            Debug.Log("The transition " + trans + " you want to remove is not exit in map");
            return;
        }

        map.Remove(trans);
    }

    /// <summary>
    /// 根据传递的转换条件，判断是否可以发生转换
    /// </summary>
    /// <param name="trans"></param>
    public StateID GetOutputState(Transition trans)
    {
        if (map.ContainsKey(trans))
            return map[trans];

        return StateID.NullStateID;
    }

    /// <summary>
    /// 在进入当前状态之前需要做的事情
    /// </summary>
    public virtual void DoBeforeEntering() { }

    /// <summary>
    /// 在离开状态之前需要做的事情
    /// </summary>
    public virtual void DoBeforeLeaving() { }

    /// <summary>
    /// 在状态机处于当前状态时，会一直调用
    /// </summary>
    public abstract void DoUpdate();
}
