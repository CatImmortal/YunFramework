using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 轮询驱动器
/// </summary>
public class UpdateDriver : MonoBehaviour {

    /// <summary>
    /// 轮询器链表
    /// </summary>
    private LinkedList<IUpdater> _updaters = new LinkedList<IUpdater>();

    /// <summary>
    /// 等待注册的轮询器队列
    /// </summary>
    private Queue<IUpdater> _needRegistUpdaters = new Queue<IUpdater>();

    /// <summary>
    /// 注册轮询器
    /// </summary>
    private void RegistUpdater(IUpdater updater)
    {
        if (updater == null)
        {
            Debug.LogError("要注册的轮询器为空");
            return;
        }

        LinkedListNode<IUpdater> current = _updaters.First;

        //尝试找一个优先级比要注册的轮询器小的元素
        while (current != null)
        {
            if (updater.Priority > current.Value.Priority)
            {
                break;
            }

            current = current.Next;
        }

        if (current != null)
        {
            //找到了优先级更小的元素
            _updaters.AddBefore(current, updater);
        }
        else
        {
            _updaters.AddLast(updater);
        }

        //保证OnInit在OnUpdate前执行
        updater.OnInit();

        Debug.Log("注册了轮询器：" + updater.GetType().FullName);
    }

        /// <summary>
    /// 添加轮询器
    /// </summary>
    public T AddUpdater<T>(GameObject go = null) where T : class , IUpdater,new()
    {
        IUpdater updater = new T
        {
            GameObject = go
        };

        _needRegistUpdaters.Enqueue(updater);

        string name = " ";
        if (go != null)
        {
            name = go.name;
        }

        Debug.Log(string.Format("在游戏物体{0}上添加了轮询器{1}",name,updater.GetType().FullName));

        return updater as T;
    }

    /// <summary>
    /// 添加轮询器
    /// </summary>
    public void AddUpdater(IUpdater updater,GameObject go = null)
    {
        if (updater == null)
        {
            Debug.LogError("要添加的轮询器为空" );
            return;
        }

        updater.GameObject = go;

        _needRegistUpdaters.Enqueue(updater);

        string name = " ";
        if (go != null)
        {
            name = go.name;
        }

        Debug.Log(string.Format("在游戏物体{0}上添加了轮询器{1}", name, updater.GetType().FullName));
    }

    /// <summary>
    /// 获取轮询器
    /// </summary>
    public T GetUpdater<T>(GameObject go = null) where T : class , IUpdater
    {

        Type updaterType = typeof(T);

        foreach (IUpdater updater in _updaters)
        {
            if (updater.GameObject == go && updater.GetType() == updaterType)
            {
                return updater as T;
            }
        }

        Debug.LogError(string.Format("未在游戏物体{0}上获取到轮询器{1}",go.name,updaterType.FullName));
        return null;
    }

    /// <summary>
    /// 获取多个轮询器
    /// </summary>
    public List<T> GetUpdaters<T>(GameObject go = null) where T : class , IUpdater
    {
        Type updaterType = typeof(T);

        List<T> updaters = new List<T>();

        foreach (IUpdater updater in _updaters)
        {
            if (updater.GameObject == go && updater.GetType() == updaterType)
            {
                updaters.Add(updater as T);
            }
        }

        if (updaters.Count == 0)
        {
            Debug.LogError(string.Format("未在游戏物体{0}上获取到轮询器{1}", go.name, updaterType.FullName));
            return null;
        }

        return updaters;

    }

    /// <summary>
    /// 销毁游戏物体
    /// </summary>
    public void Destroy(GameObject go)
    {
        if (go == null)
        {
            Debug.LogError("要销毁的游戏物体为空");
            return;
        }

        LinkedListNode<IUpdater> current = _updaters.First;
        while (current != null)
        {
            IUpdater temp = current.Value;
            current = current.Next;

            if (temp.GameObject == go)
            {
                _updaters.Remove(temp);
            }
        }

        UnityEngine.Object.Destroy(go);
        
    }

    /// <summary>
    /// 销毁轮询器
    /// </summary>
    public void Destroy(IUpdater updater)
    {
        if (updater == null)
        {
            Debug.LogError("要销毁的轮询器为空");
            return;
        }
        _updaters.Remove(updater);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //框架初始化
        FrameworkEntry.FrameworkInit(this);
    }

    private void Update()
    {
        while (_needRegistUpdaters.Count > 0)
        {
            IUpdater updater = _needRegistUpdaters.Dequeue();
            RegistUpdater(updater);
        }

        LinkedListNode<IUpdater> current = _updaters.First;
        while (current != null)
        {
            LinkedListNode<IUpdater> tempNode = current;
            current = current.Next;

            tempNode.Value.OnUpdate(Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        LinkedListNode<IUpdater> current = _updaters.First;
        while (current != null)
        {
            LinkedListNode<IUpdater> tempNode = current;
            current = current.Next;

            tempNode.Value.OnLateUpdate(Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        LinkedListNode<IUpdater> current = _updaters.First;
        while (current != null)
        {
            LinkedListNode<IUpdater> tempNode = current;
            current = current.Next;

            tempNode.Value.OnFixedUpdate(Time.deltaTime);
        }
    }

    private void OnApplicationQuit()
    {
        FrameworkEntry.IBoxDBCtrler.CloseIBoxDB();
    }


}
