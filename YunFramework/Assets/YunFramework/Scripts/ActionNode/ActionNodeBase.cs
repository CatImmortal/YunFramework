using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YunFramework.ActionNode
{
    /// <summary>
    /// 执行结点基类
    /// </summary>
    public abstract class ActionNodeBase
    {
        /// <summary>
        /// 结点执行开始时的回调
        /// </summary>
        public UnityAction _onBeganCallback = null;

        /// <summary>
        /// 是否调用过结点执行开始时的回调
        /// </summary>
        protected bool _onBeginCalled = false;

        /// <summary>
        /// 结点执行结束时的回调
        /// </summary>
        public UnityAction _onEndCallback = null;

        /// <summary>
        /// 结点是否已执行完毕
        /// </summary>
        public bool Finished { get; protected set; }

        /// <summary>
        /// 结点结束
        /// </summary>
        public void Break()
        {
            Finished = true;
        }

        /// <summary>
        /// 基于协程的方式执行结点
        /// </summary>
        public IEnumerator Execute()
        {
            if (Finished)
            {
                Reset();
            }

            while (!Execute(Time.deltaTime))
            {
                yield return null;
            }
        }

        /// <summary>
        /// 结点执行
        /// </summary>
        public bool Execute(float deltaTime)
        {
            //调用结点开始执行时的回调
            if (!_onBeginCalled)
            {
                _onBeginCalled = true;
                OnBegin();
                if (_onBeganCallback != null)
                {
                    _onBeganCallback();
                }
            }

            //执行结点
            if (!Finished)
            {
                OnExecute(deltaTime);
            }

            //如果结点已执行完毕
            if (Finished)
            {
                OnEnd();
                if (_onEndCallback != null)
                {
                    _onEndCallback();
                }
            }

            return Finished;
        }

        /// <summary>
        /// 结点重置
        /// </summary>
        public void Reset()
        {
            Finished = false;
            _onBeginCalled = false;
            OnReset();
        }

        #region 结点生命周期

        protected virtual void OnReset()
        {
        }

        protected virtual void OnBegin()
        {
        }

        protected virtual void OnExecute(float deltaTime)
        {
        }

        protected virtual void OnEnd()
        {
        }

        #endregion
    }
}

