using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Base;
public class SingletonCompnentTest{

    public static SingletonCompnentTest Instance
    {
        get
        {
            return SingletonComponent<SingletonCompnentTest>.Instance;
        }
    }

    public void Test() {
        
    }

    private SingletonCompnentTest() { }
	
}
