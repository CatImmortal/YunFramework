using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YunFramework.Load
{
    //多AB包加载器(一级目录)
    public class MultiABLoader_3
    {
        /// <summary>
        ///  下一级的引用
        /// </summary>
        private SingleABLoader_2 _curSingleAB;

        /// <summary>
        /// 当前一级目录名
        /// </summary>
        private string _curFirstDirName;

        /// <summary>
        /// 当前AB包名
        /// </summary>
        private string _currentABName;

        /// <summary>
        /// 包名与单包加载器对象的字典（防止重复加载）
        /// </summary>
        private Dictionary<string, SingleABLoader_2> _singleABDict = new Dictionary<string, SingleABLoader_2>();

        /// <summary>
        /// 包名与对应关系类字典
        /// </summary>
        private Dictionary<string, ABRelation> _abRelationDict = new Dictionary<string, ABRelation>();

        /// <summary>
        /// 所有AB包加载完成时调用的委托
        /// </summary>
        private UnityAction<string> loadAllABCompleteHandle;

        public MultiABLoader_3(string curFirstDirName, string currentABName, UnityAction<string> loadAllABCompleteHandle)
        {
            _curFirstDirName = curFirstDirName;
            _currentABName = currentABName;
            this.loadAllABCompleteHandle = loadAllABCompleteHandle;
        }

        #region AB包的加载操作
        /// <summary>
        /// 加载完成指定AB包后调用
        /// </summary>
        private void CompleteLoadAB(string abName)
        {

            //与当前AB包名相同，意味着加载完成
            if (abName.Equals(_currentABName))
            {
                if (loadAllABCompleteHandle != null)
                {
                    loadAllABCompleteHandle(abName);
                }
                Debug.Log("当前完成" + abName + "包的加载");
            }


        }

        /// <summary>
        /// 加载AB包
        /// </summary>
        public IEnumerator LoadAssetBundle(string abName)
        {
            //判断AB包关系类是否已存在
            if (!_abRelationDict.ContainsKey(abName))
            {
                //创建和AB包名对应的AB包关系类加入字典
                ABRelation relation = new ABRelation(abName);
                _abRelationDict.Add(abName, relation);
            }

            //获取对应的关系类
            ABRelation tempRelation = _abRelationDict[abName];

            //得到指定AB包的所有依赖关系（查询清单文件）
            string[] dependenceArray = FrameworkEntry.ABManifestLoader.RetrivalDependece(abName);
            foreach (string itemDependence in dependenceArray)
            {
                //添加依赖项
                tempRelation.AddDependence(itemDependence);
                //加载它依赖的包
                yield return LoadDependence(itemDependence, abName);
            }

            //是否已经加载过
            if (_singleABDict.ContainsKey(abName))
            {
                yield return _singleABDict[abName].LoadAssetBundle();
            }
            else
            {
                //未加载过，创建对应的单包加载类对象
                _curSingleAB = new SingleABLoader_2(_curFirstDirName,abName, CompleteLoadAB);
                _singleABDict.Add(abName, _curSingleAB);
                yield return _curSingleAB.LoadAssetBundle();
            }

        }

        /// <summary>
        /// 加载依赖的AB包
        /// </summary>
        private IEnumerator LoadDependence(string abName, string refName)
        {
            ABRelation tempRelation;

            if (_abRelationDict.ContainsKey(abName))
            {
                //要依赖的包的对应关系类存在，直接给它添加引用关系
                tempRelation = _abRelationDict[abName];
                tempRelation.AddReference(refName);
            }
            else
            {
                //没有就创建关系类
                tempRelation = new ABRelation(abName);
                //为要依赖的包添加引用关系
                tempRelation.AddReference(refName);
                //放入字典
                _abRelationDict.Add(abName, tempRelation);
                //加载依赖的包(递归)
                yield return LoadAssetBundle(abName);
            }


        }
        #endregion

        #region 代理的下层方法
        /// <summary>
        /// 加载资源
        /// </summary>
        public T LoadAsset<T>(string abName, string assetName, bool isCache) where T : Object
        {
            if (_singleABDict.ContainsKey(abName))
            {
                return _singleABDict[abName].LoadAsset<T>(assetName, isCache);
            }
            else
            {
                Debug.LogError("从" + abName + "包里无法获取指定资源：" + assetName + "，因为该包未加载过");
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 卸载指定资源
        /// </summary>
        public bool UnloadAsset(string abName,Object asset)
        {

            if (!_singleABDict.ContainsKey(abName))
            {
                Debug.LogError("要卸载资源的AB包没有加载过：" + abName);
                return false;
            }

            return _singleABDict[abName].UnloadAsset(asset);

        }

        /// <summary>
        /// 释放AB包内存镜像
        /// </summary>
        public void Dispose(string abName)
        {
            if (!_singleABDict.ContainsKey(abName))
            {
                Debug.LogError("要释放的AB包没有加载过：" + abName);
                return;
            }

            _singleABDict[abName].Dispose();
            _singleABDict.Remove(abName);

        }

        /// <summary>
        /// 释放AB包内存镜像与Asset内存资源
        /// </summary>
        public void DisposeAll(string abName)
        {
            if (!_singleABDict.ContainsKey(abName))
            {
                Debug.LogError("要释放的AB包没有加载过：" + abName);
                return;
            }

            _singleABDict[abName].DisposeAll();
            _singleABDict.Remove(abName);
        }

        /// <summary>
        /// 释放所有资源及AB包内存镜像
        /// </summary>
        public void DisposeAllAsset()
        {
            try
            {
                foreach (SingleABLoader_2 item in _singleABDict.Values)
                {
                    item.DisposeAll();
                }
            }
            finally
            {
                _singleABDict.Clear();
                _singleABDict = null;

                //释放其他对象占用资源
                _abRelationDict.Clear();
                _abRelationDict = null;
                _currentABName = null;
                _curFirstDirName = null;
                _curSingleAB = null;
                loadAllABCompleteHandle = null;

                //卸载没有使用的资源
                Resources.UnloadUnusedAssets();
                //垃圾收集
                System.GC.Collect();
            }


        }
    }
}

