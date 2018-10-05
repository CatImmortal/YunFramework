using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace YunFramework.Msg
{
    /// <summary>
    /// Unity事件的监听器组件
    /// </summary>
    public class EventTriggerListener : EventTrigger
    {

        #region 监听Unity事件 的委托
        public delegate void VoidDelegate(BaseEventData eventData);

        public VoidDelegate _onPointerClick;
        public VoidDelegate _onPointerDown;
        public VoidDelegate _onPointerEnter;
        public VoidDelegate _onPointerExit;
        public VoidDelegate _onPointerUp;
        
        #endregion

        /// <summary>
        /// 得到监听器组件
        /// </summary>
        public static EventTriggerListener GetListener(GameObject go)
        {
            EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
            if (listener == null)
            {
                listener = go.AddComponent<EventTriggerListener>();
            }
            return listener;
        }

        #region 重写事件回调
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (_onPointerClick != null)
            {
                _onPointerClick(eventData);
            }

            
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (_onPointerDown != null)
            {
                _onPointerDown(eventData);
            }
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (_onPointerEnter != null)
            {
                _onPointerEnter(eventData);
            }
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (_onPointerExit != null)
            {
                _onPointerExit(eventData);
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (_onPointerUp != null)
            {
                _onPointerUp(eventData);
            }
        }

       
        #endregion
    }
}

