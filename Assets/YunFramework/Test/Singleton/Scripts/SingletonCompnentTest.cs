using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
