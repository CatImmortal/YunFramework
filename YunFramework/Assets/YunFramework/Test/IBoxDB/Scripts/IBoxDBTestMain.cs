using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.DataBase;
public class IBoxDBTestMain : MonoBehaviour {

	void Start () {

        IBoxDBCtrler.Instance.CreateDataTable<BaseDBObject>("Key");
        IBoxDBCtrler.Instance.CreateDataTable<TestDBObject>("ID");
        IBoxDBCtrler.Instance.Start();

        //------------------------------------------------------------------

        BaseDBObject obj1 = new BaseDBObject()
        {
            Key = "obj1"
        };

        obj1["name"] = "张三";
        obj1["age"] = 18;
        obj1["sex"] = "男";

        IBoxDBCtrler.Instance.SaveData(obj1, obj1.Key);

        BaseDBObject tempObj = IBoxDBCtrler.Instance.GetData<BaseDBObject>("obj1");
        Debug.Log(string.Format("Key-{0} name-{1} age-{2} sex-{3}", tempObj.Key, tempObj["name"], tempObj["age"], tempObj["sex"]));


        //------------------------------------------------------------------

        TestDBObject test = new TestDBObject
        {
            ID = 0,
            num = 555,
            str = "苟利国家生死以",
            flo = 2.14159f
        };

        IBoxDBCtrler.Instance.SaveData(test, test.ID);

        TestDBObject tempTest = IBoxDBCtrler.Instance.GetData<TestDBObject>(0);
        Debug.Log(string.Format("ID-{0} num-{1} str-{2} flo-{3}", tempTest.ID, tempTest.num, tempTest.str, tempTest.flo));

        //------------------------------------------------------------------

        TestDBObject test1 = new TestDBObject
        {
            ID = 1,
            num = 666,
            str = "岂因祸福避趋之",
            flo = 3.14159f
        };

        TestDBObject test2 = new TestDBObject
        {
            ID = 2,
            num = 666,
            str = "稻花香里说丰年",
            flo = 4.14159f
        };

        TestDBObject test3 = new TestDBObject
        {
            ID = 3,
            num = 666,
            str = "听取蛙声一片",
            flo = 5.14159f
        };

        TestDBObject test4 = new TestDBObject
        {
            ID = 4,
            num = 777,
            str = "搞个大新闻",
            flo = 6.14159f
        };

        Dictionary<TestDBObject, object> map = new Dictionary<TestDBObject, object>();
        map.Add(test1, test1.ID);
        map.Add(test2, test2.ID);
        map.Add(test3, test3.ID);
        map.Add(test4, test4.ID);

        IBoxDBCtrler.Instance.SaveDatas(map);

        List<TestDBObject> datas = IBoxDBCtrler.Instance.GetDatas<TestDBObject>("num", 666);

        foreach (TestDBObject data in datas)
        {
            Debug.Log(string.Format("ID-{0} num-{1} str-{2} flo-{3}", data.ID, data.num, data.str, data.flo));
        }
    }

}
