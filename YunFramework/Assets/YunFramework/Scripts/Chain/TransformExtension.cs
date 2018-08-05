using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /// <summary>
    /// 对Transform的链式编程扩展
    /// </summary>
    public static class TransformExtension
    {
        public static Transform Chain_SetParent(this Transform self,Transform parent)
        {
            self.SetParent(parent);
            return self;
        }

        public static Transform Chain_SetPosition(this Transform self, Vector3 position)
        {
            self.position = position;
            return self;
        }

        public static Transform Chain_SetLocalPosition(this Transform self, Vector3 position)
        {
            self.localPosition = position;
            return self;
        }

        public static Transform Chain_SetEulerAngles(this Transform self, Vector3 eulerAngles)
        {
            self.eulerAngles = eulerAngles;
            return self;
        }

        public static Transform Chain_SetLocalEulerAngles(this Transform self, Vector3 eulerAngles)
        {
            self.localEulerAngles = eulerAngles;
            return self;
        }

        public static Transform Chain_SetLocalScale(this Transform self,Vector3 scale)
        {
            self.localScale = scale;
            return self;
        }

        public static Transform Chain_Translate(this Transform self, Vector3 position)
        {
            self.Translate(position);
            return self;
        }

        public static Transform Chain_Rotate(this Transform self, Vector3 eulerAngles,Space relativeTo = Space.Self)
        {
            self.Rotate(eulerAngles, relativeTo);
            return self;
        }

        public static Transform Chain_LookAt(this Transform self, Vector3 target)
        {
            self.LookAt(target);
            return self;
        }

    }



