using System;
using System.Reflection;
using UnityEngine;
using YunFramework.DataBase;
using YunFramework.DataNode;
using YunFramework.Fsm;
using YunFramework.Hotfix;
using YunFramework.Load;
using YunFramework.Procedure;

/// <summary>
/// 框架入口
/// </summary>
public static class FrameworkEntry
{
    #region 框架各模块实例

    /// <summary>
    /// 轮询驱动器
    /// </summary>
    public static UpdateDriver UpdateDriver { get; private set; }

    /// <summary>
    /// AB包清单文件加载器
    /// </summary>
    public static ABManifestLoader ABManifestLoader { get; private set; }

    /// <summary>
    /// AB包加载器
    /// </summary>
    public static AssetBundleLoader_4 AssetBundleLoader { get; private set; }

    /// <summary>
    /// Resources目录下的资源加载器
    /// </summary>
    public static ResLoader ResLoader { get; private set; }

    /// <summary>
    /// 热更新器
    /// </summary>
    public static Hotfixer Hotfixer { get; private set; }

    /// <summary>
    /// 状态机控制器
    /// </summary>
    public static FsmCtrler FsmCtrler { get; private set; }

    /// <summary>
    /// 流程控制器
    /// </summary>
    public static ProcedureCtrler ProcedureCtrler { get; private set; }

    /// <summary>
    /// 数据结点控制器
    /// </summary>
    public static DataNodeCtrler DataNodeCtrler { get; private set; }

    /// <summary>
    /// IBox数据库控制器
    /// </summary>
    public static IBoxDBCtrler IBoxDBCtrler { get; private set; }

    /// <summary>
    /// 线程交叉访问辅助器
    /// </summary>
    public static ThreadCrossHelper ThreadCrossHelper { get; private set; }

    /// <summary>
    /// Unity辅助器
    /// </summary>
    public static UnityHelper UnityHelper { get; private set; }

    #endregion


    /// <summary>
    /// 框架初始化
    /// </summary>
    public static void FrameworkInit(UpdateDriver updateDriver)
    { 
        UpdateDriver = updateDriver;

        //构造框架各模块的实例
        ABManifestLoader = CreateInstance<ABManifestLoader>();
        AssetBundleLoader = CreateInstance<AssetBundleLoader_4>();
        ResLoader = CreateInstance<ResLoader>();

        Hotfixer = CreateInstance<Hotfixer>();

        FsmCtrler = CreateInstance<FsmCtrler>();
        ProcedureCtrler = CreateInstance<ProcedureCtrler>();

        DataNodeCtrler = CreateInstance<DataNodeCtrler>();
        IBoxDBCtrler = CreateInstance<IBoxDBCtrler>();

        ThreadCrossHelper = CreateInstance<ThreadCrossHelper>();
        UnityHelper = CreateInstance<UnityHelper>();

        //加载热更新Dll
        //Hotfixer.LoadHotfixDll();

        //TODO:添加框架各模块的轮询器
        UpdateDriver.AddUpdater(FsmCtrler);
        UpdateDriver.AddUpdater(ThreadCrossHelper);

        //TODO:添加测试代码的轮询器
        //UpdateDriver.AddUpdater<ActionNodeTestMain>();

        //GameObject cube = ResLoader.LoadGameObject("UpdaterCube");
        //cube.AddUpdater<UpdaterTestMain>();

    }

    /// <summary>
    /// 构造只有无参构造方法的类的实例
    /// </summary>
    private static T CreateInstance<T>() where T : class
    {
        //从所有私有构造方法里获取无参构造方法
        ConstructorInfo[] ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
        ConstructorInfo ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

        //调用构造方法并初始化
        return ctor.Invoke(null) as T;

    }

}
