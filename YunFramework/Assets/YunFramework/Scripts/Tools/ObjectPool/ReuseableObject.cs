using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Base;
namespace YunFramework.Tools
{
    /// <summary>
    /// 可被对象池管理的对象接口
    /// </summary>
    public abstract class ReuseableObject : ScriptBase
    {
        /// <summary>
        /// 取出时调用
        /// </summary>
        public abstract void OnSpawn();

        /// <summary>
        /// 回收时调用
        /// </summary>
        public abstract void OnUnspawn();
    }
}


