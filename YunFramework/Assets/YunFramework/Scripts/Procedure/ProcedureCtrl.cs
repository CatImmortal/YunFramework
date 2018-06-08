using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Base;
using YunFramework.Fsm;
namespace YunFramework.Procedure
{
    /// <summary>
    /// 流程控制器
    /// </summary>
    public class ProcedureCtrl : Singleton<ProcedureCtrl>
    {
        #region 字段与属性

        /// <summary>
        /// 流程的状态机
        /// </summary>
        private Fsm<ProcedureCtrl> _procedureFsm;

        /// <summary>
        /// 所有流程的列表
        /// </summary>
        private List<ProcedureBase> _procedures;

        /// <summary>
        /// 入口流程
        /// </summary>
        private ProcedureBase _entranceProcedure;

        /// <summary>
        /// 当前流程
        /// </summary>
        public ProcedureBase CurrentProcedure
        {
            get
            {
                if (_procedureFsm == null)
                {
                    Debug.LogError("流程状态机为空，无法获取当前流程");
                }
                return (ProcedureBase)_procedureFsm.CurrentState;
            }
        }

        #endregion

        private ProcedureCtrl()
        {
            _procedureFsm = null;
            _procedures = new List<ProcedureBase>();
        }


        /// <summary>
        /// 添加流程
        /// </summary>
        public void AddProcedure(ProcedureBase procedure)
        {
            if (procedure == null)
            {
                Debug.LogError("要添加的流程为空");
                return;
            }
            _procedures.Add(procedure);
        }

        /// <summary>
        /// 设置入口流程
        /// </summary>
        /// <param name="procedure"></param>
        public void SetEntranceProcedure(ProcedureBase procedure)
        {
            _entranceProcedure = procedure;
        }

        /// <summary>
        /// 创建流程状态机
        /// </summary>
        public void CreateProceduresFsm()
        {
            _procedureFsm = FsmCtrl.Instance.CreateFsm(this,"", _procedures.ToArray());

            if (_entranceProcedure == null)
            {
                Debug.LogError("入口流程为空，无法开始流程");
                return;
            }

            //开始流程
            _procedureFsm.Start(_entranceProcedure.GetType());
        }

    }
}

