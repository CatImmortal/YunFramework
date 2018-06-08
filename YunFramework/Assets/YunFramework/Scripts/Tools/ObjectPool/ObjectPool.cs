using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YunFramework.Tools
{
    /// <summary>
    /// 对象池
    /// </summary>
    [System.Serializable]
    public class ObjectPool
    {
        /// <summary>
        /// 对象池的名字
        /// </summary>
        [SerializeField]
        public string _name;

        /// <summary>
        /// 对象池管理的对象预制体
        /// </summary>
        [SerializeField]
        private GameObject _prefab;

        /// <summary>
        /// 对象池最大容量
        /// </summary>
        [SerializeField]
        private int _maxCount;

        /// <summary>
        /// 对象的自动回收时间
        /// </summary>
        [SerializeField]
        private float _unspawnTime = 0;

        /// <summary>
        /// 对象池内已创建的对象列表
        /// </summary>
        private List<ReuseableObject> _roList = new List<ReuseableObject>();



        #region 对象池的操作
        /// <summary>
        /// 取出对象池里的对象
        /// </summary>
        /// <param name="unSpawn">自动回收该对象的时间</param>
        public ReuseableObject Spawn(float unspawnTime)
        {
            ReuseableObject ro = null;

            for (int i = 0; i < _roList.Count; i++)
            {
                //找一个未被激活的游戏对象
                if (!_roList[i].gameObject.activeSelf)
                {
                    ro = _roList[i];
                    ro.gameObject.SetActive(true);
                    break;
                }
            }

            //找不到就创建一个新的
            if (ro == null)
            {
                if (_roList.Count >= _maxCount)
                {
                    //满了就移除第一个
                    Object.DestroyImmediate(_roList[0].gameObject);
                    _roList.RemoveAt(0);   
                }

                ro = Object.Instantiate(_prefab).GetComponent<ReuseableObject>();
                _roList.Add(ro);

            }

            ro.OnSpawn();

            //自动回收设置
            if (unspawnTime != 0)
            {
                DelayTaskCtrl.Instance.AddDelayTask(unspawnTime, () => Unspawn(ro));
            }
            else if (_unspawnTime != 0)
            {
                DelayTaskCtrl.Instance.AddDelayTask(_unspawnTime, () => Unspawn(ro));
            }

            return ro;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        public void Unspawn(ReuseableObject ro)
        {
            if (_roList.Contains(ro) && ro.gameObject != null && ro.gameObject.activeSelf)
            {
                ro.OnUnspawn();
                ro.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 回收所有对象
        /// </summary>
        public void UnspawnAll()
        {
            for (int i = 0; i < _roList.Count; i++)
            {
                Unspawn(_roList[i]);
            }
        }

        /// <summary>
        /// 复原对象池
        /// </summary>
        public void RestorePool() {
            UnspawnAll();
            for (int i = 0; i < _roList.Count; i++)
            {
                Object.DestroyImmediate(_roList[i].gameObject);
            }
            _roList.Clear();
        }

        /// <summary>
        /// 清空对象列表
        /// </summary>
        public void ClearObjectList() {
            _roList.Clear();
        }
        #endregion


    }

}

