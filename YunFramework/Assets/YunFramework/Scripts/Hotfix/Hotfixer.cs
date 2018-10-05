using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Generated;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace YunFramework.Hotfix
{
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
            InitILRuntime(_appDomain);
            FrameworkEntry.UpdateDriver.StartCoroutine(FrameworkEntry.AssetBundleLoader.LoadAssetBundle("hotfix", "dll.unity3d", LoadAllComplete));
        }

        /// <summary>
        /// 初始化ILRuntime
        /// </summary>
        public static void InitILRuntime(AppDomain appDomain)
        {
            //注册重定向方法

            //注册委托
            appDomain.DelegateManager.RegisterDelegateConvertor<UnityAction<float>>((action) =>
            {
                return new UnityAction<float>((a) =>
                {
                    ((System.Action<float>)action)(a);
                });
            });

            CLRBindings.Initialize(appDomain);

            //注册适配器
            appDomain.RegisterCrossBindingAdaptor(new IUpdaterAdapter());

            
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
}

