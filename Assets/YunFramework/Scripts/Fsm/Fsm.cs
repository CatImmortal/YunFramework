using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunFramework.Fsm
{
    /// <summary>
    /// 状态机
    /// </summary>
    /// <typeparam name="T">状态机持有者类型</typeparam>
    public class Fsm<T> : IFsm where T:class
    {
        #region 字段与属性
        /// <summary>
        /// 状态机名字
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 状态机持有者
        /// </summary>
        public T Owner { get; private set; }

        /// <summary>
        /// 状态机里所有状态的字典
        /// </summary>
        private Dictionary<string, FsmState<T>> _states;

        /// <summary>
        /// 状态机里所有数据的字典
        /// </summary>
        private Dictionary<string, object> _datas;

        /// <summary>
        /// 状态机是否被销毁
        /// </summary>
        public bool IsDestroyed { get; private set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public FsmState<T> CurrentState { get; private set; }

        /// <summary>
        /// 当前状态运行时间
        /// </summary>
        public float CurrentStateTime { get; private set; }

        /// <summary>
        /// 获取状态机持有者类型
        /// </summary>
        public Type OwnerType
        {
            get
            {
                return typeof(T);
            }
        }
        #endregion

        #region 构造方法
        public Fsm(string name, T owner, params FsmState<T>[] states)
        {
            if (owner == null)
            {
                Debug.LogError("状态机持有者为空");
            }

            if (states == null || states.Length < 1)
            {
                Debug.LogError("状态机没有状态");
            }

            Name = name;
            Owner = owner;
            _states = new Dictionary<string, FsmState<T>>();
            _datas = new Dictionary<string, object>();

            foreach (FsmState<T> state in states)
            {
                if (state == null)
                {
                    Debug.LogError("要添加进状态机的状态为空");
                }

                string stateName = state.GetType().FullName;
                if (_states.ContainsKey(stateName))
                {
                    Debug.LogError("要添加进状态机的状态已存在：" + stateName);
                }

                _states.Add(stateName, state);
                state.OnInit(this);
            }

            CurrentStateTime = 0f;
            CurrentState = null;
            IsDestroyed = false;

        }
        #endregion

        #region 状态机状态的操作

        /// <summary>
        /// 获取状态机状态（知道明确的状态的实现类型时）
        /// </summary>
        /// <typeparam name="TState">要获取的状态机状态类型。</typeparam>
        /// <returns>要获取的状态机状态。</returns>
        public TState GetState<TState>() where TState : FsmState<T>
        {
            FsmState<T> state = null;
            if (_states.TryGetValue(typeof(TState).FullName, out state))
            {
                return (TState)state;
            }

            return null;
        }
        /// <summary>
        /// 获取状态机状态（只有状态的父类对象时）
        /// </summary>
        /// <param name="stateType">要获取的状态机状态类型。</param>
        /// <returns>要获取的状态机状态。</returns>
        public FsmState<T> GetState(Type stateType)
        {
            if (stateType == null)
            {
                Debug.LogError("要获取的状态为空");
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
            {
                Debug.LogError("要获取的状态"+ stateType .FullName+ "没有直接或间接的实现" + typeof(FsmState<T>).FullName);
            }

            FsmState<T> state = null;
            if (_states.TryGetValue(stateType.FullName, out state))
            {
                return state;
            }

            return null;
        }

        /// <summary>
        /// 开始状态机（知道明确的状态的实现类型时）
        /// </summary>
        /// <typeparam name="TState">开始的状态类型</typeparam>
        public void Start<TState>() where TState : FsmState<T>
        {
            if (CurrentState != null)
            {
                Debug.LogError("当前状态机已开始，无法再次开始");
            }

            FsmState<T> state = GetState<TState>();
            if (state == null)
            {
                Debug.Log("获取到的状态为空，无法开始：" + typeof(TState).FullName);
            }

            CurrentStateTime = 0f;
            CurrentState = state;
            CurrentState.OnEnter(this);

        }
        /// <summary>
        /// 开始状态机（只有状态的父类对象时）
        /// </summary>
        /// <param name="stateType">要开始的状态类型。</param>
        public void Start(Type stateType)
        {
            if (CurrentState != null)
            {
                Debug.LogError("当前状态机已开始，无法再次开始");
            }

            if (stateType == null)
            {
                Debug.LogError("要开始的状态为空，无法开始");
            }

            FsmState<T> state = GetState(stateType);
            if (state == null)
            {
                Debug.Log("获取到的状态为空，无法开始");
            }

            CurrentStateTime = 0f;
            CurrentState = state;
            CurrentState.OnEnter(this);
        }

        /// <summary>
        /// 状态机轮询。
        /// </summary>
        public void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (CurrentState == null)
            {
                return;
            }

            CurrentStateTime += elapseSeconds;
            CurrentState.OnUpdate(this, elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        public void ChangeState<TState>() where TState : FsmState<T>
        {
            if (CurrentState == null)
            {
                Debug.LogError("当前状态机状态为空，无法切换状态");
                return;
            }

            FsmState<T> state = GetState<TState>();
            if (state == null)
            {
                Debug.LogError("获取到的状态为空，无法切换：" + typeof(TState).FullName);
                return;
            }

            CurrentState.OnLeave(this, false);
            CurrentStateTime = 0f;
            CurrentState = state;
            CurrentState.OnEnter(this);
        }

        /// <summary>
        /// 关闭并清理状态机。
        /// </summary>
        public void Shutdown()
        {
            if (CurrentState != null)
            {
                CurrentState.OnLeave(this, true);
                CurrentState = null;
                CurrentStateTime = 0f;
            }

            foreach (KeyValuePair<string, FsmState<T>> state in _states)
            {
                state.Value.OnDestroy(this);
            }

            _states.Clear();
            _datas.Clear();

            IsDestroyed = true;
        }

        /// <summary>
        /// 抛出状态机事件。
        /// </summary>
        /// <param name="sender">事件源。</param>
        /// <param name="eventId">事件编号。</param>
        public void FireEvent(object sender, int eventId)
        {
            if (CurrentState == null)
            {
                Debug.Log("当前状态为空，无法抛出事件");
            }

            CurrentState.OnEvent(this, sender, eventId, null);
        }
        #endregion

        #region 状态机数据的操作
        /// <summary>
        /// 是否存在状态机数据。
        /// </summary>
        public bool HasData(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.Log("要查询的状态机数据名字为空");
            }

            return _datas.ContainsKey(name);
        }

        /// <summary>
        /// 获取有限状态机数据。
        /// </summary>
        public TDate GetData<TDate>(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.Log("要获取的状态机数据名字为空");
            }

            object data = null;
            _datas.TryGetValue(name, out data);
            return (TDate)data;
        }

        /// <summary>
        /// 设置有限状态机数据。
        /// </summary>
        public void SetData(string name, object data)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.Log("要设置的状态机数据名字为空");
            }

            _datas[name] = data;
        }

        /// <summary>
        /// 移除有限状态机数据。
        /// </summary>
        public bool RemoveData(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.Log("要移除的状态机数据名字为空");
            }

            return _datas.Remove(name);
        }
        #endregion



    }
}

