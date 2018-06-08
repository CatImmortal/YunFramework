using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Tools;
public class ObjectPoolTestMain : ScriptBase {

    private Stack<ReuseableCube> _cubes = new Stack<ReuseableCube>();

 

    protected override void Update()
    {
     

        if (Input.GetMouseButtonDown(0))
        {
            ReuseableCube cube = PoolCtrl.Instance.Spawn("Cube") as ReuseableCube;
            _cubes.Push(cube);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (_cubes.Count > 0)
            {
                PoolCtrl.Instance.Unspawn(_cubes.Pop(), "Cube");
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            PoolCtrl.Instance.RestorePool("Cube");
        }
        
    }
}
