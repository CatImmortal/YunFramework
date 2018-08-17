using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using YunFramework.Load;

namespace YunFramework.Config
{
    /// <summary>
    /// XML配置器
    /// </summary>
    public class XmlConfiger : IConfiger
    {
        /// <summary>
        /// 保存Xml配置信息的字典
        /// </summary>
        public Dictionary<string, string> ConfigDict { get; private set; }

        /// <summary>
        /// 资源加载器
        /// </summary>
        public ILoader Loader { get; private set; }


        public XmlConfiger(string xmlPath, string xmlRootNodeName,ILoader loader)
        {
            ConfigDict = new Dictionary<string, string>();
            Loader = loader;
            AnalysisXml(xmlPath, xmlRootNodeName);
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
        /// 初始化解析XML数据到集合中
        /// </summary>
        private void AnalysisXml(string xmlPath, string xmlRootNodeName)
        {
            if (string.IsNullOrEmpty(xmlPath) || string.IsNullOrEmpty(xmlRootNodeName))
            {
                return;
            }

            XmlReader xmlReader;  //读取器

            //开始读取Xml
            try
            {
                //加载xml配置文件
                TextAsset xmlText = Loader.LoadAsset<TextAsset>(xmlPath);

                //创建xml读取器
                xmlReader = XmlReader.Create(new StringReader(xmlText.text)); 
            }
            catch (Exception e)
            {
                throw e;
            }

            //循环解析XML
            while (xmlReader.Read())
            {
                //XML读取器从指定的根结点开始读取
                if (xmlReader.IsStartElement() && xmlReader.LocalName == xmlRootNodeName)
                {
                    //读取根结点下的所有子结点
                    using (XmlReader xmlReaderItem = xmlReader.ReadSubtree())
                    {
                        //循环的方式读取所有子结点
                        while (xmlReaderItem.Read())
                        {
                            //如果读到结点元素
                            if (xmlReaderItem.NodeType == XmlNodeType.Element)
                            {
                                string strNode = xmlReaderItem.Name;
                                //读当前行的下一个内容
                                xmlReaderItem.Read();
                                //如果读到结点内容
                                if (xmlReaderItem.NodeType == XmlNodeType.Text)
                                {
                                    //放入字典
                                    ConfigDict[strNode] = xmlReaderItem.Value;
                                }
                            }
                        }
                    }
                }
            }

        }
    }
}

