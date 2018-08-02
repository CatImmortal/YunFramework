using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Msg;
public class MsgTestMain : MonoBehaviour {

    void Start()
    {
        GameObject go = GameObject.Find("Button");
        EventTriggerListener listener = EventTriggerListener.GetListener(go);
        listener._onPointerClick += (x) => { Debug.Log("Button Click"); };

        //transform.ChainSetParent(go.transform)
        //        .ChainSetPosition(Vector3.zero)
        //        .ChainSetLocalEulerAngles(Vector3.up * 90)
        //        .ChainSetLocalScale(Vector3.one * 2);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MsgDispatcher.SendMsg("HelloWorld", "HelloWorld");
        }
    }
}
