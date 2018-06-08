using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Tools;
public class ReuseableCube : ReuseableObject
{
    public override void OnSpawn()
    {
        Debug.Log("从对象池取出了对象");
    }

    public override void OnUnspawn()
    {
        Debug.Log("从对象池回收了对象");
    }
}
