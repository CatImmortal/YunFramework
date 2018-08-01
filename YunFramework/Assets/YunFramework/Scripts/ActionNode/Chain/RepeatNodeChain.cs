using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunFramework.ActionNode
{
    /// <summary>
    /// 重复执行结点链
    /// </summary>
    public class RepeatNodeChain : NodeChainBase
    {
        /// <summary>
        /// 重复执行结点
        /// </summary>
        private RepeatNode _repeatAction;

        /// <summary>
        /// 序列执行结点
        /// </summary>
        private SequenceNode _sequenceNode;

        /// <summary>
        /// 结点链所属结点
        /// </summary>
        protected override ActionNodeBase Node
        {
            get
            {
                return _repeatAction;
            }
        }

        public RepeatNodeChain(int repeatCount)
        {
            _sequenceNode = new SequenceNode();
            _repeatAction = new RepeatNode(_sequenceNode, repeatCount);
        }

        /// <summary>
        /// 追加结点到结点链中
        /// </summary>
        public override NodeChainBase Append(ActionNodeBase node)
        {
            _sequenceNode.Append(node);
            return this;
        }
    }
}

