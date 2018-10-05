using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace YunFramework.Load
{
    /// <summary>
    /// 构建时AB包路径相关的类
    /// </summary>
    public class BuildABPath
    {
        /// <summary>
        /// 获取要打包的资源的路径
        /// </summary>
        public static string GetABResPath()
        {
            return Application.dataPath + "/" + ConstsDefine.AB_RESOURCES;
        }

        #region 获取平台相关信息
        /// <summary>
        /// 获取构建平台路径
        /// </summary>
        private static string GetPlatformPath()
        {

            string platformPath = string.Empty;

            switch (EditorUserBuildSettings.activeBuildTarget)
            {

                case BuildTarget.Android:
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                case BuildTarget.iOS:
                    platformPath = Application.streamingAssetsPath;
                    break;


                default:
                    break;
            }
            return platformPath;

        }

        /// <summary>
        /// 获取构建平台名称
        /// </summary>
        public static string GetPlatformName()
        {
            string platformName = string.Empty;

            switch (EditorUserBuildSettings.activeBuildTarget)
            {

                case BuildTarget.Android:
                    platformName = "Android";
                    break;

                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    platformName = "Windows";
                    break;

                case BuildTarget.iOS:
                    platformName = "IOS";
                    break;

                   
                default:
                    break;
            }

            return platformName;
        }
        #endregion

        /// <summary>
        /// 获取构建时AB包输出路径
        /// </summary>
        public static string GetABOutPath()
        {
            return GetPlatformPath() + "/" + GetPlatformName();
        }

       

    }
}

