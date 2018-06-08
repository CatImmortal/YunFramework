using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.UI;
using YunFramework.Load;
using YunFramework.Config;
public class ABTestMain : ScriptBase
{
   

    protected override void Start()
    {
        StartCoroutine(AssetBundleLoader_4.Instance.LoadAssetBundle("scene1", "scene1/prefab.unity3d", LoadAllABComplete));
    }

    private void LoadAllABComplete(string abName)
    {
        AssetBundleLoader_4.Instance.LoadGameObject("scene1,"+abName+",Cube.prefab", false);
    }
}