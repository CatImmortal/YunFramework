using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 对Transform的扩展
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

    public static Transform SetLocalPositionX(this Transform self, float x)
    {
        Vector3 temp = self.position;
        temp.x = x;
        self.localPosition = temp;

        return self;
    }

    public static Transform SetLocalPositionY(this Transform self, float y)
    {
        Vector3 temp = self.position;
        temp.y = y;
        self.localPosition = temp;

        return self;
    }

    public static Transform SetLocalPositionZ(this Transform self, float z)
    {
        Vector3 temp = self.position;
        temp.z = z;
        self.localPosition = temp;

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

    /// <summary>
    /// 平滑旋转
    /// </summary>
    public static IEnumerator SmoothRotate(this Transform self, Vector3 eulerAngles, float duration, Space relativeTo = Space.World)
    {
        Quaternion targetQuaternion = Quaternion.Euler(eulerAngles);
        Quaternion originalQuaternion;

        if (relativeTo == Space.World)
        {
            originalQuaternion = self.rotation; 
        }
        else
        {
            originalQuaternion = self.localRotation;
        }

        //开始平滑旋转
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;

            if (relativeTo == Space.World)
            {
                self.rotation = Quaternion.Slerp(originalQuaternion, targetQuaternion, timer / duration);
            }
            else
            {
                self.localRotation = Quaternion.Slerp(originalQuaternion, targetQuaternion, timer / duration);
            }
            
            yield return null;
        }

        if (relativeTo == Space.World)
        {
            self.rotation = targetQuaternion;
        }
        else
        {
            self.localRotation = targetQuaternion;
        }
        
    }

    /// <summary>
    /// 平滑朝向目标点
    /// </summary>
    public static IEnumerator SmoothLookAt(this Transform self, Vector3 target, float duration, bool isIgnoreY = false)
    {
        //使目标点Y轴与自身处于同一平面（即不会抬头或低头朝向目标）
        if (isIgnoreY)
        {
            target = new Vector3(target.x, self.position.y, target.z);
        }

        Quaternion targetQuaternion = Quaternion.LookRotation(target - self.position);
        Quaternion originalQuaternion = self.rotation;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            self.rotation = Quaternion.Slerp(originalQuaternion, targetQuaternion, timer / duration);

            yield return null;
        }

        self.rotation = targetQuaternion;
    }

    /// <summary>
    /// 平滑朝向目标点（2D用）
    /// </summary>
    public static IEnumerator SmoothLookAt2D(this Transform self, Vector2 target, float duration)
    {
        Vector2 pos2D = new Vector2(self.position.x, self.position.y);
        //计算夹角
        float angle = Vector2.Angle(self.right, target - pos2D);

        //计算朝向偏移
        float towardOffset = 1f;
        if (Vector2.Dot(self.up, target - pos2D) < 0)
        {
            towardOffset = -1f;
        }


        Quaternion targetQuaternion = Quaternion.Euler(new Vector3(0, 0, self.eulerAngles.z + angle * towardOffset));
        Quaternion originalQuaternion = self.rotation;

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;

            self.rotation = Quaternion.Slerp(originalQuaternion, targetQuaternion, timer / duration);

            yield return null;
        }

        self.rotation = targetQuaternion;
    }

    /// <summary>
    /// 平滑移至目标点
    /// </summary>
    public static IEnumerator SmoothMoveTo(this Transform self, Vector3 target, float duration)
    {
       
        Vector3 dir = (target - self.position).normalized;
        float totalDistance = Vector3.Distance(self.position, target);
        float speed = totalDistance / duration;

        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;

            self.Translate(dir * speed * Time.deltaTime,Space.World);

            yield return null;
        }

    }
}



