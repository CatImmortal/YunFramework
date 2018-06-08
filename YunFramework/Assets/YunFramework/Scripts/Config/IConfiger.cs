using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunFramework.Config
{
    /// <summary>
    /// 配置器的接口
    /// </summary>
    public interface IConfiger
    {
        /// <summary>
        /// 配置信息字典
        /// </summary>
        Dictionary<string,string> ConfigDict { get; }

        /// <summary>
        /// 配置信息数量
        /// </summary>
        /// <returns></returns>
        int GetConfigMaxCount();
    }
}

