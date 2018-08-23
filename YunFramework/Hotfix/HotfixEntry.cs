using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YunFramework.Load;

namespace Hotfix
{
    /// <summary>
    /// 热更新层入口
    /// </summary>
    public static class HotfixEntry
    {
        public static void Start()
        {
            Debug.Log("热更新层启动！");

            //如果发现引用丢失，那么请在重新编译解决方案后添加引用

            //在完成Hotfix工程的代码编写后，将生成出来的Hotfix.dll与Hotfix.pdb文件复制到Assets/Res/hotfix/dll/目录下，并分别为其添加扩展名.bytes
            //之后进行AB打包即可

            //ILR测试
            //ILRTestMain ilrTestMain = new ILRTestMain();
            //FrameworkEntry.UpdateDriver.AddUpdater(ilrTestMain, FrameworkEntry.ResLoader.LoadGameObject("ILRCube"));
        }

    }
}
