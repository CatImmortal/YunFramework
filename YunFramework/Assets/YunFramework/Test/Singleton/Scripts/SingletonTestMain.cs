using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SingletonTestMain : ScriptBase
    {

        protected override void Start()
        {
            ScriptSingletonTest.Instance.Test();
            SingletonCompnentTest.Instance.Test();
        }
    }


