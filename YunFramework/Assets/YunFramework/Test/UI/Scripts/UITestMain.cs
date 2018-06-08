using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.UI;
using YunFramework.Load;
using YunFramework.Config;
public class UITestMain : ScriptBase{

    protected override void Start()
    {

        UIManager.Instance.ConfiManager = new JsonConfiger("UITestJson");
        UIManager.Instance.Loader = ResourceLoader.Instance;
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            UIManager.Instance.ShowUIPanel("ButtonUI");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            UIManager.Instance.CloseUIPanel("ButtonUI");
        }
    }
}
