using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YunFramework.Tools
{
    /// <summary>
    /// Unity辅助器
    /// </summary>
    public static class UnityHelper
    {
        /// <summary>
        /// 递归查找子结点
        /// </summary>
        public static Transform FindTheChildNode(Transform parent, string childName)
        {
            //先在直接子结点里找
            Transform searchTrans = parent.Find(childName);

            //没找到就递归直接子结点，到间接子结点里找
            if (searchTrans == null)
            {
                foreach (Transform child in parent.transform)
                {
                    searchTrans = FindTheChildNode(child, childName);

                    if (searchTrans != null)
                    {
                        return searchTrans;
                    }
                }
            }

            return searchTrans;
        }

        /// <summary>
        /// 递归获取子结点上的组件
        /// </summary>
        public static T GetTheChildNodeComponent<T>(Transform Parent, string childName)
        {

            //先获取子结点
            Transform searchTrans = FindTheChildNode(Parent, childName);

            if (searchTrans != null)
            {
                return searchTrans.gameObject.GetComponent<T>();
            }
            else
            {
                return default(T);
            }

        }

        /// <summary>
        /// 递归给子结点添加组件
        /// </summary>
        public static T AddChildNodeComponent<T>(Transform Parent, string childName) where T : Component
        {
            Transform searchTrans = FindTheChildNode(Parent, childName);

            if (searchTrans != null)
            {
                //若有相同脚本则销毁
                T[] componentScriptsArray = searchTrans.GetComponents<T>();
                for (int i = 0; i < componentScriptsArray.Length; i++)
                {
                    if (componentScriptsArray[i] != null)
                    {
                        Object.DestroyImmediate(componentScriptsArray[i]);
                    }
                }

                return searchTrans.gameObject.AddComponent<T>();
            }
            else
            {
                return null;
            }
        }
    }
}

