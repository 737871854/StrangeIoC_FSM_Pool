using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine.UI;
using strange.extensions.dispatcher.eventdispatcher.api;

public class CubeView : View {
    [Inject]
    public IEventDispatcher dispatcher { get; set; }

    private Text scoreText;
    /// <summary>
    /// 做初始化
    /// </summary>
    public void Init()
    {
        scoreText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        transform.Translate(new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2))*.2f);
    }

    void OnMouseDown()
    {
        //加分
        Debug.Log("OnMouseDown");
        dispatcher.Dispatch(Demo1MediatorEvent.ClickDown);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}