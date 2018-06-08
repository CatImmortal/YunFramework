using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Fsm;
using YunFramework.Procedure;
public class ProcedurePlay : ProcedureBase {

    public override void OnUpdate(Fsm<ProcedureCtrl> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (Input.GetMouseButtonDown(0))
        {
            ChangeState<ProcedureOver>(fsm);
        }
    }
}
