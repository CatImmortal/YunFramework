using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YunFramework.ActionNode;


    /// <summary>
    /// 对IUpdater的链式编程扩展
    /// </summary>
    public static class IUpdaterExtension
    {
        /// <summary>
        /// 通过开启协程执行结点
        /// </summary>
        public static T ExecuteNode<T>(this T self, ActionNodeBase node) where T : IUpdater
        {
            UpdateDriver.Instance.StartCoroutine(node.Execute());
            return self;
        }

        /// <summary>
        /// 执行延时结点
        /// </summary>
        public static T Delay<T>(this T self, float seconds, UnityAction delayEvent) where T : IUpdater
        {
            return self.ExecuteNode(new DelayNode(seconds, delayEvent));
        }

        /// <summary>
        /// 开启序列执行结点链
        /// </summary>
        public static NodeChainBase Sequence<T>(this T self) where T : IUpdater
        {
            return new SequenceNodeChain { Executer = self };
        }

        /// <summary>
        /// 开启重复执行结点链
        /// </summary>
        public static NodeChainBase Repeat<T>(this T self, int count = -1) where T : IUpdater
        {
            return new RepeatNodeChain(count) { Executer = self };
        }
    }


