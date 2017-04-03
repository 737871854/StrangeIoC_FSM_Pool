/*
 * Copyright (c) 
 * 
 * 文件名称：   LocalizationManager.cs
 * 
 * 简    介:    本地化管理
 * 
 * 创建标识：   Mike 2017/4/3 16:06:05
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;

public class LocalizationManager
{
    private static LocalizationManager _instance;
    private static readonly object _object = new object();

    public static LocalizationManager Instance
    {
        get
        {
            if (null == _instance)
            {
                lock(_object)
                {
                    if (null == _instance)
                        _instance = new LocalizationManager();
                }
            }
            return _instance;
        }
    }

    private const string Chinese = "Localization/Chinese";
    private const string English = "Localization/English";

    public const string Language = English;

    private Dictionary<string, string> dict;

    public LocalizationManager()
    {
        dict = new Dictionary<string, string>();

        TextAsset text = Resources.Load<TextAsset>(Language);
        string[] lines = text.text.Split('\n');
        foreach(string line in lines)
        {
            if (string.IsNullOrEmpty(line)) continue;
            string[] keyvalue = line.Split('=');
            dict.Add(keyvalue[0], keyvalue[1]);
        }
    }

    public void Init()
    {
        // Do Nothing
    }

    public string GetValue(string key)
    {
        string value;
        dict.TryGetValue(key, out value);
        return value;
    }
}
