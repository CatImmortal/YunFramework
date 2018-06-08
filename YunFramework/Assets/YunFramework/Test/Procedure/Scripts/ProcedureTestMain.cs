using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Base;
using YunFramework.Procedure;
public class ProcedureTestMain : ScriptBase {

    protected override void Awake()
    {
        //添加入口流程
        ProcedureStart start = new ProcedureStart();
        ProcedureCtrl.Instance.AddProcedure(start);
        ProcedureCtrl.Instance.SetEntranceProcedure(start);

        //添加其他流程
        ProcedureCtrl.Instance.AddProcedure(new ProcedurePlay());
        ProcedureCtrl.Instance.AddProcedure(new ProcedureOver());

        //创建流程状态机
        ProcedureCtrl.Instance.CreateProceduresFsm();



    }

}
