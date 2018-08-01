using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YunFramework.Chain;

namespace YunFramework.ActionNode
{
    /// <summary>
    /// 结点链基类
    /// </summary>
    public abstract class NodeChainBase : ActionNodeBase
    {
        /// <summary>
        /// 结点链所属结点
        /// </summary>
        protected abstract ActionNodeBase Node { get; }

        /// <summary>
        /// 结点链执行者
        /// </summary>
        public MonoBehaviour Executer { get; set; }

        /// <summary>
        /// 追加结点到结点链中
        /// </summary>
        public abstract NodeChainBase Append(ActionNodeBase node);

        /// <summary>
        /// 开始执行结点链
        /// </summary>
        public void Begin()
        {
            Executer.ExecuteNode(this);
        }

        #region 结点生命周期

        protected override void OnExecute(float deltaTime)
        {
            Finished = Node.Execute(deltaTime);
        }

        #endregion

        #region 往结点链中追加结点
        /// <summary>
        /// 往结点链中追加延时结点
        /// </summary>
        public NodeChainBase Delay(float seconds, UnityAction onEndCallback = null)
        {
            return Append(new DelayNode(seconds, onEndCallback));
        }

        /// <summary>
        /// 往结点链中追加事件执行结点
        /// </summary>
        public NodeChainBase Event(params UnityAction[] onEvents)
        {
            return Append(new EventNode(onEvents));
        }

        /// <summary>
        /// 往结点链中追加条件执行结点
        /// </summary>
        public NodeChainBase Until(Func<bool> condition)
        {
            return Append(new UntilNode(condition));
        }
        #endregion


    }
}


