using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SingletonTestMain : MonoBehaviour
    {

        void Start()
        {
            ScriptSingletonTest.Instance.Test();
            SingletonCompnentTest.Instance.Test();
        }
    }


