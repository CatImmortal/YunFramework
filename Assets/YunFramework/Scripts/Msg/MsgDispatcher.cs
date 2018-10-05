using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YunFramework.Msg
{
    /// <summary>
    /// 消息派发器
    /// </summary>
    public static class MsgDispatcher
    {
        /// <summary>
        /// 消息与对应处理方法的字典
        /// </summary>
        private static Dictionary<string, List<UnityAction<object>>> _msgHandlerDict = new Dictionary<string, List<UnityAction<object>>>();


        #region IMsgReceiver的扩展方法
        /// <summary>
        /// 注册消息
        /// </summary>
        public static void RegisteMsg(this IMsgReceiver self, string msgName, UnityAction<object> handler)
        {
            if (string.IsNullOrEmpty(msgName))
            {
                Debug.LogError("要注册的消息名为空");
                return;
            }
            if (handler == null)
            {
                Debug.LogError("要注册的消息处理方法为空");
                return;
            }
            if (!_msgHandlerDict.ContainsKey(msgName))
            {
                _msgHandlerDict.Add(msgName, new List<UnityAction<object>>());
            }

            //防止重复注册
            List<UnityAction<object>> handlers = _msgHandlerDict[msgName];
            for (int i = 0; i < handlers.Count; i++)
            {
                if (handlers[i] == handler)
                {
                    Debug.Log("重复注册");
                    return;
                }
            }

            //添加进列表里
            handlers.Add(handler);
        }

        /// <summary>
        /// 注销指定消息的指定处理方法
        /// </summary>
        public static void UnregisteMsg(this IMsgReceiver self, string msgName, UnityAction<object> handler)
        {
            if (string.IsNullOrEmpty(msgName))
            {
                Debug.LogError("要注销的消息名为空");
                return;
            }
            if (handler == null)
            {
                Debug.LogError("要注销的消息处理方法为空");
                return;
            }
            if (!_msgHandlerDict.ContainsKey(msgName))
            {
                Debug.LogError("要注销的消息不在列表中");
                return;
            }

            List<UnityAction<object>> handlers = _msgHandlerDict[msgName];
            for (int i = handlers.Count - 1; i >= 0; i--)
            {
                if (handlers[i] == handler)
                {
                    handlers.RemoveAt(i);
                }
            }

            if (handlers.Count == 0)
            {
                _msgHandlerDict.Remove(msgName);
            }
        }

        /// <summary>
        /// 注销指定消息
        /// </summary>
        public static void UnregisteMsg(this IMsgReceiver self, string msgName)
        {
            if (string.IsNullOrEmpty(msgName))
            {
                Debug.LogError("要注销的消息名为空");
                return;
            }
            if (!_msgHandlerDict.ContainsKey(msgName))
            {
                Debug.LogError("要注销的消息不在列表中");
                return;
            }

            _msgHandlerDict[msgName].Clear();
            _msgHandlerDict.Remove(msgName);
        }

        /// <summary>
        /// 注销所有消息
        /// </summary>
        public static void UnregisteMsgAll(this IMsgReceiver self)
        {
            _msgHandlerDict.Clear();
        }
        #endregion

        /// <summary>
        /// 发送消息
        /// </summary>
        public static void SendMsg(string msgName,object param)
        {
            if (string.IsNullOrEmpty(msgName))
            {
                Debug.LogError("要发送的消息名为空");
                return;
            }
            if (!_msgHandlerDict.ContainsKey(msgName))
            {
                Debug.LogError("要发送的消息不在列表中：" + msgName);
                return;
            }

            //调用消息的对应处理方法
            List<UnityAction<object>> handlers = _msgHandlerDict[msgName];
            for (int i = 0; i < handlers.Count; i++)
            {
                handlers[i](param);
            }
        }
    }
}

