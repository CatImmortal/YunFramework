using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Tools;
public class DelayTaskTestMain : ScriptBase {

    protected override void Start()
    {
        for (int i = 1; i < 11; i++)
        {
            int index = i;
            DelayTaskCtrl.Instance.AddDelayTask(index, () => Debug.Log("DelayTask" + index));
        }

        DelayTaskCtrl.Instance.AddDelayTask(1, () => Debug.Log("RepeatDelayTask"),true);



    }




}
