using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YunFramework.Chain
{
    /// <summary>
    /// 对GameObject的链式编程扩展
    /// </summary>
    public static class GameObjectExtension
    {
        public static GameObject Chain_Show(this GameObject self)
        {
            self.SetActive(true);
            return self;
        }

        public static GameObject Chain_Hide(this GameObject self)
        {
            self.SetActive(false);
            return self;
        }

        public static GameObject Chain_Name(this GameObject self, string name)
        {
            self.name = name;
            return self;
        }

        public static GameObject Chain_Layer(this GameObject self, int layer)
        {
            self.layer = layer;
            return self;
        }

        public static void Chain_DestroySelf(this GameObject self)
        {
            Object.Destroy(self);
        }

    }
}

