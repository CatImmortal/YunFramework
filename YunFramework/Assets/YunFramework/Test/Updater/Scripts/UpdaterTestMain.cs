using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Load;


public class UpdaterTestMain : IUpdater
{
    public GameObject GameObject { get; set; }


    public int Priority
    {
        get
        {
            return 1;
        }
    }
    public void OnInit()
    {
        //请到FrameworkEntry中解开测试代码的注释
        GameObject = ResLoader.Instance.LoadGameObject("UpdaterCube");
        Debug.Log(666);
    }

    public void OnFixedUpdate(float deltaTime)
    {
        
    }



    public void OnLateUpdate(float deltaTime)
    {
        
    }

    public void OnUpdate(float deltaTime)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        GameObject.transform.Translate(new Vector3(h, 0, v) * deltaTime * 5);
    }

    public void OnDestroy()
    {
       
    }
}
