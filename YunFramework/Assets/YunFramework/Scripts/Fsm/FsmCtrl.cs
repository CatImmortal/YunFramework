using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Base;    
namespace YunFramework.Fsm
{
    /// <summary>
    /// 状态机控制器
    /// </summary>
    public class FsmCtrl : ScriptSingleton<FsmCtrl>
    {
        /// <summary>
        /// 所有状态机的字典
        /// </summary>
        private Dictionary<string, IFsm> fsms;
        private List<IFsm> tempFsms;

        public FsmCtrl()
        {
            fsms = new Dictionary<string, IFsm>();
            tempFsms = new List<IFsm>();
        }

        #region 生命周期

     

        protected override void Update()
        {
            tempFsms.Clear();
            if (fsms.Count <= 0)
            {
                return;
            }

            foreach (KeyValuePair<string, IFsm> fsm in fsms)
            {
                tempFsms.Add(fsm.Value);
            }

            foreach (IFsm fsm in tempFsms)
            {
                if (fsm.IsDestroyed)
                {
                    continue;
                }
                //轮询状态机
                fsm.Update(Time.deltaTime, Time.unscaledDeltaTime);
            }
        }

        protected override void OnDestroy()
        {
            foreach (KeyValuePair<string, IFsm> fsm in fsms)
            {
                fsm.Value.Shutdown();
            }

            fsms.Clear();
            tempFsms.Clear();
        }
        #endregion

        #region 状态机的操作

        /// <summary>
        /// 是否存在状态机
        /// </summary>
        public bool HasFsm<T>()
        {
            return fsms.ContainsKey(typeof(T).FullName);
        }

        /// <summary>
        /// 创建状态机。
        /// </summary>
        /// <typeparam name="T">状态机持有者类型。</typeparam>
        /// <param name="name">状态机名称。</param>
        /// <param name="owner">状态机持有者。</param>
        /// <param name="states">状态机状态集合。</param>
        /// <returns>要创建的状态机。</returns>
        public Fsm<T> CreateFsm<T>(T owner, string name = "", params FsmState<T>[] states) where T : class
        {
            if (HasFsm<T>())
            {
                Debug.LogError("要创建的状态机已存在");
            }
            if (name == "")
            {
                name = typeof(T).FullName;
            }
            Fsm<T> fsm = new Fsm<T>(name, owner, states);
            fsms.Add(name, fsm);
            return fsm;
        }

        /// <summary>
        /// 销毁状态机
        /// </summary>
        public bool DestroyFsm(string name)
        {
            IFsm fsm = null;
            if (fsms.TryGetValue(name, out fsm))
            {
                fsm.Shutdown();
                return fsms.Remove(name);
            }

            return false;
        }
        public bool DestroyFsm<T>() where T : class
        {
            return DestroyFsm(typeof(T).FullName);
        }
        public bool DestroyFsm(IFsm fsm)
        {
            return DestroyFsm(fsm.Name);
        }

      
        #endregion
    }
}

