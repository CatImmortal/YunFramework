using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 线程交叉访问辅助器
/// </summary>
public class ThreadCrossHelper :Singleton<ThreadCrossHelper> , IUpdater
{
    public GameObject GO { get; set; }

    private object _currentCallLocker = new object();

    /// <summary>
    /// 委托队列
    /// </summary>
    private  Queue<UnityAction> _callBacks = new Queue<UnityAction>();
    
    public int Priority
    {
        get
        {
            return 1024;
        }
    }

    /// <summary>
    /// 在主线程中执行方法
    /// </summary>
    public void ExecuteOnMainThread(UnityAction callBack)
    {
        lock (_currentCallLocker)
        {
            _callBacks.Enqueue(callBack);
        }
    }

    public void OnUpdate(float deltaTime)
    {
        if (_callBacks.Count > 0)
        {
            UnityAction callBack = _callBacks.Dequeue();
            if (callBack != null)
            {
                callBack();
            }
        }
    }

    public void OnInit()
    {

    }

    public void OnLateUpdate(float deltaTime)
    {
        
    }

    public void OnDestroy()
    {

    }

    public void OnFixedUpdate(float deltaTime)
    {

    }



}
