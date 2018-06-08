using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

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

        public XmlConfiger(string xmlPath, string xmlRootNodeName)
        {
            ConfigDict = new Dictionary<string, string>();
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

            XDocument xmlDoc;  //文档
            XmlReader xmlReader;  //读写器

            //开始读取Xml
            try
            {
                //加载xml配置文件
                xmlDoc = XDocument.Load(xmlPath);

                //创建xml读取器
                xmlReader = XmlReader.Create(new StringReader(xmlDoc.ToString())); 
            }
            catch (Exception e)
            {
                throw e;
            }

            //循环解析XML
            while (xmlReader.Read())
            {
                //XML读取器从指定的根节点开始读取
                if (xmlReader.IsStartElement() && xmlReader.LocalName == xmlRootNodeName)
                {
                    //读取根节点下的所有子节点
                    using (XmlReader xmlReaderItem = xmlReader.ReadSubtree())
                    {
                        //循环的方式读取所有子节点
                        while (xmlReaderItem.Read())
                        {
                            //如果读到节点元素
                            if (xmlReaderItem.NodeType == XmlNodeType.Element)
                            {
                                string strNode = xmlReaderItem.Name;
                                //读当前行的下一个内容
                                xmlReaderItem.Read();
                                //如果读到节点内容
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

