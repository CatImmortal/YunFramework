using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionNodeTestMain : IUpdater
{
    public GameObject GameObject { get; set; }


    public int Priority
    {
        get
        {
            return 1;
        }
    }

    public void OnDestroy()
    {
        
    }

    public void OnFixedUpdate(float deltaTime)
    {
        
    }

    public void OnInit()
    {
        //请到FrameWorkEntry中解开测试代码的注释
        this.Sequence()
            .Until(() => { return Input.GetKeyDown(KeyCode.Space); })
            .Delay(2.0f)
            .Event(() => { Debug.Log("延迟2秒"); })
            .Delay(1.0f)
            .Event(() => { Debug.Log("延迟1秒"); })
            .Until(() => { return Input.GetKeyDown(KeyCode.A); })
            .Event(() =>
            {
                this.Repeat(3)
                .Delay(0.5f)
                .Event(() => { Debug.Log("延迟0.5秒"); })
                .Begin();

            })
            .Begin();
    }

    public void OnLateUpdate(float deltaTime)
    {
        
    }

    public void OnUpdate(float deltaTime)
    {
        
    }
}
