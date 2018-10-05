using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunFramework.DataBase
{
    /// <summary>
    /// 基本数据对象
    /// </summary>
    public class BaseDBObject : Dictionary<string, object>
    {
        public string Key
        {
            get
            {
                return (string)base["Key"];
            }

            set
            {
                if (value.Length > 20)
                {
                    throw new ArgumentOutOfRangeException();
                }
                base["Key"] = value;
            }
        }
    }
}

