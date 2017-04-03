/*
 * Copyright (c) 
 * 
 * 文件名称：   DeactiveForTime.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Mike 2017/4/2 16:16:38
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections;

public class DeactiveForTime : MonoBehaviour
{

    void Start()
    {
        Invoke("Deactive", 5);
    }

    void Deactive()
    {
        gameObject.SetActive(false);
    }
}
