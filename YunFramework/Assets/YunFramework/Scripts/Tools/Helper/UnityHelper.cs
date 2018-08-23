using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity辅助器
/// </summary>
public class UnityHelper
{
    private UnityHelper()
    {

    }

    /// <summary>
    /// 递归查找子结点
    /// </summary>
    public Transform FindChildNode(Transform parent, string childName)
    {
        //先在直接子结点里找
        Transform searchTrans = parent.Find(childName);

        //没找到就递归直接子结点，到间接子结点里找
        if (searchTrans == null)
        {
            foreach (Transform child in parent.transform)
            {
                searchTrans = FindChildNode(child, childName);

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
    public T GetChildNodeComponent<T>(Transform Parent, string childName)
    {

        //先获取子结点
        Transform searchTrans = FindChildNode(Parent, childName);

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
    public T AddChildNodeComponent<T>(Transform Parent, string childName) where T : Component
    {
        Transform searchTrans = FindChildNode(Parent, childName);

        if (searchTrans != null)
        {
            //若有相同脚组件则销毁
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


