using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 常量的定义类
/// </summary>
public class ConstsDefine {

    #region 对象池配置文件常量
    /// <summary>
    /// 配置文件目录路径
    /// </summary>
    public const string POOLCONFIG_DIRECTORY ="/YunFramework/Scripts/Tools/ObjectPool/Resources/";

    /// <summary>
    /// 配置文件的文件名
    /// </summary>
    public const string POOLCONFIG_NAME = "PoolConfig.asset";

    /// <summary>
    /// 用于创建配置文件的路径
    /// </summary>
    public const string POOLCONFIG_CREATEPATH = "Assets/YunFramework/Scripts/Tools/ObjectPool/Resources/" + POOLCONFIG_NAME;
    #endregion

    #region UI相关常量
    /// <summary>
    /// Canvas预制体的加载路径
    /// </summary>
    public const string UI_CANVAS_PATH = "Canvas";
    #endregion

    #region AB相关常量
    /// <summary>
    /// 需要打成AB包的资源的根目录
    /// </summary>
    public const string AB_RESOURCES = "Res";

    /// <summary>
    /// AB清单文件名字
    /// </summary>
    public const string AB_MANIFEST = "AssetBundleManifest";
    #endregion
}
