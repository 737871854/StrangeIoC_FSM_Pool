/*
 * Copyright (c) 
 * 
 * 文件名称：   PatrolState.cs
 * 
 * 简    介:    巡逻状态
 * 
 * 创建标识：   Mike 2017/4/3 17:52:49
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;

public class PatrolState : FSMState
{
    private Transform[] wayPoints;
    private GameObject npc;
    private GameObject player;
    private Rigidbody npcRb;
    private int targetWayPoint;

    public PatrolState(Transform[] wp, GameObject npc, GameObject player)
    {
        this.stateID        = StateID.Patrol;
        this.wayPoints      = wp;
        this.npc            = npc;
        this.player         = player;
        this.npcRb          = npc.GetComponent<Rigidbody>();
        this.targetWayPoint = 0;
    }

    public override void DoBeforeEntering()
    {
        Debug.Log("Entering state " + ID);
    }

    public override void DoUpdate()
    {
        CheckTransition();

        PatrolMove();
    }

    /// <summary>
    /// 检查转换条件
    /// </summary>
    private void CheckTransition()
    {
        if (Vector3.Distance(npc.transform.position, player.transform.position) < 5)
        {
            fsm.PerformTransition(Transition.SawPlayer);
        }
    }

    private void PatrolMove()
    {
        npcRb.velocity = npc.transform.forward * 3;
        Transform targetTrans = wayPoints[this.targetWayPoint];
        Vector3 targetPosition = targetTrans.position;
        targetPosition.y = npc.transform.position.y;
        npc.transform.LookAt(targetPosition);
        if (Vector3.Distance(npc.transform.position, targetPosition) < 0.1f)
        {
            ++this.targetWayPoint;
            this.targetWayPoint %= wayPoints.Length;
        }
    }
}
