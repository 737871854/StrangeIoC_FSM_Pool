/*
 * Copyright (c) 
 * 
 * 文件名称：   PoolManager.cs
 * 
 * 简    介:    用于创建配置GameObjectPool文件
 * 
 * 创建标识：   Mike 2017/4/2 15:57:08
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PoolManagerEditor
{
    [MenuItem("Manager/Create GameObjectPoolConfig")]
    static void CreateGameObjectPoolList()
    {
        // 不仅只存在于内存当中，还存在与项目里面
        GameObjectPoolList poolList = ScriptableObject.CreateInstance<GameObjectPoolList>();
        AssetDatabase.CreateAsset(poolList, PoolManager.Instance.PoolConifgPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
