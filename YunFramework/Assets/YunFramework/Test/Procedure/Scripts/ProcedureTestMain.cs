using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Procedure;
public class ProcedureTestMain : MonoBehaviour {

    void Awake()
    {
        //添加入口流程
        ProcedureStart start = new ProcedureStart();
        FrameworkEntry.ProcedureCtrler.AddProcedure(start);
        FrameworkEntry.ProcedureCtrler.SetEntranceProcedure(start);

        //添加其他流程
        FrameworkEntry.ProcedureCtrler.AddProcedure(new ProcedurePlay());
        FrameworkEntry.ProcedureCtrler.AddProcedure(new ProcedureOver());

        //创建流程状态机
        FrameworkEntry.ProcedureCtrler.CreateProceduresFsm();



    }

}
