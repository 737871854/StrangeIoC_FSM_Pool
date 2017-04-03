/*
 * Copyright (c) 
 * 
 * 文件名称：   AudioManager.cs
 * 
 * 简    介:    音效管理
 * 
 * 创建标识：   Mike 2017/4/2 10:12:28
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;

public class AudioManager
{
    private static string audioTextPathPrefix = Application.dataPath + "\\Resources\\";
    private static string audioTextPathMiddle = "aduiolist";
    private static string audioTextPathPostfix = ".txt";

    public static string AudioTextPath
    {
        get
        {
            return audioTextPathPrefix + audioTextPathMiddle + audioTextPathPostfix;
        }
    }

    private Dictionary<string, AudioClip> audioClipDic = new Dictionary<string, AudioClip>();

    // 是否禁音
    public bool isMute = false;

    // StringaleIoc 注入，构造函数使用反射创建的，此够着函数不能使用
    //public AudioManager()
    //{
    //    LoadAudioClip();
    //}

    public void Init()
    {
        LoadAudioClip();
    }

    private void LoadAudioClip()
    {
        audioClipDic.Clear();
        TextAsset ta = Resources.Load<TextAsset>(audioTextPathMiddle);
        string[] lines = ta.text.Split('\n');
        foreach(string line in lines)
        {
            if (string.IsNullOrEmpty(line)) continue;
            string[] keyvalue   = line.Split(',');
            string key          = keyvalue[0];
            AudioClip value     = Resources.Load<AudioClip>(keyvalue[1]);
            audioClipDic.Add(key, value);
        }
    }

    public void Play(string name)
    {
        if (isMute)
            return;

        AudioClip ac;
        audioClipDic.TryGetValue(name, out ac);
        if (null != ac)
            AudioSource.PlayClipAtPoint(ac, Vector3.zero);
    }

    public void Play(string name, Vector3 pos)
    {
        if (isMute)
            return;

        AudioClip ac;
        audioClipDic.TryGetValue(name, out ac);
        if (null != ac)
            AudioSource.PlayClipAtPoint(ac, pos);
    }

}
