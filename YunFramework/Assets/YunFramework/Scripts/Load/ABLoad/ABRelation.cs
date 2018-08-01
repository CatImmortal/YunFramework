using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunFramework.Load
{
    /// <summary>
    /// AB包依赖关系维护类
    /// </summary>
    public class ABRelation
    {
        public string AbName { get; private set; }
        /// <summary>
        /// 当前包依赖的包（依赖项）
        /// </summary>
        private List<string> _allDependenceAB = new List<string>();

        /// <summary>
        /// 依赖当前包的包（引用项）
        /// </summary>
        private List<string> _allReferenceAB = new List<string>();

        public ABRelation(string abName)
        {
            AbName = abName;
        }

        #region 依赖关系操作
        /// <summary>
        /// 添加依赖关系
        /// </summary>
        public void AddDependence(string abName)
        {
            if (!_allDependenceAB.Contains(abName))
            {
                _allDependenceAB.Add(abName);
            }
        }

        /// <summary>
        /// 删除依赖关系
        /// </summary>
        /// <returns>true:依赖项已清空 false:依赖项未清空</returns>
        public bool RemoveDependence(string abName)
        {
            if (_allDependenceAB.Contains(abName))
            {
                _allDependenceAB.Remove(abName);
                if (_allDependenceAB.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取所有依赖项
        /// </summary>
        public List<string> GetAllDependence()
        {
            return _allDependenceAB;
        }
        #endregion

        #region 引用关系操作
        /// <summary>
        /// 添加引用关系
        /// </summary>
        public void AddReference(string abName)
        {
            if (!_allReferenceAB.Contains(abName))
            {
                _allReferenceAB.Add(abName);
            }
        }

        /// <summary>
        /// 删除引用关系
        /// </summary>
        /// <returns>true:引用项已清空 false:引用项未清空</returns>
        public bool RemoveReference(string abName)
        {
            if (_allReferenceAB.Contains(abName))
            {
                _allReferenceAB.Remove(abName);
                if (_allReferenceAB.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取所有引用项
        /// </summary>
        public List<string> GetReference()
        {
            return _allReferenceAB;
        }
        #endregion
    }
}

