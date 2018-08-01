using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunFramework.ActionNode
{
    /// <summary>
    /// 序列执行结点
    /// </summary>
    public class SequenceNode : ActionNodeBase
    {

        /// <summary>
        /// 所有子结点列表
        /// </summary>
        protected List<ActionNodeBase> _nodes = new List<ActionNodeBase>();

        /// <summary>
        /// 需要执行的子结点列表
        /// </summary>
        protected List<ActionNodeBase> _excutingNodes = new List<ActionNodeBase>();

        /// <summary>
        /// 序列是否已执行完毕
        /// </summary>
        public bool _completed = false;

        /// <summary>
        /// 子结点总个数
        /// </summary>
        public int TotalCount
        {
            get
            {
                return _excutingNodes.Count;
            }
        }

        public SequenceNode(params ActionNodeBase[] nodes)
        {
            foreach (ActionNodeBase node in nodes)
            {
                this._nodes.Add(node);
                _excutingNodes.Add(node);
            }
        }

        /// <summary>
        /// 追加新的子结点到序列结点中
        /// </summary>
        public SequenceNode Append(ActionNodeBase appendedNode)
        {
            _nodes.Add(appendedNode);
            _excutingNodes.Add(appendedNode);
            return this;
        }

        #region 结点生命周期
        protected override void OnReset()
        {
            _excutingNodes.Clear();
            foreach (ActionNodeBase node in _nodes)
            {
                node.Reset();
                _excutingNodes.Add(node);
            }
            _completed = false;
        }

        protected override void OnExecute(float deltaTime)
        {
            if (_excutingNodes.Count > 0)
            {
                //执行第一个子结点
                while (_excutingNodes[0].Execute(deltaTime))
                {
                    _excutingNodes.RemoveAt(0);
                    if (_excutingNodes.Count == 0)
                    {
                        break;
                    }
                }
            }

            //序列结点是否执行完毕，是根据其子结点是否全部执行完毕来判断的
            Finished = _excutingNodes.Count == 0;
        }
        #endregion



    }
}


