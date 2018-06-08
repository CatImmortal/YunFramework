using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YunFramework.Base;
using YunFramework.Msg;
using YunFramework.Tools;

namespace YunFramework.UI
{
    public class UIPanelBase :ScriptBase,IMsgReceiver
    {
        private UIType _type = new UIType();
        public UIType Type
        {
            get
            {
                return _type;
            }
            protected set
            {
                _type = value;
            }
        }

      

        #region 生命周期

        /// <summary>
        /// 显示
        /// </summary>
        public virtual void Display()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 重新显示
        /// </summary>
        public virtual void Redisplay()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 冻结
        /// </summary>
        public virtual void Freeze()
        {
            gameObject.SetActive(true);
        }

        #endregion

        #region 为子类封装的方法
        /// <summary>
        /// 发送消息
        /// </summary>
        protected void SendMsg(string msgName,object param)
        {
            MsgDispatcher.SendMsg(msgName, param);
        }

        /// <summary>
        /// 注册消息
        /// </summary>
        protected void RegisteMsg(string msgName,UnityAction<object> handler)
        {
            MsgDispatcher.RegisteMsg(this, msgName, handler);
        }


        /// <summary>
        /// 为子物体注册点击事件
        /// </summary>
        protected void RigisteClickEvent(string childName, EventTriggerListener.VoidDelegate handler)
        {
            //找到按钮对象
            Transform child = UnityHelper.FindTheChildNode(gameObject.transform, childName);

            //注册点击事件
            EventTriggerListener.GetListener(child.gameObject)._onPointerClick += handler;
        }

        #endregion
    }
}

