using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace YunFramework.Config
{
    /// <summary>
    /// 保存配置信息列表的类
    /// </summary>
    [Serializable]
    public class KeyValuesInfo
    {
        /// <summary>
        /// 配置信息列表
        /// </summary>
        public List<KeyValuesNode> _configInfo = new List<KeyValuesNode>();
    }
}


