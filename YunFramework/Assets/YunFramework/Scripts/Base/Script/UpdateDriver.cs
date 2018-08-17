using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 轮询驱动器
/// </summary>
public class UpdateDriver : ScriptSingleton<UpdateDriver> {


    /// <summary>
    /// 轮询器链表
    /// </summary>
    LinkedList<IUpdater> _updaters = new LinkedList<IUpdater>();

    /// <summary>
    /// 注册轮询器
    /// </summary>
    /// <param name="updater"></param>
    public void RegistUpdateDriver(IUpdater updater)
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

        //保证Init在Update前执行
        updater.OnInit();

        if (current != null)
        {
            //找到了优先级更小的元素
            _updaters.AddBefore(current, updater);
        }
        else
        {
            _updaters.AddLast(updater);
        }

        

        Debug.Log("注册了轮询器：" + updater.GetType().FullName);
    }

    /// <summary>
    /// 获取轮询器
    /// </summary>
    public T GetUpdater<T>(GameObject go) where T : class , IUpdater
    {

        Type updaterType = typeof(T);

        if (go == null)
        {
            Debug.LogError("要获取轮询器"+ updaterType.FullName + "的游戏物体为空");
            return null;
        }

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
    public List<T> GetUpdaters<T>(GameObject go) where T : class , IUpdater
    {
        Type updaterType = typeof(T);

        if (go == null)
        {
            Debug.LogError("要获取轮询器" + updaterType.FullName + "的游戏物体为空");
            return null;
        }

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
    /// 添加轮询器
    /// </summary>
    public T AddUpdater<T>(GameObject go = null) where T : class , IUpdater,new()
    {
        IUpdater updater = new T
        {
            GameObject = go
        };
        RegistUpdateDriver(updater);

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

        RegistUpdateDriver(updater);

        string name = " ";
        if (go != null)
        {
            name = go.name;
        }

        Debug.Log(string.Format("在游戏物体{0}上添加了轮询器{1}", name, updater.GetType().FullName));
    }

    /// <summary>
    /// 销毁游戏物体
    /// </summary>
    /// <param name="go"></param>
    public void Destroy(GameObject go)
    {
        if (go == null)
        {
            Debug.LogError("要销毁的游戏物体为空");
            return;
        }

        List<IUpdater> updaters = new List<IUpdater>();
        foreach (IUpdater updater in _updaters)
        {
            if (updater.GameObject != null && updater.GameObject == go)
            {
                updaters.Add(updater);
            }
        }

        foreach (IUpdater updater in updaters)
        {
            _updaters.Remove(updater);
        }

        Destroy(go);
       
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
        Hotfixer.Instance.LoadHotfixDll();

        AddUpdater<FrameworkEntry>();
    }

    private void Update()
    {
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




}
