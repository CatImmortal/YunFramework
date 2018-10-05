using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using YunFramework.Load;
namespace YunFramework.Config
{
    /// <summary>
    /// Json配置器
    /// </summary>
    public class JsonConfiger : IConfiger
    {
        /// <summary>
        /// 保存Json配置信息的字典
        /// </summary>
        public Dictionary<string, string> ConfigDict { get; private set; }

        /// <summary>
        /// 资源加载器
        /// </summary>
        public ILoader Loader { get; private set; }

        public JsonConfiger(string jsonPath,ILoader loader)
        {
            ConfigDict = new Dictionary<string, string>();
            Loader = loader;
            AnalysisJson(jsonPath);
        }

        /// <summary>
        /// 获取配置信息数量
        /// </summary>
        public int GetConfigMaxCount()
        {
            if (ConfigDict != null && ConfigDict.Count >= 1)
            {
                return ConfigDict.Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 解析Json配置文件
        /// </summary>
        private void AnalysisJson(string jsonPath)
        {

            //参数检查
            if (string.IsNullOrEmpty(jsonPath))
            {
                return;
            }

            TextAsset configText = null;
            KeyValuesInfo configInfo = null;

            //开始解析Json
            try
            {
                configText = Loader.LoadAsset<TextAsset>(jsonPath);
                configInfo = JsonUtility.FromJson<KeyValuesInfo>(configText.text);
            }
            catch (Exception e)
            {
                throw e;
            }

            //将配置信息保存到字典
            foreach (KeyValuesNode node in configInfo._configInfo)
            {
                ConfigDict.Add(node._key, node._value);
            }

        }
    }

}

