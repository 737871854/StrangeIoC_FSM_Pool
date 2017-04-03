/*
 * Copyright (c) 
 * 
 * 文件名称：   PoolManager.cs
 * 
 * 简    介:    对象池管理
 * 
 * 创建标识：   Mike 2017/4/2 16:19:47
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;

public class PoolManager
{
    private static PoolManager _instance;

    private static readonly object _object = new object();

    public static PoolManager Instance
    {
        get
        {
            if (null == _instance)
                lock (_object)
                    if (null == _instance)
                        _instance = new PoolManager();
            return _instance;
        }
    }

    private static string poolConfigPathPrefix = "Assets\\Resources\\";
    private static string poolConifgPathMiddle = "gameobjectpool";
    private static string poolConfigPathPostfix = ".asset";

    public string PoolConifgPath
    {
        get
        {
            return poolConfigPathPrefix + poolConifgPathMiddle + poolConfigPathPostfix;
        }
    }

    /// <summary>
    /// 对象池字典
    /// </summary>
    private Dictionary<string, GameObjectPool> poolDic = new Dictionary<string, GameObjectPool>();

    private PoolManager()
    {
        // 初始化
        LoadPoolConfig();
    }

    public void Init()
    {
        // DO Noting 只是实现PoolManager的的构造
    }

    /// <summary>
    /// 加载对象池配置文件
    /// </summary>
    private void LoadPoolConfig()
    {
        GameObjectPoolList poolList = Resources.Load<GameObjectPoolList>(poolConifgPathMiddle);
        foreach(GameObjectPool pool in poolList.poolList)
        {
            poolDic.Add(pool.name, pool);
        }
    }

    /// <summary>
    /// 从对象池中获取指定的对象
    /// </summary>
    /// <param name="poolName"></param>
    /// <returns></returns>
    public GameObject GetInstance(string poolName)
    {
        GameObjectPool pool;
        if (poolDic.TryGetValue(poolName, out pool))
        {
            return pool.GetInstance();
        }

        Debug.LogWarning("Pool: " + pool.name + "is not exits!");
        return null;
    }
}
