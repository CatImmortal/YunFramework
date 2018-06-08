using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YunFramework.Tools
{
    /// <summary>
    /// 延时任务模型
    /// </summary>
    public class DelayTaskModel
    {
        /// <summary>
        /// 任务执行时间
        /// </summary>
        public float TaskTime { get; set; }

        /// <summary>
        /// 延迟时间
        /// </summary>
        public float DelayTime { get; set; }

        /// <summary>
        /// 延时任务
        /// </summary>
        public UnityAction Task { get; set; }

        /// <summary>
        /// 是否重复
        /// </summary>
        public bool IsRepeat { get; set; }

        public DelayTaskModel(float taskTime, float delayTime, UnityAction task, bool isRepeat)
        {
            TaskTime = taskTime;
            DelayTime = delayTime;
            Task = task;
            IsRepeat = isRepeat;
        }
    }
}

