using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YunFramework.Base;
namespace YunFramework.Tools
{
    /// <summary>
    /// 延时任务控制器
    /// </summary>
    public class DelayTaskCtrl : ScriptSingleton<DelayTaskCtrl>
    {

        /// <summary>
        /// 延时任务模型列表
        /// </summary>
        private List<DelayTaskModel> _delayTaskList = new List<DelayTaskModel>();


        protected override void Update()
        {
            for (int i = _delayTaskList.Count - 1; i >= 0; i--)
            {
                DelayTaskModel delayTaskModel = _delayTaskList[i];
                //到达任务执行时间就执行任务
                if (delayTaskModel.TaskTime <= Time.time)
                {
                    delayTaskModel.Task();

                    //非重复任务需要从任务模型列表里移除
                    if (!delayTaskModel.IsRepeat)
                    {
                        int index = i;
                        _delayTaskList.RemoveAt(index);
                    }
                    else
                    {
                        //重复任务增加任务时间
                        delayTaskModel.TaskTime += delayTaskModel.DelayTime;
                    }
                }
            }
        }

        #region 延时任务的操作
        /// <summary>
        /// 添加延时任务
        /// </summary>
        /// <param name="delayTime">延迟时间</param>
        /// <param name="task">要延时调用的方法</param>
        /// <param name="isRepeat">是否重复调用</param>
        public DelayTaskModel AddDelayTask(float delayTime, UnityAction task, bool isRepeat = false)
        {
            DelayTaskModel delayTaskModel = new DelayTaskModel(Time.time + delayTime, delayTime, task, isRepeat);
            _delayTaskList.Add(delayTaskModel);
            return delayTaskModel;
        }

        /// <summary>
        /// 移除延时任务
        /// </summary>
        public void RemoveDelayTask(DelayTaskModel model)
        {
            if (_delayTaskList.Exists(x => x == model))
            {
                _delayTaskList.Remove(model);
            }
        }

        /// <summary>
        /// 移除所有延时任务
        /// </summary>
        public void RemoveAllDelayTask() {
            _delayTaskList.Clear();
        }
        #endregion



    }
}


