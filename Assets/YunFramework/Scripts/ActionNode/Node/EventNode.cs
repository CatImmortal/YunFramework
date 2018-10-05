using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace YunFramework.ActionNode
{
    /// <summary>
    /// 事件执行结点
    /// </summary>
    public class EventNode : ActionNodeBase
    {
        /// <summary>
        /// 结点执行时的回调
        /// </summary>
        private UnityAction _onExecuteEvent;

        public EventNode(params UnityAction[] onExecuteEvents)
        {
            foreach (UnityAction action in onExecuteEvents)
            {
                _onExecuteEvent += action;
            }
        }

        #region 结点生命周期

        protected override void OnExecute(float deltaTime)
        {
            if (_onExecuteEvent != null)
            {
                _onExecuteEvent();
            }

            Finished = true;
        }

        #endregion


    }
}

