using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对Rigidbody的扩展
/// </summary>
public static class RigidbodyExtension {

    private static WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    /// <summary>
    /// 平滑旋转
    /// </summary>
    public static IEnumerator SmoothMoveRotation(this Rigidbody self, Vector3 eulerAngles, float duration)
    {
        Quaternion originalQuaternion = self.rotation;
        Quaternion targetQuaternion = Quaternion.Euler(eulerAngles);

        //开始平滑旋转
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            self.MoveRotation(Quaternion.Lerp(originalQuaternion, targetQuaternion, timer / duration));
            yield return _waitForFixedUpdate;
        }
        self.MoveRotation(targetQuaternion);
    }

    /// <summary>
    /// 平滑朝向目标点
    /// </summary>
    public static IEnumerator SmoothLookAt(this Rigidbody self, Vector3 target, float duration, bool isIgnoreY = false)
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

            self.MoveRotation(Quaternion.Slerp(originalQuaternion, targetQuaternion, timer / duration));

            yield return _waitForFixedUpdate;
        }

        self.MoveRotation(targetQuaternion);


    }

    /// <summary>
    /// 平滑移至目标点
    /// </summary>
    public static IEnumerator SmoothMoveTo(this Rigidbody self, Vector3 target, float duration)
    {

        Vector3 dir = (target - self.position).normalized;
        float totalDistance = Vector3.Distance(self.position, target);
        float speed = totalDistance / duration;

        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;

            self.MovePosition(self.position + dir * speed * Time.deltaTime);

            yield return _waitForFixedUpdate;
        }

    }



}
