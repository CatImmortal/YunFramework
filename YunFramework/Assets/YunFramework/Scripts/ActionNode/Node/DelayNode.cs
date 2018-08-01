using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YunFramework.ActionNode
{
    /// <summary>
    /// 延时执行结点
    /// </summary>
    public class DelayNode : ActionNodeBase
    {
        /// <summary>
        /// 延时秒数
        /// </summary>
        public float _delayTime;

        /// <summary>
        /// 当前计时秒数
        /// </summary>
        private float _currentSeconds = 0.0f;

        public DelayNode(float delayTime, UnityAction onEndCallback = null)
        {
            _delayTime = delayTime;
            _onEndCallback = onEndCallback;
        }

        #region 结点生命周期

        protected override void OnReset()
        {
            _currentSeconds = 0.0f;
        }

        protected override void OnExecute(float deltaTime)
        {
            _currentSeconds += deltaTime;
            Finished = _currentSeconds >= _delayTime;
        }
        #endregion

    }
}

