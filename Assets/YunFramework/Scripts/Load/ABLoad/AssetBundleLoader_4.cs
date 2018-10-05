using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace YunFramework.Load
{
    /// <summary>
    /// AB包总加载器(根目录)
    /// </summary>
    public class AssetBundleLoader_4 : ILoader
    {
        /// <summary>
        /// 所有一级目录的字典
        /// </summary>
        private Dictionary<string, MultiABLoader_3> _allFirstDirDict = new Dictionary<string, MultiABLoader_3>();

        /// <summary>
        /// 清单文件类
        /// </summary>
        //private AssetBundleManifest _manifest = null;

        private AssetBundleLoader_4()
        {
            //加载清单文件
            FrameworkEntry.UpdateDriver.StartCoroutine(FrameworkEntry.ABManifestLoader.LoadManifetFile());
        }


        #region 加载包与资源的操作
        /// <summary>
        /// 加载指定AB包
        /// </summary>
        public IEnumerator LoadAssetBundle(string firstDirName, string abName, UnityAction<string> loadAllCompleteHandle)
        {
            //参数检查
            if (string.IsNullOrEmpty(firstDirName) || string.IsNullOrEmpty(abName))
            {
                Debug.LogError("一级目录名或AB包名为空，加载AB包失败");
            }

            //等待清单文件加载完成
            while (!FrameworkEntry.ABManifestLoader.IsLoadFinish)
            {
                yield return null;
            }

            //获取清单文件
            //_manifest = ABManifestLoader.Instance.Manifest;

            //字典里是否已有对应的多包加载器
            if (!_allFirstDirDict.ContainsKey(firstDirName))
            {
                //为当前一级目录创建多包管理器类的对象
                MultiABLoader_3 multiABLoader = new MultiABLoader_3(firstDirName, abName, loadAllCompleteHandle);
                //把当前场景和对应多包管理器类对象放入字典
                _allFirstDirDict.Add(firstDirName, multiABLoader);
            }

            //调用下一级，加载AB包
            MultiABLoader_3 tempMultiMgr = _allFirstDirDict[firstDirName];
            yield return tempMultiMgr.LoadAssetBundle(abName);

        }

        /// <summary>
        /// 加载资源
        /// </summary>
        public T LoadAsset<T>(string path, bool isCache = false) where T : Object
        {
            //从路径里切割出一级目录，包名和资源的名字
            string firstDirName = path.Split(',')[0];
            string abName = path.Split(',')[1];
            string assetName = path.Split(',')[2];



            if (_allFirstDirDict.ContainsKey(firstDirName))
            {
                MultiABLoader_3 multi = _allFirstDirDict[firstDirName];
                return multi.LoadAsset<T>(abName, assetName, isCache);
            }
            else
            {
                Debug.LogError("要加载的资源：" + assetName + "不在指定包：" + abName);
                return null;
            }
        }

        /// <summary>
        /// 加载并实例化游戏物体
        /// </summary>
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



        /// <summary>
        /// 卸载指定资源
        /// </summary>
        public bool UnloadAsset(string firstDirName, string abName, Object asset)
        {

            if (!_allFirstDirDict.ContainsKey(firstDirName))
            {
                Debug.LogError("要卸载资源的AB包的一级目录不存在字典中");
                return false;
            }

            return _allFirstDirDict[firstDirName].UnloadAsset(abName, asset);
        }

        /// <summary>
        /// 释放AB包内存镜像
        /// </summary>
        public void Dispose(string firstDirName, string abName)
        {
            if (!_allFirstDirDict.ContainsKey(firstDirName))
            {
                Debug.LogError("要释放的AB包的一级目录不存在字典中");
                return;
            }

            _allFirstDirDict[firstDirName].Dispose(abName);
        }

        /// <summary>
        /// 释放AB包内存镜像与Asset内存资源
        /// </summary>
        public void DisposeAll(string firstDirName, string abName)
        {
            if (!_allFirstDirDict.ContainsKey(firstDirName))
            {
                Debug.LogError("要释放的AB包的一级目录不存在字典中");
                return;
            }

            _allFirstDirDict[firstDirName].DisposeAll(abName);
        }

        /// <summary>
        /// 释放指定一级目录下所有AB包
        /// </summary>
        public void DisposeAllAsset(string firstDirName)
        {
            if (!_allFirstDirDict.ContainsKey(firstDirName))
            {
                Debug.LogError("要卸载AB包的一级目录不存在字典中");
                return;
            }

            _allFirstDirDict[firstDirName].DisposeAllAsset();
        }



    }
}

