using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunFramework.Load
{
    /// <summary>
    /// AB包资源加载器
    /// </summary>
    public class AssetLoader_1 : System.IDisposable
    {
        /// <summary>
        /// 当前AB包
        /// </summary>
        private AssetBundle _currentAB;

        /// <summary>
        /// 资源缓存集合
        /// </summary>
        private Hashtable _cache = new Hashtable();

        public AssetLoader_1(AssetBundle ab)
        {
            if (ab != null)
            {
                _currentAB = ab;
            }
            else
            {
                Debug.LogError("用于构造AssetLoader类的AssetBundle参数为空");
            }
        }

        #region 资源的加载与卸载

        /// <summary>
        /// 加载指定资源
        /// </summary>
        public T LoadAsset<T>(string assetName,bool isCache) where T : Object
        {
            if (_cache.Contains(assetName))
            {
                return _cache[assetName] as T;
            }
            else
            {

                T asset = _currentAB.LoadAsset<T>(assetName);

                if (asset == null)
                {
                    Debug.LogError("加载资源失败：" + assetName);
                }
                else if (isCache)
                {
                    //加入缓存
                    _cache.Add(assetName, asset);
                }

                return asset;

                
            }
        }

        /// <summary>
        /// 卸载指定资源
        /// </summary>
        public bool UnloadAsset(Object asset)
        {
            if (asset != null)
            {
                Resources.UnloadAsset(asset);
                return true;
            }
            else
            {
                Debug.LogError("卸载指定资源失败：" + asset.name);
                return false;
            }
        }
        /// <summary>
        /// 释放AB包内存镜像
        /// </summary>
        public void Dispose()
        {
            _currentAB.Unload(false);
        }

        /// <summary>
        /// 释放AB包内存镜像与Asset内存资源
        /// </summary>
        public void DisposeAll()
        {
            _currentAB.Unload(true);
        }
        #endregion

        /// <summary>
        /// 查询当前AB包中所有资源的名称
        /// </summary>
        public string[] RetriveAllAssetName()
        {
            return _currentAB.GetAllAssetNames();
        }


    }
}

