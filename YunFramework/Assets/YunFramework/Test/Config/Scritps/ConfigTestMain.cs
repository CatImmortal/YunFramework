using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Config;
using YunFramework.Load;
public class ConfigTestMain : ScriptBase {

    protected override void Start()
    {
        JsonConfiger jsonConfiger = new JsonConfiger("JsonConfigTest",ResourceLoader.Instance);
        foreach (KeyValuePair<string,string> kv in jsonConfiger.ConfigDict)
        {
            Debug.Log(kv.Key + "-" + kv.Value);
        }

        
        XmlConfiger xmlConfiger = new XmlConfiger("XmlConfigTest", "XmlConfigInfo", ResourceLoader.Instance);
        foreach (KeyValuePair<string, string> kv in xmlConfiger.ConfigDict)
        {
            Debug.Log(kv.Key + "-" + kv.Value);
        }
    }
}
