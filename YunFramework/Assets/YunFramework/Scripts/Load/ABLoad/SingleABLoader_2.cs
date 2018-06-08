using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YunFramework.Load
{
    /// <summary>
    /// 单AB包加载器(二级目录的AB包)
    /// </summary>
    public class SingleABLoader_2 : System.IDisposable
    {
        /// <summary>
        /// 下一级的引用
        /// </summary>
        private AssetLoader_1 assetLoader;

        /// <summary>
        /// AB包名
        /// </summary>
        private string _abName;

        /// <summary>
        /// AB包下载路径
        /// </summary>
        private string _abDownloadPath;

        /// <summary>
        /// 加载AB包完毕时调用的委托
        /// </summary>
        private UnityAction<string> _loadCompleteHandle;

        public SingleABLoader_2(string abName, UnityAction<string> loadComplete)
        {
            assetLoader = null;
            _abName = abName;
            _abDownloadPath = RuntimeABPath.GetWWWPath() + "/" + abName;
            _loadCompleteHandle = loadComplete;
        }

        /// <summary>
        /// 加载AB包
        /// </summary>
        public IEnumerator LoadAssetBundle()
        {

            //使用www下载
            using (WWW www = new WWW(_abDownloadPath))
            {
                yield return www;
                if (www.progress >= 1)
                {
                    AssetBundle ab = www.assetBundle;
                    if (ab != null)
                    {
                        //实例化下一级的引用
                        assetLoader = new AssetLoader_1(ab);

                        //调用委托
                        if (_loadCompleteHandle != null)
                        {
                            _loadCompleteHandle(_abName);
                        }

                        www.Dispose();
                    }
                    else
                    {
                        Debug.LogError("www下载AB包失败");
                    }
                }
            }
        }

        #region 代理的下层方法
        /// <summary>
        /// 加载指定资源
        /// </summary>
        public T LoadAsset<T>(string assetName, bool isCache) where T : Object
        {
            if (assetLoader != null)
            {
                return assetLoader.LoadAsset<T>(assetName, isCache);
            }
            else
            {
                Debug.LogError("资源加载失败，缺少AssetLoader对象的引用");
                return null;
            }
        }

        /// <summary>
        /// 卸载指定资源
        /// </summary>
        public void UnloadAsset(Object asset)
        {
            if (assetLoader != null)
            {
                assetLoader.UnloadAsset(asset);
            }
            else
            {
                Debug.LogError("资源释放失败，缺少AssetLoader对象的引用");
            }
        }

        /// <summary>
        /// 释放AB包内存镜像
        /// </summary>
        public void Dispose()
        {
            if (assetLoader != null)
            {
                assetLoader.Dispose();
            }
            else
            {
                Debug.LogError("资源释放失败，缺少AssetLoader对象的引用");
            }
        }

        /// <summary>
        /// 释放AB包内存镜像与Asset内存资源
        /// </summary>
        public void DisposeAll()
        {
            if (assetLoader != null)
            {
                assetLoader.DisposeAll();
            }
            else
            {
                Debug.LogError("资源释放失败，缺少AssetLoader对象的引用");
            }
        }

        /// <summary>
        /// 查询当前AB包中所有资源的名称
        /// </summary>
        public string[] RetriveAllAssetName()
        {
            if (assetLoader != null)
            {
                return assetLoader.RetriveAllAssetName();
            }
            else
            {
                Debug.LogError("查询失败，缺少AssetLoader对象的引用");
                return null;
            }
        }

        #endregion

 

    }
}

