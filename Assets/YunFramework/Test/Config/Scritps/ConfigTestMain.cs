using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Config;
using YunFramework.Load;
public class ConfigTestMain : MonoBehaviour {

    void Start()
    {
        JsonConfiger jsonConfiger = new JsonConfiger("JsonConfigTest",FrameworkEntry.ResLoader);
        foreach (KeyValuePair<string,string> kv in jsonConfiger.ConfigDict)
        {
            Debug.Log(kv.Key + "-" + kv.Value);
        }

        
        XmlConfiger xmlConfiger = new XmlConfiger("XmlConfigTest", "XmlConfigInfo", FrameworkEntry.ResLoader);
        foreach (KeyValuePair<string, string> kv in xmlConfiger.ConfigDict)
        {
            Debug.Log(kv.Key + "-" + kv.Value);
        }
    }
}
