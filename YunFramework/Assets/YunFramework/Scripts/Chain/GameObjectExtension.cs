using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对GameObject的扩展
/// </summary>
public static class GameObjectExtension
{
    public static GameObject Show(this GameObject self)
    {
        self.SetActive(true);
        return self;
    }

    public static GameObject Hide(this GameObject self)
    {
        self.SetActive(false);
        return self;
    }

    public static GameObject Name(this GameObject self, string name)
    {
        self.name = name;
        return self;
    }

    public static GameObject Layer(this GameObject self, int layer)
    {
        self.layer = layer;
        return self;
    }

    public static void DestroySelf(this GameObject self)
    {
        FrameworkEntry.UpdateDriver.Destroy(self);
    }

    /// <summary>
    /// 添加轮询器
    /// </summary>
    public static void AddUpdater<T>(this GameObject self) where T : class, IUpdater, new()
    {
        FrameworkEntry.UpdateDriver.AddUpdater<T>(self);
    }

    /// <summary>
    /// 添加轮询器
    /// </summary>
    public static void AddUpdater(this GameObject self , IUpdater updater)
    {
        FrameworkEntry.UpdateDriver.AddUpdater(updater, self);
    }

    /// <summary>
    /// 获取轮询器
    /// </summary>
    public static T GetUpdater<T>(this GameObject self) where T : class, IUpdater
    {
        return FrameworkEntry.UpdateDriver.GetUpdater<T>();
    }

    /// <summary>
    /// 获取多个轮询器
    /// </summary>
    public static List<T> GetUpdaters<T>(this GameObject self) where T : class, IUpdater
    {
        return FrameworkEntry.UpdateDriver.GetUpdaters<T>();
    }



}


