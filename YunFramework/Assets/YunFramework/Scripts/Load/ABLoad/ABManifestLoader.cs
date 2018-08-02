using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YunFramework.Load
{
    /// <summary>
    /// AB包清单文件加载器
    /// </summary>
    public class ABManifestLoader : Singleton<ABManifestLoader>, System.IDisposable
    {

        /// <summary>
        /// 清单文件的AB包
        /// </summary>
        private AssetBundle _abReadManifest;

        /// <summary>
        /// 清单文件的AB包路径
        /// </summary>
        private string _manifestPath = RuntimeABPath.GetWWWPath() + "/" + RuntimeABPath.GetPlatformName();

        /// <summary>
        /// 是否完成清单文件加载的标志位
        /// </summary>
        public bool IsLoadFinish { get; private set; }

        
        private AssetBundleManifest _manifest;
        /// <summary>
        /// 清单文件
        /// </summary>
        public AssetBundleManifest Manifest
        {
            get
            {
                if (IsLoadFinish && _manifest != null)
                {
                    return _manifest;
                }
                else
                {
                    Debug.Log("获取清单文件失败");
                    return null;
                }

            }

        }

        private ABManifestLoader(){

        }

        /// <summary>
        /// 加载清单文件
        /// </summary>
        /// <returns></returns>
        public IEnumerator LoadManifetFile()
        {

            using (WWW www = new WWW(_manifestPath))
            {
                yield return www;
                if (www.progress >= 1)
                {
                    //加载完成
                    _abReadManifest = www.assetBundle;
                    if (_abReadManifest == null)
                    {
                        Debug.Log("清单文件的AB包加载失败，请检查包路径");
                    }
                    else
                    {
                        //加载成功，读取清单文件
                        _manifest = _abReadManifest.LoadAsset<AssetBundleManifest>(ConstsDefine.AB_MANIFEST);
                        IsLoadFinish = true;
                    }
                }
            }
        }

        /// <summary>
        /// 查询指定包的依赖项
        /// </summary>
        public string[] RetrivalDependece(string abName)
        {
            if (_manifest != null && !string.IsNullOrEmpty(abName))
            {
                return _manifest.GetAllDependencies(abName);
            }
            else
            {
                Debug.Log("获取指定包依赖项失败：" + abName);
                return null;
            }
        }

        /// <summary>
        /// 释放AB包内存镜像与Asset内存资源
        /// </summary>
        public void Dispose()
        {
            if (_abReadManifest != null)
            {
                _abReadManifest.Unload(true);
            }
        }


    }
}

