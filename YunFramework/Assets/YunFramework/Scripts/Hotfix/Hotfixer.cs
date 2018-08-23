using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YunFramework.Load;

/// <summary>
/// 热更新器
/// </summary>
public class Hotfixer
{

    private Hotfixer()
    {

    }

    /// <summary>
    /// ILRuntime入口对象
    /// </summary>
    private AppDomain _appDomain;

    /// <summary>
    /// 加载热更新层Dll
    /// </summary>
    public void LoadHotfixDll()
    {
        _appDomain = new AppDomain();

        FrameworkEntry.UpdateDriver.StartCoroutine(FrameworkEntry.AssetBundleLoader.LoadAssetBundle("hotfix", "dll.unity3d", LoadAllComplete));
    }

    private void LoadAllComplete(string abName)
    {
        byte[] dll = FrameworkEntry.AssetBundleLoader.LoadAsset<TextAsset>("hotfix," + abName + ",Hotfix.dll.bytes", false).bytes;
        byte[] pdb = FrameworkEntry.AssetBundleLoader.LoadAsset<TextAsset>("hotfix," + abName + ",Hotfix.pdb.bytes", false).bytes;

        using (MemoryStream fs = new MemoryStream(dll))
        {
            using (MemoryStream p = new MemoryStream(pdb))
            {
                _appDomain.LoadAssembly(fs, p, new Mono.Cecil.Pdb.PdbReaderProvider());
            }
        }

        //调用热更新层入口方法
        _appDomain.Invoke("Hotfix.HotfixEntry", "Start", null, null);
    }
}
