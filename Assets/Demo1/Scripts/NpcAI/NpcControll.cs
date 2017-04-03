/*
 * Copyright (c) 
 * 
 * 文件名称：   NpcControll.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Mike 2017/4/3 17:52:17
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections;

public class NpcControll : MonoBehaviour
{
    private FSMSystem fsm;

    private GameObject player;

    public Transform[] wayPoints;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InitFSM();
    }

    /// <summary>
    /// 初始化状态机
    /// </summary>
    void InitFSM()
    {
        fsm = new FSMSystem();

        PatrolState partolState = new PatrolState(wayPoints,gameObject, player);
        partolState.AddTransition(Transition.SawPlayer, StateID.Chase);// 这里只有一个，如果还有其他转换条件，可以继续添加

        ChaseState chaseState = new ChaseState(gameObject, player);
        chaseState.AddTransition(Transition.LostPlayer, StateID.Patrol);

        fsm.AddState(partolState);
        fsm.AddState(chaseState);

        fsm.Start(StateID.Patrol);
    }

    void Update()
    {
        fsm.CurrentState.DoUpdate();
    }
}
