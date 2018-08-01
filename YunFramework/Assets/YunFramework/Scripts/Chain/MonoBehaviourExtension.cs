using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YunFramework.ActionNode;

namespace YunFramework.Chain
{
    /// <summary>
    /// 对MonoBehaviour的链式编程扩展
    /// </summary>
    public static class MonoBehaviourExtension
    {
        /// <summary>
        /// 通过开启协程执行结点
        /// </summary>
        public static T ExecuteNode<T>(this T self, ActionNodeBase node) where T : MonoBehaviour
        {
            self.StartCoroutine(node.Execute());
            return self;
        }

        /// <summary>
        /// 执行延时结点
        /// </summary>
        public static T Delay<T>(this T self, float seconds, UnityAction delayEvent) where T : MonoBehaviour
        {
            return self.ExecuteNode(new DelayNode(seconds, delayEvent));
        }

        /// <summary>
        /// 开启序列执行结点链
        /// </summary>
        public static NodeChainBase Sequence<T>(this T self) where T : MonoBehaviour
        {
            return new SequenceNodeChain { Executer = self };
        }

        /// <summary>
        /// 开启重复执行结点链
        /// </summary>
        public static NodeChainBase Repeat<T>(this T self, int count = -1) where T : MonoBehaviour
        {
            return new RepeatNodeChain(count) { Executer = self };
        }
    }
}

