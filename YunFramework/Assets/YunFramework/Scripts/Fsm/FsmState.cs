using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunFramework.Fsm
{
    /// <summary>
    /// 状态机事件的处理方法模板
    /// </summary>
    public delegate void FsmEventHandler<T>(Fsm<T> fsm, object sender, object userData) where T : class;

    /// <summary>
    /// 状态基类
    /// </summary>
    /// <typeparam name="T">状态持有者类型</typeparam>
    public abstract class FsmState<T> where T : class
    {
        /// <summary>
        /// 状态订阅的事件字典
        /// </summary>
        private Dictionary<int, FsmEventHandler<T>> _eventHandlers;

        public FsmState()
        {
            _eventHandlers = new Dictionary<int, FsmEventHandler<T>>();
        }

        #region 状态生命周期
        /// <summary>
        /// 状态机状态初始化时调用。
        /// </summary>
        /// <param name="fsm">状态机引用。</param>
        public virtual void OnInit(Fsm<T> fsm)
        {

        }

        /// <summary>
        /// 状态机状态进入时调用。
        /// </summary>
        /// <param name="fsm">状态机引用。</param>
        public virtual void OnEnter(Fsm<T> fsm)
        {

        }

        /// <summary>
        /// 状态机状态轮询时调用。
        /// </summary>
        /// <param name="fsm">状态机引用。</param>
        public virtual void OnUpdate(Fsm<T> fsm, float elapseSeconds, float realElapseSeconds)
        {

        }

        /// <summary>
        /// 状态机状态离开时调用。
        /// </summary>
        /// <param name="fsm">状态机引用。</param>
        /// <param name="isShutdown">是关闭状态机时触发。</param>
        public virtual void OnLeave(Fsm<T> fsm, bool isShutdown)
        {

        }

        /// <summary>
        /// 有限状态机状态销毁时调用。
        /// </summary>
        /// <param name="fsm">状态机引用。</param>
        public virtual void OnDestroy(Fsm<T> fsm)
        {
            _eventHandlers.Clear();
        }
        #endregion

        #region 状态切换的操作
        /// <summary>
        /// 切换当前状态机状态。
        /// </summary>
        /// <typeparam name="TState">要切换到的状态机状态类型。</typeparam>
        /// <param name="fsm">状态机引用。</param>
        protected void ChangeState<TState>(Fsm<T> fsm) where TState : FsmState<T>
        {
            if (fsm == null)
            {
                Debug.Log("需要切换状态的状态机为空，无法切换");
            }

            fsm.ChangeState<TState>();
        }
        #endregion

        #region 状态机事件的操作
        /// <summary>
        /// 订阅状态机事件。
        /// </summary>
        protected void SubscribeEvent(int eventId, FsmEventHandler<T> eventHandler)
        {
            if (eventHandler == null)
            {
                Debug.LogError("状态机事件处理方法为空，无法订阅状态机事件");
            }

            if (!_eventHandlers.ContainsKey(eventId))
            {
                _eventHandlers[eventId] = eventHandler;
            }
            else
            {
                _eventHandlers[eventId] += eventHandler;
            }
        }

        /// <summary>
        /// 取消订阅状态机事件。
        /// </summary>
        protected void UnsubscribeEvent(int eventId, FsmEventHandler<T> eventHandler)
        {
            if (eventHandler == null)
            {
                Debug.LogError("状态机事件处理方法为空，无法取消订阅状态机事件");
            }

            if (_eventHandlers.ContainsKey(eventId))
            {
                _eventHandlers[eventId] -= eventHandler;
            }
        }

        /// <summary>
        /// 响应状态机事件。
        /// </summary>
        public void OnEvent(Fsm<T> fsm, object sender, int eventId, object userData)
        {
            FsmEventHandler<T> eventHandlers = null;
            if (_eventHandlers.TryGetValue(eventId, out eventHandlers))
            {
                if (eventHandlers != null)
                {
                    eventHandlers(fsm, sender, userData);
                }
            }
        }
        #endregion
    }
}

