using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Procedure;
public class ProcedureTestMain : MonoBehaviour {

    void Awake()
    {
        //添加入口流程
        ProcedureStart start = new ProcedureStart();
        ProcedureCtrler.Instance.AddProcedure(start);
        ProcedureCtrler.Instance.SetEntranceProcedure(start);

        //添加其他流程
        ProcedureCtrler.Instance.AddProcedure(new ProcedurePlay());
        ProcedureCtrler.Instance.AddProcedure(new ProcedureOver());

        //创建流程状态机
        ProcedureCtrler.Instance.CreateProceduresFsm();



    }

}
