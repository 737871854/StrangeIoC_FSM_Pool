/*
 * Copyright (c) 
 * 
 * 文件名称：   PoolManager.cs
 * 
 * 简    介:    对象池
 * 
 * 创建标识：   Mike 2017/4/2 11:04:59
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameObjectPool
{
    /// <summary>
    /// 名字
    /// </summary>
    public string name;
    /// <summary>
    /// 预制体
    /// </summary>
    [SerializeField]
    private GameObject prefab;
    /// <summary>
    /// 最大数量
    /// </summary>
    [SerializeField]
    private int maxAmount;

    /// <summary>
    /// 存放对象池所有该对象
    /// </summary>
    private List<GameObject> goList = new List<GameObject>();

    /// <summary>
    /// 表示从资源池中获取一个实例
    /// </summary>
    public GameObject GetInstance()
    {
        foreach(GameObject go in goList)
        {
            if (!go.activeInHierarchy)
            {
                go.SetActive(true);
                return go;
            }
        }

        // 超过最大数量，移除最老的那个   ？？如果所有的对象都正被使用，这样做对吗？？？？  个人觉得这样不好，使用2个List，一个负责存放未被使用的对象，一个负责存放正在使用的对象
        if (goList.Count >= maxAmount)
        {
            GameObject go = goList[0];
            goList.RemoveAt(0);
            GameObject.Destroy(go);
        }

        GameObject temp = GameObject.Instantiate(prefab) as GameObject;
        goList.Add(temp);

        return temp;
    }
}
