using UnityEngine;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;

public interface IScoreService  {
    void RequestScore(string url);//请求分数
    void OnReceiveScore();//收到服务器端发送过来的分数
    void UpdateScore(string url, int score);

    IEventDispatcher dispatcher { get; set; }
}