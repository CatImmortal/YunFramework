using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunFramework.ActionNode
{
    /// <summary>
    /// 重复执行结点
    /// </summary>
    public class RepeatNode : ActionNodeBase
    {

        /// <summary>
        /// 要重复执行的结点
        /// </summary>
        private ActionNodeBase _node;

        /// <summary>
        /// 重复执行次数
        /// </summary>
        public int _repeatCount = 1;

        /// <summary>
        /// 当前已执行次数
        /// </summary>
        private int _curRepeatCount = 0;

        /// <summary>
        /// 是否已重复执行完毕
        /// </summary>
        public bool _completed = false;

        public RepeatNode(ActionNodeBase node, int repeatCount)
        {
            _node = node;
            _repeatCount = repeatCount;
        }

        #region 结点生命周期

        protected override void OnReset()
        {
            if (_node != null)
            {
                _node.Reset();
            }

            _curRepeatCount = 0;
            _completed = false;
        }
        protected override void OnExecute(float deltaTime)
        {
            //-1表示无限重复
            if (_repeatCount == -1)
            {
                if (_node.Execute(deltaTime))
                {
                    _node.Reset();
                }

                return;
            }

            if (_node.Execute(deltaTime))
            {
                _node.Reset();
                _curRepeatCount++;
            }

            if (_curRepeatCount == _repeatCount)
            {
                Finished = true;
                _completed = true;
            }
        }

        #endregion

    }
}


