/*
 * Copyright (c) 
 * 
 * 文件名称：   AudioWindowEditor.cs
 * 
 * 简    介:    音效管理面板
 * 
 * 创建标识：   Mike 2017/4/1 15:12:10
 * 
 * 修改描述：
 * 
 */



using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class AudioWindowEditor : EditorWindow
{
    [MenuItem("Manager/AudioManager")]
    static void CreateWindow()
    {
        AudioWindowEditor window = EditorWindow.GetWindow<AudioWindowEditor>("音效管理");
        window.Show();
    }

    private string audioName;
    private string audioPath;
    private Dictionary<string, string> audioDic = new Dictionary<string, string>();

    void Awake()
    {
        LoadAudioList();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("音效名称");
        EditorGUILayout.LabelField("音效路径");
        EditorGUILayout.LabelField("操作");
        EditorGUILayout.EndHorizontal();

        foreach(string key in audioDic.Keys)
        {
            string value;
            audioDic.TryGetValue(key, out value);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(key);
            EditorGUILayout.LabelField(value);
            if(GUILayout.Button("删除"))
            {
                audioDic.Remove(key);
                SaveAudioList();
                return;
            }
            EditorGUILayout.EndHorizontal();
        }

        audioName = EditorGUILayout.TextField("音效名字", audioName);
        audioPath = EditorGUILayout.TextField("音效路径", audioPath);
        if(GUILayout.Button("添加音效"))
        {
            object o = Resources.Load(audioPath);
            if (null == o)
            {
                Debug.LogWarning("音效不存在于" + audioPath + " 添加不成功");
                audioPath = "";
            }
            else
            {
                if (audioDic.ContainsKey(audioName))
                    Debug.Log("名字已存在，请修改");
                else
                {
                    audioDic.Add(audioName, audioPath);
                    SaveAudioList();
                }
            }
        }
    }

    /// <summary>
    /// 保存音效信息
    /// </summary>
    private void SaveAudioList()
    {
        StringBuilder sb = new StringBuilder();
        // TODO 后面要改成Json格式
        foreach (string key in audioDic.Keys)
        {
            string value;
            audioDic.TryGetValue(key, out value);
            sb.Append(key + "," + value + "\n");

        }

        File.WriteAllText(AudioManager.AudioTextPath, sb.ToString());

        AssetDatabase.Refresh();
    }

    private void LoadAudioList()
    {
        audioDic.Clear();

        if (!File.Exists(AudioManager.AudioTextPath))
            return;

        string[] lines = File.ReadAllLines(AudioManager.AudioTextPath);
        foreach(string line in lines)
        {
            if (string.IsNullOrEmpty(line)) continue;
            string[] keyvalue = line.Split(',');
            audioDic.Add(keyvalue[0], keyvalue[1]);
        }

    }
}
