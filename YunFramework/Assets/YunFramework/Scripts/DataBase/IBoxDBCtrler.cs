using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using iBoxDB.LocalServer;
using UnityEngine.Events;
using System;
namespace YunFramework.DataBase
{
    /// <summary>
    /// IBox数据库控制器
    /// </summary>
    public class IBoxDBCtrler : Singleton<IBoxDBCtrler>
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        private DB _db = null;

        /// <summary>
        /// 进行CRUD操作的对象，使用完毕无需进行释放
        /// </summary>
        private DB.AutoBox _autoBox = null;

        /// <summary>
        /// 数据表主键名字典
        /// </summary>
        private Dictionary<Type, string> _keyMap = new Dictionary<Type, string>();

        private IBoxDBCtrler()
        {
            DB.Root(Application.persistentDataPath);
            _db = new DB(ConstsDefine.DB_ADDRESS);
        }

        /// <summary>
        /// 启动数据库（在所有数据表创建完成后调用这个方法）
        /// </summary>
        public void Start()
        {
            _autoBox = _db.Open();
        }

        /// <summary>
        /// 创建数据表
        /// </summary>
        public void CreateDataTable<T>(string KeyName) where T : class
        {
            Type type = typeof(T);
            _keyMap[type] = KeyName;

            _db.GetConfig().EnsureTable<T>(type.Name, KeyName);
        }

        /// <summary>
        /// 保存一条数据
        /// </summary>
        public void SaveData<T>(T dbObj, object keyValue) where T : class
        {
            Type type = typeof(T);
            string tableName = type.Name;
            string keyName = _keyMap[type];

            if (_autoBox.SelectCount(string.Format("from {0} where {1} == ?",tableName,keyName), keyValue) <= 0)
            {
                _autoBox.Insert(tableName,dbObj);
            }
            else
            {
                _autoBox.Update(tableName, dbObj);
            }
        }

        /// <summary>
        /// 保存多条数据
        /// </summary>
        public void SaveDatas<T>(Dictionary<T, object> dataMap) where T : class
        {
            Type type = typeof(T);

            string tableName = type.Name;
            string keyName = _keyMap[type];

            IBox box = _autoBox.Cube();
            Binder binder = box.Bind(tableName);

            foreach (KeyValuePair<T,object> kv in dataMap)
            {
                T dbObj = kv.Key;
                object keyValue = kv.Value;

                if (_autoBox.SelectCount(string.Format("from {0} where {1} == ?", tableName, keyName), keyValue) <= 0)
                {
                    binder.Insert(dbObj);
                }
                else
                {
                    binder.Update(dbObj);
                }
            }

            box.Commit();
            box.Dispose();
        }

        /// <summary>
        /// 根据主键获取数据
        /// </summary>
        public T GetData<T>(object keyValue)where T : class , new()
        {
            return _autoBox.SelectKey<T>(typeof(T).Name, keyValue);
        }

        /// <summary>
        /// 根据字段值获取数据
        /// </summary>
        public List<T> GetDatas<T>(string fieldName , object fieldValue) where T : class, new()
        {
            string ql = string.Format("from {0} where {1} == ?", typeof(T).Name, fieldName);
            return GetDatasByQL<T>(ql, fieldValue);
        }

        /// <summary>
        /// 根据QL语句获取数据
        /// </summary>
        public List<T> GetDatasByQL<T>(string QL, object param) where T : class , new()
        {
            return _autoBox.Select<T>(QL, param);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void DeleteData<T>(object keyValue)
        {
            string tableName = typeof(T).Name;

            _autoBox.Delete(tableName, keyValue);
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        public void CloseIBoxDB()
        {
            _db.Close();
        }
    }

}
