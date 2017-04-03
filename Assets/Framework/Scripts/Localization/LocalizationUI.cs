/*
 * Copyright (c) 
 * 
 * 文件名称：   LocalizationUI.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Mike 2017/4/3 16:18:50
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LocalizationUI : MonoBehaviour
{
    public string key;

    void Start()
    {
        string value = LocalizationManager.Instance.GetValue(key);
        GetComponent<Text>().text = value;
    }
}
