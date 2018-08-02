using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Msg;
public class MsgReceiveCube : MonoBehaviour,IMsgReceiver {

    void Start()
    {
        this.RegisteMsg("HelloWorld", (x) => { Debug.Log(x); });
    }
}
