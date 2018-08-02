using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YunFramework.Load
{
    /// <summary>
    /// Resources目录下的资源加载器
    /// </summary>
    public class ResLoader : Singleton<ResLoader>,ILoader
    {
        /// <summary>
        /// 缓存资源的哈希表
        /// </summary>
        private Hashtable _cache = new Hashtable();

        private ResLoader()
        {
        }

        #region 加载资源的操作
        /// <summary>
        /// 加载指定类型的资源
        /// </summary>
        /// <param name="path">Resources目录下的资源路径</param>
        /// <param name="isCache">是否缓存</param>
        public T LoadAsset<T>(string path, bool isCache = false) where T : Object
        {
            if (_cache.Contains(path))
            {
                return _cache[path] as T;
            }
            else
            {

                //加载资源
                T resource = Resources.Load<T>(path);

                if (resource == null)
                {
                    Debug.Log("要加载的资源找不到：" + path);
                    return null;
                }

                //是否缓存
                if (isCache)
                {
                    _cache.Add(path, resource);
                }

                return resource;

            }
        }

        ///// <summary>
        ///// 加载并实例化游戏物体
        ///// </summary>
        public GameObject LoadGameObject(string path, bool isCache = false)
        {
            GameObject go = LoadAsset<GameObject>(path, isCache);
            GameObject goClone = Object.Instantiate(go);
            if (goClone == null)
            {
                Debug.LogError("实例化游戏物体未成功：" + path);
            }

            //去掉(Clone)后缀
            int index = goClone.name.LastIndexOf('(');
            if (index != -1)
            {
                goClone.name = goClone.name.Substring(0, index);
            }

            return goClone;
        }
        #endregion


    }
}

