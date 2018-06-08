using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunFramework.Load
{
    /// <summary>
    /// 运行时AB包路径相关的类
    /// </summary>
    public class RuntimeABPath
    {
       

        #region 获取平台相关信息
        /// <summary>
        /// 获取运行平台路径
        /// </summary>
        private static string GetPlatformPath()
        {

            string platformPath = string.Empty;

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.Android:
                    platformPath = Application.streamingAssetsPath;
                    break;
            }

            return platformPath;
        }

        /// <summary>
        /// 获取运行平台名称
        /// </summary>
        public static string GetPlatformName()
        {
            string platformName = string.Empty;

            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    platformName = "Android";
                    break;

                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                    platformName = "Windows";
                    break;

                case RuntimePlatform.IPhonePlayer:
                    platformName = "IOS";
                    break;
        
                default:
                    break;
            }

            return platformName;
        }
        #endregion

        /// <summary>
        /// 获取运行时AB包输出路径
        /// </summary>
        public static string GetABOutPath()
        {
            return GetPlatformPath() + "/" + GetPlatformName();
        }

        /// <summary>
        /// 获取WWW下载的路径
        /// </summary>
        public static string GetWWWPath()
        {
            string wwwPath = string.Empty;

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                    wwwPath = "file://" + GetABOutPath();
                    break;
                case RuntimePlatform.Android:
                    wwwPath = "jar:file://" + GetABOutPath();
                    break;
                case RuntimePlatform.IPhonePlayer:
                    wwwPath = GetABOutPath() + "/Raw";
                    break;
            }

            return wwwPath;
        }



    }

}
