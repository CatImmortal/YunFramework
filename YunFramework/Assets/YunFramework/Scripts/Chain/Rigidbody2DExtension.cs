using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对Rigidbody2D的扩展
/// </summary>
public static class Rigidbody2DExtension{

    private static WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    /// <summary>
    /// 平滑旋转
    /// </summary>
    public static IEnumerator SmoothMoveRotation(this Rigidbody2D self, float angle, float duration)
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

        //开始平滑旋转
        float timer = 0;
        float originalRotation = self.rotation;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            self.MoveRotation(Mathf.Lerp(originalRotation, angle, timer / duration));
            yield return _waitForFixedUpdate;
        }
        self.MoveRotation(angle);
    }

    /// <summary>
    /// 平滑朝向目标点
    /// </summary>
    public static IEnumerator SmoothLookAt(this Rigidbody2D self, Transform selfTrans, Vector2 target, float duration)
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

        Vector2 pos2D = new Vector2(self.position.x, self.position.y);
        //计算夹角
        float angle = Vector2.Angle(selfTrans.right, target - pos2D);

        //计算朝向偏移
        float towardOffset = 1f;
        if (Vector2.Dot(selfTrans.up, target - pos2D) < 0)
        {
            towardOffset = -1f;
        }


        float targetAngle = selfTrans.eulerAngles.z + angle * towardOffset;

        float originalAngle = self.rotation;

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;

            self.MoveRotation(Mathf.Lerp(originalAngle, targetAngle, timer / duration));

            yield return _waitForFixedUpdate;
        }

        self.MoveRotation(targetAngle);
    }

    /// <summary>
    /// 平滑移至目标点
    /// </summary>
    public static IEnumerator SmoothMoveTo(this Rigidbody2D self, Vector2 target, float duration)
    {

        Vector2 dir = (target - self.position).normalized;
        float totalDistance = Vector2.Distance(self.position, target);
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
