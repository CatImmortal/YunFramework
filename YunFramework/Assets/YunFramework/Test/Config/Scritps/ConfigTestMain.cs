using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Config;
public class ConfigTestMain : ScriptBase {

    protected override void Start()
    {
        JsonConfiger jsonConfig = new JsonConfiger("JsonConfigTest");
        foreach (KeyValuePair<string,string> kv in jsonConfig.ConfigDict)
        {
            Debug.Log(kv.Key + "-" + kv.Value);
        }

        XmlConfiger xmlConfig = new XmlConfiger("file://" + Application.dataPath + "/YunFramework/Test/Config/Resources/XmlConfigTest.xml", "XmlConfigInfo");
        foreach (KeyValuePair<string, string> kv in xmlConfig.ConfigDict)
        {
            Debug.Log(kv.Key + "-" + kv.Value);
        }
    }
}
