using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Base;
public class ScriptSingletonTest : ScriptSingleton<ScriptSingletonTest> {

    public void Test() {
        Debug.Log("ScriptSinletonTest");
    }
}
