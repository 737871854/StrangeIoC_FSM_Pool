/*
 * Copyright (c) 
 * 
 * 文件名称：   ChaseState.cs
 * 
 * 简    介:    追逐状态
 * 
 * 创建标识：   Mike 2017/4/3 17:53:20
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;

public class ChaseState : FSMState
{
    private GameObject npc;
    private Rigidbody npcRb;
    private GameObject player;

    public ChaseState(GameObject npc, GameObject player)
    {
        stateID         = StateID.Chase;
        this.npc        = npc;
        this.player     = player;
        this.npcRb      = npc.GetComponent<Rigidbody>();
    }

    public override void DoBeforeEntering()
    {
        Debug.Log("Entering state " + ID);
    }

    public override void DoUpdate()
    {
        CheckTransition();
        ChaseMove();
    }

    private void CheckTransition()
    {
        if (Vector3.Distance(npc.transform.position, player.transform.position) > 10)
        {
            fsm.PerformTransition(Transition.LostPlayer);
        }
    }

    private void ChaseMove()
    {
        npcRb.velocity = npc.transform.forward * 5;
        Vector3 targetPosition = player.transform.position;
        targetPosition.y = npc.transform.position.y;
        npc.transform.LookAt(targetPosition);
    }
}
