using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 对Transform的链式编程扩展
/// </summary>
public static class TransformExtension
{
    public static Transform Parent(this Transform self, Transform parent)
    {
        self.SetParent(parent);
        return self;
    }

    public static Transform SetPosition(this Transform self, Vector3 position)
    {
        self.position = position;
        return self;
    }

    public static Transform SetPositionX(this Transform self, float x)
    {
        Vector3 temp = self.position;
        temp.x = x;
        self.position = temp;

        return self;
    }

    public static Transform SetPositionY(this Transform self, float y)
    {
        Vector3 temp = self.position;
        temp.y = y;
        self.position = temp;

        return self;
    }

    public static Transform SetPositionZ(this Transform self, float z)
    {
        Vector3 temp = self.position;
        temp.z = z;
        self.position = temp;

        return self;
    }

    public static Transform SetLocalPosition(this Transform self, Vector3 position)
    {
        self.localPosition = position;
        return self;
    }

    public static Transform SetEulerAngles(this Transform self, Vector3 eulerAngles)
    {
        self.eulerAngles = eulerAngles;
        return self;
    }

    public static Transform SetLocalEulerAngles(this Transform self, Vector3 eulerAngles)
    {
        self.localEulerAngles = eulerAngles;
        return self;
    }

    public static Transform SetLocalScale(this Transform self, Vector3 scale)
    {
        self.localScale = scale;
        return self;
    }

    public static Transform Translate(this Transform self, Vector3 position)
    {
        self.Translate(position);
        return self;
    }

    public static Transform Rotate(this Transform self, Vector3 eulerAngles, Space relativeTo = Space.Self)
    {
        self.Rotate(eulerAngles, relativeTo);
        return self;
    }

    public static Transform LookAt(this Transform self, Vector3 target)
    {
        self.LookAt(target);
        return self;
    }

}



