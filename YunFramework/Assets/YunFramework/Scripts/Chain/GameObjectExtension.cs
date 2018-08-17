using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YunFramework.ActionNode;



public static class GameObjectExtension
{
    public static GameObject Show(this GameObject self)
    {
        self.SetActive(true);
        return self;
    }

    public static GameObject Hide(this GameObject self)
    {
        self.SetActive(false);
        return self;
    }

    public static GameObject Name(this GameObject self, string name)
    {
        self.name = name;
        return self;
    }

    public static GameObject Layer(this GameObject self, int layer)
    {
        self.layer = layer;
        return self;
    }

    public static void DestroySelf(this GameObject self)
    {
        UpdateDriver.Instance.Destroy(self);
    }



}


