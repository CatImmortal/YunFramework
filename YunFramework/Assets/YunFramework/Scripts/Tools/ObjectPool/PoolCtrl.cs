using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Base;
using YunFramework.Load;

namespace YunFramework.Tools
{
    /// <summary>
    /// 对象池管理器
    /// </summary>
    public class PoolCtrl : Singleton<PoolCtrl>
    {

        /// <summary>
        /// 对象池字典
        /// </summary>
        private Dictionary<string, ObjectPool> _poolDict = new Dictionary<string, ObjectPool>();

        private PoolCtrl()
        {
            //读取对象池的配置
            ObjectPoolConfig config = ResourceLoader.Instance.LoadAsset<ObjectPoolConfig>(ConstsDefine.POOLCONFIG_NAME.Split('.')[0]);
            foreach (ObjectPool pool in config._poolList)
            {
                if (!_poolDict.ContainsKey(pool._name))
                {
                    _poolDict.Add(pool._name, pool);
                    pool.ClearObjectList();
                }
            }
        }

        #region 对象池的操作

        /// <summary>
        /// 取出对象
        /// </summary>
        public ReuseableObject Spawn(string poolName,float unspawnTime = 0)
        {
            if (!_poolDict.ContainsKey(poolName))
            {
                Debug.LogError("没有配置该对象池：" + poolName);
                return null;
            }

            ObjectPool pool = _poolDict[poolName];
            return pool.Spawn(unspawnTime);
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        public void Unspawn(ReuseableObject ro,string poolName)
        {
            _poolDict[poolName].Unspawn(ro);
        }

        /// <summary>
        /// 回收指定对象池的所有对象
        /// </summary>
        public void UnspawnAll(string poolName)
        {
            _poolDict[poolName].UnspawnAll();
        }

        /// <summary>
        /// 回收所有对象池的对象
        /// </summary>
        public void UnspawnAll()
        {
            foreach (string name in _poolDict.Keys)
            {
                _poolDict[name].UnspawnAll();
            }
        }

        /// <summary>
        /// 复原指定对象池
        /// </summary>
        public void RestorePool(string poolName)
        {
            _poolDict[poolName].RestorePool();
        }

        /// <summary>
        /// 复原所有对象池
        /// </summary>
        public void RestorePool()
        {
            foreach (string name in _poolDict.Keys)
            {
                _poolDict[name].RestorePool();
            }
        }

        #endregion

    }
}


