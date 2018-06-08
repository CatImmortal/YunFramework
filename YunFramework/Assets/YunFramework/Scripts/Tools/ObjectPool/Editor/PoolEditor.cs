using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
namespace YunFramework.Tools
{
    public class PoolEditor
    {
       
        [MenuItem("YunFramework/Tools/Create PoolConfig")]
        private static void CreatePoolConfig()
        {
            //目录不存在就创建目录
            if (!Directory.Exists(Application.dataPath + ConstsDefine.POOLCONFIG_DIRECTORY))
            {
                Directory.CreateDirectory(Application.dataPath + ConstsDefine.POOLCONFIG_DIRECTORY);
            }

            //配置文件已存在时提示无法创建
            if (File.Exists(Application.dataPath + ConstsDefine.POOLCONFIG_DIRECTORY + ConstsDefine.POOLCONFIG_NAME))
            {
                EditorUtility.DisplayDialog("提示", "对象池配置文件已存在于该路径下：" + ConstsDefine.POOLCONFIG_DIRECTORY + "，无法再次创建", "OK");
            }
            else 
            {
                ObjectPoolConfig poolConfig = ScriptableObject.CreateInstance<ObjectPoolConfig>();
                AssetDatabase.CreateAsset(poolConfig, ConstsDefine.POOLCONFIG_CREATEPATH);
                AssetDatabase.SaveAssets();
                EditorUtility.DisplayDialog("提示", "对象池配置文件成功创建到该路径下：" + ConstsDefine.POOLCONFIG_DIRECTORY, "OK");
            }

           
        }

        

    }

}
