using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunFramework.Load
{
    /// <summary>
    /// 资源加载器接口
    /// </summary>
    public interface ILoader
    {
         T LoadAsset<T>(string path, bool isCache = false) where T : Object;
         GameObject LoadGameObject(string path, bool isCache = false);
    }
}

