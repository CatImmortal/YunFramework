using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace YunFramework.ActionNode
{
    /// <summary>
    /// 条件执行结点（会一直尝试执行直到满足条件）
    /// </summary>
    public class UntilNode : ActionNodeBase
    {

        /// <summary>
        /// 执行完毕的条件
        /// </summary>
        private Func<bool> _condition;

        public UntilNode(Func<bool> condition)
        {
            _condition = condition;
        }

        #region 结点生命周期

        protected override void OnExecute(float deltaTime)
        {
            if (_condition != null)
            {
                Finished = _condition();
            }
        }

        #endregion
    }
}


