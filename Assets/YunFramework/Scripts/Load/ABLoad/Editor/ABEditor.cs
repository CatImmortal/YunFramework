using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
namespace YunFramework.Load
{
    public class ABEditor
    {
        #region AB标记操作
        [MenuItem("YunFramework/Load/Set AB Label")]
        public static void SetABLabel()
        {
            //清空无用标记
            AssetDatabase.RemoveUnusedAssetBundleNames();

            //获取需要给AB做标记的资源根目录信息
            string needSetLabelRoot = BuildABPath.GetABResPath();
            DirectoryInfo tempInfo = new DirectoryInfo(needSetLabelRoot);

            //获取根目录下所有一级目录
            DirectoryInfo[] firstDirArray = tempInfo.GetDirectories();

            //遍历所有一级目录
            foreach (DirectoryInfo currentDir in firstDirArray)
            {
                //递归
                JudgeDIRorFileByRecursisve(currentDir, currentDir.Name);
            }


            //刷新并提示
            AssetDatabase.Refresh();
            Debug.Log("标记结束");
        }

        /// <summary>
        /// 找到所有文件的递归方法
        /// </summary>
        private static void JudgeDIRorFileByRecursisve(DirectoryInfo dirInfo, string firstDirName)
        {
            //参数检查
            if (!dirInfo.Exists)
            {
                Debug.LogError("文件或者目录名称不存在：" + dirInfo);
                return;
            }

            //获得当前文件夹的所有文件系统信息（文件与目录）
            FileSystemInfo[] fileSysArray = dirInfo.GetFileSystemInfos();

            //遍历
            foreach (FileSystemInfo tempFileSysInfo in fileSysArray)
            {
                //判断是不是文件
                FileInfo fileInfo = tempFileSysInfo as FileInfo;
                if (fileInfo == null)
                {
                    //不是文件，递归
                    DirectoryInfo tempDirInfo = tempFileSysInfo as DirectoryInfo;
                    JudgeDIRorFileByRecursisve(tempDirInfo, firstDirName);
                }
                else
                {
                    //是文件，修改AB标记
                    SetFileABLabel(fileInfo, firstDirName);
                }
            }


        }

        /// <summary>
        /// 对指定文件设置AB标记
        /// </summary>
        private static void SetFileABLabel(FileInfo fileInfo, string firstDirName)
        {

            //meta文件不作标记
            if (fileInfo.Extension == ".meta")
            {
                return;
            }

            //AB包名
            string abName = GetABName(fileInfo, firstDirName);

            //文件路径（相对路径）"Asset/....."
            int index = fileInfo.FullName.IndexOf("Asset");
            string assetFilePath = fileInfo.FullName.Substring(index);

            //修改AB包名和后缀
            AssetImporter tempImporter = AssetImporter.GetAtPath(assetFilePath);
            tempImporter.assetBundleName = abName;
            tempImporter.assetBundleVariant = "unity3d";

        }

        /// <summary>
        /// 获取AB包名
        /// </summary>
        private static string GetABName(FileInfo fileInfo, string firstDirName)
        {

            string abName = string.Empty;

            //win路径转Unity路径
            string winPath = fileInfo.FullName;
            string unityPath = winPath.Replace(@"\", "/");

            //定位 一级目录 后的字符起点
            int firstDirNameIndex = unityPath.IndexOf(firstDirName) + firstDirName.Length;
            //一级目录后的目录
            string abFileNameArea = unityPath.Substring(firstDirNameIndex + 1);

            //是否包含多级目录
            if (abFileNameArea.Contains("/"))
            {
                string[] temp = abFileNameArea.Split('/');

                //形成AB包名
                abName = firstDirName + "/" + temp[0];
            }
            else
            {
                //形成AB包名
                abName = firstDirName + "/" + firstDirName;
            }

            return abName;

        }
        #endregion

        #region AB打包操作
        [MenuItem("YunFramework/Load/Build All AB")]
        public static void BuildAB()
        {
            //AB包输出路径
            string abOutPath = BuildABPath.GetABOutPath();

            //判断输出目录是否存在
            if (!Directory.Exists(abOutPath))
            {
                Directory.CreateDirectory(abOutPath);
            }

            //获取打包平台
            BuildTarget target = BuildTarget.NoTarget;
            switch (BuildABPath.GetPlatformName())
            {
                case "Android":
                    target = BuildTarget.Android;
                    break;

                case "Windows":
                    target = BuildTarget.StandaloneWindows64;
                    break;
                case "IOS":
                    target = BuildTarget.iOS;
                    break;
              
                    
            }

            //打包生成
            BuildPipeline.BuildAssetBundles(abOutPath, BuildAssetBundleOptions.None, target);

            //刷新
            AssetDatabase.Refresh();
        }
        #endregion

        #region AB删除操作
        [MenuItem("YunFramework/Load/Delete All AB")]
        public static void DelAssetBundle()
        {

            string path = BuildABPath.GetABOutPath();
            Directory.Delete(path, true);
            File.Delete(path + ".meta");
            AssetDatabase.Refresh();

        }
        #endregion
    }
}

