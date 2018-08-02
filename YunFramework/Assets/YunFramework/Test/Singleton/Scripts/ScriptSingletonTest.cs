using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScriptSingletonTest : ScriptSingleton<ScriptSingletonTest> {

    public void Test() {
        Debug.Log("ScriptSinletonTest");
    }
}
