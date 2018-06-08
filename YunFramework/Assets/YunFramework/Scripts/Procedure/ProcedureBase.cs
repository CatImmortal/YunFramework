using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Fsm;

namespace YunFramework.Procedure
{
    /// <summary>
    /// 流程基类
    /// </summary>
    public class ProcedureBase : FsmState<ProcedureCtrl>
    {

        public override void OnEnter(Fsm<ProcedureCtrl> fsm)
        {
            base.OnEnter(fsm);
            Debug.Log("进入流程：" + GetType().FullName);
        }

        public override void OnLeave(Fsm<ProcedureCtrl> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Debug.Log("离开流程：" + GetType().FullName);
        }

    }
}

