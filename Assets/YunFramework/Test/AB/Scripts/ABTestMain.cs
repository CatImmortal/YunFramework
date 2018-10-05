using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Load;
using YunFramework.Config;
public class ABTestMain : MonoBehaviour
{
   

    void Start()
    {
        StartCoroutine(FrameworkEntry.AssetBundleLoader.LoadAssetBundle("scene1", "prefab.unity3d", LoadAllABComplete));
    }

    private void LoadAllABComplete(string abName)
    {
        //AB包加载器需要的path参数为一级目录,包名,资源名
        FrameworkEntry.AssetBundleLoader.LoadGameObject("scene1,"+abName+",Cube.prefab", false);
    }
}