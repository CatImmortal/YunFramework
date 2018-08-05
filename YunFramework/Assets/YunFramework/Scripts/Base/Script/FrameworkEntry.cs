using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Fsm;

/// <summary>
/// 框架入口
/// </summary>
public class FrameworkEntry : IUpdater
{
    public GameObject GO { get; set; }


    public int Priority
    {
        get
        {
            return int.MaxValue;
        }
    }

    public void OnInit()
    {
        //TODO:添加框架各模块的轮询器
        UpdateDriver.Instance.AddUpdater(FsmCtrler.Instance);
        UpdateDriver.Instance.AddUpdater(ThreadCrossHelper.Instance);

        //TODO:添加测试代码的轮询器
        //UpdateDriver.Instance.AddUpdater<UpdaterTestMain>();
        //UpdateDriver.Instance.AddUpdater<ActionNodeTestMain>();
        
    }


    public void OnFixedUpdate(float deltaTime)
    {
        
    }


    public void OnLateUpdate(float deltaTime)
    {
        
    }

    public void OnUpdate(float deltaTime)
    {
        
    }

    public void OnDestroy()
    {
        
    }
}
